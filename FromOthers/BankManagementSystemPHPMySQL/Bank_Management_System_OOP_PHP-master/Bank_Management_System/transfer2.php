<?php
  session_start();
  include_once 'Account.php';
  include("dbConnect.php");
?>
<?php
  if (isset($_POST['login2'])) {
    $obAccount2 = new Account();
    if($_SESSION['bal'] - $_POST['tMoney'] < 500){
      ?>
      <script>
      alert('Not Enough Balance to Transfer...');
      window.location='transfer.php'
      </script>
      <?php
    } else {
      $value = $_SESSION['bal'] - $_POST['tMoney'];
      $res1 = $obAccount2->updateBalance($_SESSION['aNum'], $value);

      $obAccount3 = new Account();
      $rs2 = $obAccount3->showBalance($_POST['payee']);

      if(mysql_num_rows($rs2) < 1) { 
      ?>
        <p>Invalid Account Number </p><a href="deposit.php">Try again!</a>
        <?php
        die(mysql_error()); // TODO: better error handling
      }       
      $row2=mysql_fetch_array($rs2);

      $value2 = $row2[2] + $_POST['tMoney'];
      $res2 = $obAccount2->updateBalance($_POST['payee'], $value2);
    
      if($res1 && $res2){
      ?>
        <script>
        alert('Transfer Succeed...');
        window.location='transfer.php'
        </script>
        <?php
      } else {
        ?>
        <script>
        alert('error updating balance...');
        window.location='transfer.php'
        </script>
        <?php
      }
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
        <h2>Deposit Money</h2>
      </div>

      <div class="panel-body">
        <div style="max-width: 600px; margin: 0 auto">
            <?php
               $obAccount = new Account();
               $acNum = $_POST['acNo'];
               $rs = $obAccount->showBalance($acNum);

               if(mysql_num_rows($rs) < 1) { 
                  ?>
                  <p>Invalid Account Number </p><a href="deposit.php">Try again!</a>
                  <?php
                  die(mysql_error()); // TODO: better error handling
               }       
               $rowa=mysql_fetch_array($rs);
            ?>
            
            <table class="table table-striped">
              <tr>
                  <td width="35%">Account Number: </td>
                  <th><?php echo $rowa[1]; $_SESSION['aNum'] = $rowa[1]; ?></th>                  
              </tr> 
              <tr>
                  <td width="35%">Customer Name: </td>
                  <th><?php echo $rowa[0]; ?></th>
              </tr> 
                
              <tr>
                <td width="35%">Balance: </td>
                <th><?php echo $rowa[2]; $curBal = $rowa[2]; $_SESSION['bal'] = $rowa[2]; ?></th>
              </tr>
              
            </table>

            <form action="" method="post">
             
              <div class="form-group">
                <label for="deposit">Enter Account Number of Payee</label>
                <input type="text" name="payee" required class="form-control" />
              </div>

              <div class="form-group">
                <label for="deposit">Enter Deposit Amount</label>
                <input type="text" name="tMoney" required class="form-control" />
              </div>
                
              <button type="submit" name="login2" class="btn btn-success">Send</button>
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