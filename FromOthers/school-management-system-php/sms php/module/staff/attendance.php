<?php
include_once('main.php');

?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
			<script type="text/javascript" src="jquery-1.12.3.js"></script>
			<script type="text/javascript" src="staffAttendance.js"></script>
			<script src = "JS/login_logout.js"></script>
			
				
	            
		</head>
    <body  onload="ajaxRequestToGetAttendancePresentThisMonth();">
             		 
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
			  <div align="center" style="background-color:orange;">
	
Select Attendance that you are present: Current Month:<input type="radio"  onclick="ajaxRequestToGetAttendancePresentThisMonth();"   value="thismonth" id="present" name="present" checked="checked"/> ALL : <input type="radio" onclick="ajaxRequestToGetAttendancePresentAll();" value="all" id="present" name="present"/>
</div>	
<hr/>
<div align="center" >
<table id="mypresent" border="1">

</table>
</div>
<hr/>
<div align="center" style="background-color:gray;">
	
Select Attendance that you are absent : Current Month:<input type="radio"  onclick="ajaxRequestToGetAttendanceAbsentThisMonth();"   value="thismonth" id="absent" name="absent" checked="checked"/> ALL : <input type="radio" onclick="ajaxRequestToGetAttendanceAbsentAll();" value="all" id="absent" name="absent"/>
</div>	
<hr/>
<div align="center" >
<table id="myabsent" border="1">

</table>
</div>

							
							
		</body>
</html>

