<?php
include("header.php");
include("dbconnection.php");
if(isset($_POST[submit]))
{
	if(isset($_GET[editid]))
	{
			$sql ="UPDATE service_type SET service_type='$_POST[servicetype]',servicecharge='$_POST[servicecharge]',description='$_POST[textarea]',status= '$_POST[select]' WHERE service_type_id='$_GET[editid]'";
		if($qsql = mysqli_query($con,$sql))
		{
			echo "<script>alert('servicetype record updated successfully...');</script>";
		}
		else
		{
			echo mysqli_error($con);
		}	
	}
	else
	{
	$sql ="INSERT INTO service_type(service_type,servicecharge,description,status) values('$_POST[servicetype]','$_POST[servicecharge]','$_POST[textarea]','$_POST[select]')";
	if($qsql = mysqli_query($con,$sql))
	{
		echo "<script>alert('servicetype record inserted successfully...');</script>";
	}
	else
	{
		echo mysqli_error($con);
	}
}
}
if(isset($_GET[editid]))
{
	$sql="SELECT * FROM service_type WHERE service_type_id='$_GET[editid]' ";
	$qsql = mysqli_query($con,$sql);
	$rsedit = mysqli_fetch_array($qsql);
	
}
?>

<div class="wrapper col2">
  <div id="breadcrumb">
    <ul>
      <li class="first">Add New service type</li></ul>
  </div>
</div>
<div class="wrapper col4">
  <div id="container">
    <h1>Add new Service type record</h1>
    <form method="post" action="" name="frmserv" onSubmit="return validateform()">
    <table width="200" border="3">
      <tbody>
        <tr>
          <td width="34%">Service Type</td>
          <td width="66%"><input type="text" name="servicetype" id="servicetype" value="<?php echo $rsedit[service_type]; ?>" /></td>
        </tr>
        <tr>
          <td>Service Charge</td>
          <td><input type="text" name="servicecharge" id="servicecharge"  value="<?php echo $rsedit[servicecharge]; ?>" /></td>
        </tr>
        <tr>
          <td>Description</td>
          <td><textarea name="textarea" id="textarea" cols="45" rows="5"><?php echo $rsedit[description] ; ?></textarea></td>
        </tr>
        <tr>
          <td>Status</td>
          <td><select name="select" id="select">
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
          <td colspan="2" align="center"><input type="submit" name="submit" id="submit" value="Submit" /></td>
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
include("footer.php");
?>
<script type="application/javascript">
var alphaExp = /^[a-zA-Z]+$/; //Variable to validate only alphabets
var alphaspaceExp = /^[a-zA-Z\s]+$/; //Variable to validate only alphabets and space
var numericExpression = /^[0-9]+$/; //Variable to validate only numbers
var alphanumericExp = /^[0-9a-zA-Z]+$/; //Variable to validate numbers and alphabets
var emailExp = /^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$/; //Variable to validate Email ID 

function validateform()
{
	if(document.frmserv.servicetype.value == "")
	{
		alert("Service Type should not be empty..");
		document.frmserv.servicetype.focus();
		return false;
	}
	else if(!document.frmserv.servicetype.value.match(alphaExp))
	{
		alert("Service Type not valid..");
		document.frmserv.servicetype.focus();
		return false;
	}
	else if(document.frmserv.servicecharge.value == "")
	{
		alert("Service charge should not be empty..");
		document.frmserv.servicecharge.focus();
		return false;
	}
	else if(!document.frmserv.servicecharge.value.match(numericExpression))
	{
		alert("Service charge not valid..");
		document.frmserv.servicecharge.focus();
		return false;
	}
	else if(document.frmserv.textarea.value == "")
	{
		alert("Description should not be empty..");
		document.frmserv.textarea.focus();
		return false;
	}
	else if(document.frmserv.select.value == "" )
	{
		alert("Kindly select the status..");
		document.frmserv.select.focus();
		return false;
	}
	else
	{
		return true;
	}
}
</script>