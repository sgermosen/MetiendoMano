<?php

    include 'connect.php';
    
    //Routes 
    
    $tpl  = 'includes/templates/'; // Template Directory
    $lang = 'includes/lang/'; // Language Directory
    $func = 'includes/func/'; //Functions Directory
    $css  = 'layout/css/'; // Css Directory 
    $js   = 'layout/js/'; // JS Directory
    
    
    //Include Important Things
    
    include $func. 'functions.php';
    include $lang. 'en.php';
    include $tpl.  'header.php';
    
    
    
    //Include Navbar In All Pages Except The One With noNavBar Variable
    
    if(!isset($nonavbar)){
        include $tpl.'navbar.php';
    }

	




