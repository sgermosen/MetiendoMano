<?php
namespace App\Core;
use App\Models\User;

class Session
{
    public static function has($key)
    {
        if(isset($_SESSION[$key])){
            return true;
        }
        return false;
    }

    public static function set($key,$value)
    {
        $_SESSION[$key]=$value;
    }

    public static function get($key)
    {
        if(isset($_SESSION[$key])){
            return $_SESSION[$key];
        }
        return null;
    }

    public static function delete($key)
    {
        if(isset($_SESSION[$key])){
            unset($_SESSION[$key]);
        }
    }

    public static function destroy()
    {
        session_destroy();
    }

    public static function getId()
    {
        return session_id();
    }

    public static function newId($deleteOldSession = false)
    {
        return session_regenerate_id($deleteOldSession);
    }

    public static function setCookie($key,$value){
        setcookie ($key, $value, time()+3600*24*7);
    }

    public static function getCookie($cookie)
    {
        if(isset($_COOKIE[$cookie])) {
           return $_COOKIE[$cookie];
        } else {
            return null;
        }
    }
    public static function deleteCookie($cookie)
    {
        setcookie($cookie, "", time() - 3600);
    }

    public static function saveLogin($username,$role,$password,$isbaned=false)
    {
        self::set('username',$username);
        self::set('password',$password);
        self::set('role',$role);
        if($isbaned){
            self::set('isbaned',true);
        }

    }

    public static function rememberLogin($username,$role,$password,$isbaned=false)
    {
        self::setCookie('username',$username);
        self::setCookie('password',$password);
        self::setCookie('role',$role);
        if($isbaned){
            self::setCookie('isbaned',true);
        }

    }

    public static function forgetLogin()
    {
        self::deleteCookie('username');
        self::deleteCookie('password');
        self::deleteCookie('role');
        self::deleteCookie('isbaned');
    }
    public function isBaned(){
        if(self::get('isbaned')||self::getCookie('isbaned')){
            return true;
        }
        return false;
    }
    public static function isLogin()
    {
        if(self::get('username')||self::getCookie('username')){
            return true;
        }
        return false;
    }

    public static function getLoginUser()
    {
        if(self::get('username')){
            return User::retrieveByUsername(self::get('username'))[0];
        }else{
            return User::retrieveByUsername(self::getCookie('username'))[0];
        }

    }
    public static function saveBackUrl(){
        self::set('back_url',$_SERVER['REQUEST_URI']);
    }

    public static function getBackUrl(){
        $url=self::get('back_url');
        self::delete('back_url');
        return $url;
    }
}
