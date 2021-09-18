<?php
session_start();
$sid=0;
if(isset($_SESSION['email']))
{
	$id=$_GET['id'];
	include 'variables.php';
	$mysqli = new mysqli($databaseHost,$databaseUsername,$databasePassword,$databaseName);

	$query = "SELECT * FROM student_data WHERE id =$id"; 
	$result = $mysqli->query($query) or die($mysqli->error);
	if($result->num_rows > 0) {
		while($row = $result->fetch_assoc()) {
			foreach($row as $val) {
				$details[] = $val;
			}
			$sid=$details[0];
		}   
	}
	if($id==$sid){
		?>
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">

		<!-- Jquery -->
		<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
		<link rel="stylesheet" href="/resources/demos/style.css">
		<title>Basic Profile</title>
		<body>
			<div class="container">
				<h1 class="well" style="text-align: center;">Student Detail</h1>
				<div class="col-lg-12 well">
					<div class="row">
						<div class="col-sm-12">
							<div class="row">
								<div class="col-sm-6 form-group">
									<label>Fullname</label>
									<input type="text" class="form-control" value="<?php echo $details[1]; ?>" readonly>
								</div>

								<div class="col-sm-6 form-group">
									<label>Image</label>
									<img src="<?php echo '../' . $details[13];?>" height="200" width="200">
								</div>
							</div>
							<div class="row">
								<div class="col-sm-6 form-group">
									<label>Gender</label>
									<input type="text" class="form-control" value="<?php echo $details[2]; ?> " readonly>
								</div>
								<div class="col-sm-6 form-group">
									<label>Blood Group</label>
									<input type="text" class="form-control" value="<?php echo $details[3]; ?> " readonly>
								</div>
							</div>
							<div class="form-group">
								<label>Address</label>
								<textarea name="address" rows="3" class="form-control" readonly> <?php echo $details[4]; ?> </textarea>
							</div>
							<div class="row">
								<div class="col-sm-3 form-group">
									<label>Date of Birth</label>
									<input type="text" class="form-control" value="<?php echo $details[12]; ?> " readonly>
								</div>
								<div class="col-sm-3 form-group">
									<label>City</label>
									<input type="text" class="form-control" value="<?php echo $details[5]; ?> " readonly>
								</div>
								<div class="col-sm-3 form-group">
									<label>State</label>
									<input type="text" class="form-control" value="<?php echo $details[6]; ?> " readonly>
								</div>
								<div class="col-sm-3 form-group">
									<label>Zip</label>
									<input type="text" class="form-control" value="<?php echo $details[7]; ?> " readonly>
								</div>
							</div>
							<div class="row">
								<div class="col-sm-4 form-group">
									<label>Phone Number</label>
									<input type="text" class="form-control" value="<?php echo $details[8]; ?> " readonly>
								</div>
								<div class="col-sm-4 form-group">
									<label>Email Address</label>
									<input type="text" class="form-control" value="<?php echo $details[9]; ?> " readonly>
								</div>
								<div class="col-sm-4 form-group">
									<label>Registration Date</label>
									<input type="text" name="reg_date" class="form-control" value="<?php echo $details[11]; ?> " readonly>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
<!-- Education Details -->			
<!--			<div class="container">
				<h1 class="well" style="text-align: center;">Educational Detail</h1>
				<div class="col-lg-12 well">
					<div class="row">
						<div class="col-sm-12">
							<div class="row">
								<div class="col-sm-12 form-group">
									<label>SSC</label>
								</div>

							</div>
						</div>
					</div>
				</div>
			</div>-->

		</body>
		</html>
		<?php
	}
	else{
		?>
		<?php 
		echo '<title> Error </title>
		<div class="container" style="margin:150px;">
		<h1 class="well" style="text-align: center;color: red !important;">Error : Invalid Student ID</h1>
		<h3 class="well" style="text-align: center;"><a href="view.php?id=1" style="color: #000080 !important;">Click here to return ID 1</a></h3>';
	}
	?>    
	<?php
}
else{
	?>
	<?php	
	echo "<script language='javascript'>alert('Login to continue');
	window.location.href='/Online-College-Admission-System-PHP-MySQL/admin';
	</script>";

}
?>