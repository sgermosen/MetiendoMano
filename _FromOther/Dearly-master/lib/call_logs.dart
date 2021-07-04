import 'package:flutter/material.dart';
import 'package:call_log/call_log.dart';
import 'package:intl/intl.dart';

class CallLogsView extends StatefulWidget {
  @override
  _CallLogsViewState createState() => _CallLogsViewState();
}

class _CallLogsViewState extends State<CallLogsView> {

  Iterable<CallLogEntry> entries;
  var entriesList;
  String currentDate;

  Future<Null> getCallEntries() async {
    entries = await CallLog.get();
    setState(() {
      entriesList = entries.toList();
      currentDate = DateFormat('dd/MM/yyyy').format(DateTime.fromMillisecondsSinceEpoch(entriesList[0].timestamp));
    });
  }


  @override
  void initState() {
    getCallEntries();
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return entries != null ? ListView.builder(
      itemCount: entries.isNotEmpty ? entries.length : 0,
        itemBuilder: (_,int index){
        String name = entriesList[index].name;
        String number = entriesList[index].number;
        String callType = entriesList[index].callType.index.toString();
        String date = DateFormat('dd/MM/yyyy').format(DateTime.fromMillisecondsSinceEpoch(entriesList[index].timestamp));
        String timeStamp = DateFormat('HH:mm').format( DateTime.fromMillisecondsSinceEpoch(entriesList[index].timestamp) );
        int dateTitle = 0;
        if(date != currentDate){
          dateTitle = 1;
          currentDate = date;
        }
        return Container(
            child: Column(
              children: <Widget>[
                dateTitle == 1 ? Container(
                  padding: EdgeInsets.only(left: 8.0, top: 8.0),
                  child: Text('$date',style: TextStyle(color: Colors.black54, fontWeight: FontWeight.bold),),
                ) : Container(),
                Stack(
                  children: <Widget>[
                    Row(
                      children: <Widget>[
                        Container(
                          child: name != '' ?
                          Container(
                            padding: EdgeInsets.all(5.0),
                            child: Text( name[0].toUpperCase(), style: TextStyle(color: Colors.white, fontSize: 22.0),),
                          )
                              : Icon(Icons.person, color: Colors.white,),
                          decoration: BoxDecoration(
                            shape: BoxShape.circle,
                            color: Color(0xff6A47F7),
                          ),
                          padding: EdgeInsets.all(10.0),
                          margin: EdgeInsets.only(left: 10.0, top: 13.0, bottom: 13.0 ),
                        ),
                        Flexible(
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: <Widget>[
                              Container(
                                padding: EdgeInsets.only(left: 10.0),
                                child: Wrap(
                                  children: <Widget>[
                                    name != '' ? Text(name, style: TextStyle(color: Color(0xff3A3131), fontSize: 17.0), softWrap: true, maxLines: 1, textAlign: TextAlign.left, overflow: TextOverflow.ellipsis,)
                                        : Text('Unkown Name', style: TextStyle(color: Color(0xff3A3131), fontSize: 17.0), softWrap: true, maxLines: 1, textAlign: TextAlign.left, overflow: TextOverflow.ellipsis,),
                                  ],
                                ),
                              ),
                              Row(
                                children: <Widget>[
                                  Container(
                                    padding: EdgeInsets.only(left: 10.0),
                                    child: callType == '0' ? Icon(Icons.call_received, size: 15.0, color: Colors.green,)
                                        : callType == '1' ? Icon(Icons.call_made, size: 15.0, color: Colors.grey,)
                                        : Icon(Icons.call_missed, size: 15.0, color: Colors.red,),
                                  ),
                                  Container(
                                    padding: EdgeInsets.only(left: 5.0),
                                    child: Wrap(
                                      children: <Widget>[
                                        number != '' && number != null ? Text(number, style: TextStyle(color: Color(0xff3A3131), fontSize: 13.0),textAlign: TextAlign.left, maxLines: 1, overflow: TextOverflow.ellipsis,)
                                            : Text('9879593420', style: TextStyle(color: Color(0xff3A3131), fontSize: 13.0),textAlign: TextAlign.left, maxLines: 1, overflow: TextOverflow.ellipsis,),
                                      ],
                                    ),
                                  )
                                ],
                              ),
                            ],
                          ),
                        ),
                      ],
                    ),
                    Positioned(
                      child: Text(timeStamp, style: TextStyle(color: Color(0xff3A3131), fontSize: 12.0),),
                      right: 12.0,
                      bottom: 7.0,
                    ),
                  ],
                )
              ],
            ),
          );
        }
    ) :
    Center(
        child: CircularProgressIndicator(valueColor: new AlwaysStoppedAnimation<Color>(Color(0xff6A47F7)),)
    );
  }
}
