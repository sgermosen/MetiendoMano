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
		
<?php

echo '<form align="center"  action="#" method="post">';
$conn=mysql_connect('localhost','root','');
if(!$conn){
   die('error connecting ');
   }
   mysql_select_db('schoolmanagementsystemdb',$conn);
   
  $sql="SELECT  reportid,courseid,studentid,message FROM report where teacherid='$check'";
  $res=mysql_query($sql);
  
 while($rn=mysql_fetch_array($res))
{
	echo "<center>";
	$string ="<table align='center' border='2'>
	<tr>
		<th> Report id</th>
		<th>Course id </th>
		<th>Student id </th>
		<th> Message</th>
		<th> checkbox</th>
		
		</tr>
	<tr>
	<td>$rn[0] </td>
	<td> $rn[1]</td>
	<td> $rn[2]</td>
	<td> $rn[3]</td>
	<td><input type='checkbox' checked='checked' name='attendance[]' value=".$rn[0]." />  </td>
	";
	echo $string;
	
   
   
     }
	 echo "</table>";
	 
   echo "<input class='menulista' type='submit' value='Delete'name='submit' />";
  echo " </form> </html>";
  echo "</center>";

?>


<?php
//print_r($_REQUEST);
if(!empty($_POST['submit'])){
$atten=$_REQUEST['attendance'];
foreach($atten as $a)
   {
	   
	   $sql="delete from report where reportid='$a'";
		$s=mysql_query($sql);
   }

if(!$s)
{
echo "failed!!!";
}
echo "succeed";
}
?>


</div>

