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
        <script src = "JS/newTeacherValidation.js"></script>
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
            <h2>Teacher Registration.</h2><hr/>
            <form action="#" method="post"onsubmit="return newTeacherValidation();" enctype="multipart/form-data">
                <table cellpadding="6">
                    <tr>
                      <td>Teacher Id:</td>
                      <td><input id="teaId"type="text" name="teacherId" placeholder="Enter Id"></td>
                    </tr>
                    <tr>
                        <td>Teacher Name:</td>
                        <td><input id="teaName" type="text" name="teacherName" placeholder="Enter Name"></td>
                    </tr>
                    <tr>
                        <td>Teacher Password:</td>
                        <td><input id="teaPassword"type="text" name="teacherPassword" placeholder="Enter Password"></td>
                    </tr>
                    <tr>
                        <td>Teacher Phone:</td>
                        <td><input id="teaPhone"type="text" name="teacherPhone" placeholder="Enter Phone Number"></td>
                    </tr>
                    <tr>
                        <td>Teacher Email:</td>
                        <td><input id="teaEmail"type="text" name="teacherEmail" placeholder="Enter Email"></td>
                    </tr>
                    <tr>
                        <td>Teacher Address:</td>
                        <td><input id="teaAddress" type="text" name="teacherAddress" placeholder="Enter Address"></td>
                    </tr>
                    <tr>
                        <td>Gender:</td>
                        <td><input type="radio" name="gender" value="Male" onclick="teaGender = this.value;"> Male <input type="radio" name="gender" value="Female" onclick="teaGender = this.value;"> Female</td>
                    </tr>
                    <tr>
                        <td>Teacher DOB:</td>
                        <td><input id="teaDOB"type="text" name="teacherDOB" placeholder="Enter DOB(yyyy-mm-dd)"></td>
                    </tr>
                    <tr>
                        <td>Teacher Hire Date:</td>
                        <td><input id="teaHireDate"name="teacherHireDate"value = "<?php echo date('Y-m-d');?>" readonly></td>
                    </tr>
                    <tr>
                        <td>Salary</td>
                        <td><input id="teaSalary"type="text" name="teacherSalary" placeholder="Enter Salary"></td>
                    </tr>
                    <tr>
                      <td>Teacher Picture:</td>
                      <td><input id="file"type="file" name="file"></td>
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
if(!empty($_FILES))
if(!empty($_POST['submit'])){
    $teaId = $_POST['teacherId'];
    $teaName = $_POST['teacherName'];
    $teaPassword = $_POST['teacherPassword'];
    $teaPhone = $_POST['teacherPhone'];
    $teaEmail = $_POST['teacherEmail'];
    $teaGender = $_POST['gender'];
    $teaDOB = $_POST['teacherDOB'];
    $teaHireDate = $_POST['teacherHireDate'];
    $teaAddress = $_POST['teacherAddress'];
    $teaSalary = $_POST['teacherSalary'];
    //$filename = $_FILES['file']['name'];
    $filetmp =$_FILES['file']['tmp_name'];
    //echo $filename;
    move_uploaded_file($filetmp,"../images/".$teaId.".jpg");
    $sql = "INSERT INTO teachers VALUES('$teaId','$teaName','$teaPassword','$teaPhone','$teaEmail','$teaAddress','$teaGender','$teaDOB','$teaHireDate','$teaSalary');";
    $success = mysql_query( $sql,$link );
    $sql = "INSERT INTO users VALUES('$teaId','$teaPassword','teacher');";
    $success = mysql_query( $sql,$link );
    if(!$success) {
        die('Could not enter data: '.mysql_error());
    }
    echo "Entered data successfully\n";
}
?>
