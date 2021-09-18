import 'package:beerstory/controller/form_dialog.dart';
import 'package:beerstory/model/bar.dart';
import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/material.dart';

/// The bar editor.
class BarEditor extends FormDialog<Bar> {
  /// The bar.
  final Bar bar;

  /// The bar editor internal constructor.
  const BarEditor._internal({
    @required this.bar,
    @required GlobalKey<FormState> formKey,
  }) : super(
          formKey: formKey,
        );

  @override
  State<StatefulWidget> createState() => _BarEditorState();

  /// Shows a bar editor for a new bar.
  static void newBar(BuildContext context) => show(context: context, bar: Bar(name: '')).then((result) {
        if (result == FormDialogResult.SUCCESS) {
          result.createSnackBarMessage(context, {'element': EzLocalization.of(context).get('formDialog.bar')});
        }
      });

  /// Shows a bar editor.
  static Future<FormDialogResult> show({
    @required BuildContext context,
    @required Bar bar,
  }) =>
      FormDialog.show(
        context: context,
        hiveObject: bar,
        hiveBox: Bar.HIVE_BOX,
        createContent: (formKey) => BarEditor._internal(
          bar: bar,
          formKey: formKey,
        ),
      );
}

/// The bar editor state.
class _BarEditorState extends FormDialogState<BarEditor> {
  @override
  List<Widget> createChildren(BuildContext context) => [
        createLabel(Icons.edit, 'barDialog.name.label'),
        TextFormField(
          decoration: InputDecoration(hintText: EzLocalization.of(context).get('barDialog.name.hint')),
          initialValue: widget.bar.name,
          validator: (value) {
            if (value.isEmpty) {
              return EzLocalization.of(context).get('error.notFilled');
            }
            return null;
          },
          onSaved: (value) => widget.bar.name = value,
        ),
        createTopPaddingWidget(createLabel(Icons.near_me, 'barDialog.address.label')),
        TextFormField(
          decoration: InputDecoration(hintText: EzLocalization.of(context).get('barDialog.address.hint')),
          initialValue: widget.bar.address,
          minLines: 1,
          maxLines: 2,
          onSaved: (value) => widget.bar.address = value,
        ),
      ];
}
