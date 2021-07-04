import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'package:youtube_api/youtube_api.dart';

mixin ListPopupTap<T extends StatefulWidget> on State<T> {
  void onTap(YT_API apiItem, BuildContext context);
}

mixin PortraitMode<T extends StatefulWidget> on State<T> {
  @override
  Widget build(BuildContext context) {
    _portraitModeOnly();
    return null;
  }

  @override
  void dispose() {
    _enableRotation();
    super.dispose();
  }
}

mixin LandScapeMode on StatelessWidget {
  @override
  Widget build(BuildContext context) {
    _landscapeModeOnly();
    return null;
  }
}

void _landscapeModeOnly() {
  SystemChrome.setPreferredOrientations([
    DeviceOrientation.landscapeRight,
    DeviceOrientation.landscapeLeft,
  ]);
}

void _portraitModeOnly() {
  SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
    DeviceOrientation.portraitDown,
  ]);
}

void _enableRotation() {
  SystemChrome.setPreferredOrientations([
    DeviceOrientation.portraitUp,
    DeviceOrientation.portraitDown,
    DeviceOrientation.landscapeRight,
    DeviceOrientation.landscapeLeft,
  ]);
}