<?php 
include('index.php');
include_once('../../service/mysqlcon.php');
$mod = "SELECT distinct cn.name,cn.id, ex.examdate ,ex.time,c.name FROM course cn,examschedule ex,class c WHERE cn.id=ex.courseid and cn.classid=c.id";
$res = mysql_query($mod);
while($row = mysql_fetch_array($res))
{ 
	echo "<table align='center' border='2'>";
	echo "<tr>";
	echo "<th>ID</th> <th>NAME</th> <th>DATE</th> <th>TIME</th> <th>CLASS</th> <br />";
	echo "</tr> <tr>";
	echo "<td>".$row[1]."</td><td>".$row[0]."</td><td> ".$row[2]."</td><td> ".$row[3]."</td><td>".$row[4]."</td>";
	echo "</tr>";
	echo"</table>";
}
?>
