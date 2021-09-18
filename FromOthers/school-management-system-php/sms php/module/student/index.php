<?php
include_once('main.php');

$st=mysql_query("SELECT *  FROM students WHERE id='$check' ");
$stinfo=mysql_fetch_array($st);

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
				    <li class="manulist" >
						    <a class ="menulista" href="index.php">Home</a>
						        <a class ="menulista" href="modify.php">Change Password</a>
								<a class ="menulista" href="course.php">My Course And Result</a>
								<a class ="menulista" href="exam.php">My Exam Schedule</a>
								<a class ="menulista" href="attendance.php">My Attendance</a>
								
								<div align="center">
								<h4>Hi!Student <?php echo $check." ";?> </h4>
								<a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						</div>
						 
				    
			
						</li>
				</ul>
			  <hr/>
			  
			  <div align="center">
			  	<h1>My Information</h1>
			  <table border="1">
			  <tr>
			  
			  <th>Student ID</th>
			  <th>Student Name</th>
			  <th>Student Phone</th>
			  <th>Student Email</th>
			  <th>Student Gender</th>
			  <th>Student DOB</th>
			  <th>Student Admission Date</th>
			  <th>Student Address</th>
			  <th>Student Parent ID</th>
			  <th>Student class ID</th>
			  <th>Student Picture</th>
			  
			  </tr>
			  <tr>
			  
			  <td><?php echo $stinfo['id'];?></td>
			  <td><?php echo $stinfo['name'];?></td>
			  <td><?php echo $stinfo['phone'];?></td>
			  <td><?php echo $stinfo['email'];?></td>
			  <td><?php echo $stinfo['sex'];?></td>
			  <td><?php echo $stinfo['dob'];?></td>
			  <td><?php echo $stinfo['addmissiondate'];?></td>
			  <td><?php echo $stinfo['address'];?></td>
			  <td><?php echo $stinfo['parentid'];?></td>
			  <td><?php echo $stinfo['classid'];?></td>
			  <td><img src="../images/<?php echo $check.".jpg";?>" height="95" width="95" alt="<?php echo $check." photo";?> "/></td>
			  </tr>
			  
			  
			  <table
								
								</div>
		</body>
</html>

