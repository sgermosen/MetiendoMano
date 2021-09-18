<html>
<?php
$request=\App\Core\Session::get('request');
$errors=$request['errors'];
$fields=$request['fields'];
?>

<head>
    <meta charset="utf-8">
    <link rel="shortcut icon" href="<?=asset('favicon.ico')?>" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Open Source LMS| Reset Password</title>
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
<div class="login-box">
    <div class="login-logo">
        <a href="/">
            <img src="<?php asset('img/logo.png')?>" alt="" width="100px"><br>
            <b>Open</b>Source
        </a>
    </div>
    <!-- /.login-logo -->
    <div class="login-box-body">
        <p class="login-box-msg">We sent you password reset email..</p>
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

</body>
</html>
