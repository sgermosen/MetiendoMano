import 'package:dearly/message_view.dart';
import 'package:flutter/material.dart';
import 'package:speech_recognition/speech_recognition.dart';
import 'package:flutter_spinkit/flutter_spinkit.dart';

class ListeningModalSheetContents extends StatefulWidget {

  State homepageState;
  SpeechRecognition speechRecognition;
  bool isAvailable;

  ListeningModalSheetContents({this.speechRecognition,this.isAvailable,this.homepageState});
  @override
  _ListeningModalSheetContentsState createState() => _ListeningModalSheetContentsState();
}

class _ListeningModalSheetContentsState extends State<ListeningModalSheetContents> {

  SpeechRecognition _speechRecognition;

  String resultText = "Speak after the beap";
  bool _isAvailable = false;
  bool _isListening = false;

  void initSpeechRecognizer() {


    _speechRecognition = new SpeechRecognition();

    _speechRecognition.setAvailabilityHandler(
          (bool result) => setState((){ _isAvailable = result; print('avaibility checked');}),
    );

    _speechRecognition.setRecognitionStartedHandler(
          () => setState(() => _isListening = true),
    );

    _speechRecognition.setRecognitionResultHandler(
          (String speech) => setState(() => resultText = speech),
    );

    _speechRecognition.setRecognitionCompleteHandler(
          () => setState(() => _isListening = false),
    );

    _speechRecognition.activate().then(
          (result) => setState(() {_isAvailable = result; print('activation');}),
    );

  }

  void startListening(){
//
//    print('started Listening');
//    print('$_isAvailable,$_isListening');

    if (_isAvailable && !_isListening) {
      _speechRecognition
          .listen(locale: "en_US")
          .then((result) {
        resultText = result;
      });
    }
  }

  @override
  void initState() {
    super.initState();
    _speechRecognition = widget.speechRecognition;
    _isAvailable = widget.isAvailable;
    initSpeechRecognizer();
    startListening();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      child: Column(
        mainAxisSize: MainAxisSize.max,
        mainAxisAlignment: MainAxisAlignment.start,
        children: <Widget>[
          Container(
            padding: EdgeInsets.only(left: 20.0, right: 20.0),
            child: AppBar(
              title: _isAvailable  && _isListening?
              Text('I\'m Listening...', style: TextStyle(color: Colors.black54, fontSize: 15.0, fontWeight: FontWeight.w400),)
                  : Text('Press the mic', style: TextStyle(color: Colors.black54, fontSize: 15.0, fontWeight: FontWeight.w400),),
              centerTitle: true,
              elevation: 0.0,
              primary: false,
              backgroundColor: Colors.white,
              leading: IconButton(
                onPressed: (){

                },
                icon: Icon(Icons.volume_up, color: Color(0xff414141),),
              ),
            ),
          ),
          Container(
            padding: EdgeInsets.only(left: 30.0, right: 30.0, top: 15.0, bottom: 25.0),
            child: Text(resultText, style: TextStyle(fontFamily: 'OpenSans', fontSize: 18.0), textAlign: TextAlign.center, maxLines: 2, overflow: TextOverflow.ellipsis,),
          ),
          Expanded(
            child: Align(
                alignment: FractionalOffset.bottomCenter,
                child: Padding(
                  padding: const EdgeInsets.only(bottom: 20.0),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.center,
                    children: <Widget>[
                      Padding(
                        padding: const EdgeInsets.only(right: 20.0),
                        child: Container(
                          height: 35.0,
                          width: 35.0,
                          child: FloatingActionButton(
                              materialTapTargetSize: MaterialTapTargetSize.padded,
                              backgroundColor: Colors.white,
                              child: Icon(Icons.close, color: Color(0xffFF0000),),
                              onPressed: (){

                                if(resultText != ""){
                                  setState(() {
                                    resultText = "";
                                    initSpeechRecognizer();
                                    Future.delayed(const Duration(milliseconds: 500), (){startListening();});
                                  });
                                }
                              }
                          ),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.all(0.0),
                        child: Container(
                          height: 70.0,
                          width: 70.0,
                          child: FloatingActionButton(
                            onPressed: (){
                              initSpeechRecognizer();
                              Future.delayed(const Duration(milliseconds: 500), (){startListening();});
                              setState(() {
                                resultText = 'Speak After the beap';
                              });
                            },
                            materialTapTargetSize: MaterialTapTargetSize.padded,
                            backgroundColor: Color(0xff6A47F7),
                            child: _isAvailable && _isListening? SpinKitWave(
                              color: Colors.white,
                              size: 20.0,
                              type: SpinKitWaveType.center,
                            ) : Image.asset('assets/mic_icon.png', height: 35.0,),
                          ),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.only(left: 20.0),
                        child: Container(
                          height: 35.0,
                          width: 35.0,
                          child: FloatingActionButton(
                              materialTapTargetSize: MaterialTapTargetSize.padded,
                              backgroundColor: Colors.white,
                              child: Icon(Icons.check, color: Color(0xff00D486),),
                              onPressed: (){
                                if(resultText!=""){
                                  Navigator.pop(context);
                                  setState(()  {
                                    MessageData.addMessage(resultText);
                                    AppBuilder.of(context).rebuild();
                                  });
                                }
                              }
                          ),
                        ),
                      ),
                    ],
                  ),
                )),
          ),
        ],
      ),
    );
  }
}