<?php
  session_start();
  include ("dbConnect.php");
  extract($_POST);
  if (isset($login)) {
    $rs = mysql_query("select * from account where Account_number = '$acNo'");
    if (mysql_num_rows($rs) < 1) {
      $found = "N";
    } else {
      $row = mysql_fetch_array($rs);
      $_SESSION['Account_number'] = $row['Account_number'];
      $_SESSION['Balance'] = $row['Balance'];
    }
  }
  if (isset($_SESSION['Account_number']) && isset($_SESSION['Balance'])) {
    header("Location: deposit2.php");
  }

?>


<!DOCTYPE html>
<html>
<head>
  <title>User Page</title>
  <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
  <script src="css/jquery.min.js"></script>
  <script src="css/bootstrap.min.js"></script>
</head>

<body>
   <div class="container">
     <nav class="navbar navbar-default">
      <div class="container-fluid">
        <div class="navbar-header">
          <a class="navbar-brand" href="index.php">Our Banking Management System</a>
        </div>
        <ul class="nav navbar-nav pull-right">
            <li><a href="user.php">Home</a></li>
                <li><a href="logout.php">Logout</a></li>
          
        </ul>
      </div>
     </nav>

     <div class="panel panel-default">
        <div class="panel-heading">
          <h2>User Login</h2>
        </div>

        <div class="panel-body">
          <div style="max-width: 600px; margin: 0 auto">
            <form action="" method="post">
              <div class="form-group">
                <label for="email">Enter Account Number</label>
                <input type="text" name="acNo" class="form-control" />
              </div>

              <?php
                if (isset($found)) {
                  echo "Incorrect Account Number" . "<br>";
                }
              ?>
              <button type="submit" name="login" class="btn btn-success">Submit</button>
            </form>
          </div>
        </div>
     </div>

     <div class="well">
      <h3>www.mycompany.com
         <span class="pull-right">Like Us: www.facebook.com/samy</span>
      </h3>
     </div>   
    
   </div>

</body>
</html>