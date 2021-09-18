import 'dart:io';

import 'package:beerstory/controller/form_dialog.dart';
import 'package:beerstory/model/bar.dart';
import 'package:beerstory/model/beer.dart';
import 'package:beerstory/utils/choice_dialog.dart';
import 'package:beerstory/utils/utils.dart';
import 'package:beerstory/view/beer_widget.dart';
import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/material.dart';
import 'package:hive/hive.dart';
import 'package:image_picker/image_picker.dart';
import 'package:intl/intl.dart';
import 'package:path_provider/path_provider.dart';
import 'package:smooth_star_rating/smooth_star_rating.dart';
import 'package:flutter_barcode_scanner/flutter_barcode_scanner.dart';

/// The beer editor.
class BeerEditor extends FormDialog<Beer> {
  /// The current edited beer.
  final Beer beer;

  /// The available bars.
  final List<Bar> bars;

  /// Whether this is a preview.
  final bool previewMode;

  /// The beer editor internal constructor.
  const BeerEditor._internal({
    @required this.beer,
    @required this.bars,
    @required this.previewMode,
    @required GlobalKey<FormState> formKey,
  }) : super(
          formKey: formKey,
        );

  /// Shows the beer editor for a new beer.
  static void newBeer(BuildContext context) => show(context: context, beer: Beer(name: '')).then((result) {
        if (result == FormDialogResult.SUCCESS) {
          result.createSnackBarMessage(context, {'element': EzLocalization.of(context).get('formDialog.beer')});
        } else if (result == FormDialogResult.OPEN_FOOD_FACTS_NOT_FOUND || result == FormDialogResult.OPEN_FOOD_FACTS_GENERIC_ERROR) {
          result.createSnackBarMessage(context);
        }
      });

  /// Shows a new beer editor.
  static Future<FormDialogResult> show({
    @required BuildContext context,
    @required Beer beer,
    bool previewMode = false,
  }) async {
    Box<Bar> bars = await Hive.openBox(Bar.HIVE_BOX);
    return FormDialog.show(
      context: context,
      hiveObject: beer,
      hiveBox: Beer.HIVE_BOX,
      createContent: (formKey) => BeerEditor._internal(
        bars: bars.values.toList(),
        beer: beer,
        previewMode: previewMode,
        formKey: formKey,
      ),
    );
  }

  @override
  State<StatefulWidget> createState() => _BeerEditorState();

  @override
  List<Widget> createActions(BuildContext context, String hiveBox, Beer hiveObject) {
    if (!previewMode) {
      return super.createActions(context, hiveBox, hiveObject);
    }

    return [
      FlatButton(
        child: Text(EzLocalization.of(context).get('beerDialog.edit').toUpperCase()),
        onPressed: () {
          Navigator.pop(context);
          BeerEditor.show(
            context: context,
            beer: beer,
          );
        },
      ),
      FlatButton(
        child: Text(MaterialLocalizations.of(context).closeButtonLabel),
        onPressed: () => Navigator.pop(context),
      ),
    ];
  }
}

/// The beer editor state.
class _BeerEditorState extends FormDialogState<BeerEditor> {
  /// The beer name.
  String _name;

  /// The beer image.
  String _image;

  /// The beer rating.
  double _rating;

  /// The beer tags.
  List<String> _tags;

  /// The beer prices.
  List<BeerPrice> _prices;

  /// Whether to show more details.
  bool _showMore = false;

  @override
  void initState() {
    _name = widget.beer.name;
    _image = widget.beer.image;
    _rating = widget.beer.rating ?? 0.0;
    _tags = widget.beer.tags ?? [];
    _prices = widget.beer.prices ?? [];
    if (_prices.isEmpty && !widget.previewMode) {
      _prices.add(BeerPrice(
        barId: null,
        price: null,
      ));
    }
    super.initState();
  }

  @override
  List<Widget> createChildren(BuildContext context) {
    List<Widget> result = [
      _createImageField(context),
      createTopPaddingWidget(createLabel(Icons.edit, 'beerDialog.name.label')),
      _createNameField(context),
      createTopPaddingWidget(createLabel(Icons.local_bar, 'beerDialog.degrees.label')),
      _createDegreesField(context),
    ];

    if (_showMore) {
      result.addAll([
        createTopPaddingWidget(createLabel(Icons.star, 'beerDialog.rating.label')),
        _createRatingField(context),
        createTopPaddingWidget(createLabel(Icons.local_offer, 'beerDialog.tags.label')),
        _createTagsField(context),
        _createTagsWidget(context),
        createTopPaddingWidget(createLabel(Icons.attach_money, 'beerDialog.prices.label')),
        _createPricesWidget(context),
        _createAddPriceButton(context),
      ]);
    } else {
      result.add(_createMoreButton(context));
    }
    result.add(_createBarcodeButton(context));

    return result;
  }

  /// Creates the image field.
  Widget _createImageField(BuildContext context) => FormField(
        builder: (state) => GestureDetector(
          child: BeerImage.fromNameImage(
            name: _name,
            image: _image,
            radius: 100,
          ),
          onTap: () {
            if (!widget.previewMode) {
              showChoiceDialog(context);
            }
          },
        ),
        onSaved: (_) => widget.beer.image = _image,
      );

  /// Creates the name field.
  Widget _createNameField(BuildContext context) => widget.previewMode
      ? _createLeftAlignedText(_name)
      : TextFormField(
          decoration: InputDecoration(hintText: EzLocalization.of(context).get('beerDialog.name.hint')),
          initialValue: _name,
          validator: (value) {
            if (value.isEmpty) {
              return EzLocalization.of(context).get('error.notFilled');
            }
            return null;
          },
          onChanged: (value) => setState(() {
            _name = value;
          }),
          onSaved: (value) => widget.beer.name = value,
        );

  /// Creates the degrees field.
  Widget _createDegreesField(BuildContext context) => widget.previewMode
      ? _createLeftAlignedText(widget.beer.degrees == null ? '?' : widget.beer.degrees.toString() + '°')
      : TextFormField(
          decoration: InputDecoration(hintText: EzLocalization.of(context).get('beerDialog.degrees.hint')),
          initialValue: (widget.beer.degrees ?? '').toString(),
          keyboardType: TextInputType.numberWithOptions(decimal: true),
          validator: (value) {
            if (value.isNotEmpty && !Utils.isNumeric(value)) {
              return EzLocalization.of(context).get('error.invalidNumber');
            }
            return null;
          },
          onSaved: (value) => widget.beer.degrees = double.tryParse(value),
        );

  /// Creates the rating field.
  Widget _createRatingField(BuildContext context) => FormField(
        builder: (state) => Padding(
          padding: EdgeInsets.only(top: 5),
          child: SmoothStarRating(
            rating: _rating,
            size: 40,
            onRatingChanged: (rating) {
              if (!widget.previewMode) {
                setState(() => _rating = rating);
              }
            },
          ),
        ),
        onSaved: (_) => widget.beer.rating = _rating,
      );

  /// Creates the tags field.
  Widget _createTagsField(BuildContext context) => widget.previewMode
      ? SizedBox.shrink()
      : TextFormField(
          decoration: InputDecoration(hintText: EzLocalization.of(context).get('beerDialog.tags.hint')),
          textInputAction: TextInputAction.next,
          onFieldSubmitted: (value) => setState(() {
            _tags.add(value);
          }),
        );

  /// Creates the tags widget.
  Widget _createTagsWidget(BuildContext context) => _tags.isEmpty && widget.previewMode
      ? _createLeftAlignedText(EzLocalization.of(context).get('beerDialog.tags.empty'))
      : Align(
          alignment: Alignment.topLeft,
          child: FormField(
            builder: (state) => Padding(
              padding: EdgeInsets.only(top: 10),
              child: Wrap(
                spacing: 4,
                runSpacing: 4,
                children: Utils.mapIndexed(
                  _tags,
                  (index, tag) => GestureDetector(
                    onTap: () {
                      if (widget.previewMode) {
                        return;
                      }

                      ChoiceDialog(
                        choices: [
                          Choice(
                            text: 'action.delete',
                            icon: Icons.delete,
                            callback: () => setState(() {
                              _tags.removeAt(index);
                            }),
                          )
                        ],
                      ).show(context);
                    },
                    child: TagWidget(
                      text: tag,
                    ),
                  ),
                ).toList(),
              ),
            ),
            onSaved: (_) => widget.beer.tags = _tags,
          ),
        );

  /// Creates the prices widget.
  Widget _createPricesWidget(BuildContext context) => _prices.isEmpty
      ? _createLeftAlignedText(EzLocalization.of(context).get('beerDialog.prices.empty'))
      : Padding(
          padding: EdgeInsets.only(top: widget.previewMode ? 4 : 0),
          child: FormField(
            builder: (state) => Column(
              children: _prices.map((price) => _createPriceWidget(context, price)).toList(),
            ),
            onSaved: (_) {
              widget.beer.prices = _prices.where((price) => price.barId != null || price.price != null).toList();
            },
          ),
        );

  /// Creates the price widget.
  Widget _createPriceWidget(BuildContext context, BeerPrice price) => Row(
        mainAxisAlignment: widget.previewMode ? MainAxisAlignment.spaceBetween : MainAxisAlignment.start,
        children: widget.previewMode
            ? [
                Flexible(
                  child: Padding(
                    padding: EdgeInsets.only(right: 20),
                    child: Text(
                      price.barId == null ? EzLocalization.of(context).get('beerDialog.prices.noBar') : widget.bars.firstWhere((bar) => bar.key == price.barId)?.name,
                      style: Theme.of(context).textTheme.body1.copyWith(fontWeight: FontWeight.bold),
                    ),
                  ),
                ),
                Text(price.price == null ? '?' : NumberFormat.simpleCurrency(locale: EzLocalization.of(context).locale.languageCode).format(price.price)),
              ]
            : [
                Flexible(
                  child: DropdownButtonFormField<int>(
                    value: price.barId,
                    items: widget.bars
                        .map(
                          (bar) => DropdownMenuItem<int>(
                            child: SizedBox(
                              width: (MediaQuery.of(context).size.width - 200) / 2,
                              child: Text(bar.name),
                            ),
                            value: bar.key,
                          ),
                        )
                        .toList()
                          ..insert(
                            0,
                            DropdownMenuItem(
                              child: Text(EzLocalization.of(context).get('beerDialog.prices.noBar')),
                              value: null,
                            ),
                          ),
                    onChanged: (value) => setState(() {
                      price.barId = value;
                    }),
                  ),
                ),
                Flexible(
                  child: Padding(
                    padding: EdgeInsets.only(
                      top: 5,
                      left: 20,
                    ),
                    child: TextFormField(
                      decoration: InputDecoration(hintText: EzLocalization.of(context).get('beerDialog.prices.hint')),
                      initialValue: (price.price ?? '').toString(),
                      keyboardType: TextInputType.numberWithOptions(decimal: true),
                      validator: (value) {
                        if (value.isNotEmpty && !Utils.isNumeric(value)) {
                          return EzLocalization.of(context).get('error.invalidNumber');
                        }
                        return null;
                      },
                      onChanged: (value) => price.price = double.tryParse(value),
                    ),
                  ),
                ),
              ],
      );

  /// Creates the barcode button.
  Widget _createBarcodeButton(BuildContext context) => widget.previewMode
      ? SizedBox.shrink()
      : AppButton(
          onPressed: () async {
            String result = await FlutterBarcodeScanner.scanBarcode(Utils.colorToHex(Theme.of(context).primaryColor), MaterialLocalizations.of(context).cancelButtonLabel, true, ScanMode.BARCODE);
            if (result == null) {
              return;
            }

            dynamic beer = await Beer.fromOpenFoodFacts(result);
            if (beer is Beer) {
              Navigator.pop(context); // TODO : Unhandled Exception: Looking up a deactivated widget's ancestor is unsafe. At this point the state of the widget's element tree is no longer stable. To safely refer to a widget's ancestor in its dispose() method, save a reference to the ancestor by calling dependOnInheritedWidgetOfExactType() in the widget's didChangeDependencies() method.
              BeerEditor.show(
                context: context,
                beer: beer,
              );
              return;
            }

            switch (beer) {
              case OpenFoodFactsResult.NOT_FOUND:
                Navigator.pop(context, FormDialogResult.OPEN_FOOD_FACTS_NOT_FOUND);
                break;
              case OpenFoodFactsResult.GENERIC_ERROR:
              default:
                Navigator.pop(context, FormDialogResult.OPEN_FOOD_FACTS_GENERIC_ERROR);
                break;
            }
          },
          text: EzLocalization.of(context).get('beerDialog.barcode'),
        );

  /// Creates the add price button.
  Widget _createAddPriceButton(BuildContext context) => widget.previewMode
      ? SizedBox.shrink()
      : AppButton(
          padding: EdgeInsets.only(top: 30),
          text: EzLocalization.of(context).get('beerDialog.prices.add'),
          onPressed: () => setState(() {
            _prices.add(BeerPrice(
              barId: null,
              price: null,
            ));
          }),
        );

  /// Creates the button that allows to show more detail.
  Widget _createMoreButton(BuildContext context) => AppButton(
        padding: EdgeInsets.only(top: 30),
        text: EzLocalization.of(context).get('action.more'),
        onPressed: () => setState(() {
          _showMore = true;
        }),
      );

  /// Creates a left aligned text widget.
  Widget _createLeftAlignedText(String text) => Align(
        alignment: AlignmentDirectional.centerStart,
        child: Padding(
          padding: EdgeInsets.only(top: 4),
          child: Text(text),
        ),
      );

  /// Shows the photo choice dialog.
  void showChoiceDialog(BuildContext context) {
    List<Choice> choices = [
      Choice(
        text: 'beerDialog.image.gallery',
        icon: Icons.photo,
        callback: () async {
          File file = await ImagePicker.pickImage(source: ImageSource.gallery);
          if (file != null) {
            String path = (await getApplicationDocumentsDirectory()).path + '/' + file.path.split('/').last;
            Utils.moveFile(file, path);
            setState(() => _image = path);
          }
        },
      ),
      Choice(
        text: 'beerDialog.image.camera',
        icon: Icons.camera_alt,
        callback: () async {
          File file = await ImagePicker.pickImage(source: ImageSource.camera);
          if (file != null) {
            String path = (await getApplicationDocumentsDirectory()).path + '/' + file.path.split('/').last;
            Utils.moveFile(file, path);
            setState(() => _image = path);
          }
        },
      ),
    ];

    if (widget.beer.image != null) {
      choices.add(Choice(
        text: 'beerDialog.image.remove',
        icon: Icons.remove,
        callback: () => widget.beer.image = null,
      ));
    }

    ChoiceDialog(choices: choices).show(context);
  }
}
