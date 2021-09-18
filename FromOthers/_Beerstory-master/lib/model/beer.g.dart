// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'beer.dart';

// **************************************************************************
// TypeAdapterGenerator
// **************************************************************************

class BeerAdapter extends TypeAdapter<Beer> {
  @override
  final typeId = 1;

  @override
  Beer read(BinaryReader reader) {
    var numOfFields = reader.readByte();
    var fields = <int, dynamic>{
      for (var i = 0; i < numOfFields; i++) reader.readByte(): reader.read(),
    };
    return Beer(
      name: fields[0] as String,
      image: fields[1] as String,
      tags: (fields[2] as List)?.cast<String>(),
      degrees: fields[3] as double,
      rating: fields[4] as double,
      prices: (fields[5] as List)?.cast<BeerPrice>(),
    );
  }

  @override
  void write(BinaryWriter writer, Beer obj) {
    writer
      ..writeByte(6)
      ..writeByte(0)
      ..write(obj.name)
      ..writeByte(1)
      ..write(obj.image)
      ..writeByte(2)
      ..write(obj.tags)
      ..writeByte(3)
      ..write(obj.degrees)
      ..writeByte(4)
      ..write(obj.rating)
      ..writeByte(5)
      ..write(obj.prices);
  }
}

class BeerPriceAdapter extends TypeAdapter<BeerPrice> {
  @override
  final typeId = 2;

  @override
  BeerPrice read(BinaryReader reader) {
    var numOfFields = reader.readByte();
    var fields = <int, dynamic>{
      for (var i = 0; i < numOfFields; i++) reader.readByte(): reader.read(),
    };
    return BeerPrice(
      barId: fields[0] as int,
      price: fields[1] as double,
    );
  }

  @override
  void write(BinaryWriter writer, BeerPrice obj) {
    writer
      ..writeByte(2)
      ..writeByte(0)
      ..write(obj.barId)
      ..writeByte(1)
      ..write(obj.price);
  }
}
