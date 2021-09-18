<?php
/**
 * Created by PhpStorm.
 * User: vamos
 * Date: 3/5/17
 * Time: 5:10 PM
 */

namespace App\Models;


use App\Core\DB\ORM;

class Category extends ORM
{
    protected static $table="category";
    protected static $pk="id";

    public function courses()
    {
        return Course::retrieveByField("cid",$this->id);
    }

    public function user()
    {
        return User::retrieveByPK($this->uid);
    }


}