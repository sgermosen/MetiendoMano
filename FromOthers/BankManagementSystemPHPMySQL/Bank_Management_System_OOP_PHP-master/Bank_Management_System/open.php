<?php
  session_start();

  include("dbConnect.php");
  include("Account.php");

  extract($_POST);

  if (isset($login)) {

    // $obCustomer = new Customer();
    $obAccount = new Account();
    // $sl_amnt = 0;
    $res1 = $obAccount->createCustomer($cName, $cStreet, $cCity);
    $res2 = $obAccount->openAccount($brName);
    $acc = $obAccount->getAcNumber();
    $res3 = mysql_query("INSERT INTO depositor(Customer_name, Account_number) VALUES('$cName', '$acc') "); 
    
    if($res1 && $res2 && $res3)
    {
        ?>
        <script>
        alert('Record inserted...');
        window.location='user.php'
        </script>
        <?php
    }
    else
    {
        ?>
        <script>
        alert('error inserting record...');
        window.location='user.php'
        </script>
        <?php
    }
  }

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
          <li><a href="user.php">Home</a></li>
          <li><a href="logout.php">Logout</a></li>
          
        </ul>
      </div>
     </nav>

     <div class="panel panel-default">
      <div class="panel-heading">
        <h2>Open Account</h2>
      </div>

      <div class="panel-body">
        <div style="max-width: 600px; margin: 0 auto">
        <form action="" method="post">
          <div class="form-group">
            <input type="text" name="cName" placeholder="Customer Name" required class="form-control" />
          </div>

          <div class="form-group">
            <input type="text" name="cStreet" placeholder="Customer Street" required class="form-control" />
          </div>

          <div class="form-group">
            <input type="text" name="cCity" placeholder="Customer City" required class="form-control" />
          </div>
          
          <div class="form-group"><p>Select a Branch
             <select name="brName" id="brid" style="width: 150px;box-sizing: border-box;border-radius: 4px;padding: 4px 0px 4px 0px;font-size: 16px;">
             <?php
               $rs=mysql_query("select * from branch order by Branch_name");
               while($row=mysql_fetch_array($rs))
               {
                 if($row[0]==$brName)
                 {
                   echo "<option value='$row[0]' selected>$row[0]</option>";
                 } else {
                   echo "<option value='$row[0]'>$row[0]</option>";
                 }
               }
             ?>
             </select></p>
          </div>

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