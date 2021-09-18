<?php

session_start();


$pageTitle = 'testing' ;



	if (isset($_SESSION['Admin_Username'])) {


		include 'init.php' ;
		
		 $output = '';
		
		
	
		
		$do = isset($_GET['do']) ? $_GET['do'] : 'Manage' ;
		
		if($do == 'Manage'){ ?>
			
			<div class="container">
				<h2 style="color:gray;"><i class="fa fa-edit"></i> View Result</h2>
				<ul class="nav nav-tabs">
					<li class="active"><a data-toggle="tab" href="#home">
						<i class="fa fa-align-justify fa-lg"></i>View Result</a>
					</li>
					<li style="float:right"><a href="logout.php">Logout</a></li>
				</ul>
				<div class="tab-content">
					
					<div id="home" class="tab-pane fade in active">
						
						 <br/>
						<h2 align="center">Ajax Live Data Search using Jquery PHP MySql</h2><br />
						<div class="form-group">
							<div class="input-group">
								<span class="input-group-addon">Search</span>
								<input type="text" name="search_text" id="search_text" placeholder="Search by Customer Details" class="form-control" />
							</div>
					   </div>
					   <br/>
					   <div id="result"></div>
	
						
						
					</div>
					
					</div>
					
				
		
			</div>
			
			
			
			
			
			
			
		 <?php }elseif($do == 'Add') { 
			
			
			
		
			
		}elseif($do == 'Insert') {
			
			
			
		}
			
			
			
		
		elseif($do == 'Edit') {
			
			
		}
			
			
		
		
		elseif($do == 'Update') {
			
			
			
	
		}elseif($do =='Delete') {

			
		}
	
		else {
			header('Location: index.php');
			exit();	
		}
		
		
	}

include $tpl.'footer.php' ;



?>

<script>
$(document).ready(function(){

 load_data();

 function load_data(query)
 {
  $.ajax({
   url:"fetch.php",
   method:"POST",
   data:{query:query},
   success:function(data)
   {
    $('#result').html(data);
   }
  });
 }
 $('#search_text').keyup(function(){
  var search = $(this).val();
  if(search != '')
  {
   load_data(search);
  }
  else
  {
   load_data();
  }
 });
});
</script>



