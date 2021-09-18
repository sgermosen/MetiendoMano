<?php
namespace App\Core;

class Router{
    public $routes=[
        'GET'=>[],
        'POST'=>[],
        'PUT'=>[],
        'DELETE'=>[]
    ];
    protected $resources=[];
    protected $models=[];
    protected $parameters;
    public static function load($file)
    {
        $router=new static;
        require $file;
        return $router;
    }
    public function define($routes)
    {
        $this->routes=$routes;
    }

    public function get($uri,$controller=null,callable $callback=null)
    {
        if(is_null($controller)&&!is_null($callback)){
            $this->callbacks=$callback;
        }else if(is_null($callback)&&!is_null($controller)){
            $this->routes['GET'][$uri]=$controller;
        }else{
            throw new \Exception("you must specify callback or controller for the route");
        }
    }

    public function post($uri,$controller=null,callable $callback=null)
    {
        if(is_null($controller)&&!is_null($callback)){
            $this->callbacks=$callback;
        }else if(is_null($callback)&&!is_null($controller)){
            $this->routes['POST'][$uri]=$controller;
        }else{
            throw new \Exception("you must specify callback or controller for the route");
        }
    }

    public function put($uri,$controller=null,callable $callback=null)
    {
        if(is_null($controller)&&!is_null($callback)){
            $this->callbacks=$callback;
        }else if(is_null($callback)&&!is_null($controller)){
            $this->routes['PUT'][$uri]=$controller;
        }else{
            throw new \Exception("you must specify callback or controller for the route");
        }
    }

    public function delete($uri,$controller=null,callable $callback=null)
    {
        if(is_null($controller)&&!is_null($callback)){
            $this->callbacks=$callback;
        }else if(is_null($callback)&&!is_null($controller)){
            $this->routes['DELETE'][$uri]=$controller;
        }else{
            throw new \Exception("you must specify callback or controller for the route");
        }
    }

    public function resource($uri,$controller){
        $this->routes['POST'][$uri]=$controller."@store";
        $this->routes['GET'][$uri]=$controller."@index";
        $this->routes['GET'][$uri."/create"]=$controller."@create";
        $this->routes['GET'][$uri."/{id}"]=$controller."@show";
        $this->routes['PUT'][$uri."/{id}"]=$controller."@update";
        $this->routes['DELETE'][$uri."/{id}"]=$controller."@destroy";
        $this->routes['GET'][$uri."/{id}/edit"]=$controller."@edit";
        $this->resources[$uri]=$uri;
    }

    public function direct($request)
    {
        $uri = $this->parseUri($request);
        if(array_key_exists($uri,$this->routes[$request->method()])){
            if(is_callable($this->routes[$request->method()][$uri])){
                App::call($this->routes[$request->method()][$uri],$request);
            }else{
                return $this->callAction($request,
                    ... explode('@',$this->routes[$request->method()][$uri])
                );
            }
        }else{

            throw new \Exception(view('errors/404'));
        }

    }


    protected function callAction($request,$controller,$action)
    {
        $controller="\\App\\Controllers\\{$controller}";
        $controller=new $controller();
        $urientry=explode("/",$request->uri())[0];
        $id=$request->getParameters($urientry);
        if(! method_exists($controller,$action)){
            throw new \Exception("{$controller} does not respond to the {$action} action.");
        }
        switch ($action){
            case "store":
                return $controller->$action($request);
                break;
            case "show":
                return $controller->$action($id);
                break;
            case "edit":
                return $controller->$action($id);
                break;
            case "update":
                return $controller->$action($request,$id);
                break;
            case "destroy":
                return $controller->$action($id);
                break;
            default:
                return $controller->$action($request);

        }

    }

    protected function parseUri($request)
    {
        $string = $request->uri();
        $pattern = '/([0-9]+)/';
        $replacement = '{id}';
        $urientry = explode("/",$request->uri())[0];
        preg_match($pattern, $string, $parameters);
        if(!empty($parameters)){
            $request->setParameters($urientry,$parameters[0]);
        }
        $uri = preg_replace($pattern, $replacement, $string);
        return $uri;
    }

}
