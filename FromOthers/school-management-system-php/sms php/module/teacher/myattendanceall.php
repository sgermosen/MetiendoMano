<?php  
include_once('main.php');
$attendmon = "SELECT DISTINCT(date) FROM attendance WHERE attendedid='$check'";
$resmon = mysql_query($attendmon);
echo "<tr> <th>Attend All Dates:</th></tr>";
while($r=mysql_fetch_array($resmon))
{
 echo "<tr><<td>",$r['date'],"</td></tr>";

}
?>
