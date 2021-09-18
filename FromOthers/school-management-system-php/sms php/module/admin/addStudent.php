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
        <script src = "JS/newStudentValidation.js"></script>
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
								<a class ="menulista" href="index.php">Manage Parent</a>
								<a class ="menulista" href="index.php">Manage Staff</a>
								<a class ="menulista" href="index.php">Course</a>
								<a class ="menulista" href="index.php">Attendance</a>
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
            <h2>Student Registration.</h2><hr/>
            <form action="#" method="post"onsubmit="return newStudentValidation();" enctype="multipart/form-data">
                <table cellpadding="6">
                    <tr>
                      <td>Student Id:</td>
                      <td><input id="stuId"type="text" name="studentId" placeholder="Enter Id"></td>
                    </tr>
                    <tr>
                        <td>Student Name:</td>
                        <td><input id="stuName" type="text" name="studentName" placeholder="Enter Name"></td>
                    </tr>
                    <tr>
                        <td>Student Password:</td>
                        <td><input id="stuPassword"type="text" name="studentPassword" placeholder="Enter Password"></td>
                    </tr>
                    <tr>
                        <td>Student Phone:</td>
                        <td><input id="stuPhone"type="text" name="studentPhone" placeholder="Enter Phone Number"></td>
                    </tr>
                    <tr>
                        <td>Student Email:</td>
                        <td><input id="stuEmail"type="text" name="studentEmail" placeholder="Enter Email"></td>
                    </tr>
                    <tr>
                        <td>Gender:</td>
                        <td><input type="radio" name="gender" value="Male" onclick="stuGender = this.value;"> Male <input type="radio" name="gender" value="Female" onclick="this.value"> Female</td>
                    </tr>
                    <tr>
                        <td>Student DOB:</td>
                        <td><input id="stuDOB"type="text" name="studentDOB" placeholder="Enter DOB(yyyy-mm-dd)"></td>
                    </tr>
                    <tr>
                        <td>Student Addmission Date:</td>
                        <td><input id="stuAddmissionDate"name="studentAddmissionDate"value = "<?php echo date('Y-m-d');?>" readonly></td>
                    </tr>
                    <tr>
                        <td>Student Address:</td>
                        <td><input id="stuAddress" type="text" name="studentAddress" placeholder="Enter Address"></td>
                    </tr>
                    <tr>
                        <td>Student Parent Id:</td>
                        <td><input id="stuParentId"type="text" name="studentParentId" placeholder="Enter Parent Id"></td>
                    </tr>
                    <tr>
                        <td>Student Class Id:</td>
                        <td><input id="stuClassId" type="text" name="studentClassId" placeholder="Enter Class Id"></td>
                    </tr>
                    <tr>
                      <td>Student Picture:</td>
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
if(!empty($_POST['submit'])){
    $stuId = $_POST['studentId'];
    $stuName = $_POST['studentName'];
    $stuPassword = $_POST['studentPassword'];
    $stuPhone = $_POST['studentPhone'];
    $stuEmail = $_POST['studentEmail'];
    $stugender = $_POST['gender'];
    $stuDOB = $_POST['studentDOB'];
    $stuAddmissionDate = $_POST['studentAddmissionDate'];
    $stuAddress = $_POST['studentAddress'];
    $stuParentId = $_POST['studentParentId'];
    $stuClassId = $_POST['studentClassId'];
    //$filename = $_FILES['file']['name'];
    $filetmp =$_FILES['file']['tmp_name'];
    move_uploaded_file($filetmp,"../images/".$stuId.".jpg");
    $sql = "INSERT INTO students VALUES('$stuId','$stuName','$stuPassword','$stuPhone','$stuEmail','$stugender','$stuDOB','$stuAddmissionDate','$stuAddress','$stuParentId','$stuClassId');";
    $success = mysql_query($sql);
    $sql = "INSERT INTO users VALUES('$stuId','$stuPassword','student');";
    $success = mysql_query($sql);
    if(!$success) {
        die('Could not enter data: '.mysql_error());
    }
    echo "Entered data successfully\n";
}
?>
