<?php
use App\Core\App;
session_start();
App::bind('config',require 'config.php');
if(App::get('config')['heroku']){
    $url = parse_url(getenv("CLEARDB_DATABASE_URL"));
    \App\Core\DB\ORM::useConnection(Connection::make([
        'name'=>substr($url["path"], 1),
        'username'=>$url["user"],
        'password'=>$url["pass"],
        'connection'=>'mysql:host='.$url["host"].';',
        'options'=>[
            PDO::ATTR_ERRMODE=>PDO::ERRMODE_EXCEPTION
        ]]));
}else{
    \App\Core\DB\ORM::useConnection(Connection::make(App::get('config')['database']));
}


require 'core/Helper.php';

