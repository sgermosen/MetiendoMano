import 'package:beerstory/model/bar.dart';
import 'package:beerstory/model/beer.dart';
import 'package:beerstory/model/history.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';
import 'package:hive_flutter/hive_flutter.dart';
import 'package:path_provider/path_provider.dart';

/// Represents an app page.
abstract class Page extends StatefulWidget {
  /// The page icon.
  final IconData icon;

  /// The page title.
  final String title;

  /// The number of actions.
  final int actionsCount;

  /// The actions builder.
  final Widget Function(BuildContext, int) actionBuilder;

  /// Creates a new page instance.
  const Page({
    @required this.icon,
    @required this.title,
    @required this.actionsCount,
    @required this.actionBuilder,
  });

  @protected
  static createHistoryButton(BuildContext context) => IconButton(
        icon: Icon(Icons.history),
        onPressed: () => Navigator.pushNamed(context, '/history'),
      );
}

/// A page which is holding a Hive box.
abstract class HivePage<T extends HiveObject> extends Page {
  /// The Hive box.
  final String hiveBox;

  /// Creates a new Hive page instance.
  const HivePage({
    @required IconData icon,
    @required String title,
    @required int actionsCount,
    @required Widget Function(BuildContext, int) actionBuilder,
    @required this.hiveBox,
  }) : super(
          icon: icon,
          title: title,
          actionsCount: actionsCount,
          actionBuilder: actionBuilder,
        );

  @override
  State<StatefulWidget> createState();
}

/// The Hive page state.
abstract class HivePageState<T extends HiveObject> extends State<HivePage<T>> {
  /// Whether Hive has been initialized.
  static bool _hiveInitialized = false;

  /// The future object holding the Hive box.
  Future<Box<T>> _futureOpenBox;

  @override
  void initState() {
    _futureOpenBox = _openBox();
    super.initState();
  }

  @override
  Widget build(BuildContext context) => LoadingFutureBuilder<Box<T>>(
        future: _futureOpenBox,
        builder: (_, box) => ValueListenableBuilder(
          valueListenable: box.listenable(),
          builder: (context, box, child) => buildHiveWidget(context, box),
        ),
      );

  /// Opens the current box.
  Future<Box<T>> _openBox() async {
    if (!_hiveInitialized) {
      Hive.init((await getApplicationDocumentsDirectory()).path);
      Hive.registerAdapter(BarAdapter());
      Hive.registerAdapter(BeerAdapter());
      Hive.registerAdapter(BeerPriceAdapter());
      Hive.registerAdapter(HistoryEntriesAdapter());
      Hive.registerAdapter(HistoryEntryAdapter());
      _hiveInitialized = true;
    }
    return Hive.openBox<T>(widget.hiveBox);
  }

  /// Builds the Hive widget.
  Widget buildHiveWidget(BuildContext context, Box<T> hiveBox);
}
