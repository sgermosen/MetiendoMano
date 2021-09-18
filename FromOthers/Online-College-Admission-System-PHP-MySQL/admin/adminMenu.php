<?php

	if(isset($_SESSION['email']))
	{
        $email = $_SESSION['email'];
        include 'variables.php';        
        $mysqli = new mysqli($databaseHost,$databaseUsername,$databasePassword,$databaseName);
        $query = "SELECT * FROM admins WHERE email = '{$_SESSION['email']}'"; 
        $result = $mysqli->query($query) or die($mysqli->error);
		if($result->num_rows > 0) 
		{
			while($row = $result->fetch_assoc()) 
			{
				foreach($row as $val) 
				{
                    $details[] = $val;
        }
      }   
		}
	}
?>
    <nav class="navbar navbar-default">
    <div class="container">
      <div class="navbar-header">
        <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
          <span class="sr-only">Toggle navigation</span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
          <span class="icon-bar"></span>
        </button>
        <a class="navbar-brand" href="admin.php">Navrachna University Admin</a>
      </div>
      <div id="navbar" class="collapse navbar-collapse">
        <ul class="nav navbar-nav">
          <li><a href="admin.php">Dashboard</a></li>
          <li><a href="records.php">Records</a></li>
        </ul>
        <ul class="nav navbar-nav navbar-right">
          <li><a href="#">Welcome, <?php echo $details[1];?> </a></li>
          <li><a href="logout.php">Logout</a></li>
        </ul>
      </div><!--/.nav-collapse -->
    </div>
  </nav>