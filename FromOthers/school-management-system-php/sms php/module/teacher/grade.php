<?php

include_once('main.php');
include_once('index.php');

if(!empty($_POST['submit'])){
$stid=$_REQUEST['mystudent'];
$crsid=$_REQUEST['mycourse'];

//print_r($_REQUEST);
$string="<form align='center' action='succeed.php'>
student id:  <input type='text' value='$stid' name='stdid' readonly /> <br />
Course id:  <input type='text' value='$crsid' name='crid' readonly /> <br />
Grade:  <input type='text' name='grade' /> <br />
<input class='menulista' type='submit' value='grade' name='sbmt' />
";
echo $string;
}


?>

<?php 

if(!empty($_POST['update'])){
$stid=$_REQUEST['mystudent'];
$crsid=$_REQUEST['mycourse'];
$sql="select grade from grade where studentid='$stid' and courseid='$crsid'";
$res=mysql_query($sql);
while($row = mysql_fetch_array($res)){
$grd=$row[0];
}
$string="<form align='center' action='succeed.php'>
student id:  <input type='text' value='$stid' name='stdid' readonly /> <br />
Course id:  <input type='text' value='$crsid' name='crid' readonly /> <br />
Grade:  <input type='text' value='$grd' name='grade' /> <br />
<input class='menulista' type='submit' value='grade' name='sbmt2' />
";
echo $string;
}

?>