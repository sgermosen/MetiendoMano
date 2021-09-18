<?php

/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 04/03/17
 * Time: 09:23 ص
 */
namespace App\Models;

use App\Core\DB\ORM;

class User extends ORM 
{
    protected static $table="users";
    protected static $pk="id";

    public function requests()
    {
        return UserRequest::retrieveByField('uid',$this->id);
    }

    public static function online()
    {
        return self::retrieveByField('online','1')[0];
    }

    public static function active()
    {
        return self::retrieveByField('state','active')[0];
    }

    public static function offline()
    {
        return self::retrieveByField('online','0')[0];
    }

    public static function disabled()
    {
        return self::retrieveByField('state','disabled')[0];
    }

    public static function banned()
    {
        return self::retrieveByField('isbaned',0)[0];
    }

}