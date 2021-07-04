import 'package:dearly/profile.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter/services.dart';
import 'package:dearly/homepage.dart';
import 'package:dearly/message_view.dart';
import 'package:dearly/walkthrough/providers/color_provider.dart';
import 'package:dearly/walkthrough/screens/onboarding/onboarding.dart';
import 'package:dearly/walkthrough/themes/styles.dart';
import 'package:provider/provider.dart';

void main(){
  SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
    DeviceOrientation.portraitDown
  ]);
  runApp(new MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return AppBuilder(builder: (context) {
      return MaterialApp(
        routes: <String, WidgetBuilder> {
          '/homepage': (BuildContext context) => new HomePage(),
        },
        debugShowCheckedModeBanner: false,
        home: UserProfile.walkthroughLoaded == true ? HomePage() : WalkthroughStart(),
      );
    });
  }
}

class WalkthroughStart extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: ChangeNotifierProvider(
        builder: (context) => ColorProvider(),
        child: Onboarding(),
      ),
    );
  }
}



