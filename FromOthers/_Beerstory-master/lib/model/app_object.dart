import 'package:flutter/cupertino.dart';
import 'package:hive/hive.dart';

/// An editable app object.
abstract class AppObject extends HiveObject {
  /// Opens the object editor.
  void openEditor(BuildContext context, [Map additionalArguments]);
}
