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
        <script src = "JS/newStaffValidation.js"></script>
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
            <h2>Staff Registration.</h2><hr/>
            <form action="#" method="post"onsubmit="return newStaffValidation();" enctype="multipart/form-data">
                <table cellpadding="6">
                    <tr>
                      <td>Staff Id:</td>
                      <td><input id="Id"type="text" name="Id" placeholder="Enter Id"></td>
                    </tr>
                    <tr>
                        <td>Staff Name:</td>
                        <td><input id="Name" type="text" name="Name" placeholder="Enter Name"></td>
                    </tr>
                    <tr>
                        <td>Staff Password:</td>
                        <td><input id="Password"type="text" name="Password" placeholder="Enter Password"></td>
                    </tr>
                    <tr>
                        <td>Staff Phone:</td>
                        <td><input id="Phone"type="text" name="Phone" placeholder="Enter Phone Number"></td>
                    </tr>
                    <tr>
                        <td>Staff Email:</td>
                        <td><input id="Email"type="text" name="Email" placeholder="Enter Email"></td>
                    </tr>
                    <tr>
                        <td>Gender:</td>
                        <td><input type="radio" name="gender" value="Male" onclick="Gender = this.value;"> Male <input type="radio" name="gender" value="Female" onclick="this.value"> Female</td>
                    </tr>
                    <tr>
                        <td>Staff DOB:</td>
                        <td><input id="DOB"type="text" name="DOB" placeholder="Enter DOB(yyyy-mm-dd)"></td>
                    </tr>
                    <tr>
                        <td>Staff Hire Date:</td>
                        <td><input id="HireDate"name="HireDate"value = "<?php echo date('Y-m-d');?>" readonly></td>
                    </tr>
                    <tr>
                        <td>Staff Address:</td>
                        <td><input id="Address" type="text" name="Address" placeholder="Enter Address"></td>
                    </tr>
                    <tr>
                        <td>Staff Salary:</td>
                        <td><input id="Salary"type="text" name="Salary" placeholder="Enter Salary"></td>
                    </tr>
                    <tr>
                      <td>Staff Picture:</td>
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
    $Id = $_POST['Id'];
    $Name = $_POST['Name'];
    $Password = $_POST['Password'];
    $Phone = $_POST['Phone'];
    $Email = $_POST['Email'];
    $gender = $_POST['gender'];
    $DOB = $_POST['DOB'];
    $HireDate = $_POST['HireDate'];
    $Address = $_POST['Address'];
    $Salary = $_POST['Salary'];
    //$filename = $_FILES['file']['name'];
    $filetmp =$_FILES['file']['tmp_name'];
    move_uploaded_file($filetmp,"../images/".$Id.".jpg");
    $sql = "INSERT INTO staff VALUES('$Id','$Name','$Password','$Phone','$Email','$gender','$DOB','$HireDate','$Address','$Salary')";
    $success = mysql_query($sql);
    $sql = "INSERT INTO users VALUES('$Id','$Password','staff')";
    $success = mysql_query($sql);
    if(!$success) {
        die('Could not enter data: '.mysql_error());
    }
    echo "Entered data successfully\n";
}
?>
