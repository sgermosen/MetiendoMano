<?php
/**
 * Created by PhpStorm.
 * User: salamaashoush
 * Date: 22/02/17
 * Time: 12:18 م
 */

namespace App\Controllers;


use App\Core\Controller;
use App\Core\Helper;
use App\Core\Request;
use App\Core\ResourceInterface;
use App\Core\Session;
use App\Models\User;

class UserController extends Controller implements ResourceInterface
{
    public function index()
    {
        if (Session::isLogin()) {
            $users = User::all();
            return view('admin/users/index', ['users' => $users]);
        }else{
            $reqs = UserRequest::all();
            return view('admin/requests/index', ['reqs' => $reqs]);
        }
    }
    public function create()
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == 'admin') {
            $users = User::all();
            return view('admin/users/create', ['users' => $users]);
        }else{
            return view('errors/503',['message'=>"You are not allowed to be here!"]);
        }
    }

    public function store(Request $request)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == 'admin') {
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'firstname' => 'required',
                    'lastname' => 'required',
                    'username' => 'required',
                    'email' => 'required|email',
                    'password' => 'required|min:8',
                    'confirm' => 'required|min:8'
                ]);
                if ($request->get('password') !== $request->get('confirm')) {
                    $errors['login'] = "Password not match";
                }
                if (!empty(User::retrieveByEmail($request->get('email')))) {
                    Session::set('error', "User already exists");
                    redirect(Session::getBackUrl(), $request->getLastFromSession());
                } else {
                    if ($errors) {
                        $request->saveToSession($errors);
                        Session::set('error', "non valid data");
                        redirect(Session::getBackUrl(), $request->getLastFromSession());
                    } else {
                        $user = new User();
                        $user->firstname = $request->get('firstname');
                        $user->lastname = $request->get('lastname');
                        $user->username = $request->get('username');
                        $user->signature = $request->get('signature');
                        $user->email = $request->get('email');
                        $user->password = password_hash($request->get('password'), PASSWORD_DEFAULT);
                        $user->image = upload_file("image");
                        $user->gender = $request->get('gender');
                        $user->country = $request->get('country');
                        $user->role = $request->get('role');
                        $user->state=$request->get('state');
                        $user->created_at = date("Y-m-d H:i:s");
                        $user->updated_at = date("Y-m-d H:i:s");
                        $user->save();

                        Session::set('message', "User Added Successfully");
                        redirect(Session::getBackUrl());
                    }
                }
            }
        }else{
            return view('errors/503',['message'=>"You are not allowed to be here!"]);
        }

    }

    public function show($id)
    {
        if(Session::isLogin()){
            try{
                if (Session::isLogin()&&Session::getLoginUser()->id == $id|| Session::isLogin()&&Session::getLoginUser()->role == "admin")
                {
                    $user = User::retrieveByPK($id);
                    return view('admin/users/show', ['user' => $user]);
                }else{
                    redirect('/login');
                }
            }catch (\Exception $e){
                return view('errors/404');
            }
        }else{
            $reqs = UserRequest::all();
            return view('admin/requests/index', ['reqs' => $reqs]);
        }


    }

    public function edit($id)
    {
        try{
            if (Session::isLogin()&&Session::getLoginUser()->id == $id|| Session::isLogin()&&Session::getLoginUser()->role == "admin") {
                $user = User::retrieveByPK($id);
                return view('admin/users/edit', ['user' => $user]);
            } else {
                return view('errors/503',['message'=>"You are not allowed to be here!"]);
            }
        }catch (\Exception $e){
            return view('errors/404');
        }
    }

    public function update(Request $request, $id)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == "admin" || Session::isLogin()&&Session::getLoginUser()->id == $id) {
            if($id ==1 && Session::getLoginUser()->id!=1 )
            {
                return view("errors/503",['message'=>"You are not allowed to do this action!"]);
            }
            $user = User::retrieveByPK($id);
            if (verifyCSRF($request)) {
                $errors = $this->validator->validate($request, [
                    'firstname' => 'required',
                    'lastname' => 'required',
                    'username' => 'required',
                    'email' => 'required|email',
                ]);
                if ($errors) {
                    $request->saveToSession($errors);
                    redirect('/users/'.$user->id, $request->getLastFromSession());
                } else {
                    $user->firstname = $request->get('firstname');
                    $user->lastname = $request->get('lastname');
                    $user->username = $request->get('username');
                    $user->signature = $request->get('signature');
                    $user->email = $request->get('email');
                    if($request->get('password')!=""){
                        $user->password = password_hash($request->get('password'), PASSWORD_DEFAULT);
                    }
                    if ($request->getFile('image')['size'] != 0) {
                        delete_file($user->image);
                        $user->image = upload_file("image");
                    }
                    $user->gender = $request->get('gender');
                    $user->country = $request->get('country');
                    if ($request->get('role'))
                    {
                        $user->role = $request->get('role');
                    }
                    $user->state=$request->get('state');
                    $user->updated_at = date("Y-m-d H:i:s");
                    $user->update();
                    Session::set('message', "User Updated Successfully");
                    redirect(Session::getBackUrl());
                }
            }
        } else {
            return view("errors/503",['message'=>"You are not allowed to be here!"]);
        }

    }

    public function destroy($id)
    {
        if (Session::isLogin()&&Session::getLoginUser()->role == "admin"&&$id!=1) {
            $user = User::retrieveByPK($id);
            delete_file($user->image);
            foreach ($user->requests() as $req){
                $req->delete();
            }
            $user->delete();
            Session::set('message', "User Deleted Successfully");
            redirect('/users');
        }else{
            return view("errors/503",['message'=>"You are not allowed to do this action!"]);
        }
    }
}