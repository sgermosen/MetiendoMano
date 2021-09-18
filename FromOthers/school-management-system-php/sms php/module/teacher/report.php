<?php
include('main.php');


?>
        <html>
		
			<head>
              
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
			<script type="text/javascript" src="jquery-1.12.3.js"></script>
			<script type="text/javascript" src="studentClassCourse.js"></script>
			<script src = "JS/login_logout.js"></script>
			
			
				</head>
		  <div class="header"><h1>School Management System</h1></div>
			  <div class="divtopcorner">
				    <img src="../../source/logo.jpg" height="150" width="150" alt="School Management System"/>
				</div>
			<br/><br/>
				<ul>
				    <li class="manulista" align="center">
						    <a class ="menulista" href="index.php">Home</a>
							
								<a class ="menulista" href="deleterep.php">Delete report</a>
								
								
								<div align="center">
								<h4>Hi! <?php echo $check." ";?></h4>
								    <a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						    </div>
						</li>
				</ul>
			  <hr/>
		</html>
		<html>
    <body  onload="ajaxRequestToGetMyCourse();">
             		 
			 
			 <form action="reportupdate.php" method="POST">
			  <div align="center" >
			 Select Class:<select id="myclass" name="myclass" onchange="ajaxRequestToGetMyCourse();">
			 <?php  


$classget = "SELECT  * FROM class where id in(select DISTINCT classid from course where teacherid='$check')";
$res= mysql_query($classget);

while($cln=mysql_fetch_array($res))
{
 echo '<option value="',$cln['id'],'" >',$cln['name'],'</option>';
   
}


?>

</select><br /><br />
Select Course<select id="mycourse" onchange="ajaxRequestToGetCourseStudent();" name="mycourse">

</select> <br />
<br/>

Select Student<select id="mystudent" onchange="" name="mystudent">

</select><br /><br /><br /><br /> 
<textarea name="report"> </textarea> <br />
<input class="menulista" type ="submit" value="Report Submit" name="submit"/>


</form>
<hr/>



			</div>					
							
		</body>
</html>
