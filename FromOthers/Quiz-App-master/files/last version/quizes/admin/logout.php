<?php

    // Start The Session 
    session_start(); 
    
    // UnSet The Session 
    session_unset();

    // Destroy The Session 
    session_destroy(); 

    header('Location: index.php');
    exit();