<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];
$images_dir = "../images/";
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
    <th>Picture</th>
</tr>";
$sql = "SELECT * FROM students WHERE id like '$searchKey%' OR name like '$searchKey%' OR classid = '$searchKey';";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $picname = $row['id'];
    $string .= '<tr><td>'.$row['id'].'</td><td>'.$row['name'].
    '</td><td>'.$row['phone'].'</td><td>'.$row['email'].
    '</td><td>'.$row['sex'].'</td><td>'.$row['dob'].
    '</td><td>'.$row['addmissiondate'].'</td><td>'.$row['address'].
    '</td><td>'.$row['parentid'].'</td><td>'.$row['classid'].
    "</td><td><img src='".$images_dir.$picname.".jpg' alt='$picname' width='150' height='150'>".'</td></tr>';
}
echo $string;
?>
