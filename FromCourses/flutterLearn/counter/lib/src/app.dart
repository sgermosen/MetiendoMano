import 'package:flutter/material.dart';

import 'package:counter/src/pages/contador_page.dart';
//import 'package:counter/src/pages/home_page.dart';

class MyApp extends StatelessWidget {
  @override
  Widget build(context) {
    return MaterialApp(
      // home: HomePage(),
      debugShowCheckedModeBanner: false,
      home: Center(child: ContadorPage()),
    );
  }
}
