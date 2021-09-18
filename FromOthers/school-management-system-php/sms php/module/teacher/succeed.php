<center>
<?php
include('index.php');
include('main.php');
if(!empty($_REQUEST['sbmt']))
{
$crid=$_REQUEST["crid"];
$std=$_REQUEST["stdid"];
$gd=$_REQUEST['grade'];
$sql="insert into grade (studentid,grade,courseid) values ('$std','$gd','$crid')";
//echo $sql;
$s=mysql_query($sql);
if(!$s)
{
echo "problem ";
}
echo "Updated successfully";
}

else
{
$crid=$_REQUEST["crid"];
$std=$_REQUEST["stdid"];
$gd=$_REQUEST['grade'];
$sql1="update  grade set  grade='$gd' WHERE studentid='$std' and courseid='$crid'";
$s=mysql_query($sql1);
if(!$s)
{
echo "problem ";
}
echo "Updated successfully";
}
?>
</center>
