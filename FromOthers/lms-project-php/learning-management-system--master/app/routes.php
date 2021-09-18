<?php
$router->resource('users', 'UserController');
$router->resource('courses', 'CoursesController');
$router->resource('cats', 'CategoryController');
$router->resource('requests', 'RequestController');
$router->resource('comments', 'CommentController');
$router->resource('materials', 'MaterialController');
$router->resource('materials/download', 'MaterialController@download');
//Pages routes
$router->get('', 'PagesController@home');
$router->get('about', 'PagesController@about');
$router->get('contact', 'PagesController@contact');
$router->get('admin', 'PagesController@admin');
$router->get('search', 'PagesController@search');
$router->get('rss', 'PagesController@rss');

//Authentication routes
$router->get('login', 'AuthController@showlogin');
$router->get('register', 'AuthController@showregister');
$router->post('login', 'AuthController@login');
$router->get('logout', 'AuthController@logout');
$router->post('register', 'AuthController@register');
$router->get('reset','AuthController@showreset');
$router->get('resetpass','AuthController@resetpass');
$router->post('changepass','AuthController@changepass');
$router->post('resetemail','AuthController@resetemail');

$router->get('activation', 'AuthController@activeIt');
$router->get('fblogin', 'AuthController@fbLogin');
$router->get('gmlogin', 'AuthController@gmLogin');



$router->get('image_load', 'AjaxController@image_load');
$router->post('delete_image', 'AjaxController@delete_image');
$router->post('file_upload', 'AjaxController@file_upload');
$router->post('delete_file', 'AjaxController@delete_file');
$router->post('image_upload', 'AjaxController@image_upload');
$router->post('request/send', 'RequestController@send');