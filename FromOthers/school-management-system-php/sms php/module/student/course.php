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
    <body  onload="ajaxRequestToGetCourse();">
             		 
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
			  <div align="center" >
			 Select Class:<select id="myclass" name="myclass" onchange="ajaxRequestToGetCourse();"><?php  


$classget = "SELECT  name FROM class where id in(select DISTINCT classid from course where studentid='$check')";
$res= mysql_query($classget);

while($cln=mysql_fetch_array($res))
{
 echo '<option value="',$cln['name'],'" >',$cln['name'],'</option>';
   
}


?>
</select>
Select Course<select id="mycourse" onchange="ajaxRequestToGetCourseInfo();" name="mycourse">

</select>
<h4>Course Information:<hr/></br><label id="mycourseteacher"></label><br/>
<table id="mycourseinfo">
</table>
<hr/>



			</div>					
							
		</body>
</html>

