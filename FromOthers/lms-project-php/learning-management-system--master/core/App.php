<?php
namespace App\Core;

class App
{

    public static $registry=[];

    public static function bind($key,$value)
    {
        static::$registry[$key]=$value;
    }

    public static function get($key)
    {
        if (! array_key_exists($key,static::$registry)) {
            throw new \Exception("No $key is found in the container.");
        }
        return static::$registry[$key];
    }

    public static function call($callable, $data)
    {
        return $callable($data);
    }
}
