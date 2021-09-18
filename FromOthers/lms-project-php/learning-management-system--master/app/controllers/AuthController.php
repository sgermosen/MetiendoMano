<?php

namespace App\Controllers;

use App\Core\Controller;
use App\Core\DB\ORM;
use App\Core\Helper;
use App\Core\Request;
use App\Core\Session;
use App\Models\User;
use Facebook\Exceptions\FacebookResponseException;
use Facebook\Exceptions\FacebookSDKException;

class AuthController extends Controller
{

    public function showlogin()
    {
        if (Session::isLogin()) {
            if (Session::isLogin()&&Session::getLoginUser()->role == "admin")
                redirect('/users');
            else
                redirect('/users/'.Session::getLoginUser()->id);
        }
        else
        return view('auth/login');

    }
    public function admin()
    {
        if(Session::isLogin()&&Session::getLoginUser()->role=='admin'){
            return view('auth/admin');
        }else{
            return view('errors/503',['message'=>'Your not allowed to be here!']);
        }

    }

    public function login(Request $request)
    {
        if (verifyCSRF($request)) {
            $errors = $this->validator->validate($request, [
                'email' => 'required|email',
                'password' => 'required|min:8'
            ]);
            if(!$errors){
                $user = User::retrieveByEmail($request->get('email'))[0];
                if ($request->get('email') == $user->email && password_verify($request->get('password'), $user->password)) {
                    if($user->state=="baned"){
                        Session::set('error',"you have been baned from login !!!");
                        redirect('/login', $request->getLastFromSession());
                    }
                    if ($user->state != "active")
                    {
                        Session::set('error',"Your account not active <br/>please go to your mail to verify you account");
                        redirect('/login', $request->getLastFromSession());

                    }

                    Session::saveLogin($user->username, $user->role, $user->password);
                    if($request->get('remember')){
                        Session::rememberLogin($user->username, $user->role, $user->password);
                    }
                }else{
                    Session::set('error',"Your account is Suspended <br/>please call us to see your issue");
                    redirect('/login', $request->getLastFromSession());
                }
            }
            if ($errors) {
                $request->saveToSession($errors);
                redirect('/login', $request->getLastFromSession());
            } else {
                redirect('/');
            }
        } else {
           return view('errors/503',['message','You not allowed to do this action']);
        }
    }

    public function logout()
    {
        Session::destroy();
        Session::forgetLogin();
        redirect('/');
    }

    public function showregister(Request $request)
    {
        if (Session::isLogin()) {
            redirect('/');
        }
        return view('auth/register');
    }

    public function register(Request $request)
    {
        if (verifyCSRF($request)) {
            $errors = $this->validator->validate($request, [
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
                redirect('/register', $request->getLastFromSession());
            } else {
                if ($errors) {
                    $request->saveToSession($errors);
                    redirect('/register', $request->getLastFromSession());
                } else {
                    $user = new User();
                    $user->username = $request->get('username');
                    $user->email = $request->get('email');
                    $user->code = md5(mt_rand());
                    $user->password = password_hash($request->get('password'), PASSWORD_DEFAULT);
                    $user->created_at = date("Y-m-d H:i:s");
                    $user->updated_at = date("Y-m-d H:i:s");
                    $user->save();
                    $id = $user->getLastInserted();
                    Session::set('message',"Your account created <br/> we sent confimation message to your registration mail <br> please go to your mail to verify you account");
                    $subject = "Welcome TO Our Learning Platform";
                    $body = 'Hi <b>'.$user->username.'</b> <br/>
                             Your login Information is:<br/>
                             <h3>email: <b>'.$user->email.'</b> </h3>
                             <h3>user : <b>'.$user->username.'</b> </h3>
                             <h3>creation date : <b>'.$user->created_at.'</b></h3>
                             actaivation Link
                             <a href="https://opensourcelms.herokuapp.com/activation?id='.$id.'&code='.$user->code.'&mail='.$user->email.'">click here to activate your acount</a><br/>
                             this link for one use only
                            ';
                    sendMail($user->email,$user->username,$subject,$body);
                    redirect("/login");
                }
            }
        }
    }

    public function  activeIt(Request $request)
    {

        $user = User::retrieveByEmail($request->get('mail'))[0];
        if ($user->id == $request->get('id') && $user->code == $request->get('code') &&  $user->email == $request->get('mail'))
        {
            $user->code = null;
            $user->state = "active";
            $user->role="student";
            $user->online = 0;
            $user->update();
            Session::saveLogin($user->username, $user->role, $user->password);
            Session::set('message', "Your acount activated successfully <br/> please update your acount info");
            redirect('/users/'.$user->id);

        }
        else{
            redirect('/login');
        }
    }

    public function fbLogin(Request $request)
    {
        $fb = getFacebookObj();
        $helper = $fb->getRedirectLoginHelper();
        try {
            $accessToken = $helper->getAccessToken();
        } catch(FacebookResponseException $e) {
            // When Graph returns an error
            echo 'Graph returned an error: ' . $e->getMessage();
            exit;
        } catch(FacebookSDKException $e) {
            // When validation fails or other local issues
            echo 'Facebook SDK returned an error: ' . $e->getMessage();
            exit;
        }

        if (! isset($accessToken)) {
            if ($helper->getError()) {
                header('HTTP/1.0 401 Unauthorized');
                echo "Error: " . $helper->getError() . "\n";
                echo "Error Code: " . $helper->getErrorCode() . "\n";
                echo "Error Reason: " . $helper->getErrorReason() . "\n";
                echo "Error Description: " . $helper->getErrorDescription() . "\n";
            } else {
                header('HTTP/1.0 400 Bad Request');
                echo 'Bad request';
            }
            exit;
        }

        $oAuth2Client = $fb->getOAuth2Client();
        $response = $fb->get('/me?fields=id,name,email,picture', $accessToken);
        $fbUser = $response->getGraphUser();
        $user = User::retrieveByEmail($fbUser['email'])[0];
        if ($user->email)
        {
            Session::saveLogin($user->username, $user->role, $user->password);
            redirect('/');
        }
        else{

            $user = new User();
            $user->username = $fbUser['name'];
            $user->email = $fbUser['email'];
            $user->code = null;
            $user->state = "active";
            $user->image = $fbUser['picture']['url'];
            $user->role="student";
            $user->online = 0;
            $user->password = password_hash($fbUser['email'], PASSWORD_DEFAULT);
            $user->created_at = date("Y-m-d H:i:s");
            $user->updated_at = date("Y-m-d H:i:s");
            $subject = "Welcome TO Our Learning Platform";
            $body = 'Hi <b>'.$user->username.'</b> <br/>
                             Your login Information is:<br/>
                             <h3>email: <b>'.$fbUser['email'].'</b> </h3>
                             <h3>user : <b>'.$fbUser['name'].'</b> </h3>
                             <h3>password : <b> is your email please change it immediately</b> </h3>
                             <h3>creation date : <b>'.$user->created_at.'</b></h3>
                            ';
            sendMail($user->email,$user->username,$subject,$body);
            Session::set('message', "Your acount activated successfully <br/> please check your email now and update your acount info");
            Session::saveLogin($user->username, $user->role, $user->password);
            $user->save();
            redirect('/users/'.$user->getLastInserted());

        }


    }

    function gmLogin()
    {
        $client_id = '60269544916-gvohmgl6dcudacgjevh1vdffhja86usi.apps.googleusercontent.com';
        $client_secret = '5aogBhoJNqOwU_1kyuNyHYlt';
        $redirect_uri = 'https://opensourcelms.herokuapp.com/gmlogin?';
        $client = new \Google_Client();
        $client->setClientId($client_id);
        $client->setClientSecret($client_secret);
        $client->setRedirectUri($redirect_uri);
        $service = new \Google_Service_Oauth2($client);
        if (isset($_GET['code'])) {
            $client->authenticate($_GET['code']);
            $_SESSION['access_token'] = $client->getAccessToken();
            header('Location: ' . filter_var($redirect_uri, FILTER_SANITIZE_URL));
            exit;
        }
        if (isset($_SESSION['access_token']) && $_SESSION['access_token']) {
            $client->setAccessToken($_SESSION['access_token']);
        }
        $gmUser = $service->userinfo->get(); //get user info
        $user = User::retrieveByEmail($gmUser->email)[0];
        if ($user->email)
        {
            Session::saveLogin($user->username, $user->role, $user->password);
            redirect('/');
        }
        else{
            $user = new User();
            $user->username = $gmUser->name;
            $user->image = $gmUser->picture;
            $user->email = $gmUser->email;
            $user->code = null;
            $user->state = "active";
            $user->role="student";
            $user->online = 0;
            $user->password = password_hash($gmUser->email, PASSWORD_DEFAULT);
            $user->created_at = date("Y-m-d H:i:s");
            $user->updated_at = date("Y-m-d H:i:s");
            $subject = "Welcome TO Our Learning Platform";
            $body = 'Hi <b>'.$user->username.'</b> <br/>
                             Your login Information is:<br/>
                             <h3>email: <b>'.$user->email.'</b> </h3>
                             <h3>user : <b>'.$user->username.'</b> </h3>
                             <h3>password : <b> is your email please change it immediately</b> </h3>
                             <h3>creation date : <b>'.$user->created_at.'</b></h3>
                            ';
            sendMail($user->email,$user->username,$subject,$body);
            Session::set('message', "Your acount activated successfully <br/> please check your email now and update your acount info");
            Session::saveLogin($user->username, $user->role, $user->password);
            $user->save();
            redirect('/users/'.$user->getLastInserted());

        }
    }


    public function showreset()
    {
        return view('auth/passwords/email');
    }

    public function resetemail(Request $request)
    {
        $errors=$this->validator->validate($request,['email'=>'required']);
        if($errors){
            Session::set('error','none valid data');
            $request->saveToSession($errors);
            redirect('/reset', $request->getLastFromSession());
        }else{
            if(empty(User::retrieveByField('email',$request->get('email')))){
                Session::set('error','Your are not registered on our website');
                redirect('/reset');
            }else{
                $user=User::retrieveByField('email',$request->get('email'))[0];
                $code=md5(mt_rand());
                $user->code = $code;
                $user->update();
                $subject = "Open Source LMS Password Reset";
                $body = 'Hi <b>'.$user->username.'</b> <br/>
                             Your can reset your password from this link<br/>
                             <h3>email: <b>'.$user->email.'</b> </h3>
                             <h3>user : <b>'.$user->username.'</b> </h3>
                             <h3>creation date : <b>'.$user->created_at.'</b></h3>
                             <a href="https://opensourcelms.herokuapp.com/resetpass?code='.$code.'&email='.$user->email.'">click here to reset your password</a><br/>
                             this link for one use only';
                sendMail($user->email,$user->username,$subject,$body);
                return view('auth/passwords/sent');
                redirect('/');
            }
        }

    }

    public function resetpass(Request $request)
    {
        if($request->get('email')){
            $user=User::retrieveByField('email',$request->get('email'))[0];
            if($request->get('code')==$user->code){
                return view('auth/passwords/reset',['user'=>$user]);
            }else{
                Session::set('error','Sorry this link is expired!! try new one');
                redirect('/login');
            }
        }else{
            return view('errors/404');
        }

    }

    public function changepass(Request $request)
    {
        if(verifyCSRF($request)){
            if($request->get('password')==$request->get('confirm')){
                $user=User::retrieveByEmail($request->get('email'))[0];
                if($request->get('code')==$user->code){
                    $user->password=password_hash($request->get('password'),PASSWORD_DEFAULT);
                    $user->code="";
                    $user->update();
                    Session::set('message','You can now login with the new password');
                    redirect('/login');
                }else{
                    Session::set('error','Somthing Wrong happened Please try agin');
                    redirect(Session::getBackUrl());
                }
            }else{
                Session::set('error','password not match');
                redirect(Session::getBackUrl());

            }

        }else{
            return view('errors/404');
        }

    }
}