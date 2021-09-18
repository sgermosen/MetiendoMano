<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];

$string = "<tr>
    <th>ID</th>
    <th>Password</th>
    <th>Father Name</th>
    <th>Mother Name</th>
    <th>Father Phone</th>
    <th>Mother Phone</th>
    <th>Address</th>
</tr>";
$sql = "SELECT * FROM parents WHERE id like '$searchKey%' OR fathername like '$searchKey%' OR mothername like '$searchKey%'";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= "<tr><td><input value='".$row['id']."'name='id' readonly >".
    "</td><td><input type='text' value='".$row['password']."'name='password'>".
    "</td><td><input type='text' value='".$row['fathername']."'name='fathername'>".
    "</td><td><input type='text' value='".$row['mothername']."'name='mothername'>".
    "</td><td><input type='text' value='".$row['fatherphone']."'name='fatherphone'>".
    "</td><td><input type='text' value='".$row['motherphone']."'name='motherphone'>".
    "</td><td><input type='text' value='".$row['address']."'name='address'>"."</td></tr>";
}
echo $string."<input type='submit' name='submit'value='Submit'>";
?>
