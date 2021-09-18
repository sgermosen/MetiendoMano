<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];
$images_dir = "../images/";
$string = "<tr>
    <th>ID</th>
    <th>Name</th>
    <th>Password</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Gender</th>
    <th>DOB</th>
    <th>Hire Date</th>
    <th>Address</th>
    <th>Salary</th>
    <th>Picture</th>
</tr>";
$sql = "SELECT * FROM staff WHERE id like '$searchKey%' OR name like '$searchKey%'";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $picname = $row['id'];
    $string .= '<tr><td>'.$row['id'].'</td><td>'.$row['name'].'</td><td>'.$row['password'].
    '</td><td>'.$row['phone'].'</td><td>'.$row['email'].
    '</td><td>'.$row['sex'].'</td><td>'.$row['dob'].
    '</td><td>'.$row['hiredate'].'</td><td>'.$row['address'].
    '</td><td>'.$row['salary'].
    "</td><td><img src='".$images_dir.$picname.".jpg' alt='$picname' width='150' height='150'>".'</td></tr>';
}
echo $string;
?>
