<?php
/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 08/03/17
 * Time: 07:04 ص
 */

namespace App\Models;


use App\Core\DB\ORM;

class Material extends ORM
{
    protected static $table='material';
    protected static $pk='id';

    public function user()
    {
        return User::retrieveByPK($this->uid);
    }

    public function course()
    {
        return Course::retrieveByPK($this->cid);
    }

    public function comments(){
        $mid="Material:".$this->id;
        return Comment::retrieveByMid($mid);
    }

    public static function pdfs(){
        return self::retrieveByField('type','pdf');
    }

    public static function docs(){
        return self::retrieveByField('type','doc');
    }

    public static function ppts(){
        return self::retrieveByField('type','ppt');
    }

    public static function videos(){
        return self::retrieveByField('type','video');
    }
}