<?php

session_start();
$pageTitle = 'Students' ;


	if (isset($_SESSION['Admin_Username'])) { 


		include 'init.php' ;
		
		$rows = getStudents() ;
		
		
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage'){ ?>
			
			<div class="container">
				<h2 style="color:gray;"><i class="fa fa-edit"></i> Manage Students</h2>
				<ul class="nav nav-tabs">
					<li class="active"><a data-toggle="tab" href="#home">
						<i class="fa fa-align-justify fa-lg"></i> Students </a>
					</li>
					<li><a data-toggle="tab" href="#add-student">
						<i class="fa fa-plus fa-lg"></i> Add Student</a>
					</li>
					<li style="float:right"><a href="logout.php">Logout</a></li>
				</ul>
				<div class="tab-content">
					
					<div id="home" class="tab-pane fade in active">
						
						<h1 class="text-center">Manage Students</h1>
						<div class="table-responsive">
							<table class="main-table text-center table table-bordered" id="exams-table">
								<tr>
									<td>CardID</td>
									<td>Name</td>
									<td>Email</td>
									<td>Grade</td>
									<td>Division</td>
									<td>Options</td>
									
								</tr>
								
								<?php
								
									foreach($rows as $row) {
										
										echo "<tr>" ;							
											echo "<td>" . $row['card_id'] . "</td>" ;
											echo "<td>" . $row['fname'] . ' ' .$row['lname'] . "</td>" ;
											echo "<td>" . $row['email'] . "</td>" ;
											echo "<td>" . $row['grade_name'] . "</td>" ;
											echo "<td>" . $row['division_name'] . "</td>" ;
											
											
											echo "<td>
											
												
												
												<a href='students.php?do=Edit&stuID=".$row['student_id']."' class='btn btn-success'><i class='fa fa-edit fa-lg'>Edit</i></a>

												
												<a href='students.php?do=Delete&stuID=".$row['student_id']."' class='btn btn-danger confirm'><i class='fa fa-close fa-sm'>Delete</i></a> " ;
										
											

											
										
											echo "</td>" ;
										echo "</tr>" ;
									
									}
							
								?>
						
							</table>						
						</div>
					
						
						
					</div>
					<div id="add-student" class="tab-pane fade">
						
						<h1 class="text-center">Add New Student</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Insert" method="POST">
								<!-- Start Username Field -->
								<div class="form-group form-group">
									<label class="col-sm-2 control-label">Card ID</label>
									<div class="col-sm-10 col-md-6">
										<input type="text" name="card-id" class="form-control" autocomplete="off" required="required" placeholder="ID To Login Into System" />
									</div>
								</div>
								<!-- End Username Field -->
								<!-- Start Password Field -->
								<div class="form-group form-group">
									<label class="col-sm-2 control-label">Password</label>
									<div class="col-sm-10 col-md-6">
										<input type="password" name="password" class="password form-control" required="required" autocomplete="new-password" placeholder="Password Must Be Hard & Complex" />

									</div>
								</div>
								<!-- End Password Field -->
								<!-- Start Email Field -->
								<div class="form-group form-group">
									<label class="col-sm-2 control-label">Email</label>
									<div class="col-sm-10 col-md-6">
										<input type="email" name="email" class="form-control" required="required" placeholder="Email Must Be Valid" />
									</div>
								</div>
								<!-- End Email Field -->
								<!-- Start Full Name Field -->
								<div class="form-group form-group">
									<label class="col-sm-2 control-label">First Name</label>
									<div class="col-sm-10 col-md-6">
										<input type="text" name="fname" class="form-control" required="required" placeholder="First Name Appear In Your Profile Page" />
									</div>
								</div>
								<div class="form-group form-group">
									<label class="col-sm-2 control-label">Last Name</label>
									<div class="col-sm-10 col-md-6">
										<input type="text" name="lname" class="form-control" required="required" placeholder="Last Name Appear In Your Profile Page" />
									</div>
								</div>
								<div class="form-group">
									<label class="col-sm-2 control-label">Grade</label>
									<div class="col-xs-2">
										<select class="form-control" name="stu-grade">
											<option value="0">Course Grade</option>
											<?php
												$coursesGrade = getAllFrom("*" , "grade" ,"","" ,"grade_id", "ASC");
												foreach($coursesGrade as $courseGrade) {
												echo "<option value='" 
													. $courseGrade['grade_id'] . "'>" . $courseGrade['grade_name'] . "</option>";
												}	
											
											?>
										</select>
									</div>
								</div>	
								
								<div class="form-group">
									<label class="col-sm-2 control-label"> Division</label>
									<div class="col-xs-2">
										<select class="form-control" name="stu-division">
											<option value="0">Course Division</option>
											<?php
												$coursesDivision = getAllFrom("*" , "division" ,"","" ,"division_id", "ASC");
												foreach($coursesDivision as $courseDivision) {
												echo "<option value='" 
													. $courseDivision['division_id'] . "'>" . $courseDivision['division_name'] . "</option>";
												}	
											
											?>
										</select>
									</div>
								</div>	
								
								<!-- End Full Name Field -->
								<!-- Start Submit Field -->
								<div class="form-group form-group">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Add Student" class="btn btn-primary btn-sub" />
									</div>
								</div>
								<!-- End Submit Field -->
							</form>
						</div>

						
						

					
				</div>
		
			</div>
			
		 <?php }elseif($do == 'Insert') {
			
			
				
			if ($_SERVER['REQUEST_METHOD'] == 'POST') {

				echo "<h1 class='text-center'>Insert Student</h1>";
				echo "<div class='container'>";

				// Get Variables From The Form

				$card_id 	= $_POST['card-id'];
				$pass 	= $_POST['password'];
				$email 	= $_POST['email'];
				$fname 	= $_POST['fname'];
				$lname 	= $_POST['lname'];
				$grade   = $_POST['stu-grade'];
				$division   = $_POST['stu-division'];

				$hashPass = sha1($_POST['password']);

				// Validate The Form

				$formErrors = array();

				if (strlen($card_id) < 4) {
					$formErrors[] = 'Card ID Mustn\'t Be Less Than <strong>4 Characters</strong>';
				}

				if (strlen($card_id) > 5) {
					$formErrors[] = 'Card ID Mustn\'t Be More Than <strong>20 Characters</strong>';
				}

				if (empty($card_id)) {
					$formErrors[] = 'Card ID Mustn\'t Be <strong>Empty</strong>';
				}

				if (empty($pass)) {
					$formErrors[] = 'Password Mustn\'t Be <strong>Empty</strong>';
				}

				if (empty($fname)) {
					$formErrors[] = 'First Name  Mustn\'t Be <strong>Empty</strong>';
				}

				if (empty($lname)) {
					$formErrors[] = 'Last Name Name  Mustn\'t Be <strong>Empty</strong>';
				}
				if ($grade == 0) {
					$formErrors[] = 'You Must Choose the <strong>Grade</strong>';
				}
				
				if ($division == 0) {
					$formErrors[] = 'You Must Choose the <strong>Grad</strong>';
				}

				// Loop Into Errors Array And Echo It

				foreach($formErrors as $error) {
					echo '<div class="alert alert-danger">' . $error . '</div>';
				}

				// Check If There's No Error Proceed The Update Operation

				if (empty($formErrors)) {

					// Check If User Exist in Database

					$check = checkItem("card_id", "student", $card_id);

					if ($check == 1) {

						$theMsg = '<div class="alert alert-danger">Sorry This Student Is Exist</div>';

						redirect($theMsg, 'back');

					} else {

						// Insert Userinfo In Database

						$stmt = $conn->prepare("INSERT INTO 
													student(card_id, email, password, fname, lname, grade_id , division_id)
											   VALUES(:zid, :zemail, :zpass, 									:zfname,:zlname,:zgrade,:zdivision) ");
						$stmt->execute(array(

							'zid' 		=> $card_id,
							'zemail' 	=> $email,
							'zpass' 	=> $hashPass,
							'zfname' 	=> $fname,
							'zlname' 	=> $lname,
							'zgrade' 	=> $grade,
							'zdivision' => $division

						));

						// Echo Success Message

						$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Inserted</div>';

						redirect($theMsg, 'students.php');

					}

				}

			} else {

				echo "<div class='container'>";

				$theMsg = '<div class="alert alert-danger">Sorry You Cant Browse This Page Directly</div>';

				redirectHome($theMsg);

				echo "</div>";

			}

			echo "</div>";
			
				
			
			
			
				}
			
			
			
			
		
		
		elseif($do == 'Edit') {
			
			$student_id = isset($_GET['stuID']) && is_numeric($_GET['stuID']) ? intval($_GET['stuID']) : 0 ;
			$stmt = $conn->prepare("SELECT * FROM student WHERE student_id = ? LIMIT 1");

			// Execute Query

			$stmt->execute(array($student_id));

			// Fetch The Data

			$row = $stmt->fetch();

			// The Row Count

			$count = $stmt->rowCount();

			// If There's Such ID Show The Form
			
			
			if ($count > 0) { ?>
				
				
						<h1 class="text-center">Edit Student</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Update" method="POST">
								<input type="hidden" name="student-id" value="<?php echo $student_id ?>" />
								<!-- Start Username Field -->
								<div class="form-group form-group-lg">
									<label class="col-sm-2 control-label">Card ID</label>
									<div class="col-sm-10 col-md-6">
										<input type="text" name="card-id" class="form-control" value="<?php echo $row['card_id'] ?>" autocomplete="off" required="required" placeholder="ID To Login Into System" />
									</div>
								</div>
								<!-- End Username Field -->
								<!-- Start Password Field -->
								<div class="form-group form-group-lg">
									<label class="col-sm-2 control-label">Password</label>
									<div class="col-sm-10 col-md-6">
										<input type="hidden" name="oldpassword" value="<?php echo $row['password'] ; ?>" />
										<input type="password" name="newpassword" class="form-control" autocomplete="new-password" placeholder="Leave Blank If You Dont Want To Change" />

									</div>
								</div>
								<!-- End Password Field -->
								<!-- Start Email Field -->
								<div class="form-group form-group-lg">
									<label class="col-sm-2 control-label">Email</label>
									<div class="col-sm-10 col-md-6">
										<input type="email" name="email" class="form-control" value="<?php echo $row['email'] ;?>" required="required" placeholder="Email Must Be Valid" />
									</div>
								</div>
								<!-- End Email Field -->
								<!-- Start Full Name Field -->
								<div class="form-group form-group-lg">
									<label class="col-sm-2 control-label">First Name</label>
									<div class="col-sm-10 col-md-6">
										<input type="text" name="fname" class="form-control" value="<?php echo $row['fname'] ;?>" required="required" placeholder="First Name Appear In Your Profile Page" />
									</div>
								</div>
								<div class="form-group form-group-lg">
									<label class="col-sm-2 control-label">Last Name</label>
									<div class="col-sm-10 col-md-6">
										<input type="text" name="lname" class="form-control" value="<?php echo $row['lname'] ;?>" required="required" placeholder="Last Name Appear In Your Profile Page" />
									</div>
								</div>
								<div class="form-group">
									<label class="col-sm-2 control-label">Grade</label>
									<div class="col-xs-2">
										<select class="form-control" name="stu-grade">
											<option value="0">Course Grade</option>
											<?php
												$coursesGrade = getAllFrom("*" , "grade" ,"","" ,"grade_id", "ASC");
												foreach($coursesGrade as $courseGrade) {
												echo "<option value='" 
													. $courseGrade['grade_id'] . "'>" . $courseGrade['grade_name'] . "</option>";
												}	
											
											?>
										</select>
									</div>
								</div>	
								
								<div class="form-group">
									<label class="col-sm-2 control-label"> Division</label>
									<div class="col-xs-2">
										<select class="form-control" name="stu-division">
											<option value="0">Course Division</option>
											<?php
												$coursesDivision = getAllFrom("*" , "division" ,"","" ,"division_id", "ASC");
												foreach($coursesDivision as $courseDivision) {
												echo "<option value='" 
													. $courseDivision['division_id'] . "'>" . $courseDivision['division_name'] . "</option>";
												}	
											
											?>
										</select>
									</div>
								</div>	
								
								<!-- End Full Name Field -->
								<!-- Start Submit Field -->
								<div class="form-group form-group-lg">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Edit Student" class="btn btn-primary btn-lg" />
									</div>
								</div>
								<!-- End Submit Field -->
							</form>
						</div>

				
				
				

				
			<?php

			// If There's No Such ID Show Error Message

			} else {

				echo "<div class='container'>";

				$theMsg = '<div class="alert alert-danger">Theres No Such ID</div>';

				redirectHome($theMsg);

				echo "</div>";

			}

			
			
			
			

		}
		
			
			
			
		
		
		
		elseif($do == 'Update') {
			
			
			echo "<h1 class='text-center'>Update Student</h1>";
			echo "<div class='container'>";
			
			
			
			
			

			if ($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				$student_id 	= $_POST['student-id'];
				$card_id 		= $_POST['card-id'];
				$email  		= $_POST['email'];
				$first_name     = $_POST['fname'];
				$last_name		= $_POST['lname'];
				$grade 			= $_POST['stu-grade'];
				$division 		= $_POST['stu-division'];
				
				$pass = empty($_POST['newpassword']) ? $_POST['oldpassword'] : sha1($_POST['newpassword']);
				
				$stmt = $conn->prepare("UPDATE 
											student 
										SET 
											card_id  	= ?, 
											email    	= ?, 
											password 	= ?,
											fname    	= ?,
											lname    	= ?,
											grade_id 	= ?,
											division_id = ?
											
										WHERE 
											student_id = ?");
				
				$stmt->execute(array($card_id , $email , $pass , $first_name , $last_name , $grade , $division ,
									$student_id)) ;
				
		
				$theMsg = '<div class="alert alert-success">Done</div>';
				redirect($theMsg, 'students.php');
		
		
				
				
				
			}else {
				
				$theMsg = '<div class="alert alert-danger">You Can\'t Browse This Page Directly</div>';
				redirect($theMsg, 'back');
				
			}

	
		}elseif($do =='Delete') {

			echo "<h1 class='text-center'>Delete Student</h1>";
			echo "<div class='container'>";

			$student_id = isset($_GET['stuID']) && is_numeric($_GET['stuID']) ? intval($_GET['stuID']) : 0;
			$check = checkItem('student_id', 'student', $student_id);

			if ($check > 0) {

				$stmt = $conn->prepare("DELETE FROM student WHERE student_id = :zid");

				$stmt->bindParam(":zid", $student_id);

				$stmt->execute();

				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Deleted</div>';

				redirect($theMsg, 'students.php');

			} else {

				$theMsg = '<div class="alert alert-danger">This ID is Not Exist</div>';

				redirect($theMsg);

			}

			echo '</div>';

		}
	
	}
		

		
		else {
		header('Location: index.php');
		exit();	
		}




include $tpl.'footer.php' ;

?>

