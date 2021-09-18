<?php
include_once('main.php');
?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
				<script src = "JS/login_logout.js"></script>
        <script src = "JS/searchForUpdateStaff.js"></script>
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
            <table>
                <tr>
                    <td><b>Search By Id Or Name: </b></td>
                    <td><input type="text" name="searchId" placeholder="Search By Id Or Name:" onkeyup="getStaffForUpdate(this.value);"></td>
                </tr>
            </table>
        </center>
        <br/>
        <center>
          <h2>Only One Staff Can Update at a time.<br/> Please Search For A Specific Staff</h2>
            <form action="#" method="post" onsubmit="return newStaffValidation();" enctype="multipart/form-data">
                <table border="1" cellpadding="6" id='updateStaffData'>
                </table>
            </form>
        </center>
		</body>
</html>
<?php
include_once('../../service/mysqlcon.php');
if(!empty($_FILES))
if(!empty($_POST['submit'])){
    $Id = $_POST['id'];
    $Name = $_POST['name'];
    $Password = $_POST['password'];
    $Phone = $_POST['phone'];
    $Email = $_POST['email'];
    $gender = $_POST['gender'];
    $DOB = $_POST['dob'];
    $hiredate = $_POST['hiredate'];
    $Address = $_POST['address'];
    $salary = $_POST['salary'];
    $filename = $_FILES['file']['name'];
    $filetmp =$_FILES['file']['tmp_name'];
    move_uploaded_file($filetmp,"../images/".$filename);
    $sql = "UPDATE staff SET id='$Id', name='$Name', password='$Password', phone='$Phone', email='$Email', sex='$gender', dob='$DOB', hiredate='$hiredate', address='$Address', salary='$salary' WHERE id='$Id'";
    $success = mysql_query( $sql,$link );
    if(!$success) {
        die('Could not Update data: '.mysql_error());
    }
    echo "Update data successfully\n";
}
?>
