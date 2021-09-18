<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$string = "<tr>
    <th>ID</th>
    <th>Student Id</th>
    <th>Amount</th>
    <th>Month</th>
    <th>Year</th>
    </tr>";
$sql = "SELECT * FROM payment WHERE month = MONTH(curdate()) AND year = YEAR(curdate())";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= "<tr><td>".$row['id']."</td><td>".$row['studentid']."</td><td>".$row['amount'].
    "</td><td>".$row['month']."</td><td>".$row['year']."</td></tr>";
}
?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
				<script src = "JS/login_logout.js"></script>
		</head>
    <body>
			  <div class="header"><h1>School Management System</h1></div>
			  <div class="divtopcorner">
				    <img src="../../source/logo.jpg" height="150" width="150" alt="School Management System"/>
				</div>
			<br/><br/>
				<ul>
				    <li class="manulist">
						    <a class ="menulista" href="index.php">Home</a>
								<a class ="menulista" href="addPayment.php">Add Payment</a>
								<a class ="menulista" href="deletePayment.php">Delete Payment</a>
								<div align="center">
								<h4>Hi!admin <?php echo $check." ";?></h4>
								    <a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						    </div>
						</li>
				</ul>
			  <hr/>
        <center>
            <table border="1">
                <?php echo $string;?>
            </table>
        </center>
		</body>
</html>
