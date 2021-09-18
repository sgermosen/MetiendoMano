<?php

	if(isset($_SESSION['email']))
	{
        $email = $_SESSION['email'];
        include 'variables.php';
               
        $mysqli = new mysqli($databaseHost,$databaseUsername,$databasePassword,$databaseName);
        $query = "SELECT count(*) FROM student_data"; 
        $q2 = "SELECT count(*) FROM courses";
        $q3 ="SELECT count(*) from education_information_be";
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
        // Count of Course Data
        $result2 = $mysqli->query($q2) or die($mysqli->error);
        if($result2->num_rows > 0) 
		{
			while($row2 = $result2->fetch_assoc()) 
			{
				foreach($row2 as $val2) 
				{
                    
                }
            }   
        }
        // Count of Be Students
        $result3 = $mysqli->query($q3) or die($mysqli->error);
        if($result3->num_rows > 0) 
		{
			while($row3 = $result3->fetch_assoc()) 
			{
				foreach($row3 as $val3) 
				{
                    
                }
            }   
        }

                // Count of Be Students
        $result3 = $mysqli->query($q3) or die($mysqli->error);
        if($result3->num_rows > 0) 
		{
			while($row3 = $result3->fetch_assoc()) 
			{
				foreach($row3 as $val3) 
				{
                    
                }
            }   
        }
	}
?>
<section id="main">
<div class="container">
<div class="row">
<div class="col-md-3">
    <div class="list-group">
        <a href="admin.php" class="list-group-item active main-color-bg">
        <span class="glyphicon glyphicon-cog" aria-hidden="true"></span> Sidemenu</a>
        <a href="allrecords.php" class="list-group-item"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Registered Student Records <span class="badge"><?php echo $val;?></span></a>
        <a href="records.php" class="list-group-item"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Applied Student Records <span class="badge"></span></a>
        <a href="be_records.php" class="list-group-item"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> BE Student Records <span class="badge"><?php echo $val3;?></span></a>
<!--     <a href="test-questions.php" class="list-group-item"><span class="glyphicon glyphicon-user" aria-hidden="true"></span> Test <span class="badge">203</span></a> -->
         <a href="addCourse.php" class="list-group-item"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span> Add Course <span class="badge"><?php echo $val2;?> </span></a>
         <a href="logout.php" class="list-group-item"><span class="glyphicon glyphicon-log-out" aria-hidden="true"></span> Logout <span class="badge"</span></a>
    </div>
</div>