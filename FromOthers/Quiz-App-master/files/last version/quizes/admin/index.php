<?php

    session_start();

    $nonavbar = '' ;
    $pageTitle = 'Admin Login' ;
    

    if(isset($_SESSION['Admin_Username'])){
     
       header('Location: dashboard.php'); // Redirect to dashboard Page
       exit();
    }
    
    include 'init.php';
    


    // Checking if User coming from http Request

    if($_SERVER['REQUEST_METHOD'] == 'POST'){
        

        $email = $_POST['email'];
        $password = $_POST['pass'];
        $hashedPass = sha1($password);
        
        // Check IF The User Exist In The DB
        
        $stmt = $conn->prepare("SELECT 
                                        id , email , password , fname 
                                FROM 
                                        professor 
                                WHERE 
                                        email = ? 
                                AND 
                                        password = ? 
                                
                                LIMIT   1 ");
        
        $stmt->execute(array($email , $hashedPass));
        $row = $stmt->fetch();
        $count = $stmt->rowCount();
        
          
        
        // The User Exist If Count > 0 
        
        if($count > 0) {
			
			$username = $row['fname'] ;
			
            //echo 'Welcome '.$username;
            $_SESSION['Admin_Username'] = $username; // Register Session 
            $_SESSION['Admin_ID'] = $row['UserID'] ;
            header('Location: dashboard.php'); // Redirect to dashboard Page
            exit();
        }
        
    }

?>

    

    <form class="login" action="<?php echo $_SERVER['PHP_SELF'] ?>" method="POST">
        
        <input class="form-control" type="text" name="email" placeholder="Your Email" autocomplete="off" required />
        <input class="form-control" type=password name="pass" placeholder="Password" autocomplete="new-password" required />
        <input class="btn btn-primary btn-block" type="submit" value="Login" />    
        
        
    </form>





<?php 
    
    include $tpl.'footer.php';
?>