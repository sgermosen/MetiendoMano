<!DOCTYPE html>
<html lang="en">
<head>
    <title>Open Source - LMS</title>
    <link rel="shortcut icon" href="<?=asset('favicon.ico')?>" />
    <!-- meta -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- css -->
    <link rel="stylesheet" href="<?php asset('css/bootstrap.min.css')?>">
    <!-- Ionicons -->
    <link rel="stylesheet" href="<?php asset('css/ionicons.min.css')?>">
    <link rel="stylesheet" href="<?php asset('css/owl.carousel.css')?>">
    <link rel="stylesheet" href="<?php asset('css/owl.theme.css')?>">
    <link rel="stylesheet" href="<?php asset('css/animate.css')?>">
    <link rel="stylesheet" href="<?php asset('css/style.css')?>">

    <!-- fonts -->
    <link href='http://fonts.googleapis.com/css?family=Roboto:400,100,100italic,300,300italic,400italic,500,500italic,700,700italic,900,900italic|Roboto+Condensed:300italic,400italic,700italic,400,300,700|Oxygen:400,300,700' rel='stylesheet'>

    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <!--[if lt IE 9]>
    <p class="browsehappy">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> to improve your experience.</p>
    <![endif]-->
</head>
<body id="home">

<!-- ****************************** Preloader ************************** -->

<div id="preloader"></div>

<!-- ****************************** Sidebar ************************** -->

<nav id="sidebar-wrapper">
    <a id="menu-close" href="#" class="close-btn toggle"><i class="ion-ios-close-empty"></i></a>
    <ul class="sidebar-nav">
        <li><a href="#home">Home</a></li>
        <li><a href="#features">Features</a></li>
        <li><a href="#gallery">Gallery</a></li>
        <li><a href="#team">Development Team</a></li>
        <li><a href="#testimonial">Precious Reviews</a></li>
        <li><a href="#contact">Contact us</a></li>
    </ul>
</nav>

<!-- ****************************** Header ************************** -->

<header class="sticky" id="header">
    <section class="container">
        <section class="row" id="logo_menu">
            <section class="col-xs-6"><a class="logo" href="">OPEN SOURCE - LMS</a></section>
            <section class="col-xs-6"><a id="menu-toggle" href="#" class="toggle wow rotateIn" data-wow-delay="1s"><i class="ion-navicon"></i></a></section>
        </section>
    </section>
</header>

<!-- ****************************** Banner ************************** -->


<section id="banner" >
    <section class="container">
        <a class="slidedown wow animated zoomIn" data-wow-delay="2s" href="#features"><i class="ion-ios-download-outline"></i></a>
        <section class="row">
            <div class="col-md-6">
                <div class="headings">
                    <img class="img-responsive" src="<?php asset('img/logo.png')?>" alt="inform me Logo" title="Inform Me" width="150px" height="200px"/>

                    <p class="wow animated fadeInLeft" style="text-decoration: brown" , "font-weight: bold"; >

                        Full Cycle Learning
                        Create the complete learning cycle from content creation to the final analysis of results.
                        No need for any additional tools or technical skills.

                        <br> WE LOVE OPEN SOURCE</p>
                    <?php if(! \App\Core\Session::isLogin()):?>
                    <div class="row">
                        <div class="col-xs-6 col-sm-6 col-md-5">
                            <div>
                                <a href="/login" class="polo-btn store wow animated bounceInUp"><i class="ion-log-in"></i> Log In</a>
                            </div>
                        </div>
                        <div class="col-xs-6 col-sm-6 col-md-5">
                            <div>
                                <a href="/register" class="polo-btn store wow animated bounceInUp"><i class="ion-locked"></i> Sign Up</a>
                            </div>
                        </div>
                    </div>
                        <?php else: ?>
                        <div class="row">
                            <div class="col-xs-6 col-sm-6 col-md-5">
                                <div>
                                    <a href="/users/<?=\App\Core\Session::getLoginUser()->id?>" class="polo-btn store wow animated bounceInUp"><i class="ion-person"></i> Your Profile</a>
                                </div>
                            </div>
                            <div class="col-xs-6 col-sm-6 col-md-5">
                                <div>
                                    <a href="/courses" class="polo-btn store wow animated bounceInUp"><i class="ion-ios-book"></i> Start Learning</a>
                                </div>
                            </div>
                        </div>
                    <?php endif;?>
                </div>
            </div>
            <div class="col-md-6 hidden-xs hidden-sm">
                <div class="hand-container">
                    <img class="iphone-hand img_res wow animated bounceInUp" data-wow-duration="1.2s" src="<?php asset('img/iphone_hand.png')?>" />
                    <div class="clearfix"></div>
                </div>
            </div>
        </section>
    </section>
</section>

<!-- ****************************** Features Section ************************** -->

<section id="features" class="block">
    <section class="container">
        <section class="row">
            <div class="title-box"><h1 class="block-title wow animated rollIn">
                    <span class="bb-top-left"></span>
                    <span class="bb-bottom-left"></span>
                    Features
                    <span class="bb-top-right"></span>
                    <span class="bb-bottom-right"></span>
                </h1></div>
        </section>

        <section class="row">
            <div class="col-sm-6 col-md-4">
                <div class="feature-box wow animated flipInX" data-wow-delay="0.3s">
                    <i class="ion-search" style="color:#9b59b6;"></i>
                    <h2>Search</h2>
                    <p> we offer a very simple search feature through our whole learning management system

                    </p>
                </div>
            </div>
            <div class="col-sm-6 col-md-4">
                <div class="feature-box wow animated flipInX" data-wow-delay="0.3s">
                    <i class="ion-ios-book" style="color:#d35400;"></i>
                    <h2>Courses</h2>
                    <p>
                        we offer very big diversity of courses , you can create , edit , list and delete courses
                    </p>
                </div>
            </div>
            <div class="col-sm-6 col-md-4">
                <div class="feature-box wow animated flipInX" data-wow-delay="0.3s">
                    <i class="ion-speedometer" style="color:#00ceb8;"></i>
                    <h2>Materials </h2>
                    <p>we offer very big diversity of materials , you can create , edit , list and delete materials </p>
                </div>
            </div>
            <div class="col-sm-6 col-md-4">
                <div class="feature-box wow animated flipInX" data-wow-delay="0.6s">
                    <i class="ion-gear-a" style="color:#c0392b;"></i>
                    <h2>Categories</h2>
                    <p> we offer very big diversity of categories , you can create , edit , list and delete categories</p>
                </div>
            </div>
            <div class="col-sm-6 col-md-4">
                <div class="feature-box wow animated flipInX" data-wow-delay="0.6s">
                    <i class="ion-link" style="color:#27ae60;"></i>
                    <h2>Users</h2>
                    <p>we support our users with very stable service with a lot of features </p>
                </div>
            </div>
            <div class="col-sm-6 col-md-4">
                <div class="feature-box wow animated flipInX" data-wow-delay="0.6s">
                    <i class="ion-information" style="color:#2c3e50;"></i>
                    <h2>Information</h2>
                    <p> we provide  the complete learning cycle from content creation to the final analysis of results.
                        No need for any additional tools or technical skills.</p>
                </div>
            </div>
        </section>
        <div class="clearfix"></div>
    </section>
</section>

<!-- ****************************** Gallery Section ************************** -->

<section id="gallery" class="block">
    <section class="container">
        <section class="row">
            <div class="title-box" style="color:#fff;"><h1 class="block-title wow animated rollIn">
                    <span class="bb-top-left" style="border-color: #fff; "></span>
                    <span class="bb-bottom-left" style="border-color: #fff; "></span>
                    Gallery
                    <span class="bb-top-right" style="border-color: #fff; "></span>
                    <span class="bb-bottom-right" style="border-color: #fff; "></span>
                </h1></div>
        </section>
        <section class="row">
            <div class="col-xs-12">
                <div id="screenshots" class="owl-carousel owl-theme">
                    <div class="item"><img src="<?php asset('img/screen1.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen2.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen3.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen4.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen5.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen6.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen7.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen8.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen9.png')?>" class="img_res wow animated zoomIn"></div>
                    <div class="item"><img src="<?php asset('img/screen10.png')?>" class="img_res wow animated zoomIn"></div>
                </div>
                <div class="customNavigation">
                    <a class="btn prev gallery-nav wow animated bounceInLeft"><i class="ion-ios-arrow-left"></i></a>
                    <a class="btn next gallery-nav wow animated bounceInRight"><i class="ion-ios-arrow-right"></i></a>
                </div>
            </div>
        </section>
    </section>
</section>

<!-- ****************************** Team Section ************************** -->

<section id="team" class="block">
    <section class="container">
        <section class="row">
            <div class="col-md-12">
                <div class="title-box">
                    <h1 class="block-title wow animated rollIn">
                        <span class="bb-top-left"></span>
                        <span class="bb-bottom-left"></span>
                        Development Team
                        <span class="bb-top-right"></span>
                        <span class="bb-bottom-right"></span>
                    </h1>
                </div>
            </div>
        </section>
        <!--Back end Development-->
        <section class="row ">
            <section class="col-md-3 col-sm-6 ">
                <div class="team-member wow animated fadeIn " data-wow-delay=="0.3s">
                    <img  src="<?php asset('img/salama.jpg')?>" class="img_res team-pic ">
                    <h2 class="wow animated fadeInDown" data-wow-delay=="0.7s">Salama  Ashoush</h2>
                    <p class="wow animated fadeIn" data-wow-delay=="0.7s"> Salama is a very active member who is
                    interested in open source technology and graphic designs </p>
                    <ul class="team-social">
                        <li class="wow animated fadeInLeft facebook"><a href="#"><i class="ion-social-facebook"></i></a></li>
                        <li class="wow animated fadeInLeft linkedin"><a href="#"><i class="ion-social-linkedin"></i></a></li>
                        <li class="wow animated fadeInRight googleplus"><a href="#"><i class="ion-social-googleplus-outline"></i></a></li>
                        <li class="wow animated fadeInRight github"><a href="#"><i class="ion-social-github"></i></a></li>
                    </ul>
                </div>
            </section>
            <section class="col-md-3 col-sm-6 ">
                <div class="team-member wow animated fadeIn " data-wow-delay=="0.3s">
                    <img  src="<?php asset('img/habib.jpg')?>" class="img_res team-pic ">
                    <h2 class="wow animated fadeInDown" data-wow-delay=="0.7s">Mohammed Habib</h2>
                    <p class="wow animated fadeIn" data-wow-delay=="0.7s">Mohammed is our major php developer and
                    has a wide range of web technologies techniques </p>
                    <ul class="team-social">
                        <li class="wow animated fadeInLeft facebook"><a href="#"><i class="ion-social-facebook"></i></a></li>
                        <li class="wow animated fadeInLeft linkedin"><a href="#"><i class="ion-social-linkedin"></i></a></li>
                        <li class="wow animated fadeInRight googleplus"><a href="#"><i class="ion-social-googleplus-outline"></i></a></li>
                        <li class="wow animated fadeInRight github"><a href="#"><i class="ion-social-github"></i></a></li>
                    </ul>
                </div>
            </section>
            <section class="col-md-3 col-sm-6 ">
                <div class="team-member wow animated fadeIn " data-wow-delay=="0.3s">
                    <img  src="img/dev-2.jpg" class="img_res team-pic ">
                    <h2 class="wow animated fadeInDown" data-wow-delay=="0.7s">Khaled Mohammed</h2>
                    <p class="wow animated fadeIn" data-wow-delay=="0.7s"> Khaled is one of our active member who loves open source
                    technologies and like to contribute in the community </p>
                    <ul class="team-social">
                        <li class="wow animated fadeInLeft facebook"><a href="#"><i class="ion-social-facebook"></i></a></li>
                        <li class="wow animated fadeInLeft linkedin"><a href="#"><i class="ion-social-linkedin"></i></a></li>
                        <li class="wow animated fadeInRight googleplus"><a href="#"><i class="ion-social-googleplus-outline"></i></a></li>
                        <li class="wow animated fadeInRight github"><a href="#"><i class="ion-social-github"></i></a></li>
                    </ul>
                </div>
            </section>
            <section class="col-md-3 col-sm-6 ">
                <div class="team-member wow animated fadeIn " data-wow-delay=="0.3s">
                    <img  src="<?php asset('img/hanan.jpg')?>" class="img_res team-pic ">
                    <h2 class="wow animated fadeInDown" data-wow-delay=="0.7s">Hanan Abozaid</h2>
                    <p class="wow animated fadeIn" data-wow-delay=="0.7s"> Hanan is a member who likes to add a value
                    to our service by building our products </p>
                    <ul class="team-social">
                        <li class="wow animated fadeInLeft facebook"><a href="#"><i class="ion-social-facebook"></i></a></li>
                        <li class="wow animated fadeInLeft linkedin"><a href="#"><i class="ion-social-linkedin"></i></a></li>
                        <li class="wow animated fadeInRight googleplus"><a href="#"><i class="ion-social-googleplus-outline"></i></a></li>
                        <li class="wow animated fadeInRight github"><a href="#"><i class="ion-social-github"></i></a></li>
                    </ul>
                </div>
            </section>
        </section>

    </section>
</section>

<!-- ****************************** Testimonial ************************** -->

<section id="testimonial" class="block">
    <section class="container">
        <section class="row">
            <div class="title-box"><h1 class="block-title wow animated rollIn">
                    <span class="bb-top-left"></span>
                    <span class="bb-bottom-left"></span>
                    Precious Reviews
                    <span class="bb-top-right"></span>
                    <span class="bb-bottom-right"></span>
                </h1></div>
        </section>
    </section>
    <section class="container">
        <section class="row">
            <section class="col-xs-12">
                <div id="review" class="owl-carousel owl-theme">
                    <div class="item">
                        <div class="row">
                            <div class="col-sm-4 col-sm-offset-1">
                                <div class="client-pic"><img class="img_res" src="<?php asset('img/islam.JPG')?>"></div>
                                <p class="review-star">
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star-outline"></i>
                                </p>
                            </div>
                            <div class="col-sm-6">
                                <p class="review-desc">
                                    As a learning management tool  , i think OS could take online instruction to another level
                                    . it's one more way to spice up boring readings and discussions  , and we are learning ,
                                    creative design results in higher learning outcomes .
                                </p>
                                <p class="client-name">

                                    Eslam Askar
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="row">
                            <div class="col-sm-4 col-sm-offset-1">
                                <div class="client-pic"><img class="img_res" src="<?php asset('img/shreen.JPG')?>"></div>
                                <p class="review-star">
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star-outline"></i>
                                </p>
                            </div>
                            <div class="col-sm-6">
                                <p class="review-desc">
                                    very helpful and easy to use learning management system.
                                </p>
                                <p class="client-name">
                                    Shereen Bhader
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="row">
                            <div class="col-sm-4 col-sm-offset-1">
                                <div class="client-pic"><img class="img_res" src="<?php asset('img/moad.jpg')?>"></div>
                                <p class="review-star">
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star-outline"></i>
                                </p>
                            </div>
                            <div class="col-sm-6">
                                <p class="review-desc">
                                    interesting platform that makes me organize the materials of courses in a very easy way.
                                </p>
                                <p class="client-name">
                                    Ahmed Moawad
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="item">
                        <div class="row">
                            <div class="col-sm-4 col-sm-offset-1">
                                <div class="client-pic"><img class="img_res" src="<?php asset('img/kesha.JPG')?>"></div>
                                <p class="review-star">
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star"></i>
                                    <i class="ion-ios-star-outline"></i>
                                </p>
                            </div>
                            <div class="col-sm-6">
                                <p class="review-desc">
                                    very simple ,  helpful and organized , for it was very interesting and oriented trial .
                                </p>
                                <p class="client-name">
                                    Ahmed Kesha
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </section>
    </section>
</section>

<!-- ****************************** Subscribe Section ************************** -->

<section id="subscribe">
    <section class="container">
        <section class="row">
            <div class="col-sm-10 col-sm-offset-1">
                <p class="susbcribe-text">
                    <strong>You can subscribe to our mailing list to follow the latest developments and places, sites and information around the clock</strong> Do not reduce the spam via e-mail Just trust that I will not use your email only to make you informed about updates contained.</p>
            </div>
        </section>
    </section>
    <section class="container subscribe-wrap">
        <section class="row">
            <div class="col-sm-12">
                <div class="row">
                    <form role="form">
                        <div class="col-xs-10">
                            <div class="form-group">
                                <input type="email" class="form-control" id="exampleInputEmail1" placeholder="Enter email">
                            </div>
                        </div>
                        <div class="col-xs-2">
                            <button type="submit" class="btn-custom">
                                <i class="ion-ios-arrow-thin-right"></i>
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </section>
    </section>
</section>

<!-- ****************************** Contact Section ************************** -->

<section id="contact">
    <section class="container contact-wrap">
        <section class="row">
            <div class="title-box"><h1 class="block-title wow animated rollIn">
                    <span class="bb-top-left"></span>
                    <span class="bb-bottom-left"></span>
                    Contact Us
                    <span class="bb-top-right"></span>
                    <span class="bb-bottom-right"></span>
                </h1></div>
        </section>
    </section>
    <section class="address">
        <div class="container">
            <div class="col-sm-12">
                <ul class="address-list">
                    <li><i class="ion-ios-location" style="background-color: rgb(255, 102, 0);"></i> <span>ITI - Nasr City <br>Cairo-Egypt</span></li>
                    <li><i class="ion-ios-telephone" style="background-color: #63cfea;"></i> <span>01007349461</span></li>
                    <li><i class="ion-email" style="background-color: #6ecba9;"></i> <span>info@OS-LMS.com</span></li>
                    <li><i class="ion-earth" style="background-color: #ff6969;"></i> <span>www.OS-LMS.com</span></li>
                </ul>
            </div>
        </div>
    </section>
    <section class="mailbox">
        <div class="container">
            <div class="col-sm-12">
                <form  mailto="info@info-me.com" name="sentMessage" id="contactForm" novalidate>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <input type="text" class="form-control" placeholder="Your Name *" id="name" required data-validation-required-message="Please enter your name.">
                                <p class="help-block text-danger"></p>
                            </div>
                            <div class="form-group">
                                <input type="email" class="form-control" placeholder="Your Email *" id="email" required data-validation-required-message="Please enter your email address.">
                                <p class="help-block text-danger"></p>
                            </div>
                            <div class="form-group">
                                <input type="tel" class="form-control" placeholder="Your Phone *" id="phone" required data-validation-required-message="Please enter your phone number.">
                                <p class="help-block text-danger"></p>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <textarea class="form-control" placeholder="Your Message *" id="message" required data-validation-required-message="Please enter a message."></textarea>
                                <p class="help-block text-danger"></p>
                                <div id="success"></div>
                                <button type="submit" class="polo-btn contact-submit"><i class="ion-paper-airplane"></i></button>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </section>
    <div class="clearfix"></div>
</section>

<!-- ****************************** Footer ************************** -->

<section id="footer">
    <section class="container">
        <section class="row">
            <div class="col-sm-6">
                <span></span>
                <h1 class="footer-logo">
                    <a href="#">Open Source</a>
                </h1>
            </div>
            <div class="col-sm-6">
                <p class="copyright">All &copy; Copyright OS-LMS 2016 </p>
            </div>
        </section>
    </section>
</section>


<!-- All the scripts -->

<script src="<?php asset('js/jquery-2.1.3.min.js')?>"></script>

<!-- Bootstrap 3.3.5 -->
<script src="<?php asset('js/bootstrap.min.js')?>"></script>
<script src="<?php asset('js/wow.min.js')?>"></script>
<script src="<?php asset('js/owl.carousel.js')?>"></script>
<script src="<?php asset('js/script.js')?>"></script>

</body>
</html>