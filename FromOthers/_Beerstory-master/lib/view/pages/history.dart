import 'package:beerstory/model/beer.dart';
import 'package:beerstory/model/history.dart';
import 'package:beerstory/utils/ordered_list_view.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:beerstory/view/history_entries_widget.dart';
import 'package:beerstory/view/pages/page.dart';
import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';

/// The history page.
class HistoryPage extends HivePage<HistoryEntries> {
  const HistoryPage()
      : super(
          icon: Icons.history,
          title: 'page.history.name',
          hiveBox: HistoryEntries.HIVE_BOX,
          actionsCount: 1,
          actionBuilder: _createActions,
        );

  @override
  State<StatefulWidget> createState() => _HistoryPageState();

  /// Creates the page actions.
  static Widget _createActions(BuildContext context, _) => IconButton(
        icon: Icon(Icons.delete),
        onPressed: () => showDialog(
          context: context,
          builder: (context) => AlertDialog(
            content: Text(EzLocalization.of(context).get('page.history.clearConfirm')),
            actions: [
              FlatButton(
                onPressed: () => Navigator.pop(context),
                child: Text(MaterialLocalizations.of(context).cancelButtonLabel),
              ),
              FlatButton(
                onPressed: () {
                  Hive.openBox<HistoryEntries>(HistoryEntries.HIVE_BOX).then((box) => box.clear());
                  Navigator.pop(context);
                },
                child: Text(MaterialLocalizations.of(context).okButtonLabel),
              ),
            ],
          ),
        ),
      );
}

/// The history page state.
class _HistoryPageState extends HivePageState<HistoryEntries> {
  /// The future object holding the openBox of the beers box.
  Future<Box<Beer>> _futureOpenBeerBox;

  @override
  void initState() {
    _futureOpenBeerBox = Hive.openBox<Beer>(Beer.HIVE_BOX);
    super.initState();
  }

  @override
  Widget buildHiveWidget(BuildContext context, Box<HistoryEntries> hiveBox) {
    if (hiveBox.isEmpty) {
      return CenteredMessage(text: 'page.history.empty');
    }

    return LoadingFutureBuilder(
      future: _futureOpenBeerBox,
      builder: (_, beers) => OrderedListView(
        items: hiveBox.values.toList(),
        itemBuilder: (List<Sortable> entries, position) => HistoryEntriesWidget(
          beers: beers,
          date: (entries[position] as HistoryEntries).date,
          historyEntries: entries[position],
        ),
        searchBox: false,
        reverseOrder: true,
      ),
    );
  }
}
