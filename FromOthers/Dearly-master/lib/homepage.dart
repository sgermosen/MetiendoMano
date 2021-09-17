import 'package:dearly/call_logs.dart';
import 'package:dearly/message_view.dart';
import 'package:flutter/material.dart';
import 'package:dearly/floating_action_button.dart';

class HomePage extends StatefulWidget {
  @override
  _HomePageState createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {

  final GlobalKey<ScaffoldState> scaffoldKey = new GlobalKey<ScaffoldState>();

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.yellow,
      floatingActionButtonLocation: FloatingActionButtonLocation.centerFloat,
      floatingActionButton: MyFloatingActionButton(homepageState: this,),
      body: DefaultTabController(
        length: 3,
        child: Scaffold(
          backgroundColor: Color(0xffF8F8F8),
          appBar: AppBar(
              elevation: 0.0,
              backgroundColor: Colors.white,
              bottom: TabBar(
                labelColor: Colors.black,
                indicatorColor: Color(0xff3B15D4),
                tabs: [
                  Tab(icon: Icon(Icons.home), text: "Home",),
                  Tab(icon: Icon(Icons.call), text: "Call",),
                  Tab(icon: Icon(Icons.video_library), text: "Media",),
                ],
              ),
              title: Center(
                child: Text('Dearly', style: TextStyle(fontFamily: 'Pacifico', color: Colors.black, fontSize: 25.0), ),
              )
          ),
          body: TabBarView(
            children: [
              MessageView(),
              CallLogsView(),
              Icon(Icons.directions_bike),
            ],
          ),
        ),
      ),
    );
  }
}