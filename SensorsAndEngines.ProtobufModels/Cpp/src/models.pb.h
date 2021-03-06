/* Automatically generated nanopb header */
/* Generated by nanopb-0.4.2 */

#ifndef PB_PROTOBUFMODELS_MODELS_PB_H_INCLUDED
#define PB_PROTOBUFMODELS_MODELS_PB_H_INCLUDED
#include <pb.h>

#if PB_PROTO_HEADER_VERSION != 40
#error Regenerate this file with the current version of nanopb generator.
#endif

#ifdef __cplusplus
extern "C" {
#endif

/* Enum definitions */
typedef enum _ProtobufModels_Environment {
    ProtobufModels_Environment_PRODUCTION = 0,
    ProtobufModels_Environment_DEVELOPMENT = 1
} ProtobufModels_Environment;

typedef enum _ProtobufModels_Command {
    ProtobufModels_Command_PROCEED = 0,
    ProtobufModels_Command_STOP = 1
} ProtobufModels_Command;

/* Struct definitions */
typedef struct _ProtobufModels_AnalogSensor {
    int32_t upper_range;
    int32_t lower_range;
    float value;
} ProtobufModels_AnalogSensor;

typedef struct _ProtobufModels_DigitalSensor {
    uint32_t timestamp;
    bool value;
} ProtobufModels_DigitalSensor;

typedef struct _ProtobufModels_Sensor {
    pb_size_t which_Type;
    union {
        ProtobufModels_DigitalSensor digital;
        ProtobufModels_AnalogSensor analog;
    } Type;
    uint32_t pin;
} ProtobufModels_Sensor;

typedef struct _ProtobufModels_Sensors {
    pb_size_t list_count;
    ProtobufModels_Sensor list[16];
    uint32_t timestamp;
    ProtobufModels_Environment environment;
} ProtobufModels_Sensors;


/* Helper constants for enums */
#define _ProtobufModels_Environment_MIN ProtobufModels_Environment_PRODUCTION
#define _ProtobufModels_Environment_MAX ProtobufModels_Environment_DEVELOPMENT
#define _ProtobufModels_Environment_ARRAYSIZE ((ProtobufModels_Environment)(ProtobufModels_Environment_DEVELOPMENT+1))

#define _ProtobufModels_Command_MIN ProtobufModels_Command_PROCEED
#define _ProtobufModels_Command_MAX ProtobufModels_Command_STOP
#define _ProtobufModels_Command_ARRAYSIZE ((ProtobufModels_Command)(ProtobufModels_Command_STOP+1))


/* Initializer values for message structs */
#define ProtobufModels_DigitalSensor_init_default {0, 0}
#define ProtobufModels_AnalogSensor_init_default {0, 0, 0}
#define ProtobufModels_Sensor_init_default       {0, {ProtobufModels_DigitalSensor_init_default}, 0}
#define ProtobufModels_Sensors_init_default      {0, {ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default, ProtobufModels_Sensor_init_default}, 0, _ProtobufModels_Environment_MIN}
#define ProtobufModels_DigitalSensor_init_zero   {0, 0}
#define ProtobufModels_AnalogSensor_init_zero    {0, 0, 0}
#define ProtobufModels_Sensor_init_zero          {0, {ProtobufModels_DigitalSensor_init_zero}, 0}
#define ProtobufModels_Sensors_init_zero         {0, {ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero, ProtobufModels_Sensor_init_zero}, 0, _ProtobufModels_Environment_MIN}

/* Field tags (for use in manual encoding/decoding) */
#define ProtobufModels_AnalogSensor_upper_range_tag 1
#define ProtobufModels_AnalogSensor_lower_range_tag 2
#define ProtobufModels_AnalogSensor_value_tag    3
#define ProtobufModels_DigitalSensor_timestamp_tag 1
#define ProtobufModels_DigitalSensor_value_tag   2
#define ProtobufModels_Sensor_digital_tag        1
#define ProtobufModels_Sensor_analog_tag         2
#define ProtobufModels_Sensor_pin_tag            3
#define ProtobufModels_Sensors_list_tag          1
#define ProtobufModels_Sensors_timestamp_tag     2
#define ProtobufModels_Sensors_environment_tag   3

/* Struct field encoding specification for nanopb */
#define ProtobufModels_DigitalSensor_FIELDLIST(X, a) \
X(a, STATIC,   SINGULAR, UINT32,   timestamp,         1) \
X(a, STATIC,   SINGULAR, BOOL,     value,             2)
#define ProtobufModels_DigitalSensor_CALLBACK NULL
#define ProtobufModels_DigitalSensor_DEFAULT NULL

#define ProtobufModels_AnalogSensor_FIELDLIST(X, a) \
X(a, STATIC,   SINGULAR, SINT32,   upper_range,       1) \
X(a, STATIC,   SINGULAR, SINT32,   lower_range,       2) \
X(a, STATIC,   SINGULAR, FLOAT,    value,             3)
#define ProtobufModels_AnalogSensor_CALLBACK NULL
#define ProtobufModels_AnalogSensor_DEFAULT NULL

#define ProtobufModels_Sensor_FIELDLIST(X, a) \
X(a, STATIC,   ONEOF,    MESSAGE,  (Type,digital,Type.digital),   1) \
X(a, STATIC,   ONEOF,    MESSAGE,  (Type,analog,Type.analog),   2) \
X(a, STATIC,   SINGULAR, UINT32,   pin,               3)
#define ProtobufModels_Sensor_CALLBACK NULL
#define ProtobufModels_Sensor_DEFAULT NULL
#define ProtobufModels_Sensor_Type_digital_MSGTYPE ProtobufModels_DigitalSensor
#define ProtobufModels_Sensor_Type_analog_MSGTYPE ProtobufModels_AnalogSensor

#define ProtobufModels_Sensors_FIELDLIST(X, a) \
X(a, STATIC,   REPEATED, MESSAGE,  list,              1) \
X(a, STATIC,   SINGULAR, UINT32,   timestamp,         2) \
X(a, STATIC,   SINGULAR, UENUM,    environment,       3)
#define ProtobufModels_Sensors_CALLBACK NULL
#define ProtobufModels_Sensors_DEFAULT NULL
#define ProtobufModels_Sensors_list_MSGTYPE ProtobufModels_Sensor

extern const pb_msgdesc_t ProtobufModels_DigitalSensor_msg;
extern const pb_msgdesc_t ProtobufModels_AnalogSensor_msg;
extern const pb_msgdesc_t ProtobufModels_Sensor_msg;
extern const pb_msgdesc_t ProtobufModels_Sensors_msg;

/* Defines for backwards compatibility with code written before nanopb-0.4.0 */
#define ProtobufModels_DigitalSensor_fields &ProtobufModels_DigitalSensor_msg
#define ProtobufModels_AnalogSensor_fields &ProtobufModels_AnalogSensor_msg
#define ProtobufModels_Sensor_fields &ProtobufModels_Sensor_msg
#define ProtobufModels_Sensors_fields &ProtobufModels_Sensors_msg

/* Maximum encoded size of messages (where known) */
#define ProtobufModels_DigitalSensor_size        8
#define ProtobufModels_AnalogSensor_size         17
#define ProtobufModels_Sensor_size               25
#define ProtobufModels_Sensors_size              440

#ifdef __cplusplus
} /* extern "C" */
#endif

#endif
