<?php
/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 21/02/17
 * Time: 11:57 م
 */

namespace App\Core;


class Validator
{
    protected $errors=[];

    public function validate($request,$rulesset){
        foreach ($rulesset as $field=>$rules){
            $rules=explode("|",$rules);
            foreach ($rules as $rule) {
                if(preg_match("/(.)+:(.)+/",$rule)){
                    $rule=explode(":",$rule);
                    $this->parsKVRule($request,$field,$rule);
                }else{
                   $this->parsRule($request,$field,$rule);
                }
            }
        }
        if(empty($this->errors)){
            return false;
        }else{
            return $this->errors;
        }
    }

    protected function parsRule($request, $field, $rule)
    {
        $value=$request->get($field);
        switch ($rule){
            case "required":
                if($value==""){
                    $this->errors[$field][]="this field is required";
                }
                break;
            case "email":
                if(!filter_var($value, FILTER_VALIDATE_EMAIL)){
                    $this->errors[$field][]="Invalid email format";
                }
                break;
            case "url":
                if (!preg_match("/\b(?:(?:https?|ftp):\/\/|www\.)[-a-z0-9+&@#\/%?=~_|!:,.;]*[-a-z0-9+&@#\/%=~_|]/i",$value)) {
                    $this->errors[$field][] = "Invalid URL";
                }
                break;
        }
    }
    protected function parsKVRule($request, $field, $rule)
    {
        $value=$request->get($field);
        switch ($rule[0]){
            case "max":
                if(strlen($value)>$rule[1]){
                    $this->errors[$field][]="not allowed more than $rule[1] character";
                }
                break;
            case "min":
                if(strlen($value)<$rule[1]){
                    $this->errors[$field][]="at least $rule[1] character";
                }
                break;
        }
    }
}