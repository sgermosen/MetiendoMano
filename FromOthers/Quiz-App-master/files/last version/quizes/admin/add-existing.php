<?php


	session_start();
	$pageTitle = 'Add Existing Question' ;


	if (isset($_SESSION['Admin_Username'])) {


		include 'init.php' ;
		
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		
		$examID = isset($_GET['examID']) && is_numeric($_GET['examID']) ? intval($_GET['examID']) : 0 ;
		
		
		// Select All Data Depend On This ID

			$stmt1 = $conn->prepare("SELECT * FROM exam WHERE exam_id = ? LIMIT 1");

			// Execute Query

			$stmt1->execute(array($examID));

			// Fetch The Data

			$row = $stmt1->fetch();

			// The Row Count

			$count = $stmt1->rowCount();

			// If There's Such ID Show The Form

			if (! $count > 0) { 
			
			
				echo msg ($type = "danger" , $body = "No Such ID") ;
				return ;
			
			}
		
		
		if($do == 'Manage') {
		
		
		?>

		<div class="container">
			
			<h1 class="text-center page-title">Add New Question</h1>						
			<div class="col-md-2"></div>
			<div style="padding-top:75px;"></div>
			<form class="form-horizontal" action="?do=Insert&examID=<?php echo $examID ?>" method="POST">
				<input type="hidden" name="examID" value="<?php echo $examID ; ?>" />
				<div class="form-group">
					<label class="col-sm-2 control-label">Question</label>
					<div class="col-xs-2" >
						<select id="questions" multiple class="form-control" name="selected-questions[]" required>
							
							
							
							<?php
								$allQuestions = getAllFrom("*" , "questions" ,"","" ,"id", "ASC");
								foreach($allQuestions as $question) {
								echo "<option value='" 
									. $question['id'] . "'>" . $question['question'] . "</option>";
								}	

							?>
						</select>
						<input type="submit" value="Add Question" class="btn btn-primary" style="margin-top:5px;width:300px;margin-left:50px" />
					</div>
					
				</div>
				
				
			</form>
		</div>



		<?php }
		
		
		elseif($do == 'Insert') {
			
			if($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				echo "<h1 class='text-center'>Insert Question</h1>";
				echo "<div class='container'>";
				
				// Getting All Variables From The Form 
				
				
				if(isset($_POST['selected-questions'])){
					
					$questionID = array() ;
					foreach($_POST['selected-questions'] as $question) {
						
						array_push($questionID ,$question ) ;
						
					}
					
					
				}
				
//				echo "<pre>" ;
//				  print_r($questionID) ;
//				echo "</pre>" ;
//				
				
				
				
				$examID = $_POST['examID'] ;
				
				
				
				
					try{
						
						
						foreach($questionID as $QID)
					{
					  $stmt = $conn->prepare ("INSERT INTO exam_has_questions 
											(exam_id, questions_id)
											VALUES(?,?)") ;
					 $stmt->execute(array($examID ,$QID ));
					}
					
					$theMsg = '<div class="alert alert-success">Added SuccssFully</div>';
					redirect($theMsg, "exams.php");
					
						
						
						
					}catch(PDOException $e) {
						$theMsg = '<div class="alert alert-danger">Sorry This Questions Exist</div>';
						redirect($theMsg , 'exams.php');
					}
					
					
					
				}
					
					
					
				
				
			}

		}
		
	


include $tpl.'footer.php' ;


?>














