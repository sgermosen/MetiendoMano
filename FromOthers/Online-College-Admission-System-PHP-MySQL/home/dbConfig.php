<?php
@ob_start();
session_start();

// Database Connection
include 'variables.php';
//Register
try
	{
	// PDO Style
	$conn = new PDO("mysql:host=$databaseHost;dbname=$databaseName;", $databaseUsername, $databasePassword);
	$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	// User Registeration Page
	if (isset($_POST['addRegister']))
		{
		$fullname = $_POST['fullname'];
		$gender = $_POST['gender'];
		$bgroup = $_POST['bgroup'];
		$address = $_POST['address'];
		$city = $_POST['city'];
		$state = $_POST['state'];
		$zip = $_POST['zip'];
		$pnumber = $_POST['pnumber'];
		$email = $_POST['email'];
		$password = $_POST['password'];
		$password2 = $_POST['password2'];
		if ($password == $password2)
			{
			$password = md5($password);
			// PDO Style Insert
			$sql = "INSERT INTO `student_data` VALUES 
				(NULL,'$fullname','$gender','$bgroup','$address','$city','$state','$zip','$pnumber','$email','$password')";
			if ($conn->query($sql))
				{
				echo "Registration Successful";
				}
			  else
				{
				echo "An Error Occured Contact SysAdmin";
				}
			}
		}
	}

	catch(PDOException $e)
		{
			echo "Connection failed: " . $e->getMessage();
		}
	
	// User Login
	if(isset($_POST['login']))
	{
		$email = $_POST['email'];
		$password = $_POST['pass'];

		if(empty($email) && empty($password))
		{
			// Do Nothing
		}
		else
		{
			$sql = "SELECT FULLNAME,EMAIL,PASSWORD FROM `student_data`";
		}
	}

?>
