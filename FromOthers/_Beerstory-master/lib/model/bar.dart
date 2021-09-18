import 'package:beerstory/controller/bar_dialog.dart';
import 'package:beerstory/model/app_object.dart';
import 'package:beerstory/utils/ordered_list_view.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:hive/hive.dart';

part 'bar.g.dart';

/// Represents a bar.
@HiveType(typeId: 0)
class Bar extends AppObject with Sortable {
  /// The bars Hive box.
  static const String HIVE_BOX = 'bars';

  /// The bar name.
  @HiveField(0)
  String name;

  /// The bar address.
  @HiveField(1)
  String address;

  /// Creates a new bar instance.
  Bar({
    @required this.name,
    this.address,
  });

  @override
  String get orderKey => name.toLowerCase();

  @override
  List<String> get searchTerms => [name, address ?? ''];

  @override
  void openEditor(BuildContext context, [Map additionalArguments]) => BarEditor.show(
        context: context,
        bar: this,
      );
}
