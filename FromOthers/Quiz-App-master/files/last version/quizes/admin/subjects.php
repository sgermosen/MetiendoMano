<?php

	session_start();
	include 'init.php' ;


		
	// Getting Subjects 


	 $stmt = $conn->prepare("SELECT 
                                        id , subj_name 
                                FROM 
                                        subject 
                                                               
                                ");
        
        $stmt->execute();
        $subjects = $stmt->fetchAll();
        $count = $stmt->rowCount();
        

	








	
	$do = isset($_GET['do']) ? $_GET['do'] : 'Manage';


	// If The Page Is Main Page
	if ($do == 'Manage') {?>
		
		
		<center><a href="?do=Add" class="btn btn-primary" style="margin:100px">Add Questions</a></center>
		
		
		
		
	<?php }
	elseif ($do == 'Add') {
		
		// Starting Add Page ?>
		
		            <h1 class="text-center page-title">Add New Questions</h1>
            <div class="container">
                
                <form class="form-horizontal" action="?do=Insert" method="POST">
                    
                     <!--Strat Username -->
				   <div class="form-group" >
                        <label class="col-sm-2 control-label">Question</label>
                         <div class="col-md-6">
                            <input type="text" name="qus" class="form-control" placeholder="Enter question"/>
					   	</div>
                   </div>
				   <!--End Username -->
                   
                   <div class="form-group" >
                        <label class="col-sm-2 control-label">option-1</label>
                        
                        <div class="col-md-6">
                            <input type="text" name="op1" class="form-control" placeholder="Enter option-1"/>
                        </div>
                   </div>
                   <div class="form-group" >
                        <label class="col-sm-2 control-label">option-2</label>
                        
                        <div class="col-md-6">
                            <input type="text" name="op2" class="form-control" placeholder="Enter option-1"/>
                        </div>
                   </div>
                   <div class="form-group" >
                        <label class="col-sm-2 control-label">option-3</label>
                        
                        <div class="col-md-6">
                            <input type="text" name="op3" class="form-control" placeholder="Enter option-1"/>
                        </div>
                   </div>
                   <div class="form-group" >
                        <label class="col-sm-2 control-label">option-3</label>
                        
                        <div class="col-md-6">
                            <input type="text" name="op4" class="form-control" placeholder="Enter option-1"/>
                        </div>
                   </div>
                    <div class="form-group" >
                        <label class="col-sm-2 control-label">Answer</label>
                        
                        <div class="col-md-6">
                            <input type="text" name="answer" class="form-control" placeholder="Enter Correct Answer"/>
                        </div>
                   </div>
					<div class="form-group">
						
						<div class="col-sm-2"></div>
						
						<div class="col-md-6">
							
							<select class="form-control" id="sel1" name="subject">
							   
							   
						   
								<option value="" disabled>choose Subjects</option>
								<?php
								foreach($subjects as $subject)
								{
									echo "<option value=".$subject['id'].">".$subject['subj_name']."</option>";
								}
								
								?>								
						  </select>
							   
						</div>
						
						   
						</div>
					<div class="col-md-4"></div>
                   <button type="submit" class="btn btn-primary col-md-2">Submit</button><br>
            
                </form>
                
                
            </div>
		
		
		
		
		
		
		
		
		
		
		
		
	
		
	<?php // Ending 
	} elseif ($do == 'Insert') { // Starting Insert Page 

	$question = $_POST['qus'] ;
	$op1 = $_POST['op1'] ;
	$op2 = $_POST['op2'] ;
	$op3 = $_POST['op3'] ;
	$op4 = $_POST['op4'] ;
	$right_ans = $_POST['answer'] ;
	$choosen_subj = intval($_POST['subject']) ;
		
		
		

	$stmt = $conn->prepare("INSERT
							INTO
								questions (question , ans1 , ans2 , ans3 , ans4 ,ans)
								VALUES(?,?,?,?,?,?)
							WHERE subj_id = ? ") ;
							
							$stmt->execute(array($question , $op1 , $op2 , $op3 , $op4 , $right_ans , $choosen_subj )) ;
							$count = $stmt->rowCount() ;

		if($count > 0 ) {
			
			
			msg ("success" , "Successfully Added") ;
			
			
			
		}



		
		
		
		
		

	

	} else {
		
		
		echo 'Error There\'s No Page With This Name';
		
		
	}




?>





<?php 
    
    include $tpl.'footer.php';
?>
