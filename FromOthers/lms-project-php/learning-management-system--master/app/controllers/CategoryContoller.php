<?php
/**
 * Created by PhpStorm.
 * User: vamos
 * Date: 3/5/17
 * Time: 5:38 PM
 */

namespace App\Controllers;


use App\Core\Controller;
use App\Core\Request;
use App\Core\ResourceInterface;
use App\Models\Category;
use App\Core\Session;

class CategoryController extends Controller implements ResourceInterface
{


    public function index()
    {
        if(Session::isLogin()){
            $cats=Category::all();
            return view('admin/cats/index',['cats'=>$cats]);
        }else{
            Session::set('message','Please Login to Start Learning');
            redirect('/login');
        }

    }

    public function create()
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == 'admin') {
            $cats = Category::all();
            return view('admin/cats/create', ['cats' => $cats]);
        }else{
            return view('errors/503',['message'=>"You are not allowed to be here!"]);
        }
    }

    public function store(Request $request)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == 'admin') {
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'name' => 'required',
                ]);
                if ($errors) {
                    $request->saveToSession($errors);
                    redirect('cats/create', ['errors'=>$request->getLastFromSession()]);
                }else {
                    $category = new Category();
                    $category->name = $request->get('name');
                    $category->save();
                    Session::set('message',"Category Added Successfully");
                    redirect('cats/create');
                }
            }else{
                return view('errors/503', ['message' => "You are not allowed to be here!"]);
            }
        }else{
            return view('errors/503', ['message' => "You are not allowed to be here!"]);
        }

    }

    public function show($id)
    {
        if(Session::isLogin()){
            try{
                $cat=Category::retrieveByPK($id);
                return view('admin/cats/show',['cat'=>$cat]);
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
            if(Session::isLogin() && Session::getLoginUser()->role == 'admin') {
                $cat = Category::retrieveByPK($id);
                return view('admin/cats/edit', ['cat' => $cat]);
            }else{
                return view('errors/503',['message'=>"You are not allowed to be here!"]);
            }
        }catch (\Exception $e){
            return view('errors/404');
        }
    }

    public function update(Request $request, $id)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == 'admin') {
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'name' => 'required',
                ]);
                if ($errors) {
                    $request->saveToSession($errors);
                    redirect('cats/create', ['errors'=>$request->getLastFromSession()]);
                }else {
                    $category = Category::retrieveByPK($id);
                    $category->name = $request->get('name');
                    $category->update();
                    Session::set('message',"Category Updated Successfully");
                    redirect('cats/create');
                }
            }else{
                return view('errors/503', ['message' => "You are not allowed to be here!"]);
            }
        }else{
            return view('errors/503', ['message' => "You are not allowed to be here!"]);
        }
    }

    public function destroy($id)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == 'admin') {
            $category=Category::retrieveByPK($id);
            foreach ($category->courses() as $course){
                $course->delete();
            }
            $category->delete();
            Session::set('message',"Category Deleted Successfully");
            redirect(Session::getBackUrl());
        }else{
            return view('errors/503', ['message' => "You are not allowed to be here!"]);
        }
    }
}