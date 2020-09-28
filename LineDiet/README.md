# Line Diet (Xamarin.Forms)
![Line Diet Promo Images](_readmeAssets/GithubHeader.png)

## Overview
Line Diet is a mobile app for tracking and graphing your diet based off of a simple engineer's diet called the "line diet", "bang-bang servo diet", or "Steve Ward diet". It was conceived by [Steve Ward](https://www.csail.mit.edu/user/1517) who said "all that you need for my diet is graph paper, a ruler, and a pencil". For more information visit the official website for Line Diet at [LineDietApp.com](http://www.linedietapp.com).

## Code Metrics
![Line Diet Code Metrics](_readmeAssets/CodeMetrics.png)

## Platforms Supported
- iOS (8.0+)
- Android (4.0.3+)

## Technologies Used
* [Xamarin.Forms](https://www.xamarin.com/forms)
* [Prism](https://github.com/PrismLibrary/Prism)
* [OxyPlot](http://www.oxyplot.org)
* [SQLite](https://www.sqlite.org)
* [Share Plugin for Xamarin and Windows](https://www.nuget.org/packages/Plugin.Share)
* [Settings Plugin for Xamarin and Windows](https://github.com/jamesmontemagno/SettingsPlugin)
* [Xamarin.Forms CarouselView](https://github.com/chrisriesgo/xamarin-forms-carouselview) *(slightly modified)*
* [Xamarin Google Play Services - Analytics](https://www.nuget.org/packages/Xamarin.GooglePlayServices.Analytics)
* [Google APIs Analytics iOS Library](https://www.nuget.org/packages/Xamarin.Google.iOS.Analytics)

## Project Features
### MVVM Architecture (using Prism)
* Data binding via Bindable Properties and Bindable Commands *(using CanExecute for button disabling)*
* Platform specific services with IPlatformInitializer *(ex: AnalyticsService, ReviewService)*
* Use of INavigationService, INavigationAware, IActiveAware, and PubSubEvent
* Binding Converters *(ex: CheckmarkVisibilityConverter)*
* Zero code-behind XAML layouts *(except for orientation UI adjustment in GraphPage.xaml.cs)*

### Custom UI with Xamarin Forms
* XAML style definitions - *color, font, label, and button styles*
* Platform specific style definitions *(ex: TitleLabelStyle, BoxButtonStyle)*
* Dynamic app themeing - *navigation bar, tab bar, menu items change colors in response to app state*
* Portrait and Landscape support - *including full-screen graph when in landscape*
* Loading indicator control - *bound to all View Models' IsBusy property - use DEBUG_SIMULATE_SLOW_RESPONSE flag to slow down*
* Onboarding carousel - *swipeable set of pages (via DataTemplates) with paging dot indicators*
* iOS Custom Renderers
	* AutoSelectEntryRenderer - *auto-selects all text on Entry field focus*
	* CustomNavigationPageRenderer - *changes Navigation Bar colors dynamically in response to app events*
	* CustomTabbedPageRenderer - *uses separate tab images for selected/unselected states, vertically offsets images to be centered (as no labels on tabs), and changes tab colors dynamically in response to app events*
	* CustomViewCellRenderer - *sets ListView selected row background color to clear (see Known Issues below)*
* Android Custom Renderers
	* CustomDatePickerRenderer - *removes underline typically shown in Android entry fields*
	* CustomEntryRenderer - *auto-selects all text on Entry field focus, removes underline typically shown in Android entry fields*
	* CustomTabbedPageRenderer - *disables swipe gesture between tabs (as graph contained in tabs is swipeable itself)*

### Etc.
* Loading/Saving of data *(via async SQLite calls)*
* Graphing of weights with OxyPlot *(includes graphing of multiple plot series, dynamic tick mark labeling based on zoom level, etc.)*
* Tracking of events and page navigation with Google Analytics *(ex: created a goal, saved a weight, opened about page, etc.)*
* Loading/Saving of app settings *(via Settings Plugin for Xamarin and Windows)*
* Sharing of app via various social networks *(via Share Plugin for Xamarin and Windows)*
* Data bound ListViews with context menu for deletion
* Support for weight units in pounds, kilograms, and stones

## Potential Future Improvements
* Support more platforms (ex: Windows Phone, Apple Watch, Windows 10, Android Wear)
* HealthKit support
* Google Fit support
* UI tests
* Unit tests
* Accessibility
* Crash reporting
* Multi-language support ([reference](https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/))
* Alternative graphing modes *(ex: bar chart instead of line graph)*
* SVGs for icons *(and relative SVG image service)*

## TODO List
* Switch to using asset catalogs for iOS app icons
* Support for local date format ([example](http://stackoverflow.com/a/37858898/18005))
* Improved getting started images for tablets
* Remove delay before graph/listings populate the first time

## Version History
* v1.00 - initial release
* v1.10 - support for pounds, kilograms, and stones as weight units (in Settings)
* v1.11 - fix for landscape graphing, added ListView virtualization, increased graph/listing item limit (see Known Issues for why Android has lower graphing limit) 

## Known Issues
* Mock services have been stubbed out, but would not work well if used for unit tests currently
* CustomViewCellRenderer does not work properly in all scenarios (works for menu ListView, but not weight listing ListView) [[External issue](http://stackoverflow.com/questions/37050207/android-datepicker-dialog-is-having-transparent-background#comment61810449_37050406)]
* Xamarin Forms Preview does not work due to conflict with OxyPlot library [[External issue](https://bugzilla.xamarin.com/show_bug.cgi?id=52158)]
* Date picker on Visual Studio Android emulators looks bad (looks fine on physical devices) [[External issue](https://forums.xamarin.com/discussion/comment/178616/#Comment_178616)]
* Nav bar color on embedded webview (ex: links from about page) are not shown on emulator [External issue?]
* OxyPlot appears to stop drawing lines connecting dots when graphing more than ~525 items - currently limiting number of items to graph on Android to avoid this [External issue?]

## Contributing to this Project ##
I welcome any contributions that will help improve the functionality and/or code quality of this project. If you have a specific idea you would like to implement feel free to contact me ([@SmartyP](http://www.smartyp.net)) directly, or submit a pull request.

## Special Thanks
* [Brian Lagunas](https://github.com/brianlagunas) - Prism
* [James Montemagno](https://github.com/jamesmontemagno) - Xamarin Plugins
* [Chris Reisgo](https://github.com/chrisriesgo/) - Xamarin Carousel
* [Visual Studio Spell Checker Extension](https://marketplace.visualstudio.com/items?itemName=EWoodruff.VisualStudioSpellCheckerVS2017andLater) 
* Everyone who has answered questions on the [Xamarin Forums](https://forums.xamarin.com) and [Stack Overflow](http://stackoverflow.com/questions/tagged/xamarin.forms)

## License
The MIT License (MIT)

Copyright (c) 2017 SmartyPantsCoding, LLC

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.