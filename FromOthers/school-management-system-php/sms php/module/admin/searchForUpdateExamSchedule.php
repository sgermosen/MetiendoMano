<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$searchKey = $_GET['key'];
$string = "<tr>
    <th>ID</th>
    <th>Exam Date</th>
    <th>Exam Time</th>
    <th>Course ID</th>
</tr>";
$sql = "SELECT * FROM examschedule WHERE id like '$searchKey%' OR examdate like '$searchKey%' OR courseid like '$searchKey%' AND MONTH(examdate) = MONTH(CURRENT_DATE) AND YEAR(examdate)=YEAR(CURRENT_DATE)";
$res = mysql_query($sql);
while($row = mysql_fetch_array($res)){
    $string .= "<tr><td><input value='".$row['id']."'name='id' readonly >".
    "</td><td><input type='text' value='".$row['examdate']."'name='examdate'>".
    "</td><td><input type='text' value='".$row['time']."'name='examtime'>".
    "</td><td><input type='text' value='".$row['courseid']."'name='courseid'>"."</td></tr>";
}
echo $string."<input type='submit' name='submit'value='Submit'>";
?>
