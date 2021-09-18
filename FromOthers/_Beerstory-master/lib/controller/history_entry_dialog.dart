import 'package:beerstory/controller/form_dialog.dart';
import 'package:beerstory/model/beer.dart';
import 'package:beerstory/model/history.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:beerstory/view/beer_widget.dart';
import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:hive/hive.dart';
import 'package:intl/intl.dart';

/// The history entry editor.
class HistoryEntryEditor extends FormDialog<HistoryEntry> {
  /// The date holder.
  final ValueHolder<DateTime> date;

  /// The current history entries (if editing).
  final HistoryEntries historyEntries;

  /// The history entry.
  final HistoryEntry historyEntry;

  /// Whether to show the "more than quantity" field.
  final bool showMoreThanQuantityField;

  /// The history entry internal constructor.
  const HistoryEntryEditor._internal({
    this.historyEntries,
    @required this.historyEntry,
    @required this.date,
    this.showMoreThanQuantityField,
    @required GlobalKey<FormState> formKey,
  }) : super(
          formKey: formKey,
        );

  @override
  State<StatefulWidget> createState() => _HistoryEntryEditorState();

  /// Shows a history entry editor for new history entry.
  static void newEntry(context) => show(
        context: context,
        historyEntry: HistoryEntry(),
        date: DateTime.now(),
        showMoreThanQuantityField: false,
      ).then((result) {
        if (result == FormDialogResult.SUCCESS) {
          showDialog(context: context, builder: (_) => AlertDialog(content: _SuccessDialogContent()));
        } else if (result == FormDialogResult.NO_BEER_FOUND) {
          result.createSnackBarMessage(context);
        }
      });

  /// Shows a new history entry editor.
  static Future<FormDialogResult> show({
    HistoryEntries historyEntries,
    @required BuildContext context,
    @required DateTime date,
    @required HistoryEntry historyEntry,
    bool showMoreThanQuantityField: true,
  }) =>
      FormDialog.show(
        context: context,
        hiveObject: historyEntry,
        hiveBox: HistoryEntries.HIVE_BOX,
        createContent: (formKey) => HistoryEntryEditor._internal(
          historyEntries: historyEntries,
          historyEntry: historyEntry,
          date: ValueHolder<DateTime>(value: date),
          formKey: formKey,
          showMoreThanQuantityField: showMoreThanQuantityField,
        ),
      );

  @override
  Future<void> persistObject(String hiveBox, HistoryEntry entry) async {
    if (!showMoreThanQuantityField) {
      entry.updateMoreThanQuantity();
    }

    if(historyEntries != null) {
      historyEntries.removeEntryAndDeleteIfNeeded(entry);
    }
    HistoryEntries entries = await HistoryEntries.insertEntry(date.value, entry);
    return entries.sortEntries(await Hive.openBox<Beer>(Beer.HIVE_BOX));
  }
}

/// The history entry editor.
class _HistoryEntryEditorState extends FormDialogState<HistoryEntryEditor> {
  /// The beer to edit.
  Beer _beer;

  /// The beer quantity.
  double _quantity;

  /// Number of times this beer has been drank.
  int _times;

  /// And the current date.
  DateTime _date;

  /// The beers list.
  List<Beer> _beers;

  /// Whether to show more details.
  bool _showMore = false;

  @override
  void initState() {
    _quantity = widget.historyEntry.quantity;
    _times = widget.historyEntry.times;
    _date = widget.date.value;

    Hive.openBox<Beer>(Beer.HIVE_BOX).then((beerBox) {
      WidgetsBinding.instance.addPostFrameCallback((_) => setState(() {
            _beers = beerBox.values.toList();
            if (_beers.isEmpty) {
              Navigator.pop(context, FormDialogResult.NO_BEER_FOUND);
              return;
            }

            _beer = beerBox.get(widget.historyEntry.beerId);
            _beers.sort((a, b) => a.orderKey.compareTo(b.orderKey));

            if (_beer == null) {
              _beer = _beers.first;
            }
          }));
    });
    super.initState();
  }

  @override
  List<Widget> createChildren(BuildContext context) {
    if (_beers == null) {
      return [CenteredCircularProgressIndicator()];
    }

    List<Widget> children = [
      BeerImage(
        beer: _beer,
        radius: 100,
      ),
      createTopPaddingWidget(createLabel(Icons.list, 'historyEntryDialog.beer.label')),
      _createSelectBeerField(context),
    ];

    if (_showMore) {
      Map<double, String> quantities = {
        33.0: EzLocalization.of(context).get('historyEntryDialog.quantity.quantities.bottle'),
        50.0: EzLocalization.of(context).get('historyEntryDialog.quantity.quantities.halfPint'),
        100.0: EzLocalization.of(context).get('historyEntryDialog.quantity.quantities.pint'),
      };

      children.addAll([
        createTopPaddingWidget(createLabel(Icons.local_bar, 'historyEntryDialog.quantity.label')),
        _createSelectQuantityField(context, quantities),
        createTopPaddingWidget(createLabel(Icons.local_bar, 'historyEntryDialog.times.label')),
        _createTimesField(context),
        _createInputDateField(context),
      ]);

      if (_quantity != null && !quantities.keys.contains(_quantity)) {
        children.insert(5, _createQuantityField(context));
      }

      if(widget.showMoreThanQuantityField) {
        children.add(_createMoreThanQuantityField(context));
      }
    } else {
      children.add(_createMoreButton(context));
    }

    return children;
  }

  /// Creates the beers select dropdown.
  Widget _createSelectBeerField(BuildContext context) => DropdownButtonFormField<Beer>(
        value: _beer,
        items: _beers
            .map((beer) => DropdownMenuItem<Beer>(
                  child: SizedBox(
                    width: MediaQuery.of(context).size.width - 200,
                    child: Text(beer.name),
                  ),
                  value: beer,
                ))
            .toList(),
        onChanged: (value) => setState(() {
          _beer = value;
        }),
        onSaved: (value) => widget.historyEntry.beerId = value.key,
      );

  /// Creates the quantity selector field.
  Widget _createSelectQuantityField(BuildContext context, Map<double, String> quantities) => DropdownButtonFormField<double>(
        value: _quantity == null || quantities.keys.contains(_quantity) ? _quantity : -1,
        items: [
          DropdownMenuItem<double>(
            child: Text(EzLocalization.of(context).get('historyEntryDialog.quantity.empty')),
            value: null,
          ),
        ]
          ..addAll(quantities.entries
              .map(
                (entry) => DropdownMenuItem<double>(
                  child: Text(entry.value),
                  value: entry.key,
                ),
              )
              .toList())
          ..add(
            DropdownMenuItem<double>(
              child: Text(EzLocalization.of(context).get('historyEntryDialog.quantity.quantities.custom')),
              value: -1,
            ),
          ),
        onChanged: (value) => setState(() {
          _quantity = value;
        }),
        onSaved: (value) {
          if (value != -1) {
            widget.historyEntry.quantity = _quantity;
          }
        },
      );

  /// Creates the quantity field.
  Widget _createQuantityField(BuildContext context) => TextFormField(
        decoration: InputDecoration(hintText: EzLocalization.of(context).get('historyEntryDialog.quantity.hint')),
        initialValue: _quantity != null && _quantity > 0 ? _quantity.toString() : '',
        keyboardType: TextInputType.numberWithOptions(decimal: true),
        validator: (value) {
          if (value.isNotEmpty && !Utils.isNumeric(value)) {
            return EzLocalization.of(context).get('error.invalidNumber');
          }
          return null;
        },
        onChanged: (value) => _quantity = double.tryParse(value),
        onSaved: (value) {
          if (_quantity == null || _quantity >= 0) {
            widget.historyEntry.quantity = double.tryParse(value);
          }
        },
      );

  /// Creates the times field.
  Widget _createTimesField(BuildContext context) => DropdownButtonFormField<int>(
        value: _times,
        items: [
          for (int i = 1; i < 11; i++)
            DropdownMenuItem<int>(
              child: Text(i.toString() + 'x'),
              value: i,
            ),
        ],
        onChanged: (value) => setState(() {
          _times = value;
        }),
        onSaved: (value) {
          widget.historyEntry.times = _times;
        },
      );

  /// Creates the input date field.
  Widget _createInputDateField(BuildContext context) => FormField(
        builder: (_) => AppButton(
          padding: EdgeInsets.only(top: 30),
          text: DateFormat.yMMMd(EzLocalization.of(context).locale.languageCode).format(_date),
          onPressed: () async {
            DateTime date = await showDatePicker(
              context: context,
              initialDate: _date,
              firstDate: DateTime(1900),
              lastDate: DateTime(2200),
            );

            if (date != null) {
              setState(() {
                _date = date;
              });
            }
          },
        ),
        onSaved: (_) => widget.date.value = _date,
      );

  /// Creates the more than quantity field.
  Widget _createMoreThanQuantityField(BuildContext context) => CheckboxFormField(
    context: context,
    child: createLabel(Icons.add, 'historyEntryDialog.moreThanQuantity.label'),
    initialValue: widget.historyEntry.moreThanQuantity,
    onSaved: (value) => widget.historyEntry.moreThanQuantity,
  );

  /// Creates the show more button.
  Widget _createMoreButton(BuildContext context) => AppButton(
        padding: EdgeInsets.only(top: 30),
        text: EzLocalization.of(context).get('action.more'),
        onPressed: () => setState(() {
          _showMore = true;
        }),
      );
}

/// The success dialog content.
class _SuccessDialogContent extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => _SuccessDialogContentState();
}

/// The success dialog content state.
class _SuccessDialogContentState extends State<_SuccessDialogContent> with TickerProviderStateMixin {
  /// The current size animation instance.
  Animation<double> _sizeAnimation;

  /// The size animation controller.
  AnimationController _sizeController;

  /// The opacity animation controller.
  AnimationController _opacityController;

  @override
  void initState() {
    super.initState();
    _initializeAndAnimate();
  }

  @override
  void didUpdateWidget(_SuccessDialogContent oldWidget) {
    _sizeController.reset();
    _opacityController.reset();

    _initializeAndAnimate();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) => SingleChildScrollView(
        child: Transform.scale(
          scale: _sizeAnimation.value,
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              SvgPicture.asset('assets/images/added.svg'),
              FadeTransition(
                opacity: _opacityController.drive(CurveTween(curve: Curves.easeOut)),
                child: _createButtons(context),
              ),
            ],
          ),
        ),
      );

  @override
  void dispose() {
    _sizeController?.dispose();
    _opacityController?.dispose();

    super.dispose();
  }

  /// Initializes and animate the widget.
  void _initializeAndAnimate() {
    _opacityController = AnimationController(duration: Duration(milliseconds: 150), vsync: this);
    _sizeController = AnimationController(duration: Duration(milliseconds: 1000), vsync: this);
    _sizeAnimation = Tween<double>(begin: 0.25, end: 1).chain(CurveTween(curve: Curves.elasticInOut)).animate(_sizeController)
      ..addListener(() {
        setState(() {});
      })
      ..addStatusListener((status) {
        if (status == AnimationStatus.completed) {
          _opacityController.forward();
        }
      });
    _sizeController.forward();
  }

  /// Creates the action buttons.
  Widget _createButtons(BuildContext context) => Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          AppButton(
            padding: EdgeInsets.only(top: 30),
            text: EzLocalization.of(context).get('successDialog.history'),
            onPressed: () {
              Navigator.pop(context);
              Navigator.pushNamed(context, '/history');
            },
          ),
          AppButton(
            text: EzLocalization.of(context).get('successDialog.close'),
            onPressed: () => Navigator.pop(context),
          ),
        ],
      );
}
