<!DOCTYPE html>
<!--
This is a starter template page. Use this page to start your new project from
scratch. This page gets rid of all links and provides the needed markup only.
-->
<html>
<head>
    <meta charset="utf-8">
    <link rel="shortcut icon" href="<?=asset('favicon.ico')?>" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title><?php echo $title?></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.5 -->
    <?php resource('css','bootstrap.min')?>
    <!-- Font Awesome -->
    <?php resource('css','font-awesome.min')?>
    <!-- Ionicons -->
    <?php resource('css','ionicons.min')?>
    <!-- Theme style -->
    <?php resource('css','AdminLTE')?>
    <!-- AdminLTE Skins. We have chosen the skin-blue for this starter
          page. However, you can choose any other skin. Make sure you
          apply the skin class to the body tag so the changes take effect.
    -->
    <?php resource('css','skins/skin-blue.min')?>
    <link rel="stylesheet" href="<?php asset('js/plugins/datatables/dataTables.bootstrap.css')?>">
    <link rel="stylesheet" href="<?php asset('js/plugins/iCheck/square/blue.css')?>">
    <?php partial('admin/froala/styles')?>

</head>
<!--
BODY TAG OPTIONS:
=================
Apply one or more of the following classes to get the
desired effect
|---------------------------------------------------------|
| SKINS         | skin-blue                               |
|               | skin-black                              |
|               | skin-purple                             |
|               | skin-yellow                             |
|               | skin-red                                |
|               | skin-green                              |
|---------------------------------------------------------|
|LAYOUT OPTIONS | fixed                                   |
|               | layout-boxed                            |
|               | layout-top-nav                          |
|               | sidebar-collapse                        |
|               | sidebar-mini                            |
|---------------------------------------------------------|
-->
<body class="hold-transition skin-blue sidebar-collapse sidebar-mini" style="height: 100%">
<div class="wrapper">

    <!-- Main Header -->
    <?php partial('admin/headernav');?>
    <!-- Left side column. contains the logo and sidebar -->
    <?php partial('admin/sidebar');?>

    <!-- Content Wrapper. Contains page content -->
    <div class="content-wrapper">

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
