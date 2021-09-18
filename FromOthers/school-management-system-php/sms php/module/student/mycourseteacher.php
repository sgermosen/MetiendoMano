<?php  
include_once('main.php');
 $em = $_REQUEST['cid'];


$courseinfo = "SELECT * FROM teachers WHERE id in (select teacherid from course where id='$em' and studentid='$check')";
$rescou = mysql_query($courseinfo);
$courseid = "SELECT * FROM class WHERE id in (select classid from course where id='$em' and studentid='$check')";
$rescoud = mysql_query($courseid);
$st=mysql_fetch_array($rescoud);

while($rn=mysql_fetch_array($rescou))
{
 echo "Teacher ID: ",$rn['id'],"<br/>";
 echo "Teacher Name: ",$rn['name'],"<br/>";
 echo "Teacher Email: ",$rn['email'],"<br/>";
  echo " Your Section : ",$st['section'],"<br/>";
  echo " Your Class Room : ",$st['room'],"<br/>";
 

}


?>
