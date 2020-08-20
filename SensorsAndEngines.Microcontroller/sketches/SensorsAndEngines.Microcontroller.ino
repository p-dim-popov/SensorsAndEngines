/*
 Name:		SensorsAndEngines.ino
 Created:	8/12/2020 8:32:28 PM
 Author:	pdimp
*/

#include <pb_arduino.h>
#include <models.pb.h>
#include <avr/wdt.h>

// Macros ---------------------------

#define INFO_LED 17
#define BAUD_RATE 115200
#define MIN_ANALOG 0
#define MAX_ANALOG 1023

// Structures -----------------------

struct Sensors
{
	//Members -----------------------
	
	Print* _printer;
	ProtobufModels_Sensors models = ProtobufModels_Sensors_init_zero;

	//Methods -----------------------

	void Setup(Stream&);
	void Read();
	void Send();
}
;

// Global Variables -----------------

static Sensors sensors;
pb_ostream_s pb_ostream;
pb_istream_t pb_istream;
uint32_t command = ProtobufModels_Command_PROCEED;

// Methods --------------------------

void ErrorBlink(const char* message = NULL);
void(* ResetFunc)(void) = 0;

// the setup function runs once when you press reset or power the board
void setup()
{
	pinMode(INFO_LED, OUTPUT);
	Serial.begin(BAUD_RATE);
	
	while (!Serial.available()) ;
	
	sensors.Setup(Serial);
	wdt_enable(WDTO_1S);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	sensors.Read();
	sensors.Send();
	
	while (!Serial.available()) ;

	switch (Serial.read())
	{
	case ProtobufModels_Command_PROCEED:
			wdt_reset();
		break;
	case ProtobufModels_Command_STOP:
		delay(2000);
	default:
		ErrorBlink(pb_ostream.errmsg);
		break;
	}
}

// Implementations ------------------

void ErrorBlink(const char* message)
{
	while (1)
	{
		digitalWrite(INFO_LED, !digitalRead(INFO_LED));
		Serial.println(message);
		delay(64);
	}
}

void Sensors::Setup(Stream& stream)
{
	pb_ostream = as_pb_ostream(stream);
	pb_istream = as_pb_istream(stream);
	this->_printer = &stream;

	if (!pb_decode(&pb_istream, ProtobufModels_Sensors_fields, &this->models)) ;
	//		ErrorBlink(istream.errmsg);
	for(byte i = 0 ; i < this->models.List_count ; i++)
		pinMode(this->models.List[i].Pin, INPUT);
}

void Sensors::Read()
{
	this->models.Timestamp = millis();
	
	for (byte i = 0; i < this->models.List_count; i++)
	{		
		switch (this->models.List[i].which_Type)
		{
		case ProtobufModels_Sensor_Digital_tag:
			{
				int currentState = digitalRead(this->models.List[i].Pin);
				if (currentState != this->models.List[i].Type.Digital.Value)
				{
					this->models.List[i].Type.Digital.Timestamp = models.Timestamp;
					this->models.List[i].Type.Digital.Value = currentState;
				}
			}
			break;
		case ProtobufModels_Sensor_Analog_tag:
			{
				int value = analogRead(this->models.List[i].Pin);
				this->models.List[i].Type.Analog.Value = 
					map(value, MIN_ANALOG, MAX_ANALOG, this->models.List[i].Type.Analog.LowerRange, this->models.List[i].Type.Analog.UpperRange);
			}
			break;
		default:
			ErrorBlink();
			break;
		}
	}
}

void Sensors::Send()
{
	switch (sensors.models.Decoding)
	{
	case ProtobufModels_Decoding_PROTOBUF:
		if (!pb_encode_ex(&pb_ostream, ProtobufModels_Sensors_fields, &this->models, PB_ENCODE_DELIMITED))
			ErrorBlink(pb_ostream.errmsg);
		break;
	case ProtobufModels_Decoding_CSV:
		{
			//			char buffer[sizeof sensors];
			//			memset(buffer, '\0', sizeof(buffer));
			String buffer;
			buffer.concat(sensors.models.Timestamp);
			buffer.concat(',');
			for (uint8_t i = 0; i < sensors.models.List_count; i++)
			{
				const ProtobufModels_Sensor& sensor = this->models.List[i];
				switch (sensor.which_Type)
				{
				case ProtobufModels_Sensor_Digital_tag:
					//memcpy(buffer + strlen(buffer), (char*)&sensor.Type.Digital.Timestamp, sizeof(typeof sensor.Type.Digital.Timestamp));
					//memcpy(buffer + strlen(buffer), ",", 0x1);
					//memcpy(buffer + strlen(buffer), (char*)&sensor.Type.Digital.Value, sizeof(typeof sensor.Type.Digital.Value));
					buffer.concat(sensor.Type.Digital.Timestamp);
					buffer.concat(',');
					buffer.concat(sensor.Type.Digital.Value);
					break;
				case ProtobufModels_Sensor_Analog_tag:
					//memcpy(buffer + strlen(buffer), (char*)&sensor.Type.Analog.Value, sizeof(typeof sensor.Type.Analog.Value));
					buffer.concat(sensor.Type.Analog.Value);
					break;
				default:
					ErrorBlink();
					break;
				}
				//memcpy(buffer, ",", 0x1);
				buffer.concat(',');
			}
			
			this->_printer->println(buffer);
		}
	default:
		break;
	}
}
