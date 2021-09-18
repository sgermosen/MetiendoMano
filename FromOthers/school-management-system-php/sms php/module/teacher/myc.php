<?php  
include_once('main.php');
 $emn = $_REQUEST['classid'];


$courses = "SELECT distinct id , name FROM course WHERE classid='$emn' and teacherid='$check'";
$rescourse = mysql_query($courses);

while($r=mysql_fetch_array($rescourse))
{
 echo $r['name'];

}


?>
