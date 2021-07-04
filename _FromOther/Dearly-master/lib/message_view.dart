import 'package:dearly/src/pages/index.dart';
import 'package:flutter/material.dart';
import 'package:flutter/rendering.dart';
import 'package:intl/intl.dart';
import 'package:youtube_api/youtube_api.dart';

import 'package:dearly/mixin.dart';
import "package:dearly/apikey.dart";
import 'package:dearly/popup.dart';
import 'package:contacts_service/contacts_service.dart';
import 'src/pages/call.dart';
import 'services/service_locator.dart';
import 'services/calls_and_messages_service.dart';

class AppBuilder extends StatefulWidget {
  final Function(BuildContext) builder;

  const AppBuilder(
      {Key key, this.builder})
      : super(key: key);

  @override
  AppBuilderState createState() => new AppBuilderState();

  static AppBuilderState of(BuildContext context) {
    return context.ancestorStateOfType(const TypeMatcher<AppBuilderState>());
  }
}

class AppBuilderState extends State<AppBuilder> {

  @override
  Widget build(BuildContext context) {
    return widget.builder(context);
  }

  void rebuild() {
    setState(() {});
  }
}

class MessageView extends StatefulWidget {

  @override
  _MessageViewState createState() => _MessageViewState();
}

class _MessageViewState extends State<MessageView> with ListPopupTap<MessageView>, PortraitMode<MessageView> {


  YoutubeAPI _youtubeAPI;
  List<YT_API> _ytResults;
  List<VideoItem> videoItem;
  String videoId;
  int checkedMessage;

  @override
  void initState() {
    _youtubeAPI = YoutubeAPI(apikey, type: "video", maxResults: 3);
    _ytResults = [];
    videoItem = [];
    checkedMessage = MessageData.messages.length;
    super.initState();
  }

  Future<Null> checkNewMessage() async {
    await callAPI(MessageData.messages.last.data[0].replaceFirst('play',''));
  }

  Future<Null> getContacts() async{
    Iterable<Contact> contacts = await ContactsService.getContacts(query : '%' + MessageData.messages.last.data[0].replaceFirst('call ','') + '%');
    contacts.forEach((c){
    setState(() {
        MessageData.messages.add(Message(type: 'Call', data: ['${c.displayName}', '${c.phones.toList()[0].value}']));
    });
    });
  }

  Future<Null> callAPI(String query, {bool nextPage}) async {
    if (_ytResults.isNotEmpty) {
      videoItem = [];
    }

    if (nextPage == null) {
      _ytResults = await _youtubeAPI.search(query);
    }
    if (nextPage == true)
      _ytResults = await _youtubeAPI.nextPage();
    else
      _ytResults = await _youtubeAPI.prevPage();

    for (YT_API result in _ytResults) {
      VideoItem item = VideoItem(
        api: result,
        listPopupTap: this,
      );
      videoItem.add(item);
    }
    setState(() {
      MessageData.messages.add(Message(type: 'VideoRecommendation', videoItems: videoItem));
    });
  }

  @override
  void onTap(YT_API apiItem, BuildContext context) {
    setState(() {
      videoId = apiItem.id;
    });

    Navigator.of(context).push(PopupVideoPlayerRoute(
      child: PopupVideoPlayer(
        videoId: videoId,
      ),
    ));
  }


  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      padding: EdgeInsets.only(bottom: 100.0),
      itemCount: MessageData.messages.length,
      itemBuilder: (_, int index){

        if(MessageData.messages[index].type == 'Sent'){
//          if(MessageData.messages[index].data[0].toLowerCase().startsWith('call')){
//            if(MessageData.lastCheckMessage != MessageData.lastSentMessage){
//              MessageData.lastCheckMessage = MessageData.lastSentMessage;
//              getContacts();
//            }
//          }
//          else if(MessageData.messages[index].data[0].toLowerCase().startsWith('play')) {
//            if(MessageData.lastCheckMessage != MessageData.lastSentMessage){
//              MessageData.lastCheckMessage = MessageData.lastSentMessage;
//              checkNewMessage();
//            }
//          }
        if(MessageData.lastCheckMessage != MessageData.lastSentMessage && index == MessageData.lastSentMessage-1){
          if(MessageData.messages[index].data[0].toLowerCase().startsWith('call')){
              MessageData.lastCheckMessage = MessageData.lastSentMessage;
              getContacts();
          }
          else if(MessageData.messages[index].data[0].toLowerCase().startsWith('play')) {
              MessageData.lastCheckMessage = MessageData.lastSentMessage;
              checkNewMessage();
          }
        }
          return SentMessage(messageText: MessageData.messages[index].data[0], messageTime: '9:04 PM',);
        }
        else if(MessageData.messages[index].type == 'Received') {
          return ReceivedMessage(messageText: MessageData.messages[index].data[0], messageTime: '9:05 PM',);
        }
        else if(MessageData.messages[index].type == 'Call'){
          return ContactMessage(contactName: MessageData.messages[index].data[0], contactPhoneNumber: MessageData.messages[index].data[1],);
        }
        else {
          return VideoRecommendationMessage(videoItem: MessageData.messages[index].videoItems);
        }

      },
    );
  }
}


class ContactMessage extends StatelessWidget {

  String contactName;
  String contactPhoneNumber;

  ContactMessage({this.contactName,this.contactPhoneNumber});

  String getChannelName(){
    contactPhoneNumber = contactPhoneNumber.replaceAll('-', '').replaceAll(' ', '');
    int len = contactPhoneNumber.length;
    contactPhoneNumber = contactPhoneNumber.substring(len-10);
    print(contactPhoneNumber);
    return (int.parse('9879593420') + int.parse('$contactPhoneNumber')).toString();
  }

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.centerLeft,
      child: Column(
        children: <Widget>[
          Container(
            margin: EdgeInsets.only(left: 10.0, top: 10.0, right: MediaQuery.of(context).size.width * 0.4),
            child: Column(
              children: <Widget>[
                Container(
                  decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.all(Radius.circular(8.0)),
                      boxShadow: [BoxShadow(
                          color: Colors.grey,
                          blurRadius: 5.0
                      ),]
                  ),
                  child: Stack(
                    children: <Widget>[
                      Row(
                        children: <Widget>[
                          Container(
                            child: Icon(Icons.person, color: Colors.white,),
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
                                      Text(contactName, style: TextStyle(color: Color(0xff3A3131), fontSize: 17.0), softWrap: true, maxLines: 1, textAlign: TextAlign.left, overflow: TextOverflow.ellipsis,)
                                    ],
                                  ),
                                ),
                                Container(
                                  padding: EdgeInsets.only(left: 10.0),
                                  child: Wrap(
                                    children: <Widget>[
                                      Text(contactPhoneNumber, style: TextStyle(color: Color(0xff3A3131), fontSize: 13.0),textAlign: TextAlign.left, maxLines: 1, overflow: TextOverflow.ellipsis,)
                                    ],
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                      Positioned(
                        child: Text('9:04 AM', style: TextStyle(color: Color(0xff3A3131), fontSize: 12.0),),
                        right: 12.0,
                        bottom: 7.0,
                      ),
                    ],
                  ),
                ),
                Row(
                  children: <Widget>[
                    Expanded(
                      child: InkWell(
                        onTap: () {
                          setupLocator();
                          locator.allowReassignment  = true;
                          CallsAndMessagesService _service = locator<CallsAndMessagesService>();
                          _service.call(contactPhoneNumber);
                        },
                        child: Container(
                          padding: EdgeInsets.all(7.0),
                          margin: EdgeInsets.only(top: 5.0, right: 2.5),
                          decoration: BoxDecoration(
                              color: Colors.white,
                              borderRadius: BorderRadius.all(Radius.circular(8.0)),
                              boxShadow: [BoxShadow(
                                  color: Colors.grey,
                                  blurRadius: 5.0
                              )]
                          ),
                          child: Icon(Icons.call, color: Color(0xff6A47F7),),
                        ),
                      ),
                    ),
                    Expanded(
                      child: InkWell(
                        onTap: () {
                          Navigator.push(
                              context,
                              MaterialPageRoute(
                                  builder: (context) => CallPage(
                                    channelName: getChannelName(),
                                  )));
                        },
                        child: Container(
                          padding: EdgeInsets.all(7.0),
                          margin: EdgeInsets.only(left: 2.5, top: 5.0),
                          decoration: BoxDecoration(
                              color: Colors.white,
                              borderRadius: BorderRadius.all(Radius.circular(8.0)),
                              boxShadow: [BoxShadow(
                                  color: Colors.grey,
                                  blurRadius: 5.0
                              )]
                          ),
                          child: Icon(Icons.videocam, color: Color(0xff6A47F7),),
                        ),
                      ),
                    )
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}


class ReceivedMessage extends StatelessWidget {

  final String messageText;
  final String messageTime;

  ReceivedMessage({this.messageText,this.messageTime});

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.centerLeft,
      child: Container(
        margin: EdgeInsets.only(left: 10.0, top: 10.0, right: MediaQuery.of(context).size.width * 0.4),
        decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.all(Radius.circular(9.0)),
            boxShadow: [BoxShadow(
                color: Colors.black.withOpacity(0.16),
                blurRadius: 6.0,
              offset: Offset(0.0,3.0),
            )]
        ),
        child: Stack(
          children: <Widget>[
            Padding(
              padding: const EdgeInsets.only(left: 15.0, right: 20.0,top: 10.0, bottom: 30.0),
              child: Wrap(
                children: <Widget>[
                  Text(messageText, style: TextStyle(color: Color(0xff3A3131), fontSize: 16.0), maxLines: 6, overflow: TextOverflow.ellipsis,)
                ],
              ),
            ),
            Positioned(
              child: Text(messageTime, style: TextStyle(color: Colors.grey, fontSize: 12.0),),
              right: 12.0,
              bottom: 7.0,
            ),
          ],
        ),
      ),
    );
  }
}

class SentMessage extends StatelessWidget {

  final String messageText;
  final String messageTime;

  SentMessage({this.messageText,this.messageTime});

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.centerRight,
      child: Container(
        margin: EdgeInsets.only(right: 10.0, top: 10.0, left: MediaQuery.of(context).size.width * 0.4),
        decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.all(Radius.circular(8.0)),
            boxShadow: [BoxShadow(
              color: Colors.black.withOpacity(0.16),
              blurRadius: 6.0,
              offset: Offset(0.0,3.0),
            )]
        ),
        child: Stack(
          children: <Widget>[
            Padding(
              padding: const EdgeInsets.only(left: 15.0, right: 20.0,top: 10.0, bottom: 30.0),
              child: Wrap(
                children: <Widget>[
                  Text(messageText, style: TextStyle(color: Color(0xff3A3131), fontSize: 16.0,), maxLines: 6, overflow: TextOverflow.ellipsis,)
                ],
              ),
            ),
            Positioned(
              child: Text(messageTime, style: TextStyle(color: Colors.grey, fontSize: 12.0),),
              right: 12.0,
              bottom: 7.0,
            ),
          ],
        ),
      ),
    );
  }
}

class VideoItem extends StatelessWidget {
  final YT_API api;
  final ListPopupTap listPopupTap;

  const VideoItem({Key key, this.api, this.listPopupTap}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: EdgeInsets.only(bottom: 10.0, left: 15.0, right: 10.0),
      child: Row(
        children: <Widget>[
          InkWell(
            onTap: (){
              listPopupTap.onTap(api, context);
            },
            child: Image.network(api.thumbnail["high"]["url"], width: MediaQuery.of(context).size.width * 0.4,),
          ),
          Expanded(
            child: Container(
              padding: EdgeInsets.only(left: 10.0),
                width: MediaQuery.of(context).size.width * 0.25,
                child: Text(api.title, softWrap: true, maxLines: 3, overflow: TextOverflow.ellipsis,)
            ),
          ),
        ],
      ),
    );
  }
}

class Message{
  String type;
  List<String> data;
  List<VideoItem> videoItems;

  Message({this.type,this.data,this.videoItems});
}

class MessageData extends _MessageViewState{
  static List<Message> messages = [
    Message(data: ['Hello Ramesh, I\'m Soneya'], type: 'Received'),
    Message(data: ['Hello Soneya'], type: 'Sent'),
  ];

  static int lastSentMessage = 2;
  static int lastCheckMessage = 2;

  static void addMessage(String data){
    messages.add(Message(data: [data], type: 'Sent'));
    lastSentMessage = messages.length;
  }

}


class VideoRecommendationMessage extends StatelessWidget {

  final List<VideoItem> videoItem;

  VideoRecommendationMessage({this.videoItem});

  @override
  Widget build(BuildContext context) {
    return Align(
      alignment: Alignment.centerRight,
      child: Container(
        margin: EdgeInsets.only(right: MediaQuery.of(context).size.width * 0.1, top: 10.0, left: 10.0),
        decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.all(Radius.circular(8.0)),
            boxShadow: [BoxShadow(
              color: Colors.black.withOpacity(0.16),
              blurRadius: 6.0,
              offset: Offset(0.0,3.0),
            )]
        ),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Padding(
              padding: const EdgeInsets.only(left: 15.0, right: 20.0,top: 10.0, bottom: 20.0),
              child: Text('Here are some Recommendations', style: TextStyle(color: Color(0xff3A3131), fontSize: 16.0), maxLines: 6, overflow: TextOverflow.ellipsis,)
            ),
            videoItem[0],
            videoItem[1],
            videoItem[2],
            Container(
              padding: EdgeInsets.only(bottom: 7.0, right: 12.0),
              alignment: Alignment.bottomRight,
              child: Text('9:05 PM', style: TextStyle(color: Colors.grey, fontSize: 12.0),),
            ),
          ],
        ),
      ),
    );
  }
}