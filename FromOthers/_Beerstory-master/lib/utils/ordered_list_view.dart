import 'dart:async';

import 'package:beerstory/utils/utils.dart';
import 'package:ez_localization/ez_localization.dart';
import 'package:flutter/material.dart';

/// An ordered and searchable listview.
class OrderedListView<T extends Sortable> extends StatefulWidget {
  /// The listview items.
  final List<T> items;

  /// The item builder.
  final Widget Function(List<T>, int) itemBuilder;

  /// Whether to show the search box.
  final bool searchBox;

  /// Whether to order the list in reverse order.
  final bool reverseOrder;

  /// Creates a new ordered listview instance.
  OrderedListView({
    @required this.items,
    @required this.itemBuilder,
    this.searchBox = true,
    this.reverseOrder = false,
  });

  @override
  State<StatefulWidget> createState() => _OrderedListViewState();
}

/// The ordered listview state.
class _OrderedListViewState<T extends Sortable> extends State<OrderedListView<T>> {
  /// The current scroll controller.
  ScrollController _scrollController;

  /// The search controller.
  TextEditingController _searchController;

  /// The current typing timer.
  Timer _typingTimer;

  /// The ordered items list.
  List<T> _items;

  @override
  void initState() {
    filterList();
    if (widget.searchBox) {
      _scrollController = ScrollController();
      _searchController = TextEditingController();

      _searchController.addListener(() {
        if (_searchController.value.text.isEmpty) {
          filterList();
          return;
        }

        _typingTimer?.cancel();
        _typingTimer = Timer(Duration(milliseconds: 500), filterList);
      });

      WidgetsBinding.instance.addPostFrameCallback((_) {
        if (_scrollController.position.maxScrollExtent >= 110) {
          _scrollController.jumpTo(110);
        }
      });
    }
    super.initState();
  }

  @override
  void didUpdateWidget(OrderedListView<T> oldWidget) {
    filterList();
    super.didUpdateWidget(oldWidget);
  }

  @override
  Widget build(BuildContext context) {
    if (_items == null) {
      return CenteredCircularProgressIndicator();
    }

    if (widget.searchBox) {
      return ListView.builder(
        controller: _scrollController,
        itemCount: _items.length + 1,
        itemBuilder: (context, position) => position == 0 ? _createSearchBox(context) : widget.itemBuilder(_items, position - 1),
      );
    }

    return ListView.builder(
      itemCount: _items.length,
      itemBuilder: (context, position) => widget.itemBuilder(_items, position),
    );
  }

  @override
  void dispose() {
    _scrollController?.dispose();
    _searchController?.dispose();
    _typingTimer?.cancel();
    super.dispose();
  }

  /// Creates the search box.
  Widget _createSearchBox(BuildContext context) => Padding(
        padding: EdgeInsets.all(20),
        child: TextField(
          controller: _searchController,
          textInputAction: TextInputAction.search,
          decoration: InputDecoration(
            border: OutlineInputBorder(
              borderRadius: BorderRadius.zero,
            ),
            suffixIcon: Icon(
              Icons.search,
              color: Colors.grey,
            ),
            labelText: EzLocalization.of(context).get('page.search'),
            contentPadding: EdgeInsets.all(10),
          ),
        ),
      );

  /// Orders and filters the current list.
  Future<void> filterList() async {
    setState(() => _items = null);

    List<T> items = List.of(widget.items);
    items.sort((a, b) => widget.reverseOrder ? b.orderKey.compareTo(a.orderKey) : a.orderKey.compareTo(b.orderKey));

    if (_searchController != null && _searchController.text.isNotEmpty) {
      String request = _searchController.text.toLowerCase();
      items.retainWhere((sortable) {
        for (String searchTerm in sortable.searchTerms) {
          if (searchTerm.toLowerCase().contains(request)) {
            return true;
          }
        }
        return false;
      });
    }

    setState(() => _items = items);
  }
}

/// A sortable object.
mixin Sortable {
  /// The order key.
  Comparable get orderKey => null;

  /// The searchable terms.
  List<String> get searchTerms => null;
}
