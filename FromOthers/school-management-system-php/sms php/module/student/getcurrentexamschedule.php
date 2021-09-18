<?php  
include_once('main.php');
 $em = $_REQUEST['curid'];


$courseincurr = "SELECT * FROM examschedule WHERE courseid='$em' ";
$resc = mysql_query($courseincurr);

echo "<tr> <th>Exam Date:</th><th>Exam Time:</th></tr>";
while($r=mysql_fetch_array($resc))
{
 echo "<tr><td>",$r['examdate'],"</td><td>",$r['time'],"</td></tr>";

}


?>
