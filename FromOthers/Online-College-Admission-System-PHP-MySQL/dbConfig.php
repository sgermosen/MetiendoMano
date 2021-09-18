<?php
@ob_start();
session_start();
$path = "../profile_images";
// Database Connection

$databaseHost = "localhost";
$databaseUsername = "root";
$databasePassword = "";
$databaseName = "admission2018";
	
	$conn = new PDO("mysql:host=$databaseHost;dbname=$databaseName;", $databaseUsername, $databasePassword);	
	$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
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
	//Register


try
{
// PDO Style

$msg = " ";
// User Registeration Page
if (isset($_POST['addRegister']))
	{
	$fullname = $_REQUEST['fullname'];
	$gender = $_REQUEST['gender'];
	$bgroup = $_REQUEST['bgroup'];
	$address = $_REQUEST['address'];
	$city = $_REQUEST['city'];
	$state = $_REQUEST['state'];
	$zip = $_REQUEST['zip'];
	$pnumber = $_REQUEST['pnumber'];
	$email = $_REQUEST['email'];
	$password = $_REQUEST['password'];
	$dob=$_REQUEST['dob'];
		// profile image
	$profile_image=$_FILES['profile_image']['tmp_name'];
	$password2 = $_REQUEST['password2'];
	if ($password == $password2){
		$query = $conn->prepare( "SELECT `email` FROM `student_data` WHERE `email` = ?" );			
		$query->bindValue( 1, $email );
		$query->execute();
		if($query->rowCount() > 0 )
		{	
			$msg = "<p style='text-align:center; color:red;'>This Email ID is already registered. Try Login</p>";
		}
		else{	
			
			$password = md5($password);
			// PDO Style Insert
			$final_save_dir = 'profile_images/';
			move_uploaded_file($_FILES['profile_image']['tmp_name'], $final_save_dir . $_FILES['profile_image']['name']);
			$image = $final_save_dir . $_FILES['profile_image']['name'];

			$sql = "INSERT INTO `student_data` VALUES 
				(NULL,'$fullname','$gender','$bgroup','$address','$city','$state','$zip','$pnumber','$email','$password',NOW(),'$dob','$image',1)";
					if ($conn->query($sql))
					{
						$msg = "<p style='text-align:center; color:green;'>Registration Successful. You Can Login now</p>";
					}
					  else
					{
						$msg = "An Error Occured Contact SysAdmin";
					}
				}
			
			}
		}

	}


catch(PDOException $e)
{
		$msg =  "Connection failed: " . $e->getMessage();
	}
?>