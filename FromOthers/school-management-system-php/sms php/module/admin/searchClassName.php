<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$string = "<option>SELECT AN OPTION</option>";
$sql = "SELECT * FROM class";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= "<option value='".$row['id']."'>".$row['name']." [".$row['section']."]</option>";
}
echo $string;
?>
