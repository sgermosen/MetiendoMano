
<?php
session_start();
include("dbconnection.php");
$sql ="select * from doctor where departmentid='$_GET[deptid]'";
$qsql = mysqli_query($con,$sql);
echo "<select class='selectpicker' name='doct' id='doct'  >";
while($qsql1=mysqli_fetch_array($qsql))
{	
	echo "<option value=''>Select doctor</option>";
	echo "<option value='$qsql1[doctorid]'>$qsql1[doctorname]</option>";		
}
echo "</select>"
?>	          
