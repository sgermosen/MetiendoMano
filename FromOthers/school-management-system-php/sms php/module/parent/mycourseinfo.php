<?php  
include_once('main.php');
 $id = $_REQUEST['id'];
 $childid = $_REQUEST['childid'];

$courseinfo = "SELECT * FROM grade WHERE courseid='$id' and studentid='$childid'";
$resc = mysql_query($courseinfo);

echo "<tr> <th>Grade</th> </tr>";
while($r=mysql_fetch_array($resc))
{
 echo "<tr> <td>",$r['grade'],"<td></tr>";

}


?>
