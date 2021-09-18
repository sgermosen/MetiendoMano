<?php
include_once('main.php');

$st=mysql_query("SELECT *  FROM parents WHERE id='$check' ");
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
						        <a class ="menulista" href="checkchild.php">Childs Information</a>
								<a class ="menulista" href="childcourse.php">Childs Course And Result</a>
								<a class ="menulista" href="childpayment.php">Child Payments</a>
								<a class ="menulista" href="childattendance.php">Childs Attendance</a>
								<a class ="menulista" href="childreport.php">Childs Report</a>
								
								<div align="center">
								<h4>Hi!Parents <?php echo $check." ";?> </h4>
								<a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						</div>
						 
				    
			
						</li>
				</ul>
			  <hr/>
			  
			  <div align="center">
			  	<h1>Parents Information</h1>
			  <table border="1">
			  <tr>
			  
			  <th>Parents ID</th>
			  <th>Parent Male Name</th>
			  <th>Parent Female Name</th>
			  <th>Parent Male Phone</th>
			  <th>Parent Female Phone</th>
			  <th>Student Address</th>
			  
			  
			  </tr>
			  <tr>
			  
			  <td><?php echo $stinfo['id'];?></td>
			  <td><?php echo $stinfo['fathername'];?></td>
			  <td><?php echo $stinfo['mothername'];?></td>
			  <td><?php echo $stinfo['fatherphone'];?></td>
			  <td><?php echo $stinfo['motherphone'];?></td>
			  <td><?php echo $stinfo['address'];?></td>
			  </tr>
			  
			  
			  <table
								
								</div>
		</body>
</html>

