<?php  
include_once('main.php');
$sid=$_REQUEST['id'];
$attendmon = "SELECT DISTINCT (date) FROM attendance WHERE  MONTH( DATE ) = MONTH( CURRENT_DATE ) and YEAR( DATE )=YEAR( CURRENT_DATE )  and date not in (select DISTINCT(date) from attendance where attendedid='$sid' and MONTH( DATE ) = MONTH( CURRENT_DATE ) and YEAR( DATE )=YEAR( CURRENT_DATE ))";
$resmon = mysql_query($attendmon);
echo "<tr> <th>Absent Date:</th></tr>";
while($r=mysql_fetch_array($resmon))
{
 echo "<tr><td>",$r['date'],"</td></tr>";

}
?>
