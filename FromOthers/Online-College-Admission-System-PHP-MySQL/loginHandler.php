<?php

session_start();
$errmsg_array = array();
$errflag = false;

    $databaseHost = "localhost";
    $databaseUsername = "root";
    $databasePassword = "";
    $databaseName = "admission2018";

    $conn = new PDO("mysql:host=$databaseHost;dbname=$databaseName;", $databaseUsername, $databasePassword);
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    
if (isset($_POST['login'])) {
        $email = $_POST['email'];
        $password = $_POST['pass'];
        
        $password = md5($password);
    if ($email == '') {
            $errmsg_arr[] = 'You must enter your Email';
            $errflag = true;
    }
    if ($password == '') {
            $errmsg_arr[] = 'You must enter your Password';
            $errflag = true;
    }

        $result = $conn->prepare("SELECT * FROM student_data WHERE email=? AND password=?");
        $result->bindParam(1, $email);
        $result->bindParam(2, $password);
        $result->execute();
        $rows = $result->fetch(PDO::FETCH_NUM);
    if ($rows > 0) {
        $_SESSION['email'] = $email;
        header("Location: ../Online-College-Admission-System-PHP-MySQL/home/");
	}
    else {  
        echo "<script language='javascript'>alert('Email ID/Password not found');
        window.location.href='/Online-College-Admission-System-PHP-MySQL/#login-modal';
         </script>";
    }
        if($errflag) {
            $_SESSION['ERRMSG_ARR'] = $errmsg_arr;
            session_write_close();
            header("location: ../Online-College-Admission-System-PHP-MySQL/");
            exit();
        }    
    }
?>