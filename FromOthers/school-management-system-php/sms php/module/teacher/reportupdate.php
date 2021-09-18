<?php

include_once('main.php');
include_once('index.php');

if(!empty($_POST['submit'])){
$stid=$_REQUEST['mystudent'];
$crsid=$_REQUEST['mycourse'];
$rprt=$_REQUEST['report'];

$sql="insert into report (studentid,teacherid,message,courseid) values ('$stid','$check','$rprt','$crsid')";
//echo $sql;
//echo $sql;
$s=mysql_query($sql);
if(!$s)
{
echo "problem ";
}
echo "report  posted";
}


?>
