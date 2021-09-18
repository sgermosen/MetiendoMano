<?php 
	session_start();
	if(isset($_SESSION['email']))
	{
    include 'variables.php';
    $mysqli = new mysqli($databaseHost,$databaseUsername,$databasePassword,$databaseName);
    $query = "SELECT count(*) FROM student_data"; 
    // Count of Student Data
    $result = $mysqli->query($query) or die($mysqli->error);
    if($result->num_rows > 0) 
    {
      while($row = $result->fetch_assoc()) 
      {
        foreach($row as $val) 
        {
                
          }
        }   
    }
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
                  <h1><span class="glyphicon glyphicon-cog" aria-hidden="true"></span> Dashboard </h1>
               </div>
            </div>
         </div>
         </div>
         </div>
      </header>
      <section id="breadcrumb">
         <div class="container">
            <ol class="breadcrumb">
               <li class="active">Dashboard</li>
            </ol>
         </div>
      </section>
      <?php include 'side-menu.php' ?>
      <div class="col-md-9">
         <!-- Website Overview -->
         <div class="panel panel-default">
            <div class="panel-heading main-color-bg">
               <h3 class="panel-title"> Overview</h3>
            </div>
            <div class="panel-body">
               <div class="col-md-12">
                  <div class="well dash-box">
                     <h2><span class="glyphicon glyphicon-user" aria-hidden="true"></span>                 
                      <?php echo $val; ?>
                     </h2>
                     <h4>Admissions</h4>
                  </div>
               </div>
            </div>
         </div>
         <!-- Latest Users -->
         <div class="panel panel-default">
            <div class="panel-heading">
               <h3 class="panel-title">Recent Admissions</h3>
            </div>
            <div class="panel-body">
               <table class="table table-striped table-hover table-bordered">
                  <tr>
                    <th>ID</th>
                     <th>Name</th>
                     <th>Email</th>
                     <th>Joined</th>
                  </tr>
                <?php
                $mysqli = mysqli_connect($databaseHost,$databaseUsername,$databasePassword,$databaseName);
                $query = "SELECT * FROM student_data ORDER BY id DESC LIMIT 5 ";
                $result = mysqli_query($mysqli,$query);       
                while($addrow = mysqli_fetch_array($result)) 
                  {
                    $newDate = date('d-M-Y',strtotime($addrow[11]));
                    echo "<tr>";
                    echo "<td>$addrow[0]</td>";
                    echo "<td>$addrow[1]</td>";
                    echo "<td>$addrow[9]</td>";
                    echo "<td>$newDate</td>";
                    echo "</tr>";

                  }
                  ?>
               </table>
             </div>
         </div>
      </div>
      </div>
      </div>
      </section>
      <!-- Modals -->
      <!-- Add Page -->
      <div class="modal fade" id="addPage" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
         <div class="modal-dialog" role="document">
            <div class="modal-content">
               <form>
                  <div class="modal-header">
                     <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                     <h4 class="modal-title" id="myModalLabel">Add Page</h4>
                  </div>
                  <div class="modal-body">
                     <div class="form-group">
                        <label>Page Title</label>
                        <input type="text" class="form-control" placeholder="Page Title">
                     </div>
                     <div class="form-group">
                        <label>Page Body</label>
                        <textarea name="editor1" class="form-control" placeholder="Page Body"></textarea>
                     </div>
                     <div class="checkbox">
                        <label>
                        <input type="checkbox"> Published
                        </label>
                     </div>
                     <div class="form-group">
                        <label>Meta Tags</label>
                        <input type="text" class="form-control" placeholder="Add Some Tags...">
                     </div>
                     <div class="form-group">
                        <label>Meta Description</label>
                        <input type="text" class="form-control" placeholder="Add Meta Description...">
                     </div>
                  </div>
                  <div class="modal-footer">
                     <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                     <button type="submit" class="btn btn-primary">Save changes</button>
                  </div>
               </form>
            </div>
         </div>
      </div>
      <!-- Bootstrap core JavaScript
         ================================================== -->
      <!-- Placed at the end of the document so the pages load faster -->
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
      <script src="js/bootstrap.min.js"></script>
   </body>
</html>
<?php 
  } 
  else
    {
      echo "<script language='javascript'>alert('Login to continue');
			window.location.href='/Online-College-Admission-System-PHP-MySQL/admin';
             </script>";  }
?>