<?php
include_once('main.php');
$count=0;
$st=mysql_query("SELECT *  FROM teachers WHERE id='$check' ");
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
             		 
		
			  				<?php include('index.php'); ?>
						</div>
						 
				    
			
						</li>
				</ul>
			 
			    <div align="center">
			  	<h1 style="background-color:black; color:white;">My Salary</h1>
				<hr/>
			  <table border="1">
			  <tr>
			  <th>Teacher Monthly Salary</th>
			 <th>Teacher Payable Salary This Month</th>
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

