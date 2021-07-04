import 'dart:ui';
import 'package:dearly/walkthrough/screens/onboarding/models/onboard_page_model.dart';
import 'package:flutter/cupertino.dart';

List<OnboardPageModel> onboardData  = [
    OnboardPageModel(
      Color(0xffffffff),
      Color(0xFF005699),
      Color(0xFFFFE074),
      2,
      'assets/walkthrough/flutter_onboarding_1.png',
      'Tired of Too Many buttons',
      'Speech Based Interface',
      'One click button for all the task',
    ),
    OnboardPageModel(
      Color(0xFF005699),
      Color(0xFFFFE074),
      Color(0xFF39393A),
      2,
      'assets/walkthrough/flutter_onboarding_2.png',
      'With single click',
      'Video call',
      'Audio/Video call to your loved ones. With Soneya as your assistant, say her the name you wish to call.',
    ),
    OnboardPageModel(
      Color(0xFFFFE074),
      Color(0xFF39393A),
      Color(0xFFE6E6E6),
      3,
      'assets/walkthrough/flutter_onboarding_3.png',
      'Watch',
      'Youtube Videos',
      'Watch Youtube videos just by your speech. Try saying \'play Pal Pal Dil Ke Paas\'.',
    ),
    OnboardPageModel(
      Color(0xFF39393A),
      Color(0xFFffffff),
      Color(0xFF39393A),
      4,
      'assets/walkthrough/flutter_onboarding_4.png',
      'You are our',
      'DEARLY',
      'The closest thing to be cared for is to care for someone else',
    ),
  ];