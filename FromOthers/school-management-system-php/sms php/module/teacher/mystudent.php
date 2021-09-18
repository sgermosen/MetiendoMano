<?php  
include_once('main.php');
 $em = $_REQUEST['id'];


$courseinfo = "SELECT * FROM students WHERE id in (select DISTINCT studentid from course  where id='$em' and teacherid='$check')";
$resc = mysql_query($courseinfo);


while($r=mysql_fetch_array($resc))
{
 echo '<option value="',$r['id'],'" >',$r['name'],'</option>';

}


?>
