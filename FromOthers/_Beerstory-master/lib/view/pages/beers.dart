import 'package:beerstory/controller/beer_dialog.dart';
import 'package:beerstory/model/beer.dart';
import 'package:beerstory/utils/ordered_list_view.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:beerstory/view/beer_widget.dart';
import 'package:beerstory/view/pages/page.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';

/// The beers page.
class BeersPage extends HivePage<Beer> {
  /// Creates a new beers page instance.
  const BeersPage()
      : super(
          icon: Icons.list,
          title: 'page.beers.name',
          hiveBox: Beer.HIVE_BOX,
          actionsCount: 2,
          actionBuilder: _createActions,
        );

  @override
  State<StatefulWidget> createState() => _BeersPageState();

  /// Creates the page actions.
  static Widget _createActions(BuildContext context, int position) => position == 0
      ? IconButton(
          icon: Icon(Icons.add),
          onPressed: () => BeerEditor.newBeer(context),
        )
      : Page.createHistoryButton(context);
}

/// The beers page state.
class _BeersPageState extends HivePageState<Beer> {
  @override
  Widget buildHiveWidget(BuildContext context, Box<Beer> hiveBox) {
    if (hiveBox.isEmpty) {
      return CenteredMessage(text: 'page.beers.empty');
    }

    return OrderedListView<Beer>(
      items: hiveBox.values.toList(),
      itemBuilder: (List<Sortable> beers, position) => BeerWidget(
        beer: beers[position],
        backgroundColor: position % 2 == 0 ? Colors.black.withOpacity(0.03) : null,
      ),
    );
  }
}
