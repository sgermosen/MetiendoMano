<?php

	session_start();
	$pageTitle = 'Manage Grades' ;

	if (isset($_SESSION['Admin_Username'])) {
		
		
		include 'init.php' ;
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage') { // Starting Manage Page ?>
			
			
		<div class="question-stats">
            <div class="container">
                 <h1 class="text-center h1-members">Grades</h1>
                 <div class="row">
								 		
                     <div class="col-md-3">                   
                         
                     </div>
                     <div class="col-md-6">
                         <div class="stat st-add-question">
                             <span><a href="grade.php?do=Add-grade">
								 
								 <i class="fa fa-plus fa-lg"></i><br>
                             	 Add Grade
								 
								 </a></span>
                         </div>
                    
                 
					 </div>
				</div>
				
				
			
			
			
		<?php } elseif($do == 'Add-grade') { // Starting Add Page
			
			
			$rows = getAllFrom("*" , "grade" ,"","" ,"grade_id", "ASC");
			
			
			?>
				
				
		<div class="container">
			<h2 style="color:#222222;"><i class="fa fa-edit"></i> Manage Professors</h2>
			<div class="table-responsive">
						<table class="main-table text-center table table-bordered" id="exams-table" style="margin-top:10px;">
							<tr>
								<td>ID</td>
								<td>Name</td>
								<td>Options</td>

							</tr>

							<?php

								foreach($rows as $row) {

									echo "<tr>" ;							
										echo "<td>" . $row['grade_id'] . "</td>" ;
										echo "<td>" . $row['grade_name'] . "</td>" ;
										echo "<td>



											<a href='grade.php?do=add-new-grade&gradeID=".$row['grade_id']."' class='btn btn-success btn-sub'><i class='fa fa-edit fa-lg'> Add Grade</i></a>


											<a href='grade.php?do=delete-grade&gradeID=".$row['grade_id']."' class='btn btn-danger confirm'><i class='fa fa-close fa-sm'> Delete</i></a> " ;





										echo "</td>" ;
									echo "</tr>" ;

								}

							?>

						</table>						
					</div>
				</div>

				
				
			
			
		<?php }
		
		elseif($do == 'add-new-grade') { ?>
			<h1 class="text-center h1-members">Add New Grades</h1>
			
			<div class="container">
				<form class="form-horizontal" action="?do=insert-new-grade" method="POST">
							
								<!--Course Name-->
								
								<div class="form-group">
									
									<label class="col-sm-2 control-label">Grade Name</label>
									<div class="col-xs-2">
										<input type="text" name="grade-name" class="form-control" autocomplete="off" placeholder="Grade Name" required="required" />
									</div>
								</div>
								
								<!-- Ending Course Name-->
					
								 <div class="form-group">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Add Grade" class="btn btn-primary btn-sub" />
									</div>
							   </div>
								
				</form>
          <!--Ending Add Course Page	-->
					</div>
			
		<?php }
		
		elseif($do == 'insert-new-grade') { // Starting Insert Page
			
			
			echo "<div class='container'>" ;
			
			
			if($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				// Getting Data From The Question Form 
				
				$grade_name = $_POST['grade-name'] ;
				
				if($grade_name == '') {
					$theMsg = "<div class='alert alert-danger'>" ."Please Enter The Grade Name " .'</div>';
					redirect($theMsg, 'back');				
				}
				
				
				
				$check = checkItem('grade_name' , 'grade' , $grade_name ) ;
				
				if($check > 0) {
					
					
					$theMsg = "<div class='alert alert-danger'>" . ' Sorry This Grade Exists</div>';
					redirect($theMsg, 'back');				
					
				}else {					
					
				$stmt = $conn->prepare("INSERT INTO
												 grade(grade_name )
											VALUES(?)") ;
				$stmt->execute(array($grade_name)) ;
				
				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record 					   Inserted</div>';
					redirect($theMsg, 'grade.php?do=Add-grade');					
					
				}
			
			}
		
			echo "</div>" ;
		
			
		}
			
			
		elseif($do == 'delete-grade') {
			
			
			echo "<h1 class='text-center'>Delete Grade</h1>";
			echo "<div class='container'>";

			$grade_id = isset($_GET['gradeID']) && is_numeric($_GET['gradeID']) ? intval($_GET['gradeID']) : 0;
			$check = checkItem('grade_id', 'grade', $grade_id);

			if ($check > 0) {

				$stmt = $conn->prepare("DELETE FROM grade WHERE grade_id = :zid");

				$stmt->bindParam(":zid", $grade_id);

				$stmt->execute();

				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Deleted</div>';

				redirect($theMsg, 'grade.php?do=Add-grade');

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
				
				
				
				
				
				
				
				
				
				