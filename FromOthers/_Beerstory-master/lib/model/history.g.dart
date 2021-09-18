// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'history.dart';

// **************************************************************************
// TypeAdapterGenerator
// **************************************************************************

class HistoryEntriesAdapter extends TypeAdapter<HistoryEntries> {
  @override
  final typeId = 3;

  @override
  HistoryEntries read(BinaryReader reader) {
    var numOfFields = reader.readByte();
    var fields = <int, dynamic>{
      for (var i = 0; i < numOfFields; i++) reader.readByte(): reader.read(),
    };
    return HistoryEntries(
      entries: (fields[0] as List)?.cast<HistoryEntry>(),
    );
  }

  @override
  void write(BinaryWriter writer, HistoryEntries obj) {
    writer
      ..writeByte(1)
      ..writeByte(0)
      ..write(obj.entries);
  }
}

class HistoryEntryAdapter extends TypeAdapter<HistoryEntry> {
  @override
  final typeId = 4;

  @override
  HistoryEntry read(BinaryReader reader) {
    var numOfFields = reader.readByte();
    var fields = <int, dynamic>{
      for (var i = 0; i < numOfFields; i++) reader.readByte(): reader.read(),
    };
    return HistoryEntry(
      beerId: fields[0] as int,
      times: fields[2] as int,
    )
      .._quantity = fields[1] as double
      ..moreThanQuantity = fields[3] as bool;
  }

  @override
  void write(BinaryWriter writer, HistoryEntry obj) {
    writer
      ..writeByte(4)
      ..writeByte(0)
      ..write(obj.beerId)
      ..writeByte(1)
      ..write(obj._quantity)
      ..writeByte(2)
      ..write(obj.times)
      ..writeByte(3)
      ..write(obj.moreThanQuantity);
  }
}
