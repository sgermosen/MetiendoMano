<?php

    function lang ($phrase) {
        
        static $lang = array(
        
            'MESSAGAE' => 'اهلا يا',
            'ADMIN' => 'ادمن'
        
        );
        
        return $lang[$phrase];
    }