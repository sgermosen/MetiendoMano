<?php

	session_start();
	$pageTitle = 'Questions' ;

	if (isset($_SESSION['Admin_Username'])) {
		
		
		include 'init.php' ;
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage') { // Starting Manage Page ?>
			
			
		<div class="question-stats">
            <div class="container">
                 <h1 class="text-center h1-members">Questions</h1>
                 <div class="row">
								 		
                     <div class="col-md-3">                   
                         
                     </div>
                     <div class="col-md-6">
                         <div class="stat st-add-question">
                             <span><a href="questions.php?do=Add">
								 
								 <i class="fa fa-plus fa-lg"></i><br>
                             	 Add Question
								 
								 </a></span>
                         </div>
                    
                 
					 </div>
				</div>
				<div class="row">
								 		
                     <div class="col-md-3">                   
                         
                     </div>
                     <div class="col-md-6">
                         <div class="stat st-view-question">
                             <span><a href="questions.php?do=View">
								 
								 <i class="fa fa-eye fa-lg"></i><br>
								 
                             	 View Question
								 
								 </a></span>
                         </div>
                    
                 
					 </div>
				</div>

			
			
			
		<?php } elseif($do == 'Add') { // Starting Add Page
			
			
			?>

				
				<h1 class="text-center page-title">Add New Question</h1>
				<div class="container">
					<form class="form-horizontal" action="?do=Insert" method="POST">
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
								<input type="submit" value="Add Question" class="btn btn-primary btn-sub" />
							</div>
                   		</div>
                   
				   
					
					</form>
				</div>
			
			
		<?php }
		
		elseif($do == 'Insert') { // Starting Insert Page
			
			
			echo "<div class='container'>" ;
			
			
			if($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				// Getting Data From The Question Form 
				
				$question_name 		= $_POST['question-name'] ;
				$answer1 			= $_POST['ans1'] ;
				$answer2 			= $_POST['ans2'] ;
				$answer3 			= $_POST['ans3'] ;
				$answer4 			= $_POST['ans4'] ;
				$rightAnswer 		= $_POST['right-ans'] ;
				$points 			= $_POST['points'] ;
				
				
				$check = checkItem('question' , 'questions' , $question_name ) ;
				
				if($check > 0) {
					
					
					$theMsg = "<div class='alert alert-danger'>" . ' Sorry This Question Exists</div>';
					redirect($theMsg, 'back');				
					
				}else {					
					
				$stmt = $conn->prepare("INSERT INTO
												 questions(question , question_points)
											VALUES(? , ?)") ;
				$stmt->execute(array($question_name , $points)) ;
				$questionID = $conn->lastInsertId();
				
				
				
				$stmt2 = $conn->prepare("INSERT INTO 
											answers (ans1,ans2,ans3,ans4,correct_ans,questions_id)
										 VALUES 	(?,?,?,?,?,?)") ;
				$stmt2->execute(array($answer1,$answer2,$answer3,$answer4,$rightAnswer,$questionID)) ;
				
				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record 					   Inserted</div>';
					redirect($theMsg, 'back');					
					
				}
			
			}
		
			echo "</div>" ;
		
			
		}elseif($do =='View') {
			
			$stmt = $conn->prepare("SELECT questions.question , questions.id , 												answers.ans1,
									answers.ans2,answers.ans3,answers.ans4,answers.correct_ans
									FROM  questions 
									INNER JOIN answers 
									ON questions.id = answers.questions_id
									
									");


			$stmt->execute() ;
			$questions = $stmt->fetchAll();
			
						?>


			<div class="container">
				<h2 style="color:gray;"><i class="fa fa-edit"></i> Questions</h2>
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
										
									foreach($questions as $question) { 

										?>


								<table class="table table-bordered " id="exams-has-questions-table" >
									<tr>
										<td style="width: 25%" >Question</td>
										<td style="width: 75%" ><?php echo $question['question'] ; ?></td>
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
													if($question['correct_ans'] == $question['ans1'] ) {
														echo "checked" ;
													} ?> ></td>

													<td><?php echo $question['ans1'] ?></td>
												</tr>
												<tr>
													<td>B</td>
													<td><input type="radio" id="radioButton" <?php 
													if($question['correct_ans'] == $question['ans2'] ) {
														echo "checked" ;
													} ?> ></td>
													<td><?php echo $question['ans2'] ?></td>
												</tr>
												<tr>
													<td>C</td>
													<td><input type="radio" id="radioButton" <?php 
													if($question['correct_ans'] == $question['ans3'] ) {
														echo "checked" ;
													} ?> ></td>
													<td><?php echo $question['ans3'] ?></td>
												</tr>
												<tr>
													<td>D</td>
													<td><input type="radio" id="radioButton" <?php 
													if($question['correct_ans'] == $question['ans4'] ) {
														echo "checked" ;
													} ?> ></td>
													<td><?php echo $question['ans4'] ?></td>
												</tr>
												<tr>
													<td></td>
													<td></td>
													<td></td>
													<td>
													<?php
													echo "<a href='questions.php?do=Delete&QID=" . $question['id'] ."' class='confirm btn btn-xs btn-danger'><i class='fa fa-close'></i> Delete</a>";

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
			
			
		}elseif($do == 'Delete') {
			
			
			echo "<h1 class='text-center'>Delete Question</h1>";
			echo "<div class='container'>";

			$question_id = isset($_GET['QID']) && is_numeric($_GET['QID']) ? intval($_GET['QID']) : 0;
			$check = checkItem('id', 'questions', $question_id);

			if ($check > 0) {

				$stmt = $conn->prepare("DELETE FROM questions WHERE id = :zid");

				$stmt->bindParam(":zid", $question_id);

				$stmt->execute();

				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Deleted</div>';

				redirect($theMsg, 'questions.php?do=View');

			} else {

				$theMsg = '<div class="alert alert-danger">This Question is Not Exist</div>';

				redirect($theMsg);

			}

			echo '</div>';
			
			
		}
		
	}else {
		header('Location: index.php');
		exit();	
	}


include $tpl.'footer.php' ;



?>
				
				
				
				
				
				
				
				
				
				