<?php 
	session_start();
	if(isset($_SESSION['email']))
	{
    ?>
<html>
<?php include 'home-menu.php';?>
<?php include 'user-side-menu.php' ; ?>
<div class="col-md-9">
  <!-- Website Overview -->
  <div class="panel panel-default">
    <div class="panel-heading main-color-bg" style="background: #BD0006 !important;">
      <h3 class="panel-title" style="color: white !important;">Instructions</h3>
    </div>
    <div class="panel-body">
      <p>1. Once registered on the portal you can apply for individual Courses given in the options 
      </p>
      <p>2. You'll have to submit your educational details in your profile in the 'Profile' section. 
      </p>
      <p>3. After submitting the educational details apply for Course. 
      </p>
      <p>4. In course selection select the respective School and then the respective course 
      </p>
    </div>
  </div>
</div>
</div>
<!-- To add same code before the last div -- >
</html>
<?php } else { ?>
<?php  						echo "<script language='javascript'>alert('You are not logged in');
			window.location.href='../Admission/';
			 </script>"; } ?>