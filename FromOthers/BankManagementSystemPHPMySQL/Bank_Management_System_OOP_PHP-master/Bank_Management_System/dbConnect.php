<?php   
   define('DB_SERVER','localhost');
   define('DB_USER','root');
   define('DB_PASS' ,'');
   define('DB_NAME', 'oopbank');

   $conn = mysql_connect(DB_SERVER,DB_USER,DB_PASS) or die('localhost connection problem'.mysql_error());
		mysql_select_db(DB_NAME, $conn);

?>   