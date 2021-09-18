import 'dart:io';

import 'package:beerstory/controller/beer_dialog.dart';
import 'package:beerstory/model/beer.dart';
import 'package:beerstory/model/history.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:beerstory/view/hive_widget.dart';
import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';
import 'package:intl/intl.dart';
import 'package:smooth_star_rating/smooth_star_rating.dart';

/// The beer widget.
class BeerWidget extends HiveWidget<Beer> {
  /// The beer.
  final Beer beer;

  /// Creates a new beer widget instance.
  const BeerWidget({
    bool addClickListeners = true,
    @required this.beer,
    Color backgroundColor,
    EdgeInsets padding = const EdgeInsets.all(20),
  }) : super(
          addClickListeners: addClickListeners,
          object: beer,
          backgroundColor: backgroundColor,
          padding: padding,
        );

  @override
  Widget buildContent(BuildContext context) {
    List<Widget> children = [_createTitle(context)];

    Widget price = _createPrice(context);
    if (price != null) {
      children.add(price);
    }

    Widget rating = _createRating(context);
    Widget tags = _createTags(context);
    if (rating != null || tags != null) {
      children.add(SizedBox.fromSize(size: Size.fromHeight(8)));

      if (rating != null) {
        children.add(rating);
      }

      if (tags != null) {
        children.add(tags);
      }
    }

    return Row(
      children: [
        Padding(
          padding: EdgeInsets.only(right: 20),
          child: BeerImage(beer: beer),
        ),
        Expanded(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: children,
          ),
        ),
      ],
    );
  }

  @override
  void onTap(BuildContext context) => BeerEditor.show(
        context: context,
        beer: beer,
        previewMode: true,
      );

  /// Creates the title.
  Widget _createTitle(BuildContext context) {
    Widget name = Padding(
      padding: EdgeInsets.only(left: 3),
      child: Text(
        beer.name,
        overflow: TextOverflow.ellipsis,
        style: Theme.of(context).textTheme.body1.copyWith(
              fontWeight: FontWeight.bold,
              fontSize: 18,
            ),
      ),
    );

    if (beer.degrees == null) {
      return name;
    }

    return Row(
      mainAxisSize: MainAxisSize.max,
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Flexible(
          child: name,
        ),
        TagWidget(text: beer.degrees.toString() + '°'),
      ],
    );
  }

  /// Creates the price.
  Widget _createPrice(BuildContext context) {
    if (beer.prices == null || beer.prices.isEmpty) {
      return null;
    }

    NumberFormat numberFormat = NumberFormat.simpleCurrency(locale: EzLocalization.of(context).locale.languageCode);
    String text;
    if (beer.prices.length == 1) {
      double price = beer.prices[0].price;
      if (price == null) {
        return null;
      }

      text = numberFormat.format(price);
    } else {
      double min;
      double max;
      for (BeerPrice price in beer.prices) {
        if (price.price == null) {
          continue;
        }
        if (min == null || price.price < min) {
          min = price.price;
        }
        if (max == null || price.price > max) {
          max = price.price;
        }
      }

      if (min == null /* || max == null */) {
        return null;
      }

      text = numberFormat.format(min) + ' — ' + numberFormat.format(max);
    }

    return Padding(
      padding: EdgeInsets.only(left: 3),
      child: Text(text),
    );
  }

  /// Creates the rating.
  Widget _createRating(BuildContext context) => beer.rating == null
      ? null
      : SmoothStarRating(
          size: 25,
          rating: beer.rating,
        );

  /// Creates the tags.
  Widget _createTags(BuildContext context) {
    if (beer.tags == null || beer.tags.isEmpty) {
      return null;
    }

    return Padding(
      padding: EdgeInsets.only(top: 4),
      child: Wrap(
        runSpacing: 4,
        spacing: 4,
        children: beer.tags.map((tag) => TagWidget(text: tag)).toList(),
      ),
    );
  }

  @override
  void deleteCallback(BuildContext context) async {
    int targetId = beer.key;
    List<HistoryEntries> history = List.of((await Hive.openBox<HistoryEntries>(HistoryEntries.HIVE_BOX)).values);
    for (HistoryEntries entries in history) {
      List<HistoryEntry> entriesList = List.of(entries.entries ?? []);
      entriesList.forEach((entry) {
        if (entry.beerId == targetId) {
          entries.removeEntryAndDeleteIfNeeded(entry);
        }
      });
    }
    super.deleteCallback(context);
  }
}

/// Allows to display a beer image.
class BeerImage extends StatelessWidget {
  /// The name.
  final String name;

  /// The image.
  final String image;

  /// The radius.
  final double radius;

  /// Creates a beer image from a beer.
  BeerImage({
    Beer beer,
    double radius = 50,
  }) : this.fromNameImage(
          name: beer?.name,
          image: beer?.image,
          radius: radius,
        );

  /// Creates a beer image from a name and an image.
  BeerImage.fromNameImage({
    this.name,
    this.image,
    this.radius = 50,
  });

  @override
  Widget build(BuildContext context) => image == null
      ? LetterAvatar(
          text: name,
          radius: radius,
        )
      : CircleAvatar(
          backgroundColor: Colors.black12,
          backgroundImage: FileImage(File(image)),
          radius: radius,
        );
}

/// A simple image that displays a letter.
class LetterAvatar extends StatelessWidget {
  /// The radius.
  final double radius;

  /// The text.
  final String text;

  /// Creates a new letter avatar instance.
  const LetterAvatar({
    this.radius = 100,
    @required this.text,
  });

  @override
  Widget build(BuildContext context) => CircleAvatar(
        backgroundColor: Theme.of(context).primaryColor,
        child: Text(
          _textToWrite,
          style: Theme.of(context).textTheme.caption.copyWith(fontSize: radius, color: Colors.white.withOpacity(0.8)),
        ),
        radius: radius,
      );

  /// Returns the text to write.
  String get _textToWrite {
    if (this.text == null) {
      return '?';
    }

    String text = this.text.replaceAll(RegExp(r"[^\s\w]"), '').trim().toUpperCase();
    if (text.isEmpty) {
      return '?';
    }

    List<String> parts = text.split(' ');
    if (parts.length == 1) {
      List<String> otherSplit = text.split('-');
      if (otherSplit.length > 1 && otherSplit[1].length > 0) {
        return otherSplit[0][0] + otherSplit[1][0];
      }

      if (text.length == 2) {
        return text;
      }

      return parts[0][0];
    }

    return parts[0][0] + parts[1][0];
  }
}
