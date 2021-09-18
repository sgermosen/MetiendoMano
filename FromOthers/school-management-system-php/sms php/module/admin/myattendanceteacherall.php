<?php  
include_once('main.php');
$sid=$_REQUEST['id'];
$attendmon = "SELECT DISTINCT(date) FROM attendance WHERE attendedid='$sid'";
$resmon = mysql_query($attendmon);
echo "<tr> <th>Attend All Dates:</th></tr>";
while($r=mysql_fetch_array($resmon))
{
 echo "<tr><<td>",$r['date'],"</td></tr>";

}
?>
