<?php  
include_once('main.php');
$sid=$_REQUEST['id'];
$attendmon = "SELECT * FROM students WHERE id='$sid'";
$resmon = mysql_query($attendmon);
echo "<tr><th>Child ID</th>
			  <th>Child Name</th>
			  <th>Child Phone</th>
			  <th>Child Email</th>
			  <th>Child Gender</th>
			  <th>Child DOB</th>
			  <th>Child Admission Date</th>
			  <th>Child Address</th>
			  <th>Child Parent ID</th>
			  <th> Child class ID</th>
			  <th> Child Image</th></tr>";
while($stinfo=mysql_fetch_array($resmon))
{
 echo "<tr><td>",$stinfo['id'],"</td>
			  <td>",$stinfo['name'],"</td>
			  <td>",$stinfo['phone'],"</td>
			  <td>",$stinfo['email'],"</td>
			  <td>",$stinfo['sex'],"</td>
			  <td>",$stinfo['dob'],"</td>
			  <td>",$stinfo['addmissiondate'],"</td>
			  <td>",$stinfo['address'],"</td>
			  <td>",$stinfo['parentid'],"</td>
			  <td>",$stinfo['classid'],"</td>";
			  echo "<td><img src='../images/",$stinfo['id'],".jpg' height='95' width='95' /></td>
			  </tr>";

}
?>
