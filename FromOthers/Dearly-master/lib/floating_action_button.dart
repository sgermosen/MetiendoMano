import 'package:flutter/material.dart';
import 'package:speech_recognition/speech_recognition.dart';
import 'package:dearly/modal_sheet.dart';

class MyFloatingActionButton extends StatefulWidget {

  State homepageState;

  MyFloatingActionButton({this.homepageState});

  @override
  _MyFloatingActionButtonState createState() => _MyFloatingActionButtonState();
}


class _MyFloatingActionButtonState extends State<MyFloatingActionButton> {

  SpeechRecognition _speechRecognition;

  bool _isAvailable = false;


//  _handleMic() async {
//    await PermissionHandler().requestPermissions(
//        [PermissionGroup.microphone]);
//  }

  void checkAvailability() {
    _speechRecognition = SpeechRecognition();
    _speechRecognition.activate().then(
          (result) => setState(() => _isAvailable = result),
    );
  }

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      crossAxisAlignment: CrossAxisAlignment.center,
      children: <Widget>[
        Padding(
          padding: const EdgeInsets.only(right: 20.0),
          child: Container(
            height: 35.0,
            width: 35.0,
            child: FloatingActionButton(
              heroTag: '1',
                materialTapTargetSize: MaterialTapTargetSize.padded,
                backgroundColor: Colors.white,
                child: Text('?', style: TextStyle(color: Color(0xff6A47F7), fontSize: 20.0, fontWeight: FontWeight.bold),),
                onPressed: (){}
            ),
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(0.0),
          child: Container(
            height: 70.0,
            width: 70.0,
            child: FloatingActionButton(
              heroTag: '2',
              materialTapTargetSize: MaterialTapTargetSize.padded,
              backgroundColor: Color(0xff6A47F7),
              child: Image.asset('assets/mic_icon.png', height: 35.0,),
              onPressed: () {
                checkAvailability();
                _isAvailable ? showModalBottomSheet(
                    context: context,
                    builder: (context) => Container(
                      color: Color(0xff737373),
                      child: Container(
                        child: ListeningModalSheetContents(speechRecognition: _speechRecognition,isAvailable: _isAvailable,),
                        height: 250.0,
                        decoration: BoxDecoration(
                          color: Colors.white,
                          borderRadius: BorderRadius.only(
                              topLeft: const Radius.circular(20.0),
                              topRight: const Radius.circular(20.0)
                          ),
                        ),
                      ),
                    )
                ) : Container();
              },
            ),
          ),
        ),
        Padding(
          padding: const EdgeInsets.only(left: 20.0),
          child: Container(
            height: 35.0,
            width: 35.0,
            child: FloatingActionButton(
              heroTag: '3',
                materialTapTargetSize: MaterialTapTargetSize.padded,
                backgroundColor: Colors.white,
                child: Icon(Icons.keyboard, color: Color(0xff6A47F7),),
                onPressed: (){}
            ),
          ),
        ),
      ],
    );
  }

}