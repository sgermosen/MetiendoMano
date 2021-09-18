<?php
include_once('../../service/mysqlcon.php');
$check=$_SESSION['login_id'];
$session=mysql_query("SELECT name  FROM admin WHERE id='$check' ");
$row=mysql_fetch_array($session);
$login_session = $loged_user_name = $row['name'];
if(!isset($login_session)){
    header("Location:../../");
}
?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
				<script src = "JS/login_logout.js"></script>
        <script src = "JS/currentDate.js"></script>
        <script src = "JS/newParentValidation.js"></script>
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
								<a class ="menulista" href="index.php">Exam Schedule</a>
								<a class ="menulista" href="index.php">Salary</a>
								<a class ="menulista" href="index.php">Report</a>
								<a class ="menulista" href="index.php">Payment</a>
								<div align="center">
								<h4>Hi!admin <?php echo $check." ";?></h4>
								<a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						</div>
						</li>
				</ul>
			  <hr/>
        <center>
            <h2>Parent Registration.</h2><hr/>
            <form action="#" method="post"onsubmit="return newParentValidation();">
                <table cellpadding="6">
                    <tr>
                      <td>Parent Id:</td>
                      <td><input id="id"type="text" name="id" placeholder="Enter Id"></td>
                    </tr>
                    <tr>
                        <td>Parent Password:</td>
                        <td><input id="password"type="text" name="password" placeholder="Enter Password"></td>
                    </tr>
                    <tr>
                        <td>Father Name:</td>
                        <td><input id="fathername"type="text" name="fathername" placeholder="Enter Father Name"></td>
                    </tr>
                    <tr>
                        <td>Mother Name:</td>
                        <td><input id="mothername"type="text" name="mothername" placeholder="Enter Mother Name"></td>
                    </tr>
                    <tr>
                        <td>Father Phone:</td>
                        <td><input id="fatherphone"type="text" name="fatherphone" placeholder="Enter Father Phone"></td>
                    </tr>
                    <tr>
                        <td>Mother Phone:</td>
                        <td><input id="motherphone"type="text" name="motherphone" placeholder="Enter Mother Phone"></td>
                    </tr>
                    <tr>
                        <td>Address:</td>
                        <td><input id="address" type="text" name="address" placeholder="Enter Address"></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="submit" name="submit"value="Submit"></td>
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
    $password = $_POST['password'];
    $fathername = $_POST['fathername'];
    $mothername = $_POST['mothername'];
    $fatherphone = $_POST['fatherphone'];
    $motherphone = $_POST['motherphone'];
    $address = $_POST['address'];
    $sql = "INSERT INTO parents VALUES('$id','$password','$fathername','$mothername','$fatherphone','$motherphone','$address')";
    $success = mysql_query( $sql,$link );
    if(!$success) {
        die('Could not enter data: '.mysql_error());
    }
    $sql = "INSERT INTO users VALUES('$id','$password','parent')";
    $success = mysql_query( $sql,$link );
    echo "Entered data successfully\n";
}
?>
