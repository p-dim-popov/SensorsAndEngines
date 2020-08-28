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
#define WDT_RESET_MS 2100

// Structures -----------------------

struct Sensors
{
	//Members -----------------------
	ProtobufModels_Sensors models = ProtobufModels_Sensors_init_zero;

	//Methods -----------------------

	void setup(Stream&);
	void read();
	void send();
}
;

// Global Variables -----------------

static Sensors sensors;
pb_ostream_s pb_ostream;
pb_istream_t pb_istream;
uint32_t command = ProtobufModels_Command_PROCEED;

// Methods --------------------------

void error_blink(ProtobufModels_Environment environment, const char* message = NULL);

void setup()
{
	pinMode(INFO_LED, OUTPUT);
	Serial.begin(BAUD_RATE);
	
	while (!Serial.available()) ;
	
	sensors.setup(Serial);
	wdt_enable(WDTO_2S);
}

void loop()
{
	sensors.read();
	sensors.send();
	
	while (!Serial.available()) ;

	switch (Serial.read())
	{
	case ProtobufModels_Command_PROCEED:
		wdt_reset();
		break;
	case ProtobufModels_Command_STOP:
		delay(WDT_RESET_MS);
	default:
		wdt_reset();
		error_blink(sensors.models.environment, pb_ostream.errmsg);
		break;
	}
}

// Implementations ------------------

void error_blink(ProtobufModels_Environment env, const char* message)
{
	while (1)
	{
		if (env == ProtobufModels_Environment_DEVELOPMENT) wdt_reset();
		digitalWrite(INFO_LED, !digitalRead(INFO_LED));
		Serial.println(message);
		delay(64);
	}
}

void Sensors::setup(Stream& stream)
{
	pb_ostream = as_pb_ostream(stream);
	pb_istream = as_pb_istream(stream);

	if (!pb_decode(&pb_istream, ProtobufModels_Sensors_fields, &this->models)) ;
	//		ErrorBlink(istream.errmsg);
	for(byte i = 0 ; i < this->models.list_count ; i++)
		pinMode(this->models.list[i].pin, INPUT);
}

void Sensors::read()
{
	this->models.timestamp = millis();
	
	for (byte i = 0; i < this->models.list_count; i++)
	{		
		switch (this->models.list[i].which_Type)
		{
		case ProtobufModels_Sensor_digital_tag:
			{
				int currentState = digitalRead(this->models.list[i].pin);
				if (currentState != this->models.list[i].Type.digital.value)
				{
					this->models.list[i].Type.digital.timestamp = models.timestamp;
					this->models.list[i].Type.digital.value = currentState;
				}
			}
			break;
		case ProtobufModels_Sensor_analog_tag:
			{
				int value = analogRead(this->models.list[i].pin);
				this->models.list[i].Type.analog.value = 
					map(value, MIN_ANALOG, MAX_ANALOG, this->models.list[i].Type.analog.lower_range, this->models.list[i].Type.analog.upper_range);
			}
			break;
		default:
			error_blink(this->models.environment);
			break;
		}
	}
}

void Sensors::send()
{
	if (!pb_encode_ex(&pb_ostream, ProtobufModels_Sensors_fields, &this->models, PB_ENCODE_DELIMITED))
		error_blink(this->models.environment, pb_ostream.errmsg);
}
