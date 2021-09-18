<?php
include_once('main.php');
$count=0;
$st=mysql_query("SELECT *  FROM staff WHERE id='$check' ");
$stinfo=mysql_fetch_array($st);

$attendmon = "SELECT DISTINCT(date) FROM attendance WHERE attendedid='$check' and  MONTH( DATE ) = MONTH( CURRENT_DATE ) and YEAR( DATE )=YEAR( CURRENT_DATE )";
$resmon = mysql_query($attendmon);

while($r=mysql_fetch_array($resmon))
{
 $count+=1;
}
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
			    <div align="center">
			  	<h1 style="background-color:orange;">My Salary</h1>
				<hr/>
			  <table border="1">
			  <tr>
			  <th>Staff Monthly Salary</th>
			 <th>Staff Payable Salary This Month</th>
			   </tr>
			  <tr>
			  <td><?php echo round($stinfo['salary']/12,2);?></td>
			 <td><?php echo round(($stinfo['salary']/300)*$count,2);?></td>
			  </tr>
			  
			  
			  <table
								
								</div>
			<hr/>
		</body>
</html>

