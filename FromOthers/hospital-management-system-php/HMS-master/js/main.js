jQuery( document ).ready(function( $ ) {
"use strict"
/*-----------------------------------------------------------------------------------*/
/* 	LOADER
/*-----------------------------------------------------------------------------------*/
$("#loader").delay(1000).fadeOut("slow");
/*-----------------------------------------------------------------------------------*/
/*		STICKY NAVIGATION
/*-----------------------------------------------------------------------------------*/
$(".sticky").sticky({topSpacing:0});
/*-----------------------------------------------------------------------------------*/
/*  FULL SCREEN
/*-----------------------------------------------------------------------------------*/
$('.full-screen').superslides({});
/*-----------------------------------------------------------------------------------
    Animated progress bars
/*-----------------------------------------------------------------------------------*/
$('.progress-bars').waypoint(function() {
  $('.progress').each(function(){
    $(this).find('.progress-bar').animate({
      width:$(this).attr('data-percent')
     },200);
});},
	{ 
	offset: '100%',
    triggerOnce: true 
});
/*-----------------------------------------------------------------------------------*/
/*	ISOTOPE PORTFOLIO
/*-----------------------------------------------------------------------------------*/
var $container = $('.port-wrap .items');
    $container.imagesLoaded(function () {
    $container.isotope({
    itemSelector: '.portfolio-item',
    layoutMode: 'masonry'
});	
});
$('.portfolio-filter li a').on('click', function () {
    $('.portfolio-filter li a').removeClass('active');
    $(this).addClass('active');
    var selector = $(this).attr('data-filter');
    $container.isotope({
      filter: selector
    });
return false;
});
//Togle Menu on click in Header
$(".menu-shows").on('click', function(){
	$(".menu-shows, .menu-shows-inner, .menu").toggleClass("active");
});

/*-----------------------------------------------------------------------------------*/
/*    Parallax
/*-----------------------------------------------------------------------------------*/
jQuery.stellar({
   horizontalScrolling: false,
   scrollProperty: 'scroll',
   positionProperty: 'position',
});
/*-----------------------------------------------------------------------------------*/
/* 	SLIDER REVOLUTION
/*-----------------------------------------------------------------------------------*/
jQuery('.tp-banner').show().revolution({
	dottedOverlay:"none",
	delay:7000,
	startwidth:1200,
	startheight:700,
	navigationType:"bullet",
	navigationArrows:"solo",
	navigationStyle:"preview3",
	parallax:"mouse",
	parallaxBgFreeze:"on",
	parallaxLevels:[7,4,3,2,5,4,3,2,1,0],												
	keyboardNavigation:"on",						
	shadow:0,
	fullWidth:"on",
	fullScreen:"off",
	shuffle:"off",						
	autoHeight:"off",						
	forceFullWidth:"off",	
	fullScreenOffsetContainer:""	
});
/*-----------------------------------------------------------------------------------*/
/* 	TESTIMONIAL SLIDER
/*-----------------------------------------------------------------------------------*/
$("#testi-slide").owlCarousel({ 
    items : 1,
	autoplay:true,
	loop:true,
	autoplayTimeout:5000,
	autoplayHoverPause:true,
	singleItem	: true,
    navigation : false,
	navText: ["<i class='lnr lnr-chevron-left'></i>","<i class='lnr lnr-chevron-right'></i>"],
	pagination : true
});
/*-----------------------------------------------------------------------------------*/
/* 	TESTIMONIAL SLIDER
/*-----------------------------------------------------------------------------------*/
$(".doct-list").owlCarousel({ 
    items : 6,
	margin:30,
    nav:true,
	loop: false,
	autoplay:true,
	autoplayTimeout:5000,
	autoplayHoverPause:true,
	singleItem	: true,
	pagination : false,
	navText: ["<i class='lnr lnr-chevron-left'></i>","<i class='lnr lnr-chevron-right'></i>"],
	pagination : false,
    responsive:{
        300:{
            items:1
        },		
		600:{
            items:3
        },
		800:{
            items:4
        },
		1000:{
            items:5
        },
		1200:{
            items:6
        }}		
});
/*-----------------------------------------------------------------------------------*/
/* 	COUNTER
/*-----------------------------------------------------------------------------------*/
$('.counter').counterUp({
    delay: 10,
    time: 300
});
/*-----------------------------------------------------------------------------------
    TESTNMONIALS STYLE 1
/*-----------------------------------------------------------------------------------*/
$('.flex-slide').flexslider({
	mode: 'fade',
	animation: "fade",
	auto: true
});
/*-----------------------------------------------------------------------------------*/
/* 	ANIMATION
/*-----------------------------------------------------------------------------------*/
var wow = new WOW({
    boxClass:     'animate',      // animated element css class (default is wow)
    animateClass: 'animated', 	// animation css class (default is animated)
    offset:       100,          // distance to the element when triggering the animation (default is 0)
    mobile:       false        // trigger animations on mobile devices (true is default)
});
wow.init();
/*-----------------------------------------------------------------------------------*/
/*    POPUP VIDEO
/*-----------------------------------------------------------------------------------*/
$('.popup-vedio').magnificPopup({
	type: 'inline',
	fixedContentPos: false,
	fixedBgPos: true,
	overflowY: 'auto',
	closeBtnInside: true,
	preloader: true,
	midClick: true,
	removalDelay: 300,
	mainClass: 'my-mfp-slide-bottom'
});
/*-----------------------------------------------------------------------------------*/
/*    POPUP VIDEO
/*-----------------------------------------------------------------------------------*/
$('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
	disableOn: 700,
	type: 'iframe',
	mainClass: 'mfp-fade',
	removalDelay: 160,
	preloader: false,
	fixedContentPos: false
});
/*-----------------------------------------------------------------------------------*/
/*    popup-gallery
/*-----------------------------------------------------------------------------------*/
$('.popup-gallery').magnificPopup({
	delegate: 'a',
	type: 'image',
	tLoading: 'Loading image #%curr%...',
	mainClass: 'mfp-img-mobile',
	gallery: {
	enabled: true,
	navigateByImgClick: true,
	preload: [0,1] // Will preload 0 - before current, and 1 after the current image
},
image: {
	tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
	titleSrc: function(item) {
		return item.el.attr('title') + '<small></small>';
	}
}
});
});
