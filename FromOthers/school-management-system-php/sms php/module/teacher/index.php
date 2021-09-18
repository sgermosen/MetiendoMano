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
								<a class ="menulista" href="updateTeacher.php">Update Profile</a>
								<a class ="menulista" href="viewProfile.php">View Profile</a>
								<a class ="menulista" href="course.php">Students Grade</a>
								<a class ="menulista" href="courses.php">Courses</a>
								<a class ="menulista" href="attendance.php">Attendance</a>
								<a class ="menulista" href="exam.php">Exam Schedule</a>
								<a class ="menulista" href="salary.php">Salary</a>
								<a class ="menulista" href="report.php">Report</a>
								<a class ="menulista" href="searchStudent.php">Search Portal</a>
								<div align="center">
								<h4>Hi! <?php echo $check." ";?></h4>
								    <a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						    </div>
						</li>
				</ul>
			  <hr/>
		</body>
</html>
