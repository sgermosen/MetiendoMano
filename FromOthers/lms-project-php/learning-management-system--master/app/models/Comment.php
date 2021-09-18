<?php
/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 08/03/17
 * Time: 06:59 ص
 */

namespace App\Models;


use App\Core\App;
use App\Core\DB\ORM;

class Comment extends ORM
{
    protected static $table='comments';
    protected static $pk='id';

    public function user()
    {
        return User::retrieveByPK($this->uid);
    }

    public function model()
    {
        $model=explode(":",$this->mid)[0];
        $modelid=explode(":",$this->mid)[1];
        return $model::retrieveByPK($modelid);
    }
}