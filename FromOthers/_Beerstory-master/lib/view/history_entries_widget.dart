import 'package:beerstory/controller/beer_dialog.dart';
import 'package:beerstory/model/beer.dart';
import 'package:beerstory/model/history.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:beerstory/view/beer_widget.dart';
import 'package:beerstory/view/hive_widget.dart';
import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';
import 'package:intl/intl.dart';

/// A widget that allows to display history entries.
class HistoryEntriesWidget extends StatelessWidget {
  /// The corresponding date.
  final DateTime date;

  /// The entries.
  final HistoryEntries historyEntries;

  /// The beers box.
  final Box<Beer> beers;

  /// Creates a new history entries widget.
  HistoryEntriesWidget({
    @required this.date,
    @required this.historyEntries,
    @required this.beers,
  });

  @override
  Widget build(BuildContext context) => Column(
        children: [
          Container(
            child: Text(
              DateFormat.yMMMMd(EzLocalization.of(context).locale.languageCode).format(date),
              style: Theme.of(context).textTheme.body1.copyWith(
                    color: Colors.white,
                    fontSize: 20,
                    fontWeight: FontWeight.bold,
                  ),
            ),
            width: MediaQuery.of(context).size.width,
            color: Theme.of(context).primaryColorDark,
            padding: EdgeInsets.all(10),
          ),
        ]..addAll(
            Utils.mapIndexed(
              historyEntries.entries,
              (index, entry) => HistoryEntryWidget(
                historyEntries: historyEntries,
                historyEntry: entry,
                beer: beers.get(entry.beerId),
                backgroundColor: index % 2 == 1 ? Colors.black.withOpacity(0.03) : null,
              ),
            ),
          ),
      );
}

/// A widget that allows to display a history entry.
class HistoryEntryWidget extends HiveWidget<HistoryEntry> {
  /// The parent history entries.
  final HistoryEntries historyEntries;

  /// The history entry.
  final HistoryEntry historyEntry;

  /// The history entry beer instance.
  final Beer beer;

  /// Creates a new history entry widget.
  HistoryEntryWidget({
    @required this.historyEntries,
    @required this.historyEntry,
    @required this.beer,
    @required Color backgroundColor,
  }) : super(
          object: historyEntry,
          backgroundColor: backgroundColor,
        );

  @override
  Widget buildContent(BuildContext context) => Column(
        crossAxisAlignment: CrossAxisAlignment.end,
        children: [
          Row(
            children: [
              Padding(
                padding: EdgeInsets.only(right: 20),
                child: TagWidget(
                  text: historyEntry.times.toString() + 'x',
                ),
              ),
              Flexible(
                child: BeerWidget(
                  beer: beer,
                  padding: null,
                  addClickListeners: false,
                ),
              ),
            ],
          ),
          Padding(
            padding: EdgeInsets.only(top: 10),
            child: Text.rich(TextSpan(children: [
              TextSpan(
                text: EzLocalization.of(context).get('page.history.total').toUpperCase(),
                style: Theme.of(context).textTheme.body1.copyWith(fontWeight: FontWeight.bold),
              ),
              TextSpan(
                text: ' ' +
                    EzLocalization.of(context).get('page.history.quantity', {
                      'prefix': historyEntry.moreThanQuantity ? '+' : '',
                      'quantity': NumberFormat.decimalPattern().format(historyEntry.quantity ?? 0),
                    }),
              ),
            ])),
          )
        ],
      );

  @override
  void onTap(BuildContext context) => BeerEditor.show(
        context: context,
        beer: beer,
        previewMode: true,
      );

  @override
  void editCallback(BuildContext context) => historyEntry.openEditor(context, {'historyEntries': historyEntries});

  @override
  void deleteCallback(BuildContext context) => historyEntries.removeEntryAndDeleteIfNeeded(historyEntry);
}
