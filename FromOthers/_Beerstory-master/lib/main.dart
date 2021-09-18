import 'package:beerstory/controller/history_entry_dialog.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:beerstory/view/pages/bars.dart';
import 'package:beerstory/view/pages/beers.dart';
import 'package:beerstory/view/pages/history.dart';
import 'package:beerstory/view/pages/page.dart';
import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:hive/hive.dart';

/// Hello world !
main() => runApp(BeerstoryApp());

/// The beerstory app class.
class BeerstoryApp extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => BeerstoryAppState();
}

/// The beerstory app state class.
class BeerstoryAppState extends State<BeerstoryApp> {
  /// The localization delegate.
  EzLocalizationDelegate ezLocalization;

  @override
  void initState() {
    ezLocalization = EzLocalizationDelegate(supportedLocales: [Locale('en'), Locale('fr')]);
    super.initState();
  }

  @override
  Widget build(BuildContext context) => MaterialApp(
        title: 'Beerstory',
        theme: ThemeData(
          highlightColor: Colors.black12,
          primarySwatch: Colors.teal,
          textTheme: TextTheme(
            title: TextStyle(
              fontFamily: 'BirdsOfParadise',
              fontSize: 34,
            ),
          ),
        ),
        localeResolutionCallback: ezLocalization.localeResolutionCallback,
        localizationsDelegates: ezLocalization.localizationDelegates,
        supportedLocales: ezLocalization.supportedLocales,
        initialRoute: '/',
        routes: {
          '/': (context) => _PagesScaffold(
                pages: [BeersPage(), BarsPage()],
              ),
          '/history': (context) => _PagesScaffold(
                pages: [HistoryPage()],
              ),
        },
      );

  @override
  void dispose() {
    Hive.close();
    super.dispose();
  }
}

/// A simple scaffold that can display pages.
class _PagesScaffold extends StatefulWidget {
  /// The pages.
  final List<Page> pages;

  /// Creates a new pages scaffold instance.
  _PagesScaffold({
    @required this.pages,
  }) : assert(pages.length > 0);

  @override
  State<StatefulWidget> createState() => _PagesScaffoldState();
}

/// The pages scaffold state class.
class _PagesScaffoldState extends State<_PagesScaffold> with SingleTickerProviderStateMixin {
  /// The current index.
  int _index = 0;

  /// The tab controller.
  TabController _controller;

  @override
  void initState() {
    if (widget.pages.length > 1) {
      _controller = TabController(
        vsync: this,
        length: widget.pages.length,
        initialIndex: _index,
      );
      _controller.addListener(() {
        setState(() => _index = _controller.index);
      });
    }
    super.initState();
  }

  @override
  void dispose() {
    _controller?.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) => Scaffold(
        appBar: _createAppBar(context),
        floatingActionButton: _createActionButton(context),
        floatingActionButtonLocation: widget.pages.length > 1 ? FloatingActionButtonLocation.centerDocked : FloatingActionButtonLocation.centerFloat,
        body: _createBody(),
        bottomNavigationBar: widget.pages.length > 1 ? _createBottomAppBar(context) : null,
        resizeToAvoidBottomPadding: false,
      );

  /// Creates the app bar.
  AppBar _createAppBar(BuildContext context) {
    Page page = widget.pages[_index];
    return AppBar(
      elevation: 0,
      centerTitle: true,
      title: Padding(
        padding: EdgeInsets.only(top: 8),
        child: Text(
          EzLocalization.of(context).get(page.title),
          style: Theme.of(context).textTheme.title.copyWith(color: Colors.white),
        ),
      ),
      actions: [
        for (int i = 0; i < page.actionsCount; i++) Builder(builder: (context) => page.actionBuilder(context, i)),
      ],
    );
  }

  /// Creates the action button.
  Widget _createActionButton(BuildContext context) => Builder(
        builder: (context) => FloatingActionButton(
          elevation: 0,
          focusElevation: 0,
          hoverElevation: 0,
          highlightElevation: 0,
          child: SvgPicture.asset(
            'assets/images/add.svg',
          ),
          backgroundColor: Theme.of(context).primaryColorDark,
          onPressed: () => HistoryEntryEditor.newEntry(context),
        ),
      );

  /// Creates the body widget.
  Widget _createBody() => widget.pages.length == 1
      ? widget.pages[0]
      : TabBarView(
          controller: _controller,
          children: widget.pages.map<Widget>((Page page) => page).toList(),
        );

  /// Creates the bottom app bar.
  Widget _createBottomAppBar(BuildContext context) => BottomAppBar(
        color: Theme.of(context).primaryColor,
        child: Padding(
          padding: EdgeInsets.symmetric(horizontal: 5),
          child: Row(
            mainAxisSize: MainAxisSize.max,
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: Utils.mapIndexed(
              widget.pages,
              (index, page) => _createBottomAppBarButton(context, index, page),
            ).toList(),
          ),
        ),
      );

  /// Creates a bottom app bar button.
  Widget _createBottomAppBarButton(BuildContext context, int index, Page page) => FlatButton.icon(
        shape: RoundedRectangleBorder(
          borderRadius: BorderRadius.circular(20),
        ),
        color: index == _index ? Theme.of(context).primaryColorDark : null,
        icon: Icon(
          page.icon,
          color: Colors.white,
        ),
        label: Text(
          EzLocalization.of(context).get(page.title),
          style: Theme.of(context).textTheme.button.copyWith(color: Colors.white),
        ),
        onPressed: () {
          if (index != _index) {
            _index = index;
            _controller.animateTo(_index);
          }
        },
      );
}
