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
        <script src = "JS/getClassName.js"></script>
        <script src = "JS/getCourseIdAndNAme.js"></script>
		</head>
    <body onload="getClassNameAndId();">
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
            <h2>Course Registration For Student.</h2><hr/>
            <form action="#" method="post">
                <table cellpadding="6">
                    <tr>
                        <td>Class ID:</td>
                        <td><select id="className" onchange="getCourseNameAndId();"></select></td>
                    </tr>
                    <tr>
                        <td>Course Name:</td>
                        <td><select id="courseName" onchange="setCourseId()"></select></td>
                    </tr>
                    <tr>
                        <td><input id="courseId"type="hidden" name="name" placeholder="Enter Name"></td>
                    </tr>
                    <tr>
                        <td>Teacher ID:</td>
                        <td><select id="teacherId" onchange=""></select></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><input type="button" name="submit"value="Submit" onclick="getAllCourseStudentAndSubmit();"></td>
                    </tr>
                </table>
            </form>
        </center>
		</body>
</html>
