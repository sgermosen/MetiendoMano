<?php
   define('DB_SERVER','localhost');
   define('DB_USER','root');
   define('DB_PASS' ,'');
   define('DB_NAME', 'demobank');

   $conn = mysql_connect(DB_SERVER,DB_USER,DB_PASS) or die('localhost connection problem'.mysql_error());
		mysql_select_db(DB_NAME, $conn);	

    $res = mysql_query("insert into so set name = 'Asif'");
    if ($res) {
    	echo "Success!";
    } else {
    	echo "Error!";
    }

    $sql = mysql_query("select * from so");
?>
<table>
	<tr>
		<th>ID</th>
	</tr>
    
    <?php
       while ($row=mysql_fetch_array($sql)) { ?>
       	 <tr>
       	 	<td><?php echo $row[0]; $x = $row[0]; ?></td>
       	 </tr>
      <?php 
        }  ?>
    

</table>
<?php
$sql2 = mysql_query("select concat('AC-', id) as id from so where id = '$x'");
$row2 = mysql_fetch_row($sql2);


echo "<br><br>result: " . $row2[0];
$y = $row2[0];

$res3 = mysql_query("insert into yourtable(IDNUMBER, ENAME) values('$y', 'Samy')");
    if ($res3) {
      echo "Success!";
    } else {
      echo "Error!";
    }

?>