<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
if($_POST['submit']){
    $id = $_POST['id'];
    $cdate = date("Y-m-d");
    $sql = "INSERT INTO attendance VALUES('','$cdate','$id')";
    $success = mysql_query($sql);
    if(!$success) {
        die('Attendance Error: '.mysql_error());
    }
    echo "Attendance Complete\n";
    header("Location:../admin/staffAttendance.php");
}
?>
