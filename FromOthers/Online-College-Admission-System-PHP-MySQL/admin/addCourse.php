<?php 
  session_start();
  $msg="";
	if(isset($_SESSION['email']))
	{
    $email = $_SESSION['email'];
    include 'variables.php';
    ?>
<html lang="en">
<head>
   <meta charset="utf-8">
   <meta http-equiv="X-UA-Compatible" content="IE=edge">
   <meta name="viewport" content="width=device-width, initial-scale=1">
   <title>Admin Area | Dashboard</title>
   <!-- Bootstrap core CSS -->
   <link href="css/bootstrap.min.css" rel="stylesheet">
   <link href="css/style.css" rel="stylesheet">
   <script src="http://cdn.ckeditor.com/4.6.1/standard/ckeditor.js"></script>
</head>
<body>
   <!-- Almost Common for Every Page -->
   <?php include'adminMenu.php' ?>
   <header id="header">
      <div class="container">
         <div class="row">
            <div class="col-md-10">
               <h1><span class="glyphicon glyphicon-cog" aria-hidden="true"></span> Add Courses </h1>
            </div>
         </div>
      </div>
      </div>
      </div>
   </header>
   <section id="breadcrumb">
      <div class="container">
         <ol class="breadcrumb">
            <li class="active">Courses</li>
         </ol>
      </div>
   </section>
   <?php include 'side-menu.php' ?>
   <div class="col-md-9">
      <!-- Website Overview -->
      <div class="panel panel-default">
         <div class="panel-heading main-color-bg">
            <h3 class="panel-title">Add Course</h3>
         </div>
         <div class="panel-body">
            <div class="row">
               <div class="col-md-10">
                  <form action="adminLoginHandler.php" method="post">
                  <div class="form-group">
                     <label>Enter Course Name</label>
							        <input type="text" name='cname' placeholder="Enter Course Name Here.." class="form-control" required>
						      </div>
                     <input class="btn btn-info" type="submit" name="addCourse" value="Submit">
                     </form>
                     <div class="panel panel-default">
            <div class="panel-heading">
               <h3 class="panel-title">Available Courses</h3>
            </div>
            <div class="panel-body">

            <table class="table table-striped table-hover table-bordered">
               <tr>
                 <th>ID</th>
                 <th>Name</th>
                 <th>Action</th>
               </tr>
             <?php
             $mysqli = mysqli_connect($databaseHost,$databaseUsername,$databasePassword,$databaseName);
             $query = "SELECT * FROM courses ORDER BY id";
             $result = mysqli_query($mysqli,$query);       
             while($addrow = mysqli_fetch_array($result)) 
               {
                 ?>
                 <tr>
                 <td><?php echo $addrow[0];?></td>
                 <td><?php echo $addrow[1];?></td>
                 <td> <a href='deleteCourse.php?id=<?php echo $addrow[0]; ?>'> Delete </a> </td>
                 </tr>
             <?php }
               ?>
            </table>
          </div>
      </div>

                  </div>
               </div>
               </div>
            </div>
         </div>
      </div>
   </div>
   </div>
   </div>
   </section>

   </html>
   <?php
  }
else{
?> 
  <?php
//echo 'not logged in ';
echo "<script language='javascript'>alert('Login to continue');
window.location.href='/Online-College-Admission-System-PHP-MySQL/admin';
       </script>";
}
?>
