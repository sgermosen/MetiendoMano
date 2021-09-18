<?php
  session_start();
  include_once 'Account.php';
  include("dbConnect.php");
?>

<!DOCTYPE html>
<html>
<head>
  <title>Login Page</title>
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
            <li><a href="admin2.php">Home</a></li>
                <li><a href="logout.php">Logout</a></li>
          
        </ul>
      </div>
     </nav>

     <div class="panel panel-default">
      <div class="panel-heading">
        <h2>Account's Information</h2>
      </div>

      <div class="panel-body">
            <?php
               $obAccount = new Account();
               
               $rs2 = $obAccount->showAllAccountInfo();

               if(mysql_num_rows($rs2) < 1) { 
                  ?>
                  <p>Invalid Account Number </p><a href="deposit.php">Try again!</a>
                  <?php
                  die(mysql_error()); // TODO: better error handling
               } ?>

              <table class="table table-striped">
                <tr>
                  <td>Account Number</td>
                  <td>Customer Name</td>
                  <td>Customer Street</td>
                  <td>Customer City</td>
                  <td>Balance</td>
                  <td>Branch Name</td>
                  <td>Branch City</td>                  
                </tr>
          <?php  while ($row2=mysql_fetch_array($rs2)){
            ?>     
              <tr>
                  <th><?php echo $row2[1]; ?></th>
                  <th><?php echo $row2[0]; ?></th>
                  <th><?php echo $row2[2]; ?></th>
                  <th><?php echo $row2[3]; ?></th>
                  <th><?php echo $row2[4]; ?></th>
                  <th><?php echo $row2[5]; ?></th>
                  <th><?php echo $row2[6]; ?></th>
              </tr>
              <?php } ?>
            </table>
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