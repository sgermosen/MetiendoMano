<?php
include ('../../service/mysqlcon.php');
session_start();
session_destroy();
mysql_close($link);

header("Location: ../../");
?>