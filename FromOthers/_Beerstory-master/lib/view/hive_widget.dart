import 'package:beerstory/model/app_object.dart';
import 'package:beerstory/utils/choice_dialog.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';

/// An app styled widget.
abstract class AppWidget extends StatelessWidget {
  /// The background color.
  final Color backgroundColor;

  /// The widget padding.
  final EdgeInsets padding;

  /// Whether to add click listeners.
  final bool addClickListeners;

  /// Creates a new app widget instance.
  const AppWidget({
    this.backgroundColor,
    this.padding = const EdgeInsets.all(20),
    this.addClickListeners = true,
  });

  @override
  Widget build(BuildContext context) {
    Widget container = Container(
      padding: padding,
      color: backgroundColor,
      child: buildContent(context),
    );

    if (!addClickListeners) {
      return container;
    }

    return InkWell(
      onLongPress: () => onLongPress(context),
      onTap: () => onTap(context),
      child: container,
    );
  }

  /// Builds the widget content.
  Widget buildContent(BuildContext context);

  /// On long press listener.
  void onLongPress(BuildContext context);

  /// On tap listener.
  void onTap(BuildContext context);
}

/// An app widget that holds an app object.
abstract class HiveWidget<T extends AppObject> extends AppWidget {
  /// The object.
  final T object;

  /// Creates a new Hive widget instance.
  const HiveWidget({
    @required this.object,
    Color backgroundColor,
    EdgeInsets padding = const EdgeInsets.all(20),
    bool addClickListeners = true,
  }) : super(
          backgroundColor: backgroundColor,
          padding: padding,
          addClickListeners: addClickListeners,
        );

  @override
  void onLongPress(BuildContext context) => ChoiceDialog(
        choices: [
          Choice(
            text: 'action.edit',
            icon: Icons.edit,
            callback: () => editCallback(context),
          ),
          Choice(
            text: 'action.delete',
            icon: Icons.delete,
            callback: () => deleteCallback(context),
          ),
        ],
      ).show(context);

  /// The edit callback.
  void editCallback(BuildContext context) => object.openEditor(context);

  /// The delete callback.
  void deleteCallback(BuildContext context) => object.delete();
}
