<html>
<?php
$request=\App\Core\Session::get('request');
$errors=$request['errors'];
$fields=$request['fields'];
//////////////generate facebook link//////////////////
$helper = getFacebookObj()->getRedirectLoginHelper();
$permissions = ['email']; // Optional permissions
$loginUrl = $helper->getLoginUrl('https://opensourcelms.herokuapp.com/fblogin', $permissions);

//////////////////////////////////////////////////////gmail///////////////////////
$client  = getGmailObj();
$authUrl = $client->createAuthUrl();
?>

<head>
    <meta charset="utf-8">
    <link rel="shortcut icon" href="<?=asset('favicon.ico')?>" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Open Source LMS| Log in</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="<?php asset('css/bootstrap.min.css')?>">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="<?php asset('css/font-awesome.min.css')?>">
    <!-- Ionicons -->
    <link rel="stylesheet" href="<?php asset('css/ionicons.min.css')?>">
    <!-- Theme style -->
    <link rel="stylesheet" href="<?php asset('css/AdminLTE.min.css')?>">
    <!-- iCheck -->
    <link rel="stylesheet" href="<?php asset('js/plugins/iCheck/square/blue.css')?>">

</head>
<body class="hold-transition login-page" style="background: url('../../../public/img/loginback.jpg') no-repeat;background-size: cover ">
<?php if(\App\Core\Session::has('message')):?>
    <div class="alert alert-success alert-dismissible">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-check"></i> Alert!</h4>
        <p><?= \App\Core\Session::get('message')?></p>
        <?php \App\Core\Session::delete('message')?>
    </div>
<?php endif;?>
<?php if(\App\Core\Session::has('error')):?>
    <div class="alert alert-danger alert-dismissible">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-check"></i> Alert!</h4>
        <p><?= \App\Core\Session::get('error')?></p>
        <?php \App\Core\Session::delete('error')?>
    </div>
<?php endif;?>
<?php if(\App\Core\Session::has('urlerror')):?>
    <div class="alert alert-danger alert-dismissible">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
        <h4><i class="icon fa fa-check"></i> Alert!</h4>
        <p><?= \App\Core\Session::get('urlerror')?></p>
        <?php \App\Core\Session::delete('urlerror')?>
        <i class="icon fa fa-arrow-circle-o-right"></i><a href="<?php \App\Core\Session::get('url')?>" class="btn-link">Go here</a>
    </div>
<?php endif;?>
<div class="login-box">
    <div class="login-logo">
        <a href="/">
            <img src="<?php asset('img/logo.png')?>" alt="" width="100px"><br>
            <b>Open</b>Source
        </a>
    </div>
    <!-- /.login-logo -->
    <div class="login-box-body">
        <p class="login-box-msg">Sign in to start your session</p>

        <form method="POST" action="/login">
            <?php csrf_field()?>
            <div class="form-group has-feedback<?= isset($errors['email']) ? ' has-error' : '' ?>">
                <input type="email" class="form-control" placeholder="Email" value="<?= $fields['email'] ?>" name="email" >
                <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                <?php if(count($errors)>0):?>
                <span class="help-block">
                        <strong><?= $errors['email'][0] ?></strong>
                </span>
                <?php endif;?>
            </div>
            <div class="form-group has-feedback<?= isset($errors['email']) ? ' has-error' : '' ?>">
                <input type="password" class="form-control" placeholder="Password" name="password">
                <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                <?php if(count($errors)>0):?>
                    <span class="help-block">
                        <strong><?= $errors['password'][0] ?></strong>
                    </span>
                <?php endif;?>
            </div>
            <div class="row">
                <div class="col-xs-8">
                    <div class="checkbox icheck">
                        <label>
                            <div class="icheckbox_square-blue" aria-checked="false" aria-disabled="false" style="position: relative;"><input type="checkbox" name="remember" style="position: absolute; top: -20%; left: -20%; display: block; width: 140%; height: 140%; margin: 0px; padding: 0px; border: 0px; opacity: 0; background: rgb(255, 255, 255);"><ins class="iCheck-helper" style="position: absolute; top: -20%; left: -20%; display: block; width: 140%; height: 140%; margin: 0px; padding: 0px; border: 0px; opacity: 0; background: rgb(255, 255, 255);"></ins></div> Remember Me
                        </label>
                    </div>
                </div>
                <!-- /.col -->
                <div class="col-xs-4">
                    <button type="submit" class="btn btn-primary btn-block btn-flat">Sign In</button>
                </div>
                <!-- /.col -->
            </div>
        </form>

        <div class="social-auth-links text-center">
            <p>- OR -</p>
            <a href="<?php echo htmlspecialchars($loginUrl)?>" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign in using Facebook</a>
            <a href="<?php echo $authUrl;?>" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i> Sign in using Google+</a>
        </div>
        <!-- /.social-auth-links -->

        <a href="/reset">I forgot my password</a><br>
        <a href="/register" class="text-center">Register a new membership</a>

    </div>
    <!-- /.login-box-body -->
</div>
<!-- /.login-box -->

<!-- jQuery 2.2.0 -->
<script src="<?php asset('js/jquery-3.1.1.min.js')?>"></script>
<!-- Bootstrap 3.3.5 -->
<script src="<?php asset('js/bootstrap.min.js')?>"></script>
<!-- iCheck -->
<script src="<?php asset('js/plugins/iCheck/icheck.min.js')?>"></script>
<script>
    $(function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    });
</script>
</body>
</html>
