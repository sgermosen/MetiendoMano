<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];
$string = "<tr>
    <th>ID</th>
    <th>Name</th>
    <th>Teacher ID</th>
    <th>Student ID</th>
    <th>Class ID</th>
</tr>";
$sql = "SELECT * FROM course WHERE id like '$searchKey%' OR name like '$searchKey%' OR teacherid like '$searchKey%' OR classid = '$searchKey' OR studentid like '$searchKey%'";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= '<tr><td>'.$row['id'].'</td><td>'.$row['name'].
    '</td><td>'.$row['teacherid'].'</td><td>'.$row['studentid'].
    '</td><td>'.$row['classid'].'</td></tr>';
}
echo $string;
?>
