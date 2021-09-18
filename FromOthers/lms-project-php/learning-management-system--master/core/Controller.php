<?php
/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 22/02/17
 * Time: 12:16 ص
 */

namespace App\Core;
use App\Core\Validator;

class Controller
{
    protected $validator;

    function __construct()
    {
        $this->validator=new Validator();
    }

    /**
     * @return mixed
     */
    public function getValidator()
    {
        return $this->validator;
    }

    /**
     * @param mixed $validator
     */
    public function setValidator($validator)
    {
        $this->validator = $validator;
    }


}