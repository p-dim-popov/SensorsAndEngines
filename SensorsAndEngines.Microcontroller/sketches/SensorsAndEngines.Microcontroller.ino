/*
 Name:		SensorsAndEngines.ino
 Created:	8/12/2020 8:32:28 PM
 Author:	pdimp
*/

#include <pb_arduino.h>
#include <models.pb.h>

// Macros ---------------------------

#define INFO_LED 16
#define BAUD_RATE 9600
#define MIN_ANALOG 0
#define MAX_ANALOG 1023

// Structures -----------------------

struct Sensors
{
	//Members -----------------------

	ProtobufModels_Sensors sensors = ProtobufModels_Sensors_init_zero;

	//Methods -----------------------

	void Setup(Stream&);
	/*
	 * Save timestamp on digital sensor change
	 */
	void Read();
	void Send(Stream&);
}
;

// Global Variables -----------------

static Sensors sensors;

// Methods --------------------------

void ErrorBlink();

// the setup function runs once when you press reset or power the board
void setup()
{
	pinMode(INFO_LED, OUTPUT);
	Serial.begin(BAUD_RATE);
	
	while (!Serial.available()) ;

	sensors.Setup(Serial);
}

// the loop function runs over and over again until power down or reset
void loop()
{
	sensors.Read();
	sensors.Send(Serial);
}

// Implementations ------------------

void ErrorBlink()
{
	while (1)
	{
		digitalWrite(INFO_LED, !digitalRead(INFO_LED));
		delay(64);
	}
}

void Sensors::Setup(Stream& stream)
{
	pb_istream_t istream = as_pb_istream(stream);
	if (!pb_decode(&istream, ProtobufModels_Sensors_fields, &sensors.List))
		ErrorBlink();

	for (byte i = 0; i < sensors.List_count; i++)
		pinMode(sensors.List[i].Pin, INPUT);
}

void Sensors::Read()
{
	sensors.Timestamp = millis();
	
	for (byte i = 0; i < sensors.List_count; i++)
	{
		ProtobufModels_Sensor& sensor = sensors.List[i];
		
		switch (sensor.which_Type)
		{
		case ProtobufModels_Sensor_Digital_tag:
			{
				int currentState = digitalRead(sensor.Pin);
				if (currentState != sensor.Type.Digital.Value)
				{
					sensor.Type.Digital.Timestamp = sensors.Timestamp;
					sensor.Type.Digital.Value = currentState;
				}
			}
			break;
		case ProtobufModels_Sensor_Analog_tag:
			{
				int value = analogRead(sensors.List[i].Pin);
				sensor.Type.Analog.Value = 
					map(value, MIN_ANALOG, MAX_ANALOG, sensor.Type.Analog.LowerRange, sensor.Type.Analog.UpperRange);
			}
			break;
		default:
			ErrorBlink();
			break;
		}
	}
}

void Sensors::Send(Stream& stream)
{
	pb_ostream_s ostream = as_pb_ostream(stream);
	if (!pb_encode(&ostream, ProtobufModels_Sensors_fields, &sensors.List))
		ErrorBlink();
}
