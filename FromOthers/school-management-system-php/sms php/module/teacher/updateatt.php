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
								<a class ="menulista" href="updateAttendence.php">Update Attendence</a>
								<a class ="menulista" href="deleteatt.php">Delete Attendende</a>
								
								<div align="center">
								<h4>Hi! <?php echo $check." ";?></h4>
								    <a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						    </div>
						</li>
				</ul>
			  <hr/>
		</html>
		<?php
		
		
$atten=$_REQUEST['mystudent'];
 $d=$_REQUEST['date'];
 /*
$conn=mysql_connect('localhost','root','');
if(!$conn){
   die('error connecting ');
   }
   mysql_select_db('schoolmanagementsystemdb',$conn);
  
	   */
	   $sql="insert into attendance(date,attendedid) values('$d','$atten')";
	  $sd= mysql_query($sql);
	  
	  if(!$sd){
	  echo "problem";
	  }
	  echo "succeed";
  
   		?>
		