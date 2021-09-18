<?php
include_once('main.php');

$st=mysql_query("SELECT *  FROM staff WHERE id='$check' ");
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
							    <a class ="menulista" href="modify.php">Modify My Information</a>
								<a class ="menulista" href="salary.php">My Salary</a>
								<a class ="menulista" href="attendance.php">My Attendance</a>
								
								<div align="center">
								<h4>Hi!Staff <?php echo $check." ";?> </h4>
								<a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						</div>
						 
				    
			
						</li>
				</ul>
			  <hr/>
			  
			  <div align="center" class="mod">
			  	<h1>Edit Information</h1>
				<p>*only gray fields Editable.</p>
				<form  onsubmit="return modifyValidate();" action="modifysave.php" method="post">
			  <table border="1">
			  <tr>
			  
			  <th>Staff ID</th>
			  <th>Staff Name</th>
			  <th>Staff Phone</th>
			  <th>Staff Email</th>
			  <th>Staff Password</th>
			  <th>Staff Gender</th>
			  <th>Staff DOB</th>
			  <th>Staff Hire Date</th>
			  <th>Staff Address</th>
			  
			  
			  </tr>
			  <tr>
			  
			  <td><?php echo $stinfo['id'];?></td>
			  <td><?php echo $stinfo['name'];?></td>
			  <td><input type="text" id="phone" name="phone" value="<?php echo $stinfo['phone'];?>"/></td>
			  <td><input type="text"  id="email" name="email" value="<?php echo $stinfo['email'];?>"/></td>
			  <td><input type="text"  id="password" name="password" value="<?php echo $stinfo['password'];?>"/></td>
			  <td><?php echo $stinfo['sex'];?></td>
			  <td><?php echo $stinfo['dob'];?></td>
			  <td><?php echo $stinfo['hiredate'];?></td>
			  <td><input type="text"  id="address" name="address" value="<?php echo $stinfo['address'];?>" /></td>
			  
			 
			  
			  
			  </table>
			  <br/>
			  <input type="submit" value="Modify"/>
			  </form>
								
								</div>
		</body>
</html>

