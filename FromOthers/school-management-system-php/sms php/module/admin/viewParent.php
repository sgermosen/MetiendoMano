<?php
include_once('main.php');
include_once('../../service/mysqlcon.php');
$sql = "SELECT * FROM parents;";
$res= mysql_query($sql);
$string = "";
while($row = mysql_fetch_array($res)){
    $string .= '<tr><td>'.$row['id'].'</td><td>'.$row['password'].
    '</td><td>'.$row['fathername'].'</td><td>'.$row['mothername'].
    '</td><td>'.$row['fatherphone'].'</td><td>'.$row['motherphone'].
    '</td><td>'.$row['address'].'</td></tr>';
}
?>
<html>
    <head>
		    <link rel="stylesheet" type="text/css" href="../../source/CSS/style.css">
				<script src = "JS/login_logout.js"></script>
        <script src = "JS/searchParent.js"></script>
		</head>
    <body>
			  <div class="header"><h1>School Management System</h1></div>
			  <div class="divtopcorner">
				    <img src="../../source/logo.jpg" height="150" width="150" alt="School Management System"/>
				</div>
			<br/><br/>
				<ul>
				    <li class="manulist">
						    <a class ="menulista" href="index.php">Home</a>
								<a class ="menulista" href="manageStudent.php">Manage Student</a>
								<a class ="menulista" href="manageTeacher.php">Manage Teacher</a>
								<a class ="menulista" href="manageParent.php">Manage Parent</a>
								<a class ="menulista" href="manageStaff.php">Manage Staff</a>
								<a class ="menulista" href="course.php">Course</a>
								<a class ="menulista" href="index.php">Attendance</a>
								<a class ="menulista" href="index.php">Exam Schedule</a>
								<a class ="menulista" href="index.php">Salary</a>
								<a class ="menulista" href="index.php">Report</a>
								<a class ="menulista" href="index.php">Payment</a>
								<div align="center">
								<h4>Hi!admin <?php echo $check." ";?></h4>
								<a class ="menulista" href="logout.php" onmouseover="changemouseover(this);" onmouseout="changemouseout(this,'<?php echo ucfirst($loged_user_name);?>');"><?php echo "Logout";?></a>
						</div>
								</li>
				</ul>
			  <hr/>
        <center>
            <table>
                <tr>
                    <td><b>Search By Id Or Name: </b></td>
                    <td><input type="text" name="searchId" placeholder="Search By Id Or Name:" onkeyup="getParent(this.value);"></td>
                </tr>
            </table>
        </center>
        <br/>
        <center><h2>Parent List</h2></center>
        <center>
            <table border="1" id='parentList'>
                <tr>
                    <th>ID</th>
                    <th>Password</th>
                    <th>Father Name</th>
                    <th>Mother Name</th>
                    <th>Father Phone</th>
                    <th>Mother Phone</th>
                    <th>Address</th>
                </tr>
                <?php echo $string;?>
            </table>
        </center>
		</body>
</html>
