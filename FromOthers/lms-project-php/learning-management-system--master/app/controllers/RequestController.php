<?php

namespace App\Controllers;

use App\Core\Controller;
use App\Core\Request;
use App\Models\UserRequest;
use App\Core\ResourceInterface;
use App\Core\Session;

/**
 * LMS by the forge team
 */
class RequestController extends Controller implements ResourceInterface
{


    public function index()
    {
        if(Session::isLogin()){
            $reqs = UserRequest::all();
            return view('admin/requests/index', ['reqs' => $reqs]);
        }else{
            Session::set('message','Please Login to Start Learning');
            redirect('/login');
        }

    }

    public function create()
    {
        if (Session::isLogin()) {
            $reqs = UserRequest::all();
            return view('admin/requests/create', ['reqs' => $reqs]);
        } else {
            return view('errors/503', ['message' => "You are not allowed to be here!"]);
        }
    }

    public function store(Request $request)
    {
        if (Session::isLogin()) {
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'title' => 'required',
                    'body' => 'required'
                ]);

                if ($errors) {
                    $request->saveToSession($errors);
                    Session::set('error', "none valid data");
                    redirect(Session::getBackUrl(), $request->getLastFromSession());
                } else {
                    $req = new UserRequest();
                    $req->title = $request->get('title');
                    $req->body = $request->get('body');
                    $req->uid = Session::getLoginUser()->id;
                    $req->created_at = date("Y-m-d H:i:s");
                    $req->updated_at = date("Y-m-d H:i:s");
                    $req->save();
                    Session::set('message', "Request Added Successfully");
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
                $req = UserRequest::retrieveByPK($id);
                return view('admin/requests/show', ['req' => $req]);
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
            if (Session::isLogin()) {
                $req = UserRequest::retrieveByPK($id);
                return view('admin/requests/edit', ['req' => $req]);
            } else {
                return view('errors/503',['message'=>"You are not allowed to be here!"]);
            }
        }catch (\Exception $e){
            return view('errors/404');
        }

    }

    public function update(Request $request, $id)
    {
        if (Session::isLogin()) {
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'title' => 'required',
                    'body' => 'required'
                ]);

                if ($errors) {
                    $request->saveToSession($errors);
                    Session::set('error', "none valid data");
                    redirect(Session::getBackUrl(), $request->getLastFromSession());
                } else {
                    $req = UserRequest::retrieveByPK($id);
                    $req->title = $request->get('title');
                    $req->body = $request->get('body');
                    $req->uid = Session::getLoginUser()->id;
                    $req->updated_at = date("Y-m-d H:i:s");
                    $req->update();
                    Session::set('message', "Request Updated Successfully");
                    redirect(Session::getBackUrl());
                }
            } else {
                return view('errors/503', ['message' => "You are not allowed to be here!"]);
            }
        }
    }

    public function destroy($id)
    {
        if (Session::isLogin()) {
            $req = UserRequest::retrieveByPK($id);
            foreach ($req->comments() as $comment){
                $comment->delete();
            }
            $req->delete();
            Session::set('message', "Request Deleted Successfully");
            redirect(Session::getBackUrl());
        }else{
            return view("errors/503",['message'=>"You are not allowed to be here!"]);
        }
    }
}
