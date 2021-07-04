import 'package:flutter/material.dart';
import 'package:dearly/fullscreen.dart';
import 'package:youtube_player_flutter/youtube_player_flutter.dart';
import 'package:dearly/mixin.dart';

class PopupVideoPlayerRoute extends ModalRoute {
  final Widget child;
  double top, left, right, bottom;

  PopupVideoPlayerRoute({
    this.child,
    this.bottom,
    this.left,
    this.right,
    this.top,
  }) {
    if (top == null) top = 20.0;
    if (bottom == null) bottom = 20.0;
    if (left == null) left = 20.0;
    if (right == null) right = 20.0;
  }

  @override
  Color get barrierColor => Colors.blueGrey.withOpacity(0.4);

  @override
  bool get barrierDismissible => true;

  @override
  String get barrierLabel => "Video Poptop";

  @override
  Widget buildPage(BuildContext context, Animation<double> animation,
      Animation<double> secondaryAnimation) {
    return Material(
      type: MaterialType.transparency,
      child: SafeArea(
        child: Container(
          margin: EdgeInsets.only(
            top: top,
            bottom: bottom,
            left: left,
            right: right,
          ),
          child: child,
        ),
      ),
    );
  }

  @override
  Widget buildTransitions(BuildContext context, Animation<double> animation,
      Animation<double> secondaryAnimation, Widget child) {
    return FadeTransition(
      opacity: animation,
      child: ScaleTransition(
        scale: animation,
        child: child,
      ),
    );
  }

  @override
  bool get maintainState => false;

  @override
  bool get opaque => false;

  @override
  Duration get transitionDuration => Duration(milliseconds: 400);
}

class PopupVideoPlayer extends StatefulWidget {
  final String videoId;

  const PopupVideoPlayer({Key key, this.videoId}) : super(key: key);

  @override
  _PopupVideoPlayerState createState() => _PopupVideoPlayerState();
}

class _PopupVideoPlayerState extends State<PopupVideoPlayer>
    with PortraitMode<PopupVideoPlayer> {
  YoutubePlayerController videoController;
  @override
  Widget build(BuildContext context) {
    super.build(context);
    return Scaffold(
      body: Center(
        child: Stack(
          fit: StackFit.loose,
          children: <Widget>[
            Container(
              alignment: Alignment.bottomCenter,
              child: ButtonBar(
                alignment: MainAxisAlignment.center,
                children: <Widget>[
                  IconButton(
                    color: Colors.red,
                    icon: Icon(Icons.play_circle_outline),
                    onPressed: () {
                      videoController?.play();
                    },
                  ),
                  IconButton(
                    icon: Icon(
                      Icons.pause,
                    ),
                    onPressed: () {
                      videoController?.pause();
                    },
                  ),
                  IconButton(
                    icon: Icon(
                      Icons.fullscreen,
                    ),
                    onPressed: () {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => VideoPlayerFullScreen(
                            videoId: widget.videoId,
                            position: videoController.value.position,
                          ),
                        ),
                      );
                    },
                  ),
                ],
              ),
            ),
            Container(
              alignment: Alignment.center,
              child: YoutubePlayer(
                context: context,
                videoId: widget.videoId,
                autoPlay: false,
                hideControls: true,
                onPlayerInitialized: (YoutubePlayerController controller) {
                  videoController = controller;
                },
              ),
            ),
            Padding(
              padding: EdgeInsets.only(bottom: 130.0),
              child: Container(
                  alignment: Alignment.centerRight,
                  child: IconButton(
                    icon: Icon(Icons.close),
                    onPressed: () {
                      Navigator.of(context).pop();
                    },
                  )),
            ),
          ],
        ),
      ),
    );
  }
}