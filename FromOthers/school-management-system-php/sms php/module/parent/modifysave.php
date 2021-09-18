<?php  
include_once('main.php');
$password=$_REQUEST['password'];
$mod = "UPDATE parents,users SET parents.password='$password',users.password = '$password' WHERE parents.id = users.userid AND parents.id ='$check'";
$resmon = mysql_query($mod);
header('location:modify.php');
?>
