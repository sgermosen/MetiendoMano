<?php

	session_start();
	$pageTitle = 'Manage divisions' ;

	if (isset($_SESSION['Admin_Username'])) {
		
		
		include 'init.php' ;
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage') { // Starting Manage Page ?>
			
			
		<div class="question-stats">
            <div class="container">
                 <h1 class="text-center h1-members">Divisions</h1>
                 <div class="row">
								 		
                     <div class="col-md-3">                   
                         
                     </div>
                     <div class="col-md-6">
                         <div class="stat st-add-question">
                             <span><a href="division.php?do=Add-division">
								 
								 <i class="fa fa-plus fa-lg"></i><br>
                             	 Add division
								 
								 </a></span>
                         </div>
                    
                 
					 </div>
				</div>
				
				
			
			
			
		<?php } elseif($do == 'Add-division') { // Starting Add Page
			
			
			$rows = getAllFrom("*" , "division" ,"","" ,"division_id", "ASC");
			
			
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
										echo "<td>" . $row['division_id'] . "</td>" ;
										echo "<td>" . $row['division_name'] . "</td>" ;
										echo "<td>



											<a href='division.php?do=add-new-division&divisionID=".$row['division_id']."' class='btn btn-success btn-sub'><i class='fa fa-edit fa-lg'> Add division</i></a>


											<a href='division.php?do=delete-division&divisionID=".$row['division_id']."' class='btn btn-danger confirm'><i class='fa fa-close fa-sm'> Delete</i></a> " ;





										echo "</td>" ;
									echo "</tr>" ;

								}

							?>

						</table>						
					</div>
				</div>

				
				
			
			
		<?php }
		
		elseif($do == 'add-new-division') { ?>
			<h1 class="text-center h1-members">Add New Division</h1>
			
			<div class="container">
				<form class="form-horizontal" action="?do=insert-new-division" method="POST">
							
								<!--Course Name-->
								
								<div class="form-group">
									
									<label class="col-sm-2 control-label">division Name</label>
									<div class="col-xs-2">
										<input type="text" name="division-name" class="form-control" autocomplete="off" placeholder="division Name" required="required" />
									</div>
								</div>
								
								<!-- Ending Course Name-->
					
								 <div class="form-group">
									<div class="col-sm-offset-2 col-sm-10">
										<input type="submit" value="Add Division" class="btn btn-primary btn-sub" />
									</div>
							   </div>
								
				</form>
          <!--Ending Add Course Page	-->
					</div>
			
		<?php }
		
		elseif($do == 'insert-new-division') { // Starting Insert Page
			
			
			echo "<div class='container'>" ;
			
			
			if($_SERVER['REQUEST_METHOD'] == 'POST') {
				
				// Getting Data From The Question Form 
				
				$division_name = $_POST['division-name'] ;
				
				if($division_name == '') {
					$theMsg = "<div class='alert alert-danger'>" ."Please Enter The division Name " .'</div>';
					redirect($theMsg, 'back');				
				}
				
				
				
				$check = checkItem('division_name' , 'division' , $division_name ) ;
				
				if($check > 0) {
					
					
					$theMsg = "<div class='alert alert-danger'>" . ' Sorry This Division Exists</div>';
					redirect($theMsg, 'back');				
					
				}else {					
					
				$stmt = $conn->prepare("INSERT INTO
												 division(division_name )
											VALUES(?)") ;
				$stmt->execute(array($division_name)) ;
				
				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record 					   Inserted</div>';
					redirect($theMsg, 'division.php?do=Add-division');					
					
				}
			
			}
		
			echo "</div>" ;
		
			
		}
			
			
		elseif($do == 'delete-division') {
			
			
			echo "<h1 class='text-center'>Delete division</h1>";
			echo "<div class='container'>";

			$division_id = isset($_GET['divisionID']) && is_numeric($_GET['divisionID']) ? intval($_GET['divisionID']) : 0;
			$check = checkItem('division_id', 'division', $division_id);

			if ($check > 0) {

				$stmt = $conn->prepare("DELETE FROM division WHERE division_id = :zid");

				$stmt->bindParam(":zid", $division_id);

				$stmt->execute();

				$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Deleted</div>';

				redirect($theMsg, 'division.php?do=Add-division');

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
				
				
				
				
				
				
				
				
				
				