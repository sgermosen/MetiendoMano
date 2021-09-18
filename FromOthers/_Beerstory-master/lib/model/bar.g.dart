// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'bar.dart';

// **************************************************************************
// TypeAdapterGenerator
// **************************************************************************

class BarAdapter extends TypeAdapter<Bar> {
  @override
  final typeId = 0;

  @override
  Bar read(BinaryReader reader) {
    var numOfFields = reader.readByte();
    var fields = <int, dynamic>{
      for (var i = 0; i < numOfFields; i++) reader.readByte(): reader.read(),
    };
    return Bar(
      name: fields[0] as String,
      address: fields[1] as String,
    );
  }

  @override
  void write(BinaryWriter writer, Bar obj) {
    writer
      ..writeByte(2)
      ..writeByte(0)
      ..write(obj.name)
      ..writeByte(1)
      ..write(obj.address);
  }
}
