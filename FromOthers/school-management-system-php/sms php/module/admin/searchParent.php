<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];
$string = "<tr>
    <th>ID</th>
    <th>Password</th>
    <th>Father Name</th>
    <th>Mother Name</th>
    <th>Father Phone</th>
    <th>Mother Phone</th>
    <th>Address</th>
</tr>";
$sql = "SELECT * FROM parents WHERE id like '$searchKey%' OR fathername like '$searchKey%' OR mothername like '$searchKey%'";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= '<tr><td>'.$row['id'].'</td><td>'.$row['password'].
    '</td><td>'.$row['fathername'].'</td><td>'.$row['mothername'].
    '</td><td>'.$row['fatherphone'].'</td><td>'.$row['motherphone'].
    '</td><td>'.$row['address'].'</td></tr>';
}
echo $string;
?>
