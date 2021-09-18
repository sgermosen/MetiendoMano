import 'dart:convert';
import 'dart:io';

import 'package:beerstory/controller/beer_dialog.dart';
import 'package:beerstory/model/app_object.dart';
import 'package:beerstory/utils/ordered_list_view.dart';
import 'package:http/http.dart' as http;
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';
import 'package:path_provider/path_provider.dart';

part 'beer.g.dart';

/// Represents a beer.
@HiveType(typeId: 1)
class Beer extends AppObject with Sortable {
  /// The beers Hive box.
  static const String HIVE_BOX = 'beers';

  /// The beer name.
  @HiveField(0)
  String name;

  /// The beer image.
  @HiveField(1)
  String image;

  /// The beer tags.
  @HiveField(2)
  List<String> tags;

  /// The beer degrees.
  @HiveField(3)
  double degrees;

  /// The beer rating.
  @HiveField(4)
  double rating;

  /// The beer prices.
  @HiveField(5)
  List<BeerPrice> prices;

  /// Creates a new beer instance.
  Beer({
    @required this.name,
    this.image,
    this.tags,
    this.degrees,
    this.rating,
    this.prices,
  });

  /// Fetches a beer from OpenFoodFacts servers.
  static Future<dynamic> fromOpenFoodFacts(String barcode) async {
    try {
      Map<String, String> headers = {'User-Agent': 'Beerstory'};
      http.Response response = await http.get(
        'https://world.openfoodfacts.org/api/v0/product/$barcode.json',
        headers: headers,
      );
      Map<String, dynamic> json = jsonDecode(response.body);
      if (json['status'] == 0) {
        return OpenFoodFactsResult.NOT_FOUND;
      }

      String name;
      String image;

      Map<String, dynamic> product = json['product'];
      if (product.containsKey('image_url')) {
        String imageUrl = product['image_url'];
        File file = new File((await getApplicationDocumentsDirectory()).path + '/' + imageUrl.split('/').last);
        await file.writeAsBytes((await http.get(imageUrl, headers: headers)).bodyBytes);

        image = file.path;
      }

      if (product.containsKey('product_name')) {
        name = product['product_name'];
      }

      if (product.containsKey('brands')) {
        name = product['brands'].split(',')[0] + ' - ' + name;
      }

      return Beer(name: name, image: image);
    } catch (ex) {
      return OpenFoodFactsResult.GENERIC_ERROR;
    }
  }

  @override
  String get orderKey => name.toLowerCase();

  @override
  List<String> get searchTerms => [name]..addAll(tags ?? []);

  @override
  void openEditor(BuildContext context, [Map additionalArguments]) => BeerEditor.show(
        context: context,
        beer: this,
      );
}

/// Represents a price associated with a bar.
@HiveType(typeId: 2)
class BeerPrice {
  /// The bar id.
  @HiveField(0)
  int barId;

  /// The price.
  @HiveField(1)
  double price;

  /// Creates a new beer price instance.
  BeerPrice({
    this.barId,
    this.price,
  });
}

/// All OpenFoodFacts possible results.
enum OpenFoodFactsResult { NOT_FOUND, GENERIC_ERROR }
