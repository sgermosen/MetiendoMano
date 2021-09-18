<?php
/**
 * Created by PhpStorm.
 * User: terminator
 * Date: 3/5/17
 * Time: 4:53 PM
 */

namespace App\Controllers;


use App\Core\Controller;
use App\Core\Request;
use App\Core\ResourceInterface;
use App\Core\Session;
use App\Models\Category;
use App\Models\Course;
use App\Models\User;

class CoursesController extends Controller implements ResourceInterface
{
    public function index()
    {
        if(Session::isLogin()){
            $courses = Course::all();
            return view('admin/courses/index',['courses'=>$courses]);
        }else{
            Session::set('message','Please Login to Start Learning');
            redirect('/login');
        }

    }

    public function create()
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == 'admin'){
            $courses = Course::all();
            $cats = Category::all();
            return view('admin/courses/create',['courses'=>$courses, 'cats'=>$cats]);
        }else{
            return view('errors/503',['message'=>"You are not allowed to be here!"]);
        }
    }

    public function store(Request $request)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == 'admin') {
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'title' => 'required',
                    'desc' => 'required',
                    'start' => 'required',
                    'end' => 'required',
                    'cat' => 'required',
                    'rank' => 'required',
                ]);

                if ($errors) {
                    $request->saveToSession($errors);
                    Session::set('error','invalid data check the form');
                    redirect(Session::getBackUrl(), ['errors' => $request->getLastFromSession()]);
                } else {
                    $course = new Course();
                    $course->title = $request->get('title');
                    $course->description = $request->get('desc');
                    $course->start = $request->get('start');
                    $course->end = $request->get('end');
                    $course->cid = $request->get('cat');
                    $course->rate = $request->get('rank');
                    $course->tid = Session::getLoginUser()->id;
                    $course->image=upload_file('image');
                    $course->save();
                    Session::set('message', "Course Added Successfully");
                    redirect(Session::getBackUrl());
                }

            } else {
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
                $course=Course::retrieveByPK($id);
                return view('admin/courses/show',['course'=>$course]);
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
                $course = Course::retrieveByPK($id);
                $cats = Category::all();
                return view('admin/courses/edit', ['course' => $course, 'cats' => $cats]);
            }else{
                return view('errors/503',['message'=>"You are not allowed to be here!"]);
            }
        }catch (\Exception $e){
            return view('errors/404');
        }

    }

    public function update(Request $request, $id)
    {
        if(Session::isLogin() && Session::getLoginUser()->role == 'admin')
        {
            $course=Course::retrieveByPK($id);
            if(verifyCSRF($request)){
                $errors = $this->validator->validate($request, [
                    'title' => 'required',
                    'desc' => 'required',
                    'start' => 'required',
                    'end' => 'required',
                    'cat' => 'required',
                    'rank' => 'required',
                ]);
            }
            if ($errors)
            {
                $request->saveToSession($errors);
                Session::set('error','invalid data check the form');
                redirect("/courses/".$course->id.'/edit', $request->getLastFromSession());
            }else{
                $course->title = $request->get('title');
                $course->description = $request->get('desc');
                $course->start = $request->get('start');
                $course->end = $request->get('end');
                $course->cid = $request->get('cat');
                $course->rate = $request->get('rank');
                $course->tid = Session::getLoginUser()->id;
                if($request->getFile('image')['size']!=0){
                    delete_file($course->image);
                    $course->image=upload_file('image');
                }
                $course->update();
                Session::set('message',"Course Updated Successfully");
                redirect('/courses/create');
            }
        }else{
            return view('errors/503',['message'=>"You are not allowed to be here!"]);
        }
    }

    public function destroy($id)
    {
        if(Session::isLogin() && Session::getLoginUser()->role == 'admin') {
            $course = Course::retrieveByPK($id);
            foreach ($course->materials() as $material){
                $material->delete();
            }
            delete_file($course->image);
            $course->delete();
            Session::set('message', "Course Deleted Successfully");
            redirect(Session::getBackUrl());
        }else{
            return view('errors/503',['message'=>"You are not allowed to be here!"]);
        }
    }


}