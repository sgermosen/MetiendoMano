<?php
include("adheader.php");
include("dbconnection.php");
if(isset($_POST[submit]))
{
	if(isset($_GET[editid]))
	{
			 $sql ="UPDATE doctor_timings SET doctorid='$_POST[select2]',start_time='$_POST[ftime]',end_time='$_POST[ttime]',status='$_POST[select]'  WHERE doctor_timings_id='$_GET[editid]'";
		if($qsql = mysqli_query($con,$sql))
		{
			echo "<script>alert('Doctor Timings record updated successfully...');</script>";
		}
		else
		{
			echo mysqli_error($con);
		}	
	}
	else
	{
	$sql ="INSERT INTO doctor_timings(doctorid,start_time,end_time,status) values('$_POST[select2]','$_POST[ftime]','$_POST[ttime]','$_POST[select]')";
	if($qsql = mysqli_query($con,$sql))
	{
		echo "<script>alert('Doctor Timings record inserted successfully...');</script>";
	}
	else
	{
		echo mysqli_error($con);
	}
}
}
if(isset($_GET[editid]))
{
	$sql="SELECT * FROM doctor_timings WHERE doctor_timings_id='$_GET[editid]' ";
	$qsql = mysqli_query($con,$sql);
	$rsedit = mysqli_fetch_array($qsql);
	
}
?>


<div class="container-fluid">
	<div class="block-header">
            <h2>Add New Doctor Timings</h2>
            
        </div>
  <div class="card">
    
   <form method="post" action="" name="frmdocttimings" onSubmit="return validateform()">
    <table class="table table-hover">
      <tbody>
        <?php
		if(isset($_SESSION[doctorid]))
		{
			echo "<input class='form-control'  type='hidden' name='select2' value='$_SESSION[doctorid]' >";
		}
		else
		{
		?>      
        <tr>
          <td width="34%" height="36">Doctor</td>
          
          <td width="66%"><select class="form-control"  name="select2" id="select2">
           <option value="">Select</option>
            <?php
          	$sqldoctor= "SELECT * FROM doctor WHERE status='Active'";
			$qsqldoctor = mysqli_query($con,$sqldoctor);
			while($rsdoctor = mysqli_fetch_array($qsqldoctor))
			{
				if($rsdoctor[doctorid] == $rsedit[doctorid])
				{
				echo "<option value='$rsdoctor[doctorid]' selected>$rsdoctor[doctorid] - $rsdoctor[doctorname]</option>";
				}
				else
				{
				echo "<option value='$rsdoctor[doctorid]'>$rsdoctor[doctorid] - $rsdoctor[doctorname]</option>";				
				}
			}
		  ?>
          
          </select></td>
        </tr>
        <?php
		}
		?>
        <tr>
          <td height="36">From</td>
          <td><input class="form-control"  type="time" name="ftime" id="ftime" value="<?php echo $rsedit[start_time]; ?>"></td>
        </tr>
        <tr>
          <td height="34">To</td>
          <td><input  class="form-control" type="time" name="ttime" id="ttime"  value="<?php echo $rsedit[end_time]; ?>" ></td>
        </tr>
        <tr>
          <td height="33">Status</td>
          <td><select class="form-control"  name="select" id="select">
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
           </select></td>
        </tr>
        <tr>
          <td height="36" colspan="2" align="center"><input class="btn btn-default" type="submit" name="submit" id="submit" value="Submit" /></td>
        </tr>
      </tbody>
    </table>
    </form>
    <p>&nbsp;</p>
  </div>
</div>
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
	if(document.frmdocttimings.select2.value == "")
	{
		alert("doctor name should not be empty..");
		document.frmdocttimings.select2.focus();
		return false;
	}
	else if(document.frmdocttimings.ftime.value == "")
	{
		alert("from time should not be empty..");
		document.frmdocttimings.ftime.focus();
		return false;
	}
	else if(document.frmdocttimings.ttime.value == "")
	{
		alert("To time should not be empty..");
		document.frmdocttimings.ttime.focus();
		return false;
	}
	
	else if(document.frmdocttimings.select.value == "" )
	{
		alert("Kindly select the status..");
		document.frmdocttimings.select.focus();
		return false;
	}
	else
	{
		return true;
	}
}
</script>