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
    <body  onload="ajaxRequestToGetChild();">
             		 
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
			  <div align="center" >
			  
			  	  
			  Select your Child:<select id="childid" name="childid" onchange="ajaxRequestToGetChild();" style="background-color:white;"><?php  


$classget = "SELECT  * FROM students where parentid='$check'";
$res= mysql_query($classget);

while($cln=mysql_fetch_array($res))
{
	
 echo '<option value="',$cln['id'],'" >',$cln['name'],'</option>';
   
}

?>

</select>
<hr/>

 Select Class:<select id="myclass" name="myclass" onchange="ajaxRequestToGetCourse();">
 
</select>

Select Course<select id="mycourse" onchange="ajaxRequestToGetCourseReportInfo();" name="mycourse">

</select>
<h4>Course Information:<hr/></br><label id="mycourseteacher"></label><br/>
<table id="mycoursereportinfo">
</table>
<hr/>



			</div>					
							
		</body>
</html>

