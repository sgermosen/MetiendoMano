<?php
include_once('mysqlcon.php');
$myid=$_POST['myid'];
$mypassword=$_POST['mypassword'];
$myid = stripslashes($myid);
$mypassword = stripslashes($mypassword);
$myid = mysql_real_escape_string($myid);
$mypassword = mysql_real_escape_string($mypassword);
$_SESSION['login_id']=$myid;
$sql="SELECT usertype FROM users WHERE userid='$myid' and password='$mypassword'";
$result=mysql_query($sql);
$count=mysql_num_rows($result);
$type=mysql_fetch_array($result);
$control=$type['usertype'];
//echo $control;

if($count!=1 || !isset($control)){
    header("Location:../index.php?login=false");
}
else if($count==1 && $control=="admin"){
    header("Location:../module/admin");
}
else if($count==1 && $control=="teacher"){
    header("Location:../module/teacher");
}

else if($count==1 && $control=="student"){
    header("Location:../module/student");
}
else if($count==1 && $control=="staff"){
    header("Location:../module/staff");
}
else if($count==1 && $control=="parent"){
	 
    header("Location:../module/parent");
}
else {
    header("Location:../index.php?login=false");
}
?>
