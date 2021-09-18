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
			  <form align="center" action="#" method="post">
			  
			  Student id: <input type="text" name="stdid" placeholder="Student id" /><br />
			  Date:       <input type ="text" name="date" placeholder="YYYY-MM-DD" /><br />
			  <input type="submit" name="submit" value="delete" />
			  </form>
			  
			  
		</html>
		<div align="center">
			 <?php  
			 if(!empty($_POST['submit'])){
			 //print_r($_REQUEST);
			 $s=$_REQUEST['stdid'];
			$d= $_REQUEST['date'];
			$sql="DELETE FROM attendance WHERE attendedid='$s' and date='$d'";
			$s=mysql_query($sql);
			if(!$s)
			{
			echo "problem";
			}
			echo "success";
			 }
			?>
			
			</div>

