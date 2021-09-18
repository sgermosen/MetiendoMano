<?php
include_once('main.php');
/*include_once('../../service/mysqlcon.php');
$string = "<tr>
    <th>Click To Dlete</th>
    <th>ID</th>
    <th>Student Id</th>
    <th>Amount</th>
    <th>Month</th>
    <th>Year</th>
    </tr>";
$sql = "SELECT * FROM payment WHERE month = MONTH(curdate()) AND year = YEAR(curdate())";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= "<form action='deletePaymentableData.php' method='post'>".
    "<tr><td><input type='submit' name='submit' value='Delete'></td>".
    "</td><td>".$row['id']."</td><td>".$row['studentid']."</td><td>".$row['amount'].
    "</td><td>".$row['month']."</td><td>".$row['year']."</td></tr></form>";
}*/
?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
				<script src = "JS/login_logout.js"></script>
        <script src = "JS/searchPayment.js"></script>
		

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
								<a class ="menulista" href="manageStudent.php">Manage Student</a>
								<a class ="menulista" href="manageTeacher.php">Manage Teacher</a>
								<a class ="menulista" href="manageParent.php">Manage Parent</a>
								<a class ="menulista" href="manageStaff.php">Manage Staff</a>
								<a class ="menulista" href="course.php">Course</a>
								<a class ="menulista" href="attendance.php">Attendance</a>
								<a class ="menulista" href="examSchedule.php">Exam Schedule</a>
								<a class ="menulista" href="salary.php">Salary</a>
								<a class ="menulista" href="report.php">Report</a>
								<a class ="menulista" href="payment.php">Payment</a>
								<div align="center">
								<h4>Hi!admin <?php echo $check." ";?></h4>
								    <a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						    </div>
						</li>
				</ul>
			  <hr/>
        <center>
            <h1>Delete Payment</h1>
            <table>
                <tr>
                    <td><b>Search By Payment Id Or Student Id</b></td>
                    <td><input type="text" onkeyup="getPayment(this.value);"></td>
                </tr>
            </table>
            <div id="paymentList">
            </div>
        </center>
		</body>
</html>
