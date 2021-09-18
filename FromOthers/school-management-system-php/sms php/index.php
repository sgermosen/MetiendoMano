<?php
$login_code= isset($_REQUEST['login']) ? $_REQUEST['login'] : '1';
if($login_code=="false"){
    $login_message="Wrong Credentials !";
	  $color="red";
}
else{
    $login_message="<br><br><br><br>Please Login (Admin, Teachers, Students, Parents)";
	  $color="green";
}
?>
<!DOCTYPE html>
<html >
    <head>
        <meta charset="UTF-8">
	      <script src="source/js/loginValidate.js"></script>
        <title>School Management System | VetBosSel</title>
    </head>
    <body>
        <center>
	          <img src="source/ssblogo.png" />
	          <hr/>
            <?php echo "<font size='4' color='$color'>$login_message</font>";?>
            <form  action="service/check.access.php" onsubmit="return loginValidate();" method="post"><br/>
                <input type="text" id="myid" name="myid" placeholder="Your Id" style="width: 250px;height: 40px;border-radius: 10px" autofocus=""   /><br/><br/><br/>
                <input type="password" id="mypassword" name="mypassword" placeholder="Your Password" style="width: 250px;height: 40px;border-radius: 10px"  /><br/><br/><br/>
                <input type="submit" value="Login" style="width: 120px;height: 40px;border-radius: 10px;background-color: blue;color: white" />
            </form>
        </center>
    </body>
</html>
