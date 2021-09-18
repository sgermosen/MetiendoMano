import 'package:beerstory/controller/history_entry_dialog.dart';
import 'package:beerstory/model/app_object.dart';
import 'package:beerstory/utils/ordered_list_view.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';
import 'package:intl/intl.dart';

import 'beer.dart';

part 'history.g.dart';

/// Represents a list of history entries associated with a date.
@HiveType(typeId: 3)
class HistoryEntries extends HiveObject with Sortable {
  /// The history Hive box.
  static const String HIVE_BOX = 'history';

  /// The key date formatter.
  static DateFormat formatter = DateFormat('yyyy-MM-dd');

  /// The entries list.
  @HiveField(0)
  List<HistoryEntry> entries;

  /// Creates a new history entries instance.
  HistoryEntries({
    @required this.entries,
  });

  /// Returns the date.
  DateTime get date => formatter.parse(key);

  /// Inserts the specified entry at the specified date.
  static Future<HistoryEntries> insertEntry(DateTime date, HistoryEntry entry, [Box<HistoryEntries> entriesBox]) async {
    if (entriesBox == null) {
      entriesBox = await Hive.openBox<HistoryEntries>(HIVE_BOX);
    }

    String stringDate = formatter.format(date);
    HistoryEntries dateEntries = entriesBox.get(stringDate);
    if (dateEntries == null) {
      dateEntries = HistoryEntries(entries: [entry]);
      entriesBox.put(stringDate, dateEntries);
      return dateEntries;
    }

    for (HistoryEntry dateEntry in dateEntries.entries) {
      if (dateEntry.beerId == entry.beerId) {
        dateEntry.addToEntry(entry);
        dateEntries.save();
        return dateEntries;
      }
    }

    dateEntries.entries.add(entry);
    dateEntries.save();
    return dateEntries;
  }

  /// Removes the specified entry and deletes the entries list if empty.
  Future<void> removeEntryAndDeleteIfNeeded(HistoryEntry entry) async {
    entries.remove(entry);
    if (entries.isEmpty) {
      delete();
      return;
    }

    save();
  }

  @override
  Comparable get orderKey => key ?? '';

  /// Sorts the entries thanks to the beers box.
  Future<void> sortEntries(Box<Beer> beers) {
    entries.sort((a, b) => beers.get(a.beerId).name.compareTo(beers.get(b.beerId).name));
    return save();
  }
}

/// Represents an history entry.
@HiveType(typeId: 4)
class HistoryEntry extends AppObject {
  /// The beer id.
  @HiveField(0)
  int beerId;

  /// The quantity.
  @HiveField(1)
  double _quantity;

  /// The number of times this beer has been drank.
  @HiveField(2)
  int times;

  /// Whether this is more than the current quantity.
  @HiveField(3)
  bool moreThanQuantity;

  /// Creates a new history entry instance.
  HistoryEntry({
    this.beerId,
    double quantity,
    this.times = 1,
  }) {
    _quantity = quantity;
    updateMoreThanQuantity();
  }

  /// Returns the quantity.
  double get quantity => this._quantity;

  /// Sets the quantity.
  set quantity(double quantity) {
    _quantity = quantity;
    updateMoreThanQuantity();
  }

  /// Adds the specified entry to this entry.
  void addToEntry(HistoryEntry entry) {
    if (entry._quantity == null) {
      moreThanQuantity = true;
    } else {
      _quantity = (_quantity ?? 0) + entry._quantity;
    }

    times += entry.times;
  }

  /// Updates the moreThanQuantity field.
  void updateMoreThanQuantity() => moreThanQuantity = _quantity == null;

  @override
  void openEditor(BuildContext context, [Map additionalArguments]) {
    dynamic historyEntries = additionalArguments['historyEntries'];
    if (historyEntries == null || !(historyEntries is HistoryEntries)) {
      throw ArgumentError.notNull("additionalArguments['historyEntries']");
    }

    HistoryEntryEditor.show(
      context: context,
      date: historyEntries.date,
      historyEntries: historyEntries,
      historyEntry: this,
      showMoreThanQuantityField: true,
    );
  }
}
