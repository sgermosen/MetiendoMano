<?php
/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 22/02/17
 * Time: 12:18 م
 */

namespace App\Controllers;


use App\Core\Controller;
use App\Core\Request;
use App\Core\ResourceInterface;
use App\Core\Session;
use App\Models\Course;
use App\Models\Material;


class MaterialController extends Controller implements ResourceInterface
{

    public function index()
    {
        if(Session::isLogin()){
            $courses=Course::all();
            return view('admin/materials/index',['courses'=>$courses]);
        }else{
            Session::set('message','Please Login to Start Learning');
            redirect('/login');
        }

    }

    public function create()
    {
        if (Session::isLogin()&&Session::getLoginUser()->role=='admin') {
            $materials=Material::all();
            $courses=Course::all();
            return view('admin/materials/create',['materials'=>$materials,'courses'=>$courses]);
        } else {
            return view('errors/503', ['message' => "You are not allowed to be here!"]);
        }
    }

    public function store(Request $request)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role=="admin") {
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'cid' => 'required',
                    'name'=>'required',
                    'type'=>'required'
                ]);

                if ($errors) {
                    $request->saveToSession($errors);
                    Session::set('error', "none valid data");
                    redirect(Session::getBackUrl(), $request->getLastFromSession());
                } else {
                    $material = new Material();
                    $material->cid = $request->get('cid');
                    $material->name = $request->get('name');
                    $material->type = $request->get('type');
                    $material->status = $request->get('status');
                    $material->downloaded = 0;
                    if($request->get('type')=='video'){
                        if(strpos($request->get('vlink'),"watch?v=")){
                            $vlink=explode("watch?v=",$request->get('vlink'));
                            $material->link=$vlink[0]."embed/".$vlink[1];
                        }else if(strpos($request->get('vlink'),"embed")){
                            $vlink=explode("watch?v=",$request->get('vlink'));
                            $material->link=$vlink[0]."embed/".$vlink[1];
                        }else{
                            $request->saveToSession($errors);
                            Session::set('error', "invalid youtube link");
                            redirect(Session::getBackUrl(), $request->getLastFromSession());
                        }

                    }else{
                        $material->link=upload_file("link");
                    }
                    $material->description=$request->get('description');
                    $material->created_at = date("Y-m-d H:i:s");
                    $material->updated_at = date("Y-m-d H:i:s");
                    $material->save();
//                    var_dump($material);
//                    die();
                    Session::set('message', "Material Added Successfully");
                    redirect(Session::getBackUrl());
                }
            } else {
                return view('errors/503', ['message' => "You are not allowed to be here!"]);
            }
        }
    }

    public function show($id)
    {
        if(Session::isLogin()){
            try{
                $material = Material::retrieveByPK($id);
                if($material->status=='hide'){
                    return view('errors/503',['message'=>'You are not allowed to view this material']);
                }
                return view('admin/materials/show', ['material' => $material]);
            }catch (\Exception $e){
                return view('errors/404');
            }
        }else{
            Session::set('message','Please Login to Start Learning');
            redirect('/login');
        }



    }

    public function edit($id)
    {
        try{
            if (Session::isLogin()&&Session::getLoginUser()->role=="admin") {
                $material = Material::retrieveByPK($id);
                $courses=Course::all();
                return view('admin/materials/edit', ['courses'=>$courses,'material' => $material]);
            } else {
                return view('errors/503',['message'=>"You are not allowed to be here!"]);
            }
        }catch (\Exception $e){
            return view('errors/404');
        }

    }

    public function update(Request $request, $id)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role=="admin") {
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'cid' => 'required',
                    'name'=>'required',
                    'type'=>'required'
                ]);

                if ($errors) {
                    $request->saveToSession($errors);
                    Session::set('error', "none valid data");
                    redirect(Session::getBackUrl(), $request->getLastFromSession());
                } else {
                    $material = Material::retrieveByPK($id);
                    $material->cid = $request->get('cid');
                    $material->name = $request->get('name');
                    if($request->get('type')=='video'){
                        if($material->type!='video') {
                            delete_file($material->link);
                        }
                        if(strpos($request->get('vlink'),"watch?v=")){
                            $vlink=explode("watch?v=",$request->get('vlink'));
                            $material->link=$vlink[0]."embed/".$vlink[1];
                        }else if(strpos($request->get('vlink'),"embed")){
                            $vlink=explode("watch?v=",$request->get('vlink'));
                            $material->link=$vlink[0]."embed/".$vlink[1];
                        }else{
                            $request->saveToSession($errors);
                            Session::set('error', "invalid youtube link");
                            redirect(Session::getBackUrl(), $request->getLastFromSession());
                        }
                    }else{
                        if($request->getFile('link')['size']!=0){
                            delete_file($material->link);
                            $material->link=upload_file("link");
                        }else{
                            $request->saveToSession($errors);
                            Session::set('error', "invalid file");
                            redirect(Session::getBackUrl(), $request->getLastFromSession());
                        }
                    }
                    $material->type = $request->get('type');
                    $material->description=$request->get('description');
                    $material->updated_at = date("Y-m-d H:i:s");
                    $material->update();
                    Session::set('message', "Material Updated Successfully");
                    redirect(Session::getBackUrl());
                }
            } else {
                return view('errors/503', ['message' => "You are not allowed to be here!"]);
            }
        }
    }

    public function destroy($id)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role=="admin") {
            $material = Material::retrieveByPK($id);
            foreach ($material->comments() as $comment){
                $comment->delete();
            }
            if($material->type!='video'){
                delete_file($material->link);
            }
            $material->delete();
            Session::set('message', "Material Deleted Successfully");
            redirect(Session::getBackUrl());
        }else{
            return view("errors/503",['message'=>"You are not allowed to be here!"]);
        }
    }

    public function download($request)
    {
        if(Session::isLogin()){
            $material=Material::retrieveByPK($request->get('mat'));
            if($material->status=='lock'){
                return view('errors/503',['message'=>'Sorry this material is locked from download']);
            }
            $material->downloaded=$material->downloaded+1;
            $material->update();
            if($material->type!='video'){
                header("Location:$material->link");
            }else{
                header("Location:$material->link");
                exit();
            }
        }else{
            Session::set('message','Please Login to Start Learning');
            redirect('/login');
        }


    }
}