/*
 Name:		SensorsAndEngines.ino
 Created:	8/12/2020 8:32:28 PM
 Author:	pdimp
*/

#include <pb_arduino.h>
#include <models.pb.h>

// Macros ---------------------------

#define BAUD_RATE 9600

struct Sensors
{
	//Members -----------------------

	ProtobufModels_Sensors sensors = ProtobufModels_Sensors_init_zero;

	//Setup -------------------------

	void Setup();
	/*
	 * Save timestamp on digital sensor change
	 */
	void Read();
	void Send(pb_ostream_t&);
};

// Global Variables -----------------

static Sensors sensors;

// Methods --------------------------

void ErrorBlink();

// the setup function runs once when you press reset or power the board
void setup()
{
	pinMode(17, OUTPUT);
	Serial.begin(BAUD_RATE);
	
	while (!Serial.available()) 
		;

	sensors.Setup();
}

// the loop function runs over and over again until power down or reset
void loop()
{
	static pb_ostream_t ostream = as_pb_ostream(Serial);
	sensors.Read();
	sensors.Send(ostream);
}

void ErrorBlink()
{
	while (1)
	{
		digitalWrite(17, !digitalRead(17));
		delay(128);
	}
}

void Sensors::Setup()
{
	pb_istream_t istream = as_pb_istream(Serial);
	if (!(boolean)pb_decode(&istream, ProtobufModels_Sensors_fields, &sensors.List))
		ErrorBlink();

	for (byte i = 0; i < sensors.List_count; i++)
		pinMode(sensors.List[i].Pin, INPUT);
}

void Sensors::Read()
{
	for (byte i = 0; i < sensors.List_count; i++)
	{
		switch (sensors.List[i].which_Type)
		{
		case ProtobufModels_Sensor_Digital_tag:
			{
				int state = digitalRead(sensors.List[i].Pin);
				if (1)
				{
		 
				}
			}
			break;
		case ProtobufModels_Sensor_Analog_tag:
			{

			}
			break;
		}

	}
}

void Sensors::Send(pb_ostream_t& ostream)
{

}
