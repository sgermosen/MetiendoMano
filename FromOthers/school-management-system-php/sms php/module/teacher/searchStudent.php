<?php
include_once('main.php');
include_once('index.php');
include_once('../../service/mysqlcon.php');
?>
<html>
<div align="center">
<form action="#" method="GET">

key: <input type="text"  name='key' placeholder="st-XXX-X"/>
<input type="submit"  name='submit' value="submit"/>
</form>
</div>
</html>
<table border="2" align="center">
<?php
if(!empty($_GET['submit'])){
$searchKey = $_GET['key'];
$images_dir = "../images";
$string = "<tr>
    <th>ID</th>
    <th>Name</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Gender</th>
    <th>DOB</th>
    <th>Addmission Date</th>
    <th>Address</th>
    <th>Parent Id</th>
    <th>Class Id</th>
    <th>picture</th>

</tr>";
$sql = "SELECT * FROM students WHERE id like '$searchKey%' OR name like '$searchKey%' OR classid = '$searchKey';";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $picname = $row['id'];
	echo "<div align='center'>";
	
    echo '<tr><td>'.$row['id'].'</td><td>'.$row['name'].
    '</td><td>'.$row['phone'].'</td><td>'.$row['email'].
    '</td><td>'.$row['sex'].'</td><td>'.$row['dob'].
    '</td><td>'.$row['addmissiondate'].'</td><td>'.$row['address'].
    '</td><td>'.$row['parentid'].'</td><td>'.$row['classid'].'</td>';
	
	echo "</div>";
	
  // echo $string;
echo "<td><img src='".$images_dir."/".$picname.".jpg' alt='$picname' width='150' height='150' >".'</td></tr>'; 
}

echo "</table>";

//echo $images_dir.$picname.".jpg";
}
?>

