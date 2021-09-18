<img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/design/ggg.jpg" width="1550">


[![NPM Version][npm-image]][npm-url]
[![Build Status][travis-image]][travis-url]
[![Downloads Stats][npm-downloads]][npm-url]



Quiz App
========
Welcome to QuizApp's open source Android app! Come on in, take your shoes
off, stay a while explore how examination's 's native squad has built and
continues to build the app, _discover_ our implementation of [RxJava](https://github.com/ReactiveX/RxJava) in logic-
filled [view models](https://github.com/mohamedebrahim96/MET-Quiz),
and maybe even create an issue or two.
Short blurb about what your product does.

> _Quiz android app for my collage ...and it's my graduation project_




Purpose of the Project
========================
- Attempting all the quizzes will be easy for everyone.
- Access to the quiz for the revision even after the 3 months challenge. 
- Easy to manage quizzes and keeping the track of all attempted quizzes.
- Android Resources: Useful resources related to Android based on a search keyword
- replace using paper.

Getting Started
================
Follow these instructions to build and run the project without data; note that
the app will be blank.

1. Clone this repository from git hub link (https://github.com/mohamedebrahim96/Quiz-App.git)

<img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/files2/qalBB.png" width="240">


2. Download the appropriate [JDK](http://www.oracle.com/technetwork/java/javase/downloads/jdk8-downloads-2133151.html)
for your system. We are currently on JDK 8.
3.Install Android Studio (https://developer.android.com/sdk/index.html).
4. `cd` into the project repo and run `Quize` to your Android
   development environment. Keep an eye on the output to see if   any manual steps
   are required.
5. Import the project. Open Android Studio, click `Open an existing Android
   Studio project` and select the project. Gradle will build the project.
6. Run the app. Click `Run > Run 'app'`. After the project builds you'll be
   prompted to build or launch an emulator.

Features
========
- Educational (based on Exam).
- Added new Exams and question Done by the Professor
- It has database to store questions , Exams and previous results 
- Every exam have it's own duration (manage by Professor)
- Timmer of 500 mill sec for each question (Timer at top center)
- Random questions in exam (It will peek random ques and will show it to the user)
- Good and Extensible UI.
- It's offline exam besed on local server
- Compatibility with Android API-14 and above
- minSdkVersion 14
- targetSdkVersion 25 (android Nougat)

***


# Screenshots
<img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/device-2018-06-09-003821.png" width="250"><img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/device-2018-06-09-004017.png" width="250"><img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/device-2018-06-09-004332.png" width="250"><img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/device-2018-06-09-004515.png" width="250"><img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/34384048_1733282310086755_3229344794140475392_n.jpg" width="250"><img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/34394812_1733282236753429_6339434315159437312_n.jpg" width="250"><img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/34473973_1733282786753374_571604875163467776_n.jpg" width="250">
<img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/Screenshot_2018-06-21-08-24-15-679.jpeg" width="250">
<img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/screenshots/Android%20App/Screenshot_2018-06-21-09-09-43-504.jpeg" width="250">


***

Application Requirements
====================
Basic authentication & Details -
1) Splash Screen
2) Login Screen
   We intend to keep only Google login for authenticating users as of now.
3) Sign up
One time screen after the user authenticates. This screen will take user details. The fields will include:
card number
email
password
first Name
last Name
Photo/Select Avatar

====================================

4) Home Screen
There will be a list of all the quizzes and each quiz item will contain the following information:
Screen title
Student's Name
Exam
settings
results
barcode Fragment

===============================

5)Results
Overall Score.
Number of quizzes attempted.
Attempted Quizzes history & score.
Course Notes / External resources.

***

Contributing
===============
We intend for this project to be an educational resource: we are excited to share our wins, mistakes, and methodology of Android development as we work in the open. Our primary focus is to continue improving the app for our users in line with our roadmap.

The best way to submit feedback and report bugs is to open a Github issue. Please be sure to include your operating system, device, version number, and steps to reproduce reported bugs. Keep in mind that all participants will be expected to follow our code of conduct.

***

Code of Conduct
==============
We aim to share our knowledge and findings to the professor as we work daily to improve our product, for Examnition community, in a safe and open space. We work as we live, as kind and considerate students who learn and grow from giving and receiving positive, constructive feedback. professor reserve the right to delete or edit any of his students.


color palette
==========

<img src="https://github.com/mohamedebrahim96/Quiz-App/raw/master/files/files2/1fffff.jpg" width="350">




## Supported Devices
```sh
* android Kitkat 4.4.2 (API-19)
* android Lollipop 5.0/5.1(API-21)
* android Marshmallow (API-23)
* android nougat 7.0 (API-24)
* android tablets 
```



## Third-party
```sh
* Room Persistence Library V1.1.0
* espresso-core:3.0.1 (https://github.com/chiuki/espresso-samples)
* picasso 2.3.2 (https://github.com/square/picasso)
* Calligraphy 2.3.0 (https://github.com/chrisjenx/Calligraphy)
* SwitchButton 2.0.0 (https://github.com/kyleduo/SwitchButton)
* Slider 1.1.5 (https://github.com/daimajia/AndroidImageSlider)
* Retrofit 2.4.0 (https://github.com/square/retrofit)
* Glide 4.2.0 (https://github.com/bumptech/glide)
* Jsoup 1.10.3 (https://github.com/jhy/jsoup)
```


## Release History

* 0.2.1
   * updating Exam layers and layouts.
   * make some improvements and enhancement.
   * upload results on the server.
   * make the android app repo on Github.com
* 0.2.0
    * add a local Database (Room).
* 0.1.1
    * Fix some bugs.
    * new Activitys new layouts.
* 0.1.0
    * add advanced settings of the user to add ip etc..
    * add login&signup layouts/fragmnts.
* 0.0.1
    * Add exam timers and points.



## Updates log

* april 29, 2018
   -updating Exam layers and layouts.
   -make some improvements and enhancement.
   -upload results on the server.
   -make the android app repo on Github.com
* mar 9, 2018
     -add a local Database (Room).
     -store the results and view them
* Sep 26, 2017
     -Fix some bugs.
     -new Activitys new layouts.
* Aug 29, 2017
     -add advanced settings of the user to add ip etc..
     -add login&signup layouts/fragmnts.
* Apr 4, 2014
      -Add exam timers and points.





License
=======

    Copyright 2018 Vacuum, Inc.

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
    
    
<!-- Markdown link & img dfn's -->
[npm-image]: https://img.shields.io/npm/v/datadog-metrics.svg?style=flat-square
[npm-url]: https://npmjs.org/package/datadog-metrics
[npm-downloads]: https://img.shields.io/npm/dm/datadog-metrics.svg?style=flat-square
[travis-image]: https://img.shields.io/travis/dbader/node-datadog-metrics/master.svg?style=flat-square
[travis-url]: https://travis-ci.org/dbader/node-datadog-metrics
[wiki]: https://github.com/yourname/yourproject/wiki

