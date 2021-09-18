<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];
echo $searchKey;
$ar = preg_split('/(?<=[0-9])(?=[a-z]+)/i',$searchKey);
$string = "<option>SELECT AN OPTION</option>";
$sql = "SELECT * FROM availablecourse WHERE classid = '$ar[0]'";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= "<option value='".$row['id']."'>".$row['name']."</option>";
}
echo $string;
?>
