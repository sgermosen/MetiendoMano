<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];

$string = "";
$sql = "SELECT * FROM teachers WHERE id like '$searchKey%' OR name like '$searchKey%';";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= '<tr><td>'.$row['id'].'</td><td>'.$row['name'].
    '</td><td>'.$row['phone'].'</td><td>'.$row['email'].
    '</td><td>'.$row['address'].'</td><td>'.$row['sex'].'</td><td>'.$row['dob'].
    '</td><td>'.$row['hiredate'].'</td><td>'.$row['salary'].'</td></tr>';
}
echo $string;
?>
