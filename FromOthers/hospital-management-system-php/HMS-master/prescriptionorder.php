<?php
include("adheader.php");
include("dbconnection.php");
if(isset($_POST[submit]))
{
	if(isset($_GET[editid]))
	{
		$sql ="UPDATE prescription SET treatment_records_id='$_POST[treatmentid]',doctorid='$_POST[select2]',patientid='$_POST[patientid]',prescriptiondate='$_POST[date]',status='$_POST[select]' WHERE prescription_id='$_GET[editid]'";
		if($qsql = mysqli_query($con,$sql))
		{
			echo "<script>alert('prescription record updated successfully...');</script>";
		}
		else
		{
			echo mysqli_error($con);
		}	
	}
	else
	{
		
		
		$sql ="INSERT INTO prescription(treatment_records_id,doctorid,patientid,prescriptiondate,status) values('$_POST[treatmentid]','$_POST[doctorid]','$_POST[patientid]','$_POST[date]','Active')";
		if($qsql = mysqli_query($con,$sql))
		{
			$insid= mysqli_insert_id($con);
			$prescriptionid= $insid;
			$prescriptiondate= $_POST[date];
			$billtype="Prescription charge";
			$billamt=0;			
		$sql ="UPDATE orders SET deliverydate='$_POST[date]', status ='Active',prescriptionid='$prescriptionid' WHERE orderid='$_GET[orderid]'";
		$qsql = mysqli_query($con,$sql);
			echo "<script>alert('prescription record inserted successfully...');</script>";
			echo "<script>window.location='prescriptionorderdetail.php?prescriptionid=" . $insid . "&patientid=$_GET[patientid]';</script>";
		}
		else
		{
			echo mysqli_error($con);
		}
	}
}
if(isset($_GET[editid]))
{
	$sql="SELECT * FROM prescription WHERE prescriptionid='$_GET[editid]' ";
	$qsql = mysqli_query($con,$sql);
	$rsedit = mysqli_fetch_array($qsql);
	
}
if(isset($_GET[orderid]))
{
		$sqlorders ="SELECT * FROM orders where orderid='$_GET[orderid]' ";
		$qsqlorders = mysqli_query($con,$sqlorders);
		$rsorder = mysqli_fetch_array($qsqlorders);
}
?>

<div class="wrapper col2">
  <div id="breadcrumb">
    <ul>
      <li class="first">Add New Prescription</li></ul>
  </div>
</div>
<div class="wrapper col4">
  <div id="container">
    <h1>Add new prescription record</h1>
     <form method="post" action="" name="frmpres" onSubmit="return validateform()">
     <input type="hidden" name="patientid" value="<?php echo $rsorder[patientid]; ?>"  />
     
     <input type="hidden" name="doctorid" value="<?php echo $rsorder[doctorid]; ?>"  />
     <input type="hidden" name="treatmentid" value="0"  />
    <table width="200" border="3">
      <tbody>
        <tr>
          <td>Patient</td>
          <td>
            <?php
		  	$sqlpatient= "SELECT * FROM patient WHERE status='Active' AND patientid='$rsorder[patientid]'";
			$qsqlpatient = mysqli_query($con,$sqlpatient);
			while($rspatient=mysqli_fetch_array($qsqlpatient))
			{
				echo "$rspatient[patientid]- $rspatient[patientname]";
			}
		  ?></td>
        </tr>
        <tr>
          <td width="34%">Doctor</td>
          <td width="66%">
            <?php
          	$sqldoctor= "SELECT * FROM doctor WHERE status='Active'";
			$qsqldoctor = mysqli_query($con,$sqldoctor);
			while($rsdoctor = mysqli_fetch_array($qsqldoctor))
			{
				if($rsdoctor[doctorid] == $rsorder[doctorid])
				{
				echo "<option value='$rsdoctor[doctorid]' selected>$rsdoctor[doctorid]-$rsdoctor[doctorname]</option>";
				}
			}
		  ?></td>
        </tr>
        
        <?php
		if(isset($_SESSION[adminid]))
		{
		?>
        <tr>
          <td>Expected Delivery Date</td>
          <td><input type="date" name="date" id="date" value="<?php echo date("Y-m-d"); ?>" /></td>
        </tr>
        <?php
		}
		?>
        <tr>
          <td colspan="2" align="center"><input type="submit" name="submit" id="submit" value="Submit" /></td>
        </tr>
      </tbody>
    </table>
    <p>&nbsp;</p>
</div>
 <div class="clear"></div>
  </div>
</div>
<?php
include("adfooter.php");
?>
<script type="application/javascript">
function validateform()
{
	if(document.frmpres.select2.value == "")
	{
		alert("Doctor name should not be empty..");
		document.frmpres.select2.focus();
		return false;
	}
	
	else if(document.frmpres.select3.value == "")
	{
		alert("Patient name should not be empty..");
		document.frmpres.select3.focus();
		return false;
	}
	else if(document.frmpres.date.value == "")
	{
		alert("Prescription date should not be empty..");
		document.frmpres.date.focus();
		return false;
	}
	else if(document.frmpres.select.value == "" )
	{
		alert("Kindly select the status..");
		document.frmpres.select.focus();
		return false;
	}
	else
	{
		return true;
	}
}
</script>