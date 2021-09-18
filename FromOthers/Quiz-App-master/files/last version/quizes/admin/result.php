<?php

session_start();
$pageTitle = 'Results' ;


	if (isset($_SESSION['Admin_Username'])) {


		include 'init.php' ;
		
		
	
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage'){ ?>
			
			<div class="container">
				<h2 style="color:gray;"><i class="fa fa-edit"></i> View Result</h2>
				<ul class="nav nav-tabs">
					<li class="active"><a data-toggle="tab" href="#home">
						<i class="fa fa-align-justify fa-lg"></i> View Result</a>
					</li>
					<li style="float:right"><a href="logout.php">Logout</a></li>
				</ul>
				<div class="tab-content">
					
					<div id="home" class="tab-pane fade in active">
						
						<h1 class="text-center page-title">Search For Results</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Search" method="POST">
							
								
									<!--Courese Grade-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Grade</label>
									<div class="col-xs-2">
										<select class="form-control" name="grade">
											<option value="0">Grade</option>
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
									<label class="col-sm-2 control-label">Division</label>
									<div class="col-xs-2">
										<select class="form-control" name="division">
											<option value="0">Division</option>
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
										<input type="submit" value="Search Students" class="btn btn-primary btn-sub" />
									</div>
							   </div>
				
					</div>
			
				    </div>
	
					</div>
			
		
			</div>
			
			
			
			
			
			
			
	 <?php }elseif($do == 'Search') { 
			
			
			echo "<h1 class='text-center'>Search Students</h1>";
			echo "<div class='container'>";

			if ($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				$grade 		= $_POST['grade'];
				$division   = $_POST['division'];
				
				$query = "SELECT student.student_id , card_id , fname ,lname , grade.grade_name , 					division.division_name 
							FROM student 
							INNER JOIN grade ON student.grade_id = grade.grade_id 
							INNER JOIN division ON student.division_id = division.division_id 
							WHERE student.grade_id = ? AND student.division_id = ?";
				
				
				$stmt = $conn->prepare($query);
				$stmt->execute(array($grade , $division));
				$rows = $stmt->fetchAll();
				
				?>
					<div class="table-responsive">
							<table class="main-table text-center table table-bordered" id="exams-table">
								<tr>
									<td>Student ID</td>
									<td>Student Name</td>
									<td>Grade</td>
									<td>Division</td>
									<td>Options</td>
									
									
									
									
								</tr>
								
								<?php
								
									foreach($rows as $row) {
										
										echo "<tr>" ;							
											echo "<td>" . $row['card_id'] . "</td>" ;
											echo "<td>" . $row['fname'] . ' ' .$row['lname'] . "</td>" ;
											echo "<td>" . $row['grade_name'] . "</td>" ;
											echo "<td>" . $row['division_name'] . "</td>" ;
//											
											
											
											echo "<td>
											
												<a href='result.php?do=View-Res&studentID=".$row['student_id']."' class='btn btn-primary btn-sub'><i class='fa fa-search fa-lg'>View Result</i></a>
												
												" ;
										
												
												

											
										
											echo "</td>" ;
										echo "</tr>" ;
									
									}
							
								?>
						
							</table>						
						</div>
					
					 
				<?php 
				
				
				
				
				if($stmt->rowCount() > 0) { 
					
					
						
					
				}else {
					//echo "No Data Found";
				}
				
				
				
				
			}else {
				
				$theMsg = '<div class="alert alert-danger">You Can\'t Browse This Page Directly</div>';
				redirect($theMsg, 'back');
				
			}
			
	
		}elseif($do == 'View-Res') {
			
			
			$student_id = isset($_GET['studentID']) && is_numeric($_GET['studentID']) ? intval($_GET['studentID']) : 0 ;
			
			
			
				
				
				$query = "SELECT student.card_id , student.fname , student.lname , grade.grade_name 		,division.division_name , exam_results.total_points ,  exam_results.exam_id        , exam_results.correct_ans , exam_results.wrong_ans , exam.exam_name
						FROM student 
						INNER JOIN grade ON student.grade_id = grade.grade_id
						INNER JOIN division on student.division_id = division.division_id 
						INNER JOIN exam_results on student.student_id = exam_results.student_id 
						INNER JOIN exam ON exam_results.exam_id = exam.exam_id
						WHERE student.student_id = ?";
			
			$stmt = $conn->prepare($query);
			$stmt->execute(array($student_id));
			
			$row = $stmt->fetch();
			
			
			
			$stmt0 = $conn->prepare("SELECT COUNT(exam_has_questions.questions_id) FROM 									exam_has_questions
									WHERE exam_has_questions.exam_id = ? LIMIT 1");
			$stmt0->execute(array($row['exam_id']));
			$questions_no = $stmt0->fetchColumn();
			
			
			
			if($stmt->rowCount() > 0) { ?>
				
				
				<div class="container">
				<h2 style="color:gray;"><i class="fa fa-edit"></i> View Result</h2>
				<ul class="nav nav-tabs">
					<li class="active"><a data-toggle="tab" href="#home">
						<i class="fa fa-align-justify fa-lg"></i> View Result</a>
					</li>
					<li style="float:right"><a href="logout.php">Logout</a></li>
				</ul>
				<div class="tab-content">
					
					<div id="home" class="tab-pane fade in active">
						
				<div class="row">
					<div class="col-sm-offset-2 col-sm-10">
						
						<div class="col-sm-8">
						<div class="panel panel-default">
							
							<div class="panel-body">
								<ul class="list-unstyled latest-users">
									<li>Student ID: <span class="stu-res"><b><?php echo $row['card_id']; ?></b></span></li>
									<li>Student Name: <span class="stu-res"><b><?php echo $row['fname'] . ' ' . $row['lname'] ;?></b></span></li>
									<li>Grade: <span class="stu-res"><b><?php echo $row['grade_name']; ?></b></span></li>
									<li>Division: <span class="stu-res"><b><?php echo $row['division_name'] ;?></b></span></li>
									<li>Exam Name: <span class="stu-res"><b><?php echo $row['exam_name'] ;?></b></span></li>
									<li>Total Questions: <span class="stu-res"><b><?php echo $questions_no ; ?></b></span></li>
									<li>No of Correct Answers : <span class="stu-res"><b><?php echo $row['correct_ans'] ; ?></b></span></li>
									<li>No of Wrong Answers : <span class="stu-res"><b><?php echo $row['wrong_ans'] ; ?></b></span></li>
									<li>Student Score:<span class="stu-res"> <b><?php echo $row['total_points'] ;?></b></span></li>
									
								</ul>
							</div>
						</div>
					</div>
					
					</div>
					

				</div>
	
					</div>
					
					</div>
			
			</div>
			
				
			<?php }else {
				
				$theMsg = '<div class="alert alert-danger">No Data To View</div>';
				redirect($theMsg, 'result.php' , 5);
				
				
			}


		}
			
			
			
		
		elseif($do == 'Edit') {
			
			
		}
			
			
		
		
		elseif($do == 'Update') {
			
			
			
	
		}elseif($do =='Delete') {

			echo "<h1 class='text-center'>Delete Category</h1>";
			echo "<div class='container'>";

			$examID = isset($_GET['examID']) && is_numeric($_GET['examID']) ? intval($_GET['examID']) : 0;
			$check = checkItem('exam_id', 'exam', $examID);

			if ($check > 0) {

				$stmt = $conn->prepare("DELETE FROM exam WHERE exam_id = :zid");

				$stmt->bindParam(":zid", $examID);

				$stmt->execute();

				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Deleted</div>';

				redirect($theMsg, 'back');

			} else {

				$theMsg = '<div class="alert alert-danger">This ID is Not Exist</div>';

				redirect($theMsg);

			}

			echo '</div>';

		}
	
		else {
			header('Location: index.php');
			exit();	
		}
		
		
	}

include $tpl.'footer.php' ;



?>