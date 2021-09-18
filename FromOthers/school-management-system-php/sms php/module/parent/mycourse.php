<?php  
include_once('main.php');
 $childid = $_REQUEST['childid'];
 $classid= $_REQUEST['classid'];


$courses = "SELECT * FROM course WHERE classid='$classid' and studentid='$childid'";
$rescourse = mysql_query($courses);

while($r=mysql_fetch_array($rescourse))
{
 echo '<option value="',$r['id'],'" >',$r['name'],'</option>';

}


?>
