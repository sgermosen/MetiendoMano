<?php

session_start();
$pageTitle = 'Courses' ;


	if (isset($_SESSION['Admin_Username'])) { 


		include 'init.php' ;
		
		$rows = getCourses() ;
		
		
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage'){ ?>
			
			<div class="container">
				<h2 style="color:gray;"><i class="fa fa-edit"></i> Manage Courses</h2>
				<ul class="nav nav-tabs">
					<li class="active"><a data-toggle="tab" href="#home">
						<i class="fa fa-align-justify fa-lg"></i> Courses List</a>
					</li>
					<li><a data-toggle="tab" href="#add-course">
						<i class="fa fa-plus fa-lg"></i> Add Course</a>
					</li>
					<li style="float:right"><a href="logout.php">Logout</a></li>
				</ul>
				<div class="tab-content">
					
					<div id="home" class="tab-pane fade in active">
						
						<h1 class="text-center">Manage Courses</h1>
						<div class="table-responsive">
							<table class="main-table text-center table table-bordered" id="exams-table">
								<tr>
									<td>Course Name</td>
									<td>Division</td>
									<td>Grade</td>
									<td>Options</td>
									
								</tr>
								
								<?php
								
									foreach($rows as $row) {
										
										echo "<tr>" ;							
											echo "<td>" . $row['course_name'] . "</td>" ;
											echo "<td>" . $row['division_name'] . "</td>" ;
											echo "<td>" . $row['grade_name'] . "</td>" ;
											
											echo "<td>
											
												
												
												<a href='courses.php?do=Edit&courseID=".$row['course_id']."' class='btn btn-success'><i class='fa fa-edit fa-lg'>Edit</i></a>

												
												<a href='courses.php?do=Delete&courseID=".$row['course_id']."' class='btn btn-danger confirm'><i class='fa fa-close fa-sm'>Delete</i></a> " ;
										
											

											
										
											echo "</td>" ;
										echo "</tr>" ;
									
									}
							
								?>
						
							</table>						
						</div>
					
						
						
					</div>
				
					
					<div id="add-course" class="tab-pane fade">
						
						<h1 class="text-center page-title">Add New Course</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Insert" method="POST">
							
								
								
								<!--Course Name-->
								
								<div class="form-group">
									
									<label class="col-sm-2 control-label">Course Name</label>
									<div class="col-xs-2">
										<input type="text" name="course-name" class="form-control" autocomplete="off" placeholder="Course Name" required="required" />
									</div>
								</div>
								
								<!-- Ending Course Name-->
								
								
									<!--Courese Grade-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Course Grade</label>
									<div class="col-xs-2">
										<select class="form-control" name="course-grade">
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
								
                	<!--Courese Division-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Course Division</label>
									<div class="col-xs-2">
										<select class="form-control" name="course-division">
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

								
								 <div class="form-group">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Add Course" class="btn btn-primary btn-sub" />
									</div>
							   </div>
								
					
          <!--Ending Add Course Page	-->
					</div>
					

				</div>
		
			</div>
			
		 <?php }elseif($do == 'Insert') {
			
			if($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				echo "<h1 class='text-center'>Insert Course</h1>";
				echo "<div class='container'>";
				
				// Getting All Variables From The Form 
				
				$grade_id = $_POST['course-grade'];
				$division_id = $_POST['course-division'];
				$course_name = $_POST['course-name'];
			
					 
					
				insertCourse($grade_id , $division_id , $course_name);
				
					
				}
			
				echo "</div>";
			
			}
			
			
			
			
		
		
		elseif($do == 'Edit') {
			
			$course_id = isset($_GET['courseID']) && is_numeric($_GET['courseID']) ? intval($_GET['courseID']) : 0 ;
			
			$stmt = $conn->prepare("SELECT * FROM courses WHERE course_id = ? LIMIT 1") ;
			$stmt->execute(array($course_id)) ;
			$row = $stmt->fetch() ;
			$count = $stmt->rowCount(); // IF Count > 0 This User ID Exist 
			
			if($count > 0) {
					
					
					$courseInfo = getCoursesInfo($course_id) ;
					
					
					
					?>
					
					
						<h1 class="text-center page-title">Edit Course</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Update" method="POST">
								<input type="hidden" name="course-id" value="<?php echo $course_id ?>" />
								
								<!--course Name-->
								
								<div class="form-group">
									
									<label class="col-sm-2 control-label">course Name</label>
									<div class="col-xs-2">
										<input type="text" name="course-name" class="form-control" autocomplete="off" placeholder="Course Name" value="<?php echo $row['course_name']  ?>" required="required" />
									</div>
								</div>
								<!--Courese Grade-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Course Grade</label>
									<div class="col-xs-2">
										<select class="form-control" name="course-grade">
											<option value="<?php echo $courseInfo['grade_id']; ?>"><?php echo $courseInfo['grade_name'] ; ?></option>
											<?php
												$coursesGrade = getAllFrom("*" , "grade" , "","" ,"grade_id", "ASC");
												foreach($coursesGrade as $courseGrade) {
												echo "<option value='" 
													. $courseGrade['grade_id'] . "'>" . $courseGrade['grade_name'] . "</option>";
												}	
											
											?>
										</select>
									</div>
								</div>	
								
                				<!--Courese Division-->
								
								
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Course Division</label>
									<div class="col-xs-2">
										<select class="form-control" name="course-division">
											
											<option value="<?php echo $courseInfo['division_id']; ?>"><?php echo $courseInfo['division_name']; ?></option>
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

								
								 <div class="form-group">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Edit Course" class="btn btn-primary" />
									</div>
							   </div>
								 
							
								</form>
							 
						
						</div>


				
				
			<?php }
		}
		
			
			
			
		
		
		
		elseif($do == 'Update') {
			
			
			echo "<h1 class='text-center'>Update Course</h1>";
			echo "<div class='container'>";
			
			
			
			
			

			if ($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				$course_name 		= $_POST['course-name'];
				$course_grade  		= $_POST['course-grade'];
				$course_division    = $_POST['course-division'];
				$course_id		    = $_POST['course-id'];
				
				$stmt = $conn->prepare("UPDATE 
											courses 
										SET 
											course_name  = ?, 
											grade_id     = ?, 
											division_id	 = ?
											
										WHERE 
											course_id = ?");
				
				$stmt->execute(array($course_name , $course_grade , $course_division , $course_id )) ;
				
		
				$theMsg = '<div class="alert alert-success">Done</div>';
				redirect($theMsg, 'courses.php');
		
		
				
				
				
			}else {
				
				$theMsg = '<div class="alert alert-danger">You Can\'t Browse This Page Directly</div>';
				redirect($theMsg, 'back');
				
			}

	
		}elseif($do =='Delete') {

			echo "<h1 class='text-center'>Delete Course</h1>";
			echo "<div class='container'>";

			$course_id = isset($_GET['courseID']) && is_numeric($_GET['courseID']) ? intval($_GET['courseID']) : 0;
			$check = checkItem('course_id', 'courses', $course_id);

			if ($check > 0) {

				$stmt = $conn->prepare("DELETE FROM courses WHERE course_id = :zid");

				$stmt->bindParam(":zid", $course_id);

				$stmt->execute();

				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Deleted</div>';

				redirect($theMsg, 'courses.php');

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

