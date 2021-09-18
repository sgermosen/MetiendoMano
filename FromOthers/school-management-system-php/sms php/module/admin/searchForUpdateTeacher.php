<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];

$string = "<tr>
    <th>ID</th>
    <th>Name</th>
    <th>Password</th>
    <th>Phone</th>
    <th>Email</th>
    <th>Address</th>
    <th>Gender</th>
    <th>DOB</th>
    <th>Hire Date</th>
    <th>Salary</th>
    </tr>";
$sql = "SELECT * FROM teachers WHERE id like '$searchKey%' OR name like '$searchKey%';";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= "<tr><td><input value='".$row['id']."'name='id' readonly >".
    "</td><td><input type='text' value='".$row['name']."'name='name'>".
    "</td><td><input type='text' value='".$row['password']."'name='password'>".
    "</td><td><input type='text' value='".$row['phone']."'name='phone'>".
    "</td><td><input type='text' value='".$row['email']."'name='email'>".
    "</td><td><input type='text' value='".$row['address']."'name='address'>".
    "</td><td><input type='text' value='".$row['sex']."'name='gender'>".
    "</td><td><input type='text' value='".$row['dob']."'name='dob'>".
    "</td><td><input type='text' value='".$row['hiredate']."'name='hiredate'>".
    "</td><td><input type='text' value='".$row['salary']."'name='salary'>".
    "<td><input type='file' name='pic' accept='image/*'></td>".
    "</td></tr>";
}
echo $string."<input type='submit' name='submit'value='Submit'>";
?>
