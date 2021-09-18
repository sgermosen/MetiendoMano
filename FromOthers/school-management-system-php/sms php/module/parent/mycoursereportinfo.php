<?php  
include_once('main.php');
 $id = $_REQUEST['id'];
 $childid = $_REQUEST['childid'];

$courseinfo = "SELECT * FROM report WHERE courseid='$id' and studentid='$childid'";
$resc = mysql_query($courseinfo);

echo "<tr> <th>Report Message</th> </tr>";
while($r=mysql_fetch_array($resc))
{
 echo "<tr> <td>",$r['message'],"<td></tr>";

}


?>
