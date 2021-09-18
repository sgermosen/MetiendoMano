<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Online Attendance Management System</title>
	<!-- BOOTSTRAP STYLES-->
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
     <!-- FONTAWESOME STYLES-->
    <link href="assets/css/font-awesome.css" rel="stylesheet" />
     <!-- MORRIS CHART STYLES-->
   
        <!-- CUSTOM STYLES-->
    <link href="assets/css/custom.css" rel="stylesheet" />
     <!-- GOOGLE FONTS-->
   <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
     <!-- TABLE STYLES-->
    <link href="assets/js/dataTables/dataTables.bootstrap.css" rel="stylesheet" />
<?php
        include './Database/Controler.php';
?>
</head>
<body style="border:20px solid #1e1e90">
    <b><center>
    <font phase="Times New Roman"color="black" size="20%"><br>
    Online Attendance Management System   </b><br>
    </font>
    <center><br>
	<br>
        <img src="img/logossb.png" ><br><br>
	<br>
	
    <font phase="Times New Roman"color="white" size="5%">
    <div style="background-color: #357ebd; height:6%;border-radius: 5px; width:16%; margin-left:1%;  align:left;">  
    <b>Sign In</b>
	
    </font><br>  </center>

    <div style="background-color: white; height:30%; width:20%; margin-top:0%; margin-right:-10;  align:left; 
         padding: 1%">  
       <form method="post" role="form"  enctype="multipart/form-data" >
        
       <br> <input type="text"  id="unm" name="unm" placeholder="Enter Username" class="form-control" required autofocus><br>
       <input type="password"  id="pwd" name="pwd" placeholder="Enter password" class="form-control" required><br>
       <button type="submit" id="login" name="login" value="submit" class="btn btn-primary" style="width:50%">LogIn
       </button>
       </div>
    </div>  
    </form>
<!-- JQUERY SCRIPTS -->
    <script src="assets/js/jquery-1.10.2.js"></script>
      <!-- BOOTSTRAP SCRIPTS -->
    <script src="assets/js/bootstrap.min.js"></script>
    <!-- METISMENU SCRIPTS -->
    <script src="assets/js/jquery.metisMenu.js"></script>
      <!-- CUSTOM SCRIPTS -->
    <script src="assets/js/custom.js"></script>
    
</body>
</html>