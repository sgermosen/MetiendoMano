<?php
include_once('main.php');

    $id = $_REQUEST['id'];
    $sg= "DELETE FROM payment WHERE id = '$id'";
    $success = mysql_query($sg);

    header("Location:deletePayment.php");

?>
