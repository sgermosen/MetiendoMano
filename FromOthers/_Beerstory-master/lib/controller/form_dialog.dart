import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';

/// Represents a form dialog result.
class FormDialogResult {
  /// "Success" form dialog result.
  static const FormDialogResult SUCCESS = FormDialogResult(snackBarBackgroundColor: Colors.green, textKey: 'formDialog.success');

  /// "No beer found" form dialog result.
  static const FormDialogResult NO_BEER_FOUND = FormDialogResult(snackBarBackgroundColor: Colors.red, textKey: 'error.addBeerFirst');

  /// "OFF beer not found" form dialog result.
  static const FormDialogResult OPEN_FOOD_FACTS_NOT_FOUND = FormDialogResult(snackBarBackgroundColor: Colors.red, textKey: 'error.openFoodFactsNotFound');

  /// "OFF beer not found" form dialog result.
  static const FormDialogResult OPEN_FOOD_FACTS_GENERIC_ERROR = FormDialogResult(snackBarBackgroundColor: Colors.red, textKey: 'error.openFoodFactsGenericError');

  /// "Cancelled" form dialog result.
  static const FormDialogResult CANCELLED = FormDialogResult(snackBarBackgroundColor: Colors.red);

  /// The SnackBar background color.
  final Color snackBarBackgroundColor;

  /// The SnackBar text color.
  final Color snackBarTextColor;

  /// The text to show.
  final String textKey;

  /// Creates a new form dialog result instance.
  const FormDialogResult({
    @required this.snackBarBackgroundColor,
    this.snackBarTextColor = Colors.white,
    this.textKey,
  });

  /// Creates the SnackBar message with the corresponding text key.
  void createSnackBarMessage(BuildContext context, [Map<String, String> arguments]) {
    if (textKey == null) {
      return;
    }

    Scaffold.of(context).showSnackBar(SnackBar(
      backgroundColor: snackBarBackgroundColor,
      content: Text(
        EzLocalization.of(context).get(textKey, arguments),
        style: Theme.of(context).textTheme.body1.copyWith(color: snackBarTextColor),
      ),
      duration: Duration(seconds: 1),
    ));
  }
}

/// Represents a dialog that holds a form.
abstract class FormDialog<T extends HiveObject> extends StatefulWidget {
  /// The form key.
  final GlobalKey<FormState> formKey;

  /// Creates a new form dialog instance.
  const FormDialog({
    @required this.formKey,
  });

  /// Shows a new form dialog.
  static Future<FormDialogResult> show<T extends HiveObject>({
    @required BuildContext context,
    @required T hiveObject,
    @required String hiveBox,
    @required FormDialog Function(GlobalKey<FormState>) createContent,
  }) {
    final GlobalKey<FormState> formKey = GlobalKey<FormState>();

    return showDialog<FormDialogResult>(
      context: context,
      builder: (_) {
        FormDialog formDialog = createContent(formKey);
        return AlertDialog(
          contentPadding: EdgeInsets.all(0),
          content: formDialog,
          actions: formDialog.createActions(context, hiveBox, hiveObject),
        );
      },
    );
  }

  /// Creates the dialog actions.
  List<Widget> createActions(BuildContext context, String hiveBox, T hiveObject) => [
        FlatButton(
          child: Text(MaterialLocalizations.of(context).cancelButtonLabel),
          onPressed: () => Navigator.pop(context, FormDialogResult.CANCELLED),
        ),
        FlatButton(
          child: Text(MaterialLocalizations.of(context).okButtonLabel),
          onPressed: () async {
            if (!formKey.currentState.validate()) {
              return;
            }

            formKey.currentState.save();
            persistObject(hiveBox, hiveObject);

            Navigator.pop(context, FormDialogResult.SUCCESS);
          },
        ),
      ];

  /// Saves the object.
  Future<void> persistObject(String hiveBox, T hiveObject) async {
    if (hiveObject.isInBox) {
      return hiveObject.save();
    } else {
      Box<T> box = await Hive.openBox(hiveBox);
      return box.add(hiveObject);
    }
  }
}

/// The form dialog state class.
abstract class FormDialogState<T extends FormDialog> extends State<T> {
  @override
  Widget build(BuildContext context) => Container(
        width: MediaQuery.of(context).size.width,
        child: Form(
          key: widget.formKey,
          child: SingleChildScrollView(
            padding: EdgeInsets.all(30),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              children: createChildren(context),
            ),
          ),
        ),
      );

  /// Creates a top padding widget holding the specified child.
  Widget createTopPaddingWidget(Widget child) => Padding(
        padding: EdgeInsets.only(top: 30),
        child: child,
      );

  /// Creates a label.
  Widget createLabel(IconData icon, String textKey) => LabelWidget(
        icon: icon,
        textKey: textKey,
      );

  /// Creates the form children.
  List<Widget> createChildren(BuildContext context);
}

/// A simple form label widget with an icon and a text.
class LabelWidget extends StatelessWidget {
  /// The icon.
  final IconData icon;

  /// The text.
  final String textKey;

  /// Creates a new label instance.
  const LabelWidget({
    @required this.icon,
    @required this.textKey,
  });

  @override
  Widget build(BuildContext context) => Row(
        children: [
          Padding(
            padding: EdgeInsets.only(right: 6),
            child: Icon(
              icon,
              size: 16,
            ),
          ),
          Flexible(
            child: Text(
              EzLocalization.of(context).get(textKey),
              style: Theme.of(context).textTheme.body1.copyWith(fontWeight: FontWeight.bold),
            ),
          ),
        ],
      );
}
