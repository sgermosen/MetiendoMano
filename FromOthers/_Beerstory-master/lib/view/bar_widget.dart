import 'package:beerstory/model/bar.dart';
import 'package:beerstory/model/beer.dart';
import 'package:beerstory/view/hive_widget.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';
import 'package:url_launcher/url_launcher.dart';

/// Allows to show a bar.
class BarWidget extends HiveWidget<Bar> {
  /// The bar.
  final Bar bar;

  /// Creates a new bar widget instance.
  const BarWidget({
    this.bar,
    Color backgroundColor,
  }) : super(
          object: bar,
          backgroundColor: backgroundColor,
        );

  @override
  Widget buildContent(BuildContext context) {
    Widget name = Text(
      bar.name,
      overflow: TextOverflow.ellipsis,
      style: Theme.of(context).textTheme.body1.copyWith(
        fontWeight: FontWeight.bold,
        fontSize: 18,
      ),
    );

    if(bar.address == null || bar.address.isEmpty) {
      return name;
    }

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        name,
        Text(
          bar.address,
          overflow: TextOverflow.ellipsis,
        ),
      ],
    );
  }

  @override
  void onTap(BuildContext context) => bar.address == null ? onLongPress(context) : _showBarOnMap(context);

  @override
  void deleteCallback(BuildContext context) async {
    int barKey = bar.key;
    Box<Beer> beers = await Hive.openBox<Beer>(Beer.HIVE_BOX);
    for(Beer beer in beers.values) {
      bool hasChanged = false;
      if(beer.prices == null || beer.prices.isEmpty) {
        continue;
      }

      List<BeerPrice> prices = List.of(beer.prices);
      for(BeerPrice price in prices) {
        if(price.barId == barKey) {
          beer.prices.remove(price);
          hasChanged = true;
        }
      }

      if(hasChanged) {
        beer.save();
      }
    }
    super.deleteCallback(context);
  }

  /// Opens Google Maps to show the bar address.
  void _showBarOnMap(BuildContext context) => launch('https://www.google.com/maps/search/?api=1&query=' + Uri.encodeQueryComponent(bar.address));
}
