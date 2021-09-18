<?php
include_once('main.php');

?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
			<script type="text/javascript" src="jquery-1.12.3.js"></script>
			<script type="text/javascript" src="stdatt.js"></script>
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
								<a class ="menulista" href="updateAttendence.php">Update Attendence</a>
								<a class ="menulista" href="deleteatt.php">Delete Attendende</a>
								<a class ="menulista" href="viewAttendance.php">view my Attendende</a>
								<a class ="menulista" href="viewAttendancest.php">view students Attendende</a>
								
								<div align="center">
								<h4>Hi! <?php echo $check." ";?> </h4>
								<a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						</div>
						 
				    
			
						</li>
				</ul>
			  <hr/>
			  <div align="center" style="background-color:orange;" onload="ajaxRequestToGetAttendanceAbsentThisMonth();">
			  Select Student:<select id="mystudent" name="mystudent" onchange="ajaxRequestToGetAttendancePresentThisMonth();">
			  <?php 
		$sql="SELECT DISTINCT(students.id),students.name FROM students INNER JOIN course ON students.classid=course.classid and course.teacherid='$check'";
		$res=mysql_query($sql);
		while($cln=mysql_fetch_array($res))
{
 echo '<option value="',$cln['id'],'" >',$cln['name'],'</option>';
   
}
?>
	</select>
Select Month: Current Month:<input type="radio"  onclick="ajaxRequestToGetAttendancePresentThisMonth();"   value="thismonth" id="present" name="present" checked="checked"/> ALL : <input type="radio" onclick="ajaxRequestToGetAttendancePresentAll();" value="all" id="present" name="present"/>
</div>	
<hr/>
<div align="center" >

<table id="mypresent" border="1">

</table>
</div>
<hr/>
<div align="center" style="background-color:gray;">
	
Select Month : Current Month:<input type="radio"  onclick="ajaxRequestToGetAttendanceAbsentThisMonth();"   value="thismonth" id="absent" name="absent" checked="checked"/> ALL : <input type="radio" onclick="ajaxRequestToGetAttendanceAbsentAll();" value="all" id="absent" name="absent"/>
</div>	
<hr/>
<div align="center" >
<table id="myabsent" border="1">

</table>
</div>

							
							
		</body>
</html>

