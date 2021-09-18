<?php

/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 04/03/17
 * Time: 09:23 ص
 */
namespace App\Models;

use App\Core\DB\ORM;

class Course extends ORM
{
    protected static $table="course";
    protected static $pk="id";

    
    public function category()
    {
        return Category::retrieveByPK($this->cid);
    }

    public function materials()
    {
        return Material::retrieveByField('cid',$this->id);
    }
}