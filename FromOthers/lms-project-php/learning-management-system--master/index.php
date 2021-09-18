<?php
use App\Core\{Router,Request};
require 'vendor/autoload.php';
require 'core/bootstrap.php';
Router::load('app/routes.php')->direct(new Request);
