	jQuery(document).ready(function($) {
	
		  jQuery("#blue" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/blue.css");
			  return false;
		  });
		  
		  jQuery("#green" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/green.css");
			  return false;
		  });
		  
		  jQuery("#red" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/red.css");
			  return false;
		  });
		  
		  
		  jQuery("#yellow" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/yellow.css");
			  return false;
		  });
		  
		  jQuery("#brown" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/brown.css");
			  jQuery(".logo img" ).attr("src", "images/logo.png");
			  return false;
		  });
		  
		  jQuery("#cyan" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/cyan.css");
			  jQuery(".logo img" ).attr("src", "images/logo.png");
			  return false;
		  });
		  
		  jQuery("#purple" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/purple.css");
			  return false;
		  });
		  
		  jQuery("#sky-blue" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/sky-blue.css");
			  return false;
		  });
		  
		  
		  jQuery("#gray" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/gray.css");

			  return false;
		  });
		  
		  
		  jQuery("#black" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/black.css");
			  return false;
		  });
		  
		  
		  
		  jQuery("#default" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/sky-default.css");
			  return false;
		  });
		  
		  
		  
		  jQuery("#white" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/white.css");
			  return false;
		  });
		  
		  
		  jQuery("#light" ).click(function(){
			  jQuery("#color" ).attr("href", "css/light.css");
			  return false;
		  });
		  
		  jQuery("#dark" ).click(function(){
			  jQuery("#color" ).attr("href", "css/dark.css");
			  return false;
		  });



		  jQuery("#peter-river" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/peter-river.css");			 
			  return false;
		  });
		  
		  jQuery("#nephritis" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/nephritis.css");
			  return false;
		  });
		  
		  jQuery("#amethyst" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/amethyst.css");
			  return false;
		  });
		  
		  jQuery("#wet-asphalt" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/wet-asphalt.css");
			  return false;
		  });
		  
		  jQuery("#night-shade" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/night-shade.css");
			  return false;
		  });
		  
		  jQuery("#sun-flowers" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/sun-flowers.css");
			  return false;
		  });
		  
		  jQuery("#orange" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/orange.css");
			  return false;
		  });
		  
		  jQuery("#carrot" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/carrot.css");
			  return false;
		  });
		  
		  
		  jQuery("#alizarin" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/alizarin.css");
			  return false;
		  });
		  
		  jQuery("#sliver" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/sliver.css");
			  return false;
		  });
		  
		  
		  jQuery("#green-sea" ).click(function(){
			  jQuery("#color" ).attr("href", "css/colors/green-sea.css");
			  return false;
		  });
		  
		  
		  jQuery("#box-layout" ).click(function(){
			  jQuery("#color" ).attr("href", "css/box-layout.css");
			  return false;
		  });


		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  
		  jQuery(".layouts #boxed" ).click(function(){
			  jQuery("#wrap" ).addClass("boxed-layout");
			  jQuery("body" ).addClass("bg1-body");
		  });
		  jQuery(".layouts #wide" ).click(function(){
			  jQuery("#wrap" ).removeClass("boxed-layout");
			  jQuery("body" ).removeClass("bg1");
		  });
		  
		  
		  jQuery(".layouts #dark-night" ).click(function(){
			  jQuery("#wrap" ).addClass("dark-version");
		  });
		  jQuery(".layouts #light-day" ).click(function(){
			  jQuery("#wrap" ).removeClass("dark-version");
		  });
		  
		  
		  
		  //add backgrounds
		  jQuery("#bg-one" ).click(function(){
			  jQuery("body" ).addClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-two" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).addClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-three" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).addClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-four" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).addClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-five" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).addClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-six" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).addClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-seven" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).addClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-eight" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).addClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-nine" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).addClass("bg9");
			  jQuery("body" ).removeClass("bg10");
		  });
		  
		  jQuery("#bg-ten" ).click(function(){
			  jQuery("body" ).removeClass("bg1");
			  jQuery("body" ).removeClass("bg2");
			  jQuery("body" ).removeClass("bg3");
			  jQuery("body" ).removeClass("bg4");
			  jQuery("body" ).removeClass("bg5");
			  jQuery("body" ).removeClass("bg6");
			  jQuery("body" ).removeClass("bg7");
			  jQuery("body" ).removeClass("bg8");
			  jQuery("body" ).removeClass("bg9");
			  jQuery("body" ).addClass("bg10");
		  });
		  jQuery("#bg-one, #bg-two, #bg-three, #bg-four, #bg-five, #bg-six, #bg-seven, #bg-eight, #bg-nine, #bg-ten").click(function(){
			  jQuery("#wrapper").addClass("boxed-layout");
		  });
		  jQuery("#wide").click(function(){
			  jQuery("body").removeClass("bg1 bg2 bg3 bg4 bg5 bg6 bg7 bg8 bg9 bg10");
		  });
		  
		  
		  jQuery("#light").click(function(){
			  	jQuery("#footer").addClass("light");
				jQuery("#footer").removeClass("dark");
				jQuery("#footer img" ).attr("src", "images/footer-logo.jpg");
		   });
		   jQuery("#dark").click(function(){
			  	jQuery("#footer").addClass("dark");
				jQuery("#footer").removeClass("light");
				jQuery("#footer img" ).attr("src", "images/footer-logo-dark.jpg");
		   });
		   
		   jQuery("#header-n").click(function(){
			  	jQuery("body").removeClass("fixed-header");
		   });
		   jQuery("#header-f").click(function(){
				jQuery("body").addClass("fixed-header");
		   });
		  
		  
		  
		  // picker buttton
		  jQuery(".picker_close").click(function(){
			  
			  	jQuery("#choose_color").toggleClass("position");
			  
		   });
		   
		   //header
			
			//stickey header
			jQuery(window).scroll(function() {    
				var scroll = jQuery(window).scrollTop();	
				if (scroll >= 40) {
					jQuery(".fixed-header").addClass("small-header");
				}
				else {
					jQuery(".fixed-header").removeClass("small-header");
				}
			});
		   
		  
		  
	});