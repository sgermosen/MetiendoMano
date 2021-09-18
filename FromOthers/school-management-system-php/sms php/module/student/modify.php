<?php
include_once('main.php');

$st=mysql_query("SELECT password  FROM students WHERE id='$check' ");
$stinfo=mysql_fetch_array($st);

?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
				<script src = "JS/login_logout.js"></script>
				<script src = "JS/modifyValidate.js"></script>
		</head>
		<style>
		input {
    text-align: center;
    background-color: gray;
           }
		
		</style>
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
			  
			  <div align="center" class="mod">
			  	<h1>Change Password</h1>
				
				<form  onsubmit="return modifyValidate();" action="modifysave.php" method="post">
			  <table border="1">
			  <tr>
			  <th>Student Password</th>
			 </tr>
			  <tr>
			  
			  <td><input type="text"  id="password" name="password" value="<?php echo $stinfo['password'];?>"/></td>
			</table>
			  <br/>
			  <input type="submit" value="Change Password"/>
			  </form>
								
								</div>
		</body>
</html>

