<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];
$string = "<tr>
    <th>ID</th>
    <th>Name</th>
    <th>Salary</th>
    <th>Payable Salary</th>
    </tr>";
$sql = "SELECT s.id,s.name,s.salary,ROUND(s.salary*count(a.date)/300) AS currentmonthlysalary FROM staff s,attendance a WHERE s.id=a.attendedid AND s.id like '$searchKey%' AND MONTH(a.date)=(SELECT month(curdate()) FROM dual) GROUP BY a.attendedid";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= "<tr><td><input value='".$row['id']."'name='id' readonly>".
    "</td><td><input type='text' value='".$row['name']."'name='name' readonly>".
    "</td><td><input type='text' value='".$row['salary']."'name='salary'>".
    "</td><td>".$row['currentmonthlysalary']."</td></tr>";
}
echo $string."<input type='submit' name='submit'value='Submit'>";
?>
