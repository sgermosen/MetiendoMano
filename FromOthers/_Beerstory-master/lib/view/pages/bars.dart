import 'package:beerstory/controller/bar_dialog.dart';
import 'package:beerstory/model/bar.dart';
import 'package:beerstory/utils/ordered_list_view.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:beerstory/view/bar_widget.dart';
import 'package:beerstory/view/pages/page.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';

/// The bars page.
class BarsPage extends HivePage<Bar> {
  /// Creates a new bars page instance.
  const BarsPage()
      : super(
          icon: Icons.local_bar,
          title: 'page.bars.name',
          hiveBox: Bar.HIVE_BOX,
          actionsCount: 2,
          actionBuilder: _createActions,
        );

  @override
  State<StatefulWidget> createState() => _BarsPageState();

  /// Creates the page actions.
  static Widget _createActions(BuildContext context, int position) => position == 0
      ? IconButton(
          icon: Icon(Icons.add),
          onPressed: () => BarEditor.newBar(context),
        )
      : Page.createHistoryButton(context);
}

/// The bars page state.
class _BarsPageState extends HivePageState<Bar> {
  @override
  Widget buildHiveWidget(BuildContext context, Box<Bar> hiveBox) {
    if (hiveBox.isEmpty) {
      return CenteredMessage(text: 'page.bars.empty');
    }

    return OrderedListView<Bar>(
      items: hiveBox.values.toList(),
      itemBuilder: (List<Sortable> bars, position) => BarWidget(
        bar: bars[position],
        backgroundColor: position % 2 == 0 ? Colors.black.withOpacity(0.03) : null,
      ),
    );
  }
}
