<?php
include_once('main.php');
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
            <h1>Student Tuition Fees</h1>
            <form action="#" method="post">
                <table cellpadding="6">
                  <tr>
                      <td>Student ID:</td>
                      <td><input type="text" name="id" placeholder="Enter Student Id."></td>
                  </tr>
                  <tr>
                      <td>Ammount:</td>
                      <td><input type="text" name="ammount" placeholder="Enter Ammount."></td>
                  </tr>
                  <tr>
                      <td>Month:</td>
                      <td><input type="text" name="month" placeholder="Enter Month.(April as 4)"></td>
                  </tr>
                  <tr>
                      <td>Year:</td>
                      <td><input type="text" name="year" placeholder="Enter Year.(2016)"></td>
                  </tr>
                  <tr>
                      <td></td>
                      <td><input type="submit" name="submit" value="Submit"></td>
                  </tr>
                </table>
            </form>
        </center>
		</body>
</html>
<?php
include_once('../../service/mysqlcon.php');
if(!empty($_POST['submit'])){
    $id = $_POST['id'];
    $ammount = $_POST['ammount'];
    $month = $_POST['month'];
    $year = $_POST['year'];
    $sql = "INSERT INTO payment VALUES('','$id','$ammount','$month','$year')";
    $success = mysql_query( $sql,$link );
    if(!$success) {
        die('Could not enter data: '.mysql_error());
    }
    echo "Transaction successfull\n";
}
?>
