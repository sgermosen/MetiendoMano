<?php  
include_once('main.php');
$attendmon = "SELECT DISTINCT(date) FROM attendance WHERE attendedid='$check' and  MONTH( DATE ) = MONTH( CURRENT_DATE ) and YEAR( DATE )=YEAR( CURRENT_DATE )";
$resmon = mysql_query($attendmon);
echo "<tr> <<th>Attend Current Month Date:</th></tr>";
while($r=mysql_fetch_array($resmon))
{
 echo "<tr><td>",$r['date'],"</td></tr>";

}
?>
