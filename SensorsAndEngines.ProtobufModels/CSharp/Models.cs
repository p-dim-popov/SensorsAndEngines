// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: models.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace SensorsAndEngines.ProtobufModels {

  /// <summary>Holder for reflection information generated from models.proto</summary>
  public static partial class ModelsReflection {

    #region Descriptor
    /// <summary>File descriptor for models.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ModelsReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cgxtb2RlbHMucHJvdG8SDlByb3RvYnVmTW9kZWxzIjEKDURpZ2l0YWxTZW5z",
            "b3ISEQoJVGltZXN0YW1wGAEgASgFEg0KBVZhbHVlGAIgASgIIkUKDEFuYWxv",
            "Z1NlbnNvchISCgpVcHBlclJhbmdlGAEgASgFEhIKCkxvd2VyUmFuZ2UYAiAB",
            "KAUSDQoFVmFsdWUYAyABKAIimAEKBlNlbnNvchIwCgdEaWdpdGFsGAEgASgL",
            "Mh0uUHJvdG9idWZNb2RlbHMuRGlnaXRhbFNlbnNvckgAEi4KBkFuYWxvZxgC",
            "IAEoCzIcLlByb3RvYnVmTW9kZWxzLkFuYWxvZ1NlbnNvckgAEhcKD01lYXN1",
            "cmVtZW50VW5pdBgDIAEoCRILCgNQaW4YBCABKAVCBgoEVHlwZSJCCgdTZW5z",
            "b3JzEiQKBExpc3QYASADKAsyFi5Qcm90b2J1Zk1vZGVscy5TZW5zb3ISEQoJ",
            "VGltZXN0YW1wGAIgASgFQiOqAiBTZW5zb3JzQW5kRW5naW5lcy5Qcm90b2J1",
            "Zk1vZGVsc2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::SensorsAndEngines.ProtobufModels.DigitalSensor), global::SensorsAndEngines.ProtobufModels.DigitalSensor.Parser, new[]{ "Timestamp", "Value" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SensorsAndEngines.ProtobufModels.AnalogSensor), global::SensorsAndEngines.ProtobufModels.AnalogSensor.Parser, new[]{ "UpperRange", "LowerRange", "Value" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SensorsAndEngines.ProtobufModels.Sensor), global::SensorsAndEngines.ProtobufModels.Sensor.Parser, new[]{ "Digital", "Analog", "MeasurementUnit", "Pin" }, new[]{ "Type" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SensorsAndEngines.ProtobufModels.Sensors), global::SensorsAndEngines.ProtobufModels.Sensors.Parser, new[]{ "List", "Timestamp" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class DigitalSensor : pb::IMessage<DigitalSensor> {
    private static readonly pb::MessageParser<DigitalSensor> _parser = new pb::MessageParser<DigitalSensor>(() => new DigitalSensor());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<DigitalSensor> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SensorsAndEngines.ProtobufModels.ModelsReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DigitalSensor() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DigitalSensor(DigitalSensor other) : this() {
      timestamp_ = other.timestamp_;
      value_ = other.value_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public DigitalSensor Clone() {
      return new DigitalSensor(this);
    }

    /// <summary>Field number for the "Timestamp" field.</summary>
    public const int TimestampFieldNumber = 1;
    private int timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    /// <summary>Field number for the "Value" field.</summary>
    public const int ValueFieldNumber = 2;
    private bool value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as DigitalSensor);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(DigitalSensor other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Timestamp != other.Timestamp) return false;
      if (Value != other.Value) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Timestamp != 0) hash ^= Timestamp.GetHashCode();
      if (Value != false) hash ^= Value.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Timestamp != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Timestamp);
      }
      if (Value != false) {
        output.WriteRawTag(16);
        output.WriteBool(Value);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Timestamp != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Timestamp);
      }
      if (Value != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(DigitalSensor other) {
      if (other == null) {
        return;
      }
      if (other.Timestamp != 0) {
        Timestamp = other.Timestamp;
      }
      if (other.Value != false) {
        Value = other.Value;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Timestamp = input.ReadInt32();
            break;
          }
          case 16: {
            Value = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed partial class AnalogSensor : pb::IMessage<AnalogSensor> {
    private static readonly pb::MessageParser<AnalogSensor> _parser = new pb::MessageParser<AnalogSensor>(() => new AnalogSensor());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AnalogSensor> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SensorsAndEngines.ProtobufModels.ModelsReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AnalogSensor() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AnalogSensor(AnalogSensor other) : this() {
      upperRange_ = other.upperRange_;
      lowerRange_ = other.lowerRange_;
      value_ = other.value_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AnalogSensor Clone() {
      return new AnalogSensor(this);
    }

    /// <summary>Field number for the "UpperRange" field.</summary>
    public const int UpperRangeFieldNumber = 1;
    private int upperRange_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int UpperRange {
      get { return upperRange_; }
      set {
        upperRange_ = value;
      }
    }

    /// <summary>Field number for the "LowerRange" field.</summary>
    public const int LowerRangeFieldNumber = 2;
    private int lowerRange_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int LowerRange {
      get { return lowerRange_; }
      set {
        lowerRange_ = value;
      }
    }

    /// <summary>Field number for the "Value" field.</summary>
    public const int ValueFieldNumber = 3;
    private float value_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Value {
      get { return value_; }
      set {
        value_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AnalogSensor);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AnalogSensor other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (UpperRange != other.UpperRange) return false;
      if (LowerRange != other.LowerRange) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Value, other.Value)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (UpperRange != 0) hash ^= UpperRange.GetHashCode();
      if (LowerRange != 0) hash ^= LowerRange.GetHashCode();
      if (Value != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Value);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (UpperRange != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(UpperRange);
      }
      if (LowerRange != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(LowerRange);
      }
      if (Value != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Value);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (UpperRange != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(UpperRange);
      }
      if (LowerRange != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(LowerRange);
      }
      if (Value != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(AnalogSensor other) {
      if (other == null) {
        return;
      }
      if (other.UpperRange != 0) {
        UpperRange = other.UpperRange;
      }
      if (other.LowerRange != 0) {
        LowerRange = other.LowerRange;
      }
      if (other.Value != 0F) {
        Value = other.Value;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            UpperRange = input.ReadInt32();
            break;
          }
          case 16: {
            LowerRange = input.ReadInt32();
            break;
          }
          case 29: {
            Value = input.ReadFloat();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Sensor : pb::IMessage<Sensor> {
    private static readonly pb::MessageParser<Sensor> _parser = new pb::MessageParser<Sensor>(() => new Sensor());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Sensor> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SensorsAndEngines.ProtobufModels.ModelsReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sensor() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sensor(Sensor other) : this() {
      measurementUnit_ = other.measurementUnit_;
      pin_ = other.pin_;
      switch (other.TypeCase) {
        case TypeOneofCase.Digital:
          Digital = other.Digital.Clone();
          break;
        case TypeOneofCase.Analog:
          Analog = other.Analog.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sensor Clone() {
      return new Sensor(this);
    }

    /// <summary>Field number for the "Digital" field.</summary>
    public const int DigitalFieldNumber = 1;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SensorsAndEngines.ProtobufModels.DigitalSensor Digital {
      get { return typeCase_ == TypeOneofCase.Digital ? (global::SensorsAndEngines.ProtobufModels.DigitalSensor) type_ : null; }
      set {
        type_ = value;
        typeCase_ = value == null ? TypeOneofCase.None : TypeOneofCase.Digital;
      }
    }

    /// <summary>Field number for the "Analog" field.</summary>
    public const int AnalogFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SensorsAndEngines.ProtobufModels.AnalogSensor Analog {
      get { return typeCase_ == TypeOneofCase.Analog ? (global::SensorsAndEngines.ProtobufModels.AnalogSensor) type_ : null; }
      set {
        type_ = value;
        typeCase_ = value == null ? TypeOneofCase.None : TypeOneofCase.Analog;
      }
    }

    /// <summary>Field number for the "MeasurementUnit" field.</summary>
    public const int MeasurementUnitFieldNumber = 3;
    private string measurementUnit_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string MeasurementUnit {
      get { return measurementUnit_; }
      set {
        measurementUnit_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Pin" field.</summary>
    public const int PinFieldNumber = 4;
    private int pin_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Pin {
      get { return pin_; }
      set {
        pin_ = value;
      }
    }

    private object type_;
    /// <summary>Enum of possible cases for the "Type" oneof.</summary>
    public enum TypeOneofCase {
      None = 0,
      Digital = 1,
      Analog = 2,
    }
    private TypeOneofCase typeCase_ = TypeOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public TypeOneofCase TypeCase {
      get { return typeCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void ClearType() {
      typeCase_ = TypeOneofCase.None;
      type_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Sensor);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Sensor other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Digital, other.Digital)) return false;
      if (!object.Equals(Analog, other.Analog)) return false;
      if (MeasurementUnit != other.MeasurementUnit) return false;
      if (Pin != other.Pin) return false;
      if (TypeCase != other.TypeCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (typeCase_ == TypeOneofCase.Digital) hash ^= Digital.GetHashCode();
      if (typeCase_ == TypeOneofCase.Analog) hash ^= Analog.GetHashCode();
      if (MeasurementUnit.Length != 0) hash ^= MeasurementUnit.GetHashCode();
      if (Pin != 0) hash ^= Pin.GetHashCode();
      hash ^= (int) typeCase_;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (typeCase_ == TypeOneofCase.Digital) {
        output.WriteRawTag(10);
        output.WriteMessage(Digital);
      }
      if (typeCase_ == TypeOneofCase.Analog) {
        output.WriteRawTag(18);
        output.WriteMessage(Analog);
      }
      if (MeasurementUnit.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(MeasurementUnit);
      }
      if (Pin != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(Pin);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (typeCase_ == TypeOneofCase.Digital) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Digital);
      }
      if (typeCase_ == TypeOneofCase.Analog) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Analog);
      }
      if (MeasurementUnit.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(MeasurementUnit);
      }
      if (Pin != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Pin);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Sensor other) {
      if (other == null) {
        return;
      }
      if (other.MeasurementUnit.Length != 0) {
        MeasurementUnit = other.MeasurementUnit;
      }
      if (other.Pin != 0) {
        Pin = other.Pin;
      }
      switch (other.TypeCase) {
        case TypeOneofCase.Digital:
          if (Digital == null) {
            Digital = new global::SensorsAndEngines.ProtobufModels.DigitalSensor();
          }
          Digital.MergeFrom(other.Digital);
          break;
        case TypeOneofCase.Analog:
          if (Analog == null) {
            Analog = new global::SensorsAndEngines.ProtobufModels.AnalogSensor();
          }
          Analog.MergeFrom(other.Analog);
          break;
      }

      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            global::SensorsAndEngines.ProtobufModels.DigitalSensor subBuilder = new global::SensorsAndEngines.ProtobufModels.DigitalSensor();
            if (typeCase_ == TypeOneofCase.Digital) {
              subBuilder.MergeFrom(Digital);
            }
            input.ReadMessage(subBuilder);
            Digital = subBuilder;
            break;
          }
          case 18: {
            global::SensorsAndEngines.ProtobufModels.AnalogSensor subBuilder = new global::SensorsAndEngines.ProtobufModels.AnalogSensor();
            if (typeCase_ == TypeOneofCase.Analog) {
              subBuilder.MergeFrom(Analog);
            }
            input.ReadMessage(subBuilder);
            Analog = subBuilder;
            break;
          }
          case 26: {
            MeasurementUnit = input.ReadString();
            break;
          }
          case 32: {
            Pin = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Sensors : pb::IMessage<Sensors> {
    private static readonly pb::MessageParser<Sensors> _parser = new pb::MessageParser<Sensors>(() => new Sensors());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Sensors> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SensorsAndEngines.ProtobufModels.ModelsReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sensors() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sensors(Sensors other) : this() {
      list_ = other.list_.Clone();
      timestamp_ = other.timestamp_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Sensors Clone() {
      return new Sensors(this);
    }

    /// <summary>Field number for the "List" field.</summary>
    public const int ListFieldNumber = 1;
    private static readonly pb::FieldCodec<global::SensorsAndEngines.ProtobufModels.Sensor> _repeated_list_codec
        = pb::FieldCodec.ForMessage(10, global::SensorsAndEngines.ProtobufModels.Sensor.Parser);
    private readonly pbc::RepeatedField<global::SensorsAndEngines.ProtobufModels.Sensor> list_ = new pbc::RepeatedField<global::SensorsAndEngines.ProtobufModels.Sensor>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::SensorsAndEngines.ProtobufModels.Sensor> List {
      get { return list_; }
    }

    /// <summary>Field number for the "Timestamp" field.</summary>
    public const int TimestampFieldNumber = 2;
    private int timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Sensors);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Sensors other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!list_.Equals(other.list_)) return false;
      if (Timestamp != other.Timestamp) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= list_.GetHashCode();
      if (Timestamp != 0) hash ^= Timestamp.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      list_.WriteTo(output, _repeated_list_codec);
      if (Timestamp != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Timestamp);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += list_.CalculateSize(_repeated_list_codec);
      if (Timestamp != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Timestamp);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Sensors other) {
      if (other == null) {
        return;
      }
      list_.Add(other.list_);
      if (other.Timestamp != 0) {
        Timestamp = other.Timestamp;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            list_.AddEntriesFrom(input, _repeated_list_codec);
            break;
          }
          case 16: {
            Timestamp = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
