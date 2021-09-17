import 'package:dearly/homepage.dart';
import 'package:flutter/material.dart';
import 'package:dearly/walkthrough/providers/color_provider.dart';
import 'package:dearly/walkthrough/screens/onboarding/components/onboard_page.dart';
import 'package:dearly/walkthrough/screens/onboarding/data/onboard_page_data.dart';
import 'package:provider/provider.dart';

class Onboarding extends StatelessWidget{
  final PageController pageController = PageController();

  @override
  Widget build(BuildContext context) {
    ColorProvider colorProvider = Provider.of<ColorProvider>(context);
    return Stack(
      children: <Widget>[
        PageView.builder(
          controller: pageController,
          itemCount: onboardData.length,
          itemBuilder: (context, index) {
            print(index);
            return OnboardPage(
              pageController: pageController,
              pageModel: onboardData[index],
            );
          },
        ),
        Container(
          width: double.infinity,
          height: 70,
          child: Align(
            alignment: Alignment.bottomCenter,
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              crossAxisAlignment: CrossAxisAlignment.baseline,
              textBaseline: TextBaseline.alphabetic,
              children: <Widget>[
                Padding(
                  padding: const EdgeInsets.only(left: 32.0),
//                  child: Text(
//                    'fun with',
//                    style: Theme.of(context).textTheme.title.copyWith(
//                          color: colorProvider.color,
//                        ),
//                  ),
                ),
                Padding(
                  padding: const EdgeInsets.only(right: 32.0),
                  child: InkWell(
                    onTap: () {
                      Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (context) => HomePage()));
                    },
                    child: Text(
                      'Skip',
                      style: TextStyle(
                        color: colorProvider.color,
                      ),
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ],
    );
  }
}
