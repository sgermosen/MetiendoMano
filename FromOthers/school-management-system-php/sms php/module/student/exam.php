<?php
include_once('main.php');

?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
			<script type="text/javascript" src="jquery-1.12.3.js"></script>
			<script type="text/javascript" src="studentClassCourse.js"></script>
			<script src = "JS/login_logout.js"></script>
			
				
	            
		</head>
    <body  onload="ajaxRequestToGetCourseCurrentExamSchedule();">
             		 
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
			  <div align="center" style="background-color:orange;">
	
Select Runing Course Exam Schedule:<select id="curcourse" onchange="ajaxRequestToGetCourseCurrentExamSchedule();" name="curcourse"><?php  


$classget = "SELECT  DISTINCT id,name FROM course where classid in(select DISTINCT classid from students where id='$check') and studentid='$check'";
$res= mysql_query($classget);

while($clnn=mysql_fetch_array($res))
{
 echo '<option value="',$clnn['id'],'" >',$clnn['name'],'</option>';
   
}


?>
</select>
</div>	
<hr/>
<div align="center">
<table id="mycourseexamschedule" border="1" >
</table>
</div>


							
							
		</body>
</html>

