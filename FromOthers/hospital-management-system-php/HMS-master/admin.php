<?php
include("adheader.php");
include("dbconnection.php");
if(isset($_POST[submit]))
{
	if(isset($_GET[editid]))
	{
		$sql ="UPDATE admin SET adminname='$_POST[adminname]',loginid='$_POST[loginid]',password='$_POST[password]',status='$_POST[select]' WHERE adminid='$_GET[editid]'";
		if($qsql = mysqli_query($con,$sql))
		{
			echo "<div class='alert alert-success'>
			Admin Record updated successfully
			</div>";
		}
		else
		{
			echo mysqli_error($con);
		}	
	}
	else
	{
		$sql ="INSERT INTO admin(adminname,loginid,password,status) values('$_POST[adminname]','$_POST[loginid]','$_POST[password]','$_POST[select]')";
		if($qsql = mysqli_query($con,$sql))
		{
			echo "<div class='alert alert-success'>
			Admin Record Inserted successfully
			</div>";
		}
		else
		{
			echo mysqli_error($con);
		}
	}
}
if(isset($_GET[editid]))
{
	$sql="SELECT * FROM admin WHERE adminid='$_GET[editid]' ";
	$qsql = mysqli_query($con,$sql);
	$rsedit = mysqli_fetch_array($qsql);
	
}
?>

<div class="container-fluid">
	<div class="block-header">
		<h2> Add New Admin </h2>
	</div>
	<div class="row clearfix">
		<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
			<div class="card">

				<form method="post" action="" name="frmadminprofile" onSubmit="return validateform()">


					<div class="body">
						<div class="row clearfix">
							<div class="col-sm-12">   
								<div class="form-group">
									<label> Admin Name</label>
									<div class="form-line">
										<input type="text" class="form-control"  name="adminname" id="adminname" value="<?php echo $rsedit[adminname]; ?>"/>
									</div>
								</div>

							</div>	

						</div>
						<div class="row clearfix"> 
							<div class="col-sm-12">                           
								<div class="form-group">
									<label>Admin Log in Id</label>
									<div class="form-line">
										<input type="text" class="form-control" name="loginid" id="loginid" value="<?php echo $rsedit[loginid]; ?>" />
									</div>
								</div>    
							</div>                      
						</div>  
						<div class="row clearfix"> 
							<div class="col-sm-12">                              
								<div class="form-group">
									<label> Admin Password</label>
									<div class="form-line">
										<input type="password" class="form-control"  name="password" id="password" value="<?php echo $rsedit[password]; ?>"/>
									</div>
								</div>
							</div>                          
						</div> 
						<div class="row clearfix"> 
							<div class="col-sm-12">                              
								<div class="form-group">
									<label>Confirm Admin Password</label>
									<div class="form-line">
										<input type="password" class="form-control"  name="cnfirmpassword" id="cnfirmpassword" value="<?php echo $rsedit[confirmpassword]; ?>"/>
									</div>
								</div>
							</div>                          
						</div> 
						<div class="row clearfix">                            
							<div class="col-sm-3 col-xs-12">
								<div class="form-group drop-custum">
									<label>Status</label>

									<select class="form-control show-tick" name="select">
										<option value="" selected>Select One</option>
										<?php
										$arr = array("Active","Inactive");
										foreach($arr as $val)
										{
											if($val == $rsedit[status])
											{
												echo "<option value='$val' selected>$val</option>";
											}
											else
											{
												echo "<option value='$val'>$val</option>";			  
											}
										}
										?>
									</select>
								</div>
							</div>                            
						</div>                    

						<div class="col-sm-12">
							<input type="submit" class="btn btn-raised g-bg-cyan" name="submit" id="submit" value="Submit" />

						</div>
					</div>


				</form>
			</div>
		</div>
	</div>
</div>

				<?php
				include("adfooter.php");
				?>
				<script type="application/javascript">
var alphaExp = /^[a-zA-Z]+$/; //Variable to validate only alphabets
var alphaspaceExp = /^[a-zA-Z\s]+$/; //Variable to validate only alphabets and space
var numericExpression = /^[0-9]+$/; //Variable to validate only numbers
var alphanumericExp = /^[0-9a-zA-Z]+$/; //Variable to validate numbers and alphabets
var emailExp = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/; //Variable to validate Email ID 

function validateform()
{
	if(document.frmadmin.adminname.value == "")
	{
		alert("Admin name should not be empty..");
		document.frmadmin.adminname.focus();
		return false;
	}
	else if(!document.frmadmin.adminname.value.match(alphaspaceExp))
	{
		alert("Admin name not valid..");
		document.frmadmin.adminname.focus();
		return false;
	}
	else if(document.frmadmin.loginid.value == "")
	{
		alert("Login ID should not be empty..");
		document.frmadmin.loginid.focus();
		return false;
	}
	else if(!document.frmadmin.loginid.value.match(alphanumericExp))
	{
		alert("Login ID not valid..");
		document.frmadmin.loginid.focus();
		return false;
	}
	else if(document.frmadmin.password.value == "")
	{
		alert("Password should not be empty..");
		document.frmadmin.password.focus();
		return false;
	}
	else if(document.frmadmin.password.value.length < 8)
	{
		alert("Password length should be more than 8 characters...");
		document.frmadmin.password.focus();
		return false;
	}
	else if(document.frmadmin.password.value != document.frmadmin.cnfirmpassword.value )
	{
		alert("Password and confirm password should be equal..");
		document.frmadmin.password.focus();
		return false;
	}
	else if(document.frmadmin.select.value == "" )
	{
		alert("Kindly select the status..");
		document.frmadmin.select.focus();
		return false;
	}
	else
	{
		return true;
	}
}
</script>