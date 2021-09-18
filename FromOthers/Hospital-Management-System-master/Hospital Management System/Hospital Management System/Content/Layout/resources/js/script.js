$(document).ready(function() {
    
    /*--For the sticky navigation---*/
    $('.js-section-features').waypoint(function(direction) {
        if(direction == 'down') {
            $('nav').addClass('sticky');
        }
        else {
            $('nav').removeClass('sticky');
        }
    }, {
        offset: '60px'
    });
    
    /*Scroll on buttons*/
    $('.js-scroll-to-plan').click(function() {
        $('html, body').animate({scrollTop: $('.js-section-plans').offset().top}, 1500);
    });
    $('.js-scroll-to-start').click(function() {
        $('html, body').animate({scrollTop: $('.js-section-features').offset().top}, 1200);
    });
    
    /*Navigation Scroll*/
    $('a[href*="#"]')
      .not('[href="#"]')
      .not('[href="#0"]')
      .click(function(event) {
        if (
          location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') 
          && 
          location.hostname == this.hostname
        ) {
          var target = $(this.hash);
          target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
          if (target.length) {
            event.preventDefault();
            $('html, body').animate({
              scrollTop: target.offset().top
            }, 1000, function() {
              var $target = $(target);
              $target.focus();
              if ($target.is(":focus")) {
                return false;
              } else {
                $target.attr('tabindex','-1');
                $target.focus(); 
              };
            });
          }
        }
    });
    
    
    /*Animation on scroll*/
    $('.js-wp-1').waypoint(function(direction) {
        $('.js-wp-1').addClass('animated fadeIn');        
    }, {
        offset: '50%'
    });
    
    $('.js-wp-2').waypoint(function(direction) {
        $('.js-wp-2').addClass('animated fadeInUp');        
    }, {
        offset: '50%'
    });
    
    $('.js-wp-3').waypoint(function(direction) {
        $('.js-wp-3').addClass('animated fadeIn');        
    }, {
        offset: '50%'
    });
    
    $('.js-wp-4').waypoint(function(direction) {
        $('.js-wp-4').addClass('animated pulse');        
    }, {
        offset: '50%'
    });
    
    
    /* mobile navigation */
    $('.js-mobile-nav-icon').click(function(){
        var nav = $('.js-main-nav');
        var icon = $('.js-mobile-nav-icon i');
        
        nav.slideToggle(200)
        if(icon.hasClass('ion-ios-menu')) {
            icon.addClass('ion-ios-close');
            icon.removeClass('ion-ios-menu');
        } else {
            icon.removeClass('ion-ios-close');
            icon.addClass('ion-ios-menu');
        }
        
    });
    
});