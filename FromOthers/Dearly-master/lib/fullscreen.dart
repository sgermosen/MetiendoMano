import 'package:flutter/material.dart';
import 'package:dearly/mixin.dart';
import 'package:youtube_player_flutter/youtube_player_flutter.dart';

class VideoPlayerFullScreen extends StatelessWidget with LandScapeMode {
  final String videoId;
  final Duration position;

  VideoPlayerFullScreen({
    Key key,
    this.videoId,
    this.position,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    super.build(context);
    return Material(
      child: Stack(
        children: <Widget>[
          Container(
            alignment: Alignment.center,
            child: YoutubePlayer(
              context: context,
              videoId: videoId,
              autoPlay: true,
              showVideoProgressIndicator: true,
            ),
          ),
          Container(
            alignment: Alignment.topLeft,
            child: IconButton(
              iconSize: 40.0,
              color: Colors.blue,
              icon: Icon(Icons.close),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
          )
        ],
      ),
    );
  }
}