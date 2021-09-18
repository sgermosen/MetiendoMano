import 'dart:io';

import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/material.dart';

/// Contains some useful methods.
class Utils {
  /// Maps indexed items by their index associated to their value.
  static Iterable<E> mapIndexed<E, T>(Iterable<T> items, E Function(int index, T item) f) sync* {
    var index = 0;

    for (final item in items) {
      yield f(index, item);
      index = index + 1;
    }
  }

  /// Checks whether the given value is numeric.
  static bool isNumeric(String str) {
    if (str == null) {
      return false;
    }
    return double.tryParse(str) != null;
  }

  /// Transforms the given color to its hex code.
  static String colorToHex(Color color, {bool alpha = false, bool leadingHashSign = true}) {
    String value = color.value.toRadixString(16).padLeft(8);
    if(!alpha) {
      value = value.substring(2);
    }

    return (leadingHashSign ? '#' : '') + value;
  }

  /// Moves a file to another path.
  static Future<File> moveFile(File sourceFile, String newPath) async {
    try {
      return await sourceFile.rename(newPath);
    } on FileSystemException catch (_) {
      final newFile = await sourceFile.copy(newPath);
      await sourceFile.delete();
      return newFile;
    }
  }
}

/// A simple centered text.
class CenteredMessage extends StatelessWidget {
  /// The text key.
  final String text;

  /// Creates a new centered message instance.
  const CenteredMessage({
    this.text,
  });

  @override
  Widget build(BuildContext context) => Center(
        child: Text(
          EzLocalization.of(context).get(text),
        ),
      );
}

/// A tag widget.
class TagWidget extends StatelessWidget {
  /// The tag text.
  final String text;

  /// Creates a new tag widget instance.
  const TagWidget({
    this.text,
  });

  @override
  Widget build(BuildContext context) => Container(
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(16),
          color: Theme.of(context).primaryColor,
        ),
        padding: EdgeInsets.symmetric(
          vertical: 5,
          horizontal: 10,
        ),
        child: Text(
          text,
          overflow: TextOverflow.ellipsis,
          style: Theme.of(context).textTheme.body1.copyWith(color: Colors.white),
        ),
      );
}

/// A simple app styled button.
class AppButton extends StatelessWidget {
  /// The button padding.
  final EdgeInsets padding;

  /// The text.
  final String text;

  /// Callback for when the button has been pressed.
  final VoidCallback onPressed;

  /// Creates a new app button instance.
  const AppButton({
    this.padding,
    @required this.text,
    @required this.onPressed,
  });

  @override
  Widget build(BuildContext context) => Container(
        padding: padding,
        width: MediaQuery.of(context).size.width,
        child: FlatButton(
          child: Text(
            text.toUpperCase(),
            style: Theme.of(context).textTheme.button.copyWith(color: Colors.white),
          ),
          onPressed: onPressed,
          color: Theme.of(context).primaryColor,
        ),
      );
}

/// Allows to display a circular progress indicator while loading data.
class LoadingFutureBuilder<T> extends StatelessWidget {
  /// The future.
  final Future<T> future;

  /// The builder function.
  final Widget Function(BuildContext, T) builder;

  /// Creates a new loading future builder instance.
  const LoadingFutureBuilder({
    @required this.future,
    @required this.builder,
  });

  @override
  Widget build(BuildContext context) => FutureBuilder<T>(
        future: future,
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.done) {
            if (snapshot.hasError) {
              print(snapshot.error);
              return CenteredMessage(text: 'error.unableLoad');
            }

            return builder(context, snapshot.data);
          }

          return CenteredCircularProgressIndicator();
        },
      );
}

/// A simple centered circular progress indicator.
class CenteredCircularProgressIndicator extends StatelessWidget {
  @override
  Widget build(BuildContext context) => Center(
        child: CircularProgressIndicator(),
      );
}

/// A simple value holder.
class ValueHolder<T> {
  /// The value.
  T value;

  /// Creates a new value holder.
  ValueHolder({
    this.value,
  });
}

/// A checkbox form field.
class CheckboxFormField extends FormField<bool> {
  /// Creates a new checkbox form field instance.
  CheckboxFormField({
    @required Widget child,
    @required BuildContext context,
    FormFieldSetter<bool> onSaved,
    FormFieldValidator<bool> validator,
    bool initialValue = false,
    bool autovalidate = false,
  }) : super(
          onSaved: onSaved,
          validator: validator,
          initialValue: initialValue,
          autovalidate: autovalidate,
          builder: (FormFieldState<bool> state) => _build(state, child),
        );

  static Widget _build(FormFieldState<bool> state, Widget child) {
    Widget column = child;
    if(state.hasError) {
      column = Column(
        children: [column, Text(
          state.errorText,
          style: TextStyle(color: Theme.of(state.context).errorColor),
        )],
      );
    }

    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Flexible(
          child: child,
        ),
        Checkbox(
          onChanged: state.didChange,
          value: state.value,
        ),
      ],
    );
  }
}
