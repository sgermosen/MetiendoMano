<?php
session_start();
$host="localhost";
$username="root";
$password="";
$db_name="sms";
$link=mysql_connect("$host", "$username", "$password")or die("Cannot Connect");
mysql_select_db("$db_name")or die("Cannot Select DB");
?>
