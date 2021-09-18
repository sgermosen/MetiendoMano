<?php

    function lang ($phrase) {
        
        static $lang = array(
            
            // NavBar Links 
        
            'HOME_ADMIN'        => 'Yabla4Elmahalla' ,
            'CATEGORIES'        => 'Categories' ,
            'ITEMS'             => 'Items' ,
            'MEMBERS'           => 'Members' ,
            'STATISTICS'        => 'Statistics' ,
            'LOGS'              => 'Logs' ,
            ''                  => '' ,
            ''                  => '' ,
            ''                  => '' ,
            
           
        
        );
        
        return $lang[$phrase];
    }