<?php
include("header.php");
include("dbconnection.php");
if(isset($_POST[submit]))
{
	$servicetypeid= $_POST[servicetypeid];
	$billtype = "Service Charge";
	include("insertbillingrecord.php");
}
?>
<div class="wrapper col2">
  <div id="breadcrumb">
    <ul>
      <li class="first">Add Service Charge</li></ul>
  </div>
</div>
<div class="wrapper col4">
  <div id="container">
    <h1>Add new Service Charge records</h1>
     <form method="post" action="" name="frmbill" onSubmit="return validateform()">

    <table width="342" border="3">
      <tbody>
        <tr>
          <td width="34%">Date </td>
          <td width="66%"><input min="<?php echo date("Y-m-d"); ?>" value="<?php echo date("Y-m-d"); ?>" type="date" name="date" id="date"></td>
        </tr>
        <tr>
         
         
          </select>
          </td>
        </tr>
        <tr>
          <td>Extra charges</td>
          <td><input type="text" name="amount" id="amount"></td>
        </tr>
         
        <tr>
          <td colspan="2" align="center"><input type="submit" name="submit" id="submit" value="Add Service charge" /></td>
        </tr>
      </tbody>
    </table>
    </form>
<?php
$billappointmentid = $_GET[appointmentid];
include("viewbilling.php");
?>
<table width="342" border="3">
<thead>
  <tr>
          <td colspan="2" align="center"><a href='patientreport.php?patientid=<?php echo $_GET[patientid]; ?>'><strong>View Patient Report>></strong></a></td>
        </tr>
      </thead>
    </table>
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
	if(document.frmbill.treatment.value == "")
	{
		alert("Treatment Type should not be empty..");
		document.frmbill.treatment.focus();
		return false;
	}
	else if(!document.frmbill.treatment.value.match(alphaspaceExp))
	{
		alert("Treatment Type not valid..");
		document.frmbill.treatment.focus();
		return false;
	}
	else if(document.frmbill.date.value == "")
	{
		alert("Billing Date should not be empty..");
		document.frmbill.date.focus();
		return false;
	}
	else if(document.frmbill.time.value == "")
	{
		alert("Billing Time should not be empty..");
		document.frmbill.time.focus();
		return false;
	}
	else if(document.frmbill.amount.value == "")
	{
		alert("Amount should not be empty..");
		document.frmbill.amount.focus();
		return false;
	}
	else if(!document.frmbill.amount.value.match(numericExpression))
	{
		alert("Amount not valid..");
		document.frmbill.amount.focus();
		return false;
	}
	else if(document.frmbill.discount.value == "")
	{
		alert("Discount should not be empty..");
		document.frmbill.discount.focus();
		return false;
	}
	else if(!document.frmbill.discount.value.match(numericExpression))
	{
		alert("Discount  not valid..");
		document.frmbill.discount.focus();
		return false;
	}
	else if(document.frmbill.tax.value == "")
	{
		alert("Tax Amount should not be empty..");
		document.frmbill.tax.focus();
		return false;
	}
	else if(!document.frmbill.tax.value.match(numericExpression))
	{
		alert("Tax Amount not valid..");
		document.frmbill.tax.focus();
		return false;
	}
	else if(document.frmbill.bill.value == "")
	{
		alert("Bill Amount should not be empty..");
		document.frmbill.bill.focus();
		return false;
	}
	else if(!document.frmbill.bill.value.match(numericExpression))
	{
		alert("Bill Amount not valid..");
		document.frmbill.bill.focus();
		return false;
	}
	else if(document.frmbill.textarea.value == "")
	{
		alert("Discount Reason should not be empty..");
		document.frmbill.textarea.focus();
		return false;
	}
	else if(!document.frmbill.textarea.value.match(alphaspaceExp))
	{
		alert("Discount Reason  not valid..");
		document.frmbill.textarea.focus();
		return false;
	}
	else if(document.frmbill.paid.value == "")
	{
		alert("Paid Amount should not be empty..");
		document.frmbill.paid.focus();
		return false;
	}
	else if(!document.frmbill.paid.value.match(numericExpression))
	{
		alert("Paid Amount not valid..");
		document.frmbill.paid.focus();
		return false;
	}
	else if(document.frmbill.Dtime.value == "")
	{
		alert("Discharge Time should not be empty..");
		document.frmbill.Dtime.focus();
		return false;
	}
	else if(document.frmbill.Ddate.value == "")
	{
		alert("Discharge Date should not be empty..");
		document.frmbill.Ddate.focus();
		return false;
	}
	else
	{
		return true;
	}
	
}
</script>