<?php

session_start();
$pageTitle = 'Professor' ;


	if (isset($_SESSION['Admin_Username'])) { 


		include 'init.php' ;
		
		$rows = getProfessors() ;
		
		
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage'){ ?>
			
			<div class="container">
				<h2 style="color:#222222;"><i class="fa fa-edit"></i> Manage Professors</h2>
				<ul class="nav nav-tabs">
					<li class="active"><a data-toggle="tab" href="#home">
						<i class="fa fa-align-justify fa-lg"></i> Professors </a>
					</li>
					<li><a data-toggle="tab" href="#add-professor">
						<i class="fa fa-plus fa-lg"></i> Add Professor</a>
					</li>
					<li style="float:right"><a href="logout.php">Logout</a></li>
				</ul>
				<div class="tab-content">
					
					<div id="home" class="tab-pane fade in active">
						
						<h1 class="text-center">Manage Professors</h1>
						<div class="table-responsive">
							<table class="main-table text-center table table-bordered" id="exams-table">
								<tr>
									<td>ID</td>
									<td>Name</td>
									<td>Email</td>
									<td>Options</td>
									
								</tr>
								
								<?php
								
									foreach($rows as $row) {
										
										echo "<tr>" ;							
											echo "<td>" . $row['id'] . "</td>" ;
											echo "<td>" . $row['fname'] . ' ' .$row['lname'] . "</td>" ;
											echo "<td>" . $row['email'] . "</td>" ;
											echo "<td>
											
												
												
												<a href='prof.php?do=Edit&profID=".$row['id']."' class='btn btn-success'><i class='fa fa-edit fa-lg'>Edit</i></a>

												
												<a href='prof.php?do=Delete&profID=".$row['id']."' class='btn btn-danger confirm'><i class='fa fa-close fa-sm'>Delete</i></a> " ;
										
											

											
										
											echo "</td>" ;
										echo "</tr>" ;
									
									}
							
								?>
						
							</table>						
						</div>
					
						
						
					</div>
					<div id="add-professor" class="tab-pane fade">
						
						<h1 class="text-center">Add New Professor</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Insert" method="POST">
								
								
								<!-- Start Email Field -->
								<div class="form-group form-group-sm">
									<label class="col-sm-2 control-label">Email</label>
									<div class="col-sm-10 col-md-6">
										<input type="email" name="email" class="form-control" required="required" placeholder="Email Must Be Valid" />
									</div>
								</div>
								<!-- End Email Field -->
								
								<!-- Start Password Field -->
								<div class="form-group form-group-sm">
									<label class="col-sm-2 control-label">Password</label>
									<div class="col-sm-10 col-md-6">
										<input type="password" name="password" class="password form-control" required="required" autocomplete="new-password" placeholder="Password Must Be Hard & Complex" />

									</div>
								</div>
								<!-- End Password Field -->
								
								<!-- Start First Name Field -->
								<div class="form-group form-group-sm">
									<label class="col-sm-2 control-label">First Name</label>
									<div class="col-sm-10 col-md-6">
										<input type="text" name="fname" class="form-control" required="required" placeholder="First Name Appear In Your Profile Page" />
									</div>
								</div>
								<div class="form-group form-group-sm">
									<label class="col-sm-2 control-label">Last Name</label>
									<div class="col-sm-10 col-md-6">
										<input type="text" name="lname" class="form-control" required="required" placeholder="Last Name Appear In Your Profile Page" />
									</div>
								</div>
							
								<!-- End Full Name Field -->
								
								<!-- Start Submit Field -->
								<div class="form-group form-group-sm">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Add Professor" class="btn btn-primary btn-sm btn-sub" />
									</div>
								</div>
								<!-- End Submit Field -->
							</form>
						</div>

						
						

					
				</div>
		
			</div>
			
		 <?php }elseif($do == 'Insert') {
			
			
				
			if ($_SERVER['REQUEST_METHOD'] == 'POST') {

				echo "<h1 class='text-center'>Add Professor</h1>";
				echo "<div class='container'>";

				// Get Variables From The Form

				$pass 	= $_POST['password'];
				$email 	= $_POST['email'];
				$fname 	= $_POST['fname'];
				$lname 	= $_POST['lname'];
			
				$hashPass = sha1($_POST['password']);

				// Validate The Form

				$formErrors = array();

				

				if (empty($pass)) {
					$formErrors[] = 'Password Mustn\'t Be <strong>Empty</strong>';
				}

				if (empty($fname)) {
					$formErrors[] = 'First Name  Mustn\'t Be <strong>Empty</strong>';
				}

				if (empty($lname)) {
					$formErrors[] = 'Last Name Name  Mustn\'t Be <strong>Empty</strong>';
				}
				

				// Loop Into Errors Array And Echo It

				foreach($formErrors as $error) {
					echo '<div class="alert alert-danger">' . $error . '</div>';
				}

				// Check If There's No Error Proceed The Update Operation

				if (empty($formErrors)) {

					// Check If User Exist in Database

					$check = checkItem("email", "professor", $email);

					if ($check == 1) {

						$theMsg = '<div class="alert alert-danger">Sorry This Professor Is Exist</div>';

						redirect($theMsg, 'back');

					} else {

						// Insert Userinfo In Database

						$stmt = $conn->prepare("INSERT INTO 
													professor(email, password, fname, lname)
											   VALUES(:zemail, :zpass, 									:zfname,:zlname) ");
						$stmt->execute(array(

							
							'zemail' 	=> $email,
							'zpass' 	=> $hashPass,
							'zfname' 	=> $fname,
							'zlname' 	=> $lname
							

						));

						// Echo Success Message

						$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Inserted</div>';

						redirect($theMsg, 'prof.php');

					}

				}

			} else {

				echo "<div class='container'>";

				$theMsg = '<div class="alert alert-danger">Sorry You Cant Browse This Page Directly</div>';

				redirect($theMsg , 'prof.php' );

				echo "</div>";

			}

			echo "</div>";
			
				
			
			
			
				}
			
			
			
			
		
		
		elseif($do == 'Edit') {
			
			$professor_id = isset($_GET['profID']) && is_numeric($_GET['profID']) ? intval($_GET['profID']) : 0 ;
			$stmt = $conn->prepare("SELECT * FROM professor WHERE id = ? LIMIT 1");

			// Execute Query

			$stmt->execute(array($professor_id));

			// Fetch The Data

			$row = $stmt->fetch();

			// The Row Count

			$count = $stmt->rowCount();

			// If There's Such ID Show The Form
			
			
			if ($count > 0) { ?>
				
				
						<h1 class="text-center">Edit Professor Account</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Update" method="POST">
								<input type="hidden" name="professor-id" value="<?php echo $professor_id ?>" />
								
								<!-- Start Email Field -->
								<div class="form-group form-group-lg">
									<label class="col-sm-2 control-label">Email</label>
									<div class="col-sm-10 col-md-6">
										<input type="email" name="email" class="form-control" value="<?php echo $row['email'] ;?>" required="required" placeholder="Email Must Be Valid" />
									</div>
								</div>
								<!-- End Email Field -->
								
								<!-- Start Password Field -->
								<div class="form-group form-group-lg">
									<label class="col-sm-2 control-label">Password</label>
									<div class="col-sm-10 col-md-6">
										<input type="hidden" name="oldpassword" value="<?php echo $row['password'] ; ?>" />
										<input type="password" name="newpassword" class="form-control" autocomplete="new-password" placeholder="Leave Blank If You Dont Want To Change" />

									</div>
								</div>
								<!-- End Password Field -->
								
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
							
								<!-- End Full Name Field -->
								<!-- Start Submit Field -->
								<div class="form-group form-group-lg">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Edit Professor" class="btn btn-primary btn-lg" />
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
			
			
			echo "<h1 class='text-center'>Update Professor</h1>";
			echo "<div class='container'>";
			
			
			
			
			

			if ($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				$professor_id 	= $_POST['professor-id'];
				$email  		= $_POST['email'];
				$first_name     = $_POST['fname'];
				$last_name		= $_POST['lname'];
			
				
				$pass = empty($_POST['newpassword']) ? $_POST['oldpassword'] : sha1($_POST['newpassword']);
				
				$stmt = $conn->prepare("UPDATE 
											professor 
										SET 
											
											email    	= ?, 
											password 	= ?,
											fname    	= ?,
											lname    	= ?
											
											
										WHERE 
											id = ?");
				
				$stmt->execute(array($email , $pass , $first_name , $last_name ,
									$professor_id)) ;
				
		
				$theMsg = '<div class="alert alert-success">Done</div>';
				redirect($theMsg, 'prof.php');
		
		
				
				
				
			}else {
				
				$theMsg = '<div class="alert alert-danger">You Can\'t Browse This Page Directly</div>';
				redirect($theMsg, 'back');
				
			}

	
		}elseif($do =='Delete') {

			echo "<h1 class='text-center'>Delete Category</h1>";
			echo "<div class='container'>";

			$professor_id = isset($_GET['profID']) && is_numeric($_GET['profID']) ? intval($_GET['profID']) : 0 ;
			$check = checkItem('id', 'professor', $professor_id);

			if ($check > 0) {

				$stmt = $conn->prepare("DELETE FROM professor WHERE id = :zid");

				$stmt->bindParam(":zid", $professor_id);

				$stmt->execute();

				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Deleted</div>';

				redirect($theMsg, 'prof.php');

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

