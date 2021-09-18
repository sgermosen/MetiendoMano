
import 'package:bank_app/screens/home_screen.dart';
import 'package:flutter/material.dart';

void main() {
  runApp(
    MaterialApp(
      title: 'Bank App',
      home: HomeScreen()
      // routes: <String, WidgetBuilder>{
      //   '/HomeScreen': (BuildContext context) => new HomeScreen()
      // },
    ),
  );
}
