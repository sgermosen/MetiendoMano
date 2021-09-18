<?php
include("adheader.php");
include("dbconnection.php");
if(isset($_POST[submit]))
{
	if(isset($_GET[editid]))
	{
		$sql ="UPDATE treatment SET treatmenttype='$_POST[treatmenttype]',treatment_cost='$_POST[treatmentcost]',note='$_POST[textarea]',status='$_POST[select]' WHERE treatmentid='$_GET[editid]'";
		if($qsql = mysqli_query($con,$sql))
		{
			echo "<script>alert('treatment record updated successfully...');</script>";
		}
		else
		{
			echo mysqli_error($con);
		}	
	}
	else
	{
		$sql ="INSERT INTO treatment(treatmenttype,treatment_cost,note,status) values('$_POST[treatmenttype]','$_POST[treatmentcost]', '$_POST[textarea]','$_POST[select]')";
		if($qsql = mysqli_query($con,$sql))
		{
			echo "<script>alert('treatment record inserted successfully...');</script>";
		}
		else
		{
			echo mysqli_error($con);
		}
	}
}
if(isset($_GET[editid]))
{
	$sql="SELECT * FROM treatment WHERE treatmentid='$_GET[editid]' ";
	$qsql = mysqli_query($con,$sql);
	$rsedit = mysqli_fetch_array($qsql);
	
}
?>


<div class="container-fluid">
	<div class="block-header">
		<h2>Add New treatment</h2>
	</div>
	<div class="row clearfix">
		<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
			<div class="card">



				<form method="post" action="" name="frmtreat" onSubmit="return validateform()">
					<div class="row">
						<div class="col-sm-4 col-xs-12">
							<div class="form-group">
								<label for="type">Treatment Type</label>
								<div class="form-line">
									<input type="text" class="form-control" name="treatmenttype" id="treatmenttype"
									value="<?php echo $rsedit[treatmenttype]; ?>">
								</div>
							</div>
						</div>
						<div class="col-sm-4 col-xs-12">
							<div class="form-group">
								<label for="type">Treatment Cost</label>
								<div class="form-line">
									<input type="text" class="form-control" name="treatmentcost" id="treatmentcost"
									value="<?php echo $rsedit[treatment_cost]; ?>" />
								</div>
							</div>
						</div>
						<div class="col-sm-4 col-xs-12">
							<div class="form-group">
								<label>Status</label>
								<div class="form-line">

									<select name="select" id="select" class=" form-control show-tick">
										<option value="">Select</option>
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
					</div>
					<div class="col-sm-12">
						<div class="form-group">
							<label for="">Note</label>
							<div class="form-line">
								<textarea name="textarea" class="form-control no-resize" id="textarea" cols="45"
								rows="5"><?php echo $rsedit[note] ; ?></textarea>


							</div>
						</div>
					</div>
					<div class="col-sm-12">
						<input type="submit" name="submit" id="submit" value="Submit" class="btn btn-raised" />
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
function validateform() {
	if (document.frmtreat.treatmenttype.value == "") {
		alert("Treatment type should not be empty..");
		document.frmtreat.treatmenttype.focus();
		return false;
	} else if (!document.frmtreat.treatmenttype.value.match(alphaspaceExp)) {
		alert("Treatment type not valid..");
		document.frmtreat.treatmenttype.focus();
		return false;
	} else if (document.frmtreat.select.value == "") {
		alert("Kindly select the status..");
		document.frmtreat.select.focus();
		return false;
	} else {
		return true;
	}
}
</script>