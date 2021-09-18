<?php

session_start();
$pageTitle = 'Exams' ;


	if (isset($_SESSION['Admin_Username'])) {


		include 'init.php' ;
		
		
		$query = "SELECT 
				  		 cr.course_name , cr.course_id , exam.exam_name , grade_name , exam.exam_id , exam.exam_duration , exam.exam_status ,division.division_name
				  FROM courses AS cr 
				  INNER JOIN courses_has_exam AS courses_exams 
				  ON cr.course_id = courses_exams.courses_id 
				  INNER JOIN exam ON exam.exam_id = courses_exams.exam_id
				  INNER JOIN grade ON cr.grade_id = grade.grade_id
				  INNER JOIN division ON cr.division_id = division.division_id" ; 
		
		$stmt = $conn->prepare($query) ; 
		$stmt->execute() ;
		
		$rows = $stmt->fetchAll();
		
		
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage'){ ?>
			
			<div class="container">
				<h2 style="color:gray;"><i class="fa fa-edit"></i> Manage Exam</h2>
				<ul class="nav nav-tabs">
					<li class="active"><a data-toggle="tab" href="#home">
						<i class="fa fa-align-justify fa-lg"></i> Exam List</a>
					</li>
					<li><a data-toggle="tab" href="#add-exam">
						<i class="fa fa-plus fa-lg"></i> Add Exam</a>
					</li>
					<li style="float:right"><a href="logout.php">Logout</a></li>
				</ul>
				<div class="tab-content">
					
					<div id="home" class="tab-pane fade in active">
						
						<h1 class="text-center">Manage Exams</h1>
						<div class="table-responsive">
							<table class="main-table text-center table table-bordered" id="exams-table">
								<tr>
									<td>ExamID</td>
									<td>Course</td>
									<td>Division</td>
									<td>Grade</td>
									<td>Exam Name</td>
									<td>Duration</td>
									<td>Options</td>
									
								</tr>
								
								<?php
								
									foreach($rows as $row) {
										
										echo "<tr>" ;							
											echo "<td>" . $row['exam_id'] . "</td>" ;
											echo "<td>" . $row['course_name'] . "</td>" ;
											echo "<td>" . $row['division_name'] . "</td>" ;
											echo "<td>" . $row['grade_name'] . "</td>" ;
											echo "<td>" . $row['exam_name'] . "</td>" ;
											echo "<td>" 
													. $row['exam_duration']
													."<span id='duration-span'> Mins</span>" 
												. "</td>" ;
											
											echo "<td>
											
												<a href='exams.php?do=Add&examID=".$row['exam_id']."' class='btn btn-primary btn-sub'><i class='fa fa-plus fa-lg'> Add Question</i></a>
												
												<a href='exams.php?do=Edit&examID=".$row['exam_id']."' class='btn btn-success'><i class='fa fa-edit fa-lg'>Edit</i></a>

												<a href='exams.php?do=ViewQ&examID=".$row['exam_id']."' class='btn btn-warning'><i class='fa fa-search fa-lg'> View Questions</i></a>
												
												<a href='exams.php?do=Delete&examID=".$row['exam_id']."' class='btn btn-danger confirm'><i class='fa fa-close fa-sm'>Delete</i></a> " ;
										
												if($row['exam_status'] == 0) {
													echo " <a href='exams.php?do=Activate&examID=".$row['exam_id']."' class='btn btn-info'><i class='fa fa-check fa-lg'></i> Activate</a>" ;
												} 
											
												

											
										
											echo "</td>" ;
										echo "</tr>" ;
									
									}
							
								?>
						
							</table>						
						</div>
					
						
						
					</div>
					<div id="add-exam" class="tab-pane fade">
						
						<h1 class="text-center page-title">Add New Exam</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Insert" method="POST">
								
								<?php
										$stmt = $conn->prepare("SELECT max(exam_id) 
																FROM exam LIMIT 1 ");
										$stmt->execute();
										$lastExamId = $stmt->fetch();
											
									?>
								
								<input type="hidden" name="last-exam-id" value="<?php echo $lastExamId[0] ?>" />
								
									<!--Courese Name-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Course Name</label>
									<div class="col-xs-2">
										<select class="form-control" name="selected-course">
											<option value="0">none</option>
											<?php
												$allCourses = getAllFrom("*" , "courses" ,"","" ,"course_id", "ASC");
												foreach($allCourses as $course) {
												echo "<option value='" 
													. $course['course_id'] . "'>" . $course['course_name'] . "</option>";
												}	
											
											?>
										</select>
									</div>
								</div>	
								
								<!--Exam Name-->
								
								<div class="form-group">
									
									<label class="col-sm-2 control-label">Exam Name</label>
									<div class="col-xs-2">
										<input type="text" name="exam-name" class="form-control" autocomplete="off" placeholder="Exam Name" required="required" />
									</div>
								</div>
								
								<!--Date-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Date</label>
									<div class="col-xs-2">
										<input id="datepicker" type="text" class="form-control" placeholder="Date" name="exam-date">
									</div>
								</div>
							<!--Exam Duration-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">
										Duration(Minutes)
									</label>
									<div class="col-xs-2">
										<input type="text" name="exam-duration" class="form-control" placeholder="Duration">
									</div>
								</div>
								
						 <!--Exam Visibility--> 
							   <div class="form-group">
									<label class="col-sm-2 control-label">Visibility</label>
									<div class="col-md-6">
										<div>
											<input id="vis-yes" type="radio" 
												   name="exam-visib" value = "1" checked />
											<label for="vis-yes">Yes</label>
										</div>
										<div>
											<input id="vis-no" type="radio" 
												   name="exam-visib" value="0" />
											<label for="vis-no">No</label>
										</div>
									</div>
							   </div>
								
							   <div class="form-group">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Add Exam" class="btn btn-primary btn-sub" />
									</div>
							   </div>
                   
							</form>
						
						</div>
<!--						Ending Add Exam Page	-->
					</div>
					
				</div>
		
			</div>
			
			
			
			
			
			
			
		 <?php }elseif($do == 'Add') { // Starting Add Question in The Exam 
			
			
			
			
			$examID = isset($_GET['examID']) && is_numeric($_GET['examID']) ? intval($_GET['examID']) : 0 ;
			
			
			// Select All Data Depend On This ID

			$stmt = $conn->prepare("SELECT * FROM exam WHERE exam_id = ? LIMIT 1");

			// Execute Query

			$stmt->execute(array($examID));

			// Fetch The Data

			$row = $stmt->fetch();

			// The Row Count

			$count = $stmt->rowCount();

			// If There's Such ID Show The Form

			if (!$count > 0) { 
			
			
				echo msg ($type = "danger" , $body = "No Such ID") ;
				return ;
			
			}
			
			
			?>


				<h1 class="text-center page-title">Add New Question</h1>
				<div class="container">
					<div class="row">
						
						<div class="col-md-5"></div>
						<div class="col-sm-3">
							<a href="add-existing.php?examID=<?php echo $examID ?>" class="btn btn-primary btn-sm" style="margin-bottom:10px;"><i class="fa fa-plus"></i> Add Existing</a>
						</div>
					
					</div>
					
					<form class="form-horizontal" action="?do=Insert-question" method="POST">
						<input type="hidden" name="examid" value="<?php echo $examID ?>" />
						<div class="form-group">                    
							<label class="col-sm-2 control-label">Question</label>
							<div class="col-sm-10 col-md-8">                         
								<input type="text" name="question-name" class="form-control" autocomplete="off" placeholder="Text Of Question" required="required" />
							</div>
						</div>
						<div class="form-group">                    
							<label class="col-sm-2 control-label">Answer 1</label>
							<div class="col-sm-10 col-md-8">                         
								<input type="text" name="ans1" class="form-control" autocomplete="off" placeholder="Text Of Answer" required="required" />
							</div>
						</div>
				   		<div class="form-group">                    
							<label class="col-sm-2 control-label">Answer 2</label>
							<div class="col-sm-10 col-md-8">                         
								<input type="text" name="ans2" class="form-control" autocomplete="off" placeholder="Text Of Answer" required="required" />
							</div>
						</div>
				   		<div class="form-group">                    
							<label class="col-sm-2 control-label">Answer 3</label>
							<div class="col-sm-10 col-md-8">                         
								<input type="text" name="ans3" class="form-control" autocomplete="off" placeholder="Text Of Answer" required="required" />
							</div>
						</div>
				   		<div class="form-group">                    
							<label class="col-sm-2 control-label">Answer 4</label>
							<div class="col-sm-10 col-md-8">                         
								<input type="text" name="ans4" class="form-control" autocomplete="off" placeholder="Text Of Answer" required="required" />
							</div>
						</div>
						<div class="form-group">                    
							<label class="col-sm-2 control-label">Right Answer</label>
							<div class="col-sm-10 col-md-8">                         
								<input type="text" name="right-ans" class="form-control" autocomplete="off" placeholder="Text Of Right Answer" required="required" />
							</div>
						</div>
						<div class="form-group">                    
							<label class="col-sm-2 control-label">Points</label>
							<div class="col-sm-10 col-md-8">                         
								<input type="text" name="points" class="form-control" autocomplete="off" placeholder="Points Of The Question" required="required" />
							</div>
						</div>
						<div class="form-group">
							<div class="col-sm-offset-2 col-sm-10">
								<input type="submit" value="Add Question" class="btn btn-primary " />
							</div>
                   		</div>
                   
				   
					
					</form>
				</div>



			<?php
			
			
			
			
		}elseif($do == 'Insert') {
			
			if($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				echo "<h1 class='text-center'>Insert Exam</h1>";
				echo "<div class='container'>";
				
				// Getting All Variables From The Form 
				
				$courseID = $_POST['selected-course'];
				$examName = $_POST['exam-name'];
				$examDate = $_POST['exam-date'];
				$examDuration = $_POST['exam-duration'];
				$examVisib = $_POST['exam-visib'];
				$lastExamID = $_POST['last-exam-id'];
				
				
					 
					
				
				// Check If The Exam Exists in Database
				
				$check = checkItem("exam_name" , "exam" , $examName) ;
				
				if($check == 1) {
					
					
					$theMsg = '<div class="alert alert-danger">Sorry This Exam Not Exist</div>';
					redirect($theMsg, 'back');
				}else {
					
					// Insert The Exam 
					
					$query = "INSERT INTO
								exam(exam_name,exam_start_date,exam_duration,exam_status)
								VALUES(:zname,:zdate,:zduration,:zstatus)" ;
					
					$stmt = $conn->prepare($query);
					$stmt->execute(array(
					
						'zname' => $examName,
						'zdate' => $examDate,
						'zduration' => $examDuration,
						'zstatus' => $examVisib
					
					));
					
					
					$lastExamID += 1 ;
					
				
					
					
					// insert into Courses_has_exams
					
					
					$query2 = "INSERT INTO courses_has_exam(courses_id, exam_id) 
								VALUES (?,?)" ;
					
					$stmt2 = $conn->prepare($query2);
					$stmt2->execute(array($courseID,$lastExamID));
					
					
					
					
					
					$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Inserted</div>';
					redirect($theMsg, 'back');

					
				}
			
				echo "</div>";
			
			}
			
			
			
			
		}elseif($do =='Insert-question') {
			
			
			if($_SERVER['REQUEST_METHOD'] == 'POST' )  {
				
				// Getting Data From The Question Form 
				
				$questionName 		= $_POST['question-name'] ;
				$answer1 			= $_POST['ans1'] ;
				$answer2 			= $_POST['ans2'] ;
				$answer3		    = $_POST['ans3'] ;
				$answer4 			= $_POST['ans4'] ;
				$rightAnswer 		= $_POST['right-ans'] ;
				$examID 			= $_POST['examid'];
				$points 			= $_POST['points'] ;
				
				$check = checkItem('question' , 'questions' , $questionName ) ;
				
				if($check > 0) {
					
					
					$theMsg = "<div class='alert alert-danger'>" . ' Sorry This Question Exists</div>';
					redirect($theMsg, 'back');				
					
				}else {
					
					$stmt = $conn->prepare("INSERT INTO
												 questions(question , question_points)
											VALUES(? , ?)") ;
				$stmt->execute(array($questionName , $points)) ;
				$questionID = $conn->lastInsertId();
				
				
				
				$stmt2 = $conn->prepare("INSERT INTO 
											answers (ans1,ans2,ans3,ans4,correct_ans,questions_id)
										 VALUES 	(?,?,?,?,?,?)") ;
				$stmt2->execute(array($answer1,$answer2,$answer3,$answer4,$rightAnswer,$questionID)) ;
				
				
				$stmt3 = $conn->prepare("INSERT INTO 
												exam_has_questions (exam_id , questions_id)
										 VALUES (?,?)") ;	
				$stmt3->execute(array($examID , $questionID));
				
				
				if($stmt3->rowCount() > 0) {
					
					
					$theMsg = '<div class="alert alert-success">Added SuccissFully</div>';
					redirect($theMsg, 'back');
			
				}
					
					
				}			
		
			}
		
		}
		
		elseif($do == 'Edit') {
			
			$examID = isset($_GET['examID']) && is_numeric($_GET['examID']) ? intval($_GET['examID']) : 0 ;
			$stmt = $conn->prepare("SELECT * FROM exam WHERE exam_id = ? LIMIT 1") ;
			$stmt->execute(array($examID)) ;
			$row = $stmt->fetch() ;
			$count = $stmt->rowCount(); // IF Count > 0 This User ID Exist 
			
			if($count > 0) { ?>
				
						<h1 class="text-center page-title">Edit Exam</h1>
						<div class="container">
							<form class="form-horizontal" action="?do=Update" method="POST">
								<input type="hidden" name="exam-id" value="<?php echo $examID ?>" />
								
								<!--Exam Name-->
								
								<div class="form-group">
									
									<label class="col-sm-2 control-label">Exam Name</label>
									<div class="col-xs-2">
										<input type="text" name="exam-name" class="form-control" autocomplete="off" placeholder="Exam Name" value="<?php echo $row['exam_name']  ?>" required="required" />
									</div>
								</div>
								
								<!--Date-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">Date</label>
									<div class="col-xs-2">
										<input id="datepicker" type="text" class="form-control"  value="<?php echo $row['exam_start_date'] ?>" placeholder="Date" name="exam-date">
									</div>
								</div>
							<!--Exam Duration-->
								
								<div class="form-group">
									<label class="col-sm-2 control-label">
										Duration(Minutes)
									</label>
									<div class="col-xs-2">
										<input type="text" name="exam-duration" class="form-control" value="<?php echo $row['exam_duration'] ?>" placeholder="Duration">
									</div>
								</div>
								
						 <!--Exam Visibility--> 
							   <div class="form-group">
									<label class="col-sm-2 control-label">Visibility</label>
									<div class="col-md-6">
										<div>
											<input id="vis-yes" type="radio" 
												   name="exam-visib" value = "1" 
												   
												   <?php
												   	
														if($row['exam_status'] == 1) {
															echo "checked" ;
														}
				
												   ?>
												   
												   />
											<label for="vis-yes">Yes</label>
										</div>
										<div>
											<input id="vis-no" type="radio" 
												   name="exam-visib" value="0" 
												   
												   <?php
												   		
														if($row['exam_status'] == 0) {
															echo "checked" ;
														}
												   	
												   ?>
												   
												   
												   />
											<label for="vis-no">No</label>
										</div>
									</div>
							   </div>
								
							   <div class="form-group">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Update Exam" class="btn btn-primary" />
									</div>
							   </div>
                   
							</form>
						
						</div>





				
				
				
			<?php }
			
			
		}
		
		
		elseif($do == 'Update') {
			
			
			echo "<h1 class='text-center'>Update Exam</h1>";
			echo "<div class='container'>";

			if ($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				$examName = $_POST['exam-name'];
				$examDate = $_POST['exam-date'];
				$examDuration = $_POST['exam-duration'];
				$examVisib = $_POST['exam-visib'];
				$examID = $_POST['exam-id'];
				
				
				$stmt = $conn->prepare("UPDATE 
											exam 
										SET 
											exam_name = ?, 
											exam_start_date	 = ?, 
											exam_duration	 = ?, 
											exam_status = ?
											
										WHERE 
											exam_id = ?");
				
				$stmt->execute(array($examName , $examDate , $examDuration , $examVisib , $examID)) ;

				$theMsg = '<div class="alert alert-success">Done</div>';
				redirect($theMsg, 'exams.php');
				
				
				
				
			}else {
				
				$theMsg = '<div class="alert alert-danger">You Can\'t Browse This Page Directly</div>';
				redirect($theMsg, 'back');
				
			}

	
		}elseif($do =='Delete') {

			echo "<h1 class='text-center'>Delete Exam</h1>";
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
		elseif($do == 'ViewQ') {
			
			
			
			$examID = isset($_GET['examID']) && is_numeric($_GET['examID']) ? intval($_GET['examID']) : 0 ;

			$stmt = $conn->prepare("SELECT questions.question , questions.id , 																exam_has_questions.questions_id , exam_has_questions.exam_id as examID ,answers.ans1,
			answers.ans2,answers.ans3,answers.ans4,answers.correct_ans
															FROM  exam_has_questions 
															INNER JOIN questions 
															ON questions.id = exam_has_questions.questions_id 
															INNER JOIN answers
															ON exam_has_questions.questions_id = answers.questions_id
															WHERE exam_has_questions.exam_id = ?");
														

			$stmt->execute(array($examID)) ;
			$exam_questions = $stmt->fetchAll();
			
			// echo "<pre>";

			// 	print_r($exam_questions);

			// echo "<pre>";


			

			
			?>


			<div class="container">
				<h2 style="color:gray;"><i class="fa fa-edit"></i> Manage Exam</h2>
				<ul class="nav nav-tabs">
					<li class="active"><a data-toggle="tab" href="#home">
						<i class="fa fa-align-justify fa-lg"></i> Question List</a>
					</li>
					<li style="float:right"><a href="logout.php">Logout</a></li>
				</ul>
				<div class="tab-content">
					
					<div id="home" class="tab-pane fade in active">
						
						<h1 class="text-center">View Questions</h1>
						<div class="table-responsive">
							<table class="main-table text-center table table-bordered" id="exams-table">
								<tr>
									<td style="width: 100%">Question</td>
							
								</tr>
								<tr>

									<td>

										<?php
										
									foreach($exam_questions as $questions) { 

										?>


								<table class="table table-bordered " id="exams-has-questions-table" >
									<tr>
										<td style="width: 25%" >Question</td>
										<td style="width: 75%" ><?php echo $questions['question'] ; ?></td>
									</tr>
									<tr>
										<td height="200" >Options</td>
										<td>
											<table class="table table-bordered">
												<tr>
													<td>No.</td>
													<td>Correct</td>
													<td>Choises</td>
													<td>Delete</td>
												</tr>
												<tr>
													<td>A</td>
													<td><input type="radio" id="radioButton" <?php 
													if($questions['correct_ans'] == $questions['ans1'] ) {
														echo "checked" ;
													} ?> ></td>

													<td><?php echo $questions['ans1'] ?></td>
												</tr>
												<tr>
													<td>B</td>
													<td><input type="radio" id="radioButton" <?php 
													if($questions['correct_ans'] == $questions['ans2'] ) {
														echo "checked" ;
													} ?> ></td>
													<td><?php echo $questions['ans2'] ?></td>
												</tr>
												<tr>
													<td>C</td>
													<td><input type="radio" id="radioButton" <?php 
													if($questions['correct_ans'] == $questions['ans3'] ) {
														echo "checked" ;
													} ?> ></td>
													<td><?php echo $questions['ans3'] ?></td>
												</tr>
												<tr>
													<td>D</td>
													<td><input type="radio" id="radioButton" <?php 
													if($questions['correct_ans'] == $questions['ans4'] ) {
														echo "checked" ;
													} ?> ></td>
													<td><?php echo $questions['ans4'] ?></td>
												</tr>
												<tr>
													<td></td>
													<td></td>
													<td></td>
													<td>
													<?php
													echo "<a href='exams.php?do=Delete-E-Q&QID=" . $questions['questions_id'] . "&examID=" . $questions['examID'] . "' class='confirm btn btn-xs btn-danger'><i class='fa fa-close'></i> Delete</a>";

													?>

													</td>

													<!-- <a href='exams.php?do=Delete&examID=".$row['exam_id']."' class='btn btn-danger confirm'><i class='fa fa-close fa-lg'>Delete</i></a> " ; -->

												</tr>
											</table>
										</td>
									</tr>
								</table>



										<?php	}
										?>

									</td>
									
								</tr>
							</table>						
						</div>
				
					</div>
				
					</div>
					
				</div>
		
			
			
				<?php

		}elseif($do == 'Delete-E-Q') {
			echo "<h1 class='text-center'>Delete Question</h1>";
			echo "<div class='container'>";

			$questionID = isset($_GET['QID']) && is_numeric($_GET['QID']) ? intval($_GET['QID']) : 0;
			$examID = isset($_GET['examID']) && is_numeric($_GET['examID']) ? intval($_GET['examID']) : 0;
			$check = checkItem('questions_id', 'exam_has_questions', $questionID);
			$checkExam = checkItem('exam_id', 'exam_has_questions', $examID);

			if ($check > 0 and $checkExam > 0) {

				$stmt = $conn->prepare("DELETE FROM exam_has_questions WHERE questions_id = ? AND exam_id = ?" );
				$stmt->execute(array($questionID,$examID));
				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Deleted</div>';
				redirect($theMsg, 'back');
			} else {
				$theMsg = '<div class="alert alert-danger">This ID is Not Exist</div>';
				redirect($theMsg);

			}

		echo '</div>';



			




		}
		
		elseif ($do == 'Activate') {

			echo "<h1 class='text-center'>Activate Member</h1>";
			echo "<div class='container'>";

				// Check If Get Request userid Is Numeric & Get The Integer Value Of It

				$exam_id = isset($_GET['examID']) && is_numeric($_GET['examID']) ? intval($_GET['examID']) : 0;

				// Select All Data Depend On This ID

				$check = checkItem('exam_id', 'exam', $exam_id);

				// If There's Such ID Show The Form

				if ($check > 0) {

					$stmt = $conn->prepare("UPDATE exam SET exam_status = 1 WHERE exam_id = ?");

					$stmt->execute(array($exam_id));

					$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Updated</div>';

					
					redirect($theMsg, 'exams.php');

				} else {

					$theMsg = '<div class="alert alert-danger">This ID is Not Exist</div>';

					redirectHome($theMsg);

				}

			echo '</div>';

		}




	}else {


		header('Location: index.php');
		exit();	

	}









include $tpl.'footer.php' ;



?>