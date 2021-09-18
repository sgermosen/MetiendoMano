<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];
$string = "<option>SELECT AN OPTION</option>";
$sql="SELECT * FROM teachers WHERE id = (SELECT teacherid FROM takencoursebyteacher WHERE courseid = '$searchKey')";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    echo $row['id'].'<br/>';
    $string .= "<option value='".$row['id']."'>".$row['name']."</option>";
}
echo $string;
?>
