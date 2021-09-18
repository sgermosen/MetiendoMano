<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$stringTeacher = "<tr>
    <th>ID</th>
    <th>Name</th>
    <th>Salary</th>
    <th>Payable Salary</th>
    </tr>";
$sql = "SELECT t.id,t.name,t.salary,ROUND(t.salary*count(a.date)/300) AS currentmonthlysalary FROM teachers t,attendance a WHERE t.id=a.attendedid AND MONTH(a.date)=(SELECT month(curdate()) FROM dual) GROUP BY a.attendedid";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $stringTeacher .= "<tr><td>".$row['id'].
    "</td><td>".$row['name']."</td><td>".$row['salary'].
    "</td><td>".$row['currentmonthlysalary']."</td></tr>";
}

$stringStaff = "<tr>
    <th>ID</th>
    <th>Name</th>
    <th>Salary</th>
    <th>Payable Salary</th>
    </tr>";
$sql = "SELECT s.id,s.name,s.salary,ROUND(s.salary*count(a.date)/300) AS currentmonthlysalary FROM staff s,attendance a WHERE s.id=a.attendedid AND MONTH(a.date)=(SELECT month(curdate()) FROM dual) GROUP BY a.attendedid";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $stringStaff .= "<tr><td>".$row['id'].
    "</td><td>".$row['name']."</td><td>".$row['salary'].
    "</td><td>".$row['currentmonthlysalary']."</td></tr>";
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
								<a class ="menulista" href="updateTeacherSalary.php">Update Teacher Salary</a>
                <a class ="menulista" href="updateStaffSalary.php">Update Staff Salary</a>
								<div align="center">
								<h4>Hi!admin <?php echo $check." ";?></h4>
								    <a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						    </div>
						</li>
				</ul>
			  <hr/>
        <center>
            <h1>Teacher Salary List</h1>
            <table border="1">
                <?php echo $stringTeacher;?>
            </table>
        </center><hr/>
        <center>
            <h1>Staff Salary List</h1>
            <table border="1">
                <?php echo $stringStaff;?>
            </table>
        </center>
		</body>
</html>
