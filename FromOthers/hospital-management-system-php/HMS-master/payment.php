<?php
session_start();
include("header.php");
include("dbconnection.php");
if(isset($_POST[submit]))
{
		$sql ="INSERT INTO payment(patientid,appointmentid,paiddate,paidtime,paidamount,status) values('$_GET[patientid]','$_GET[appointmentid]','$_POST[date]','$_POST[time]','$_POST[paidamount]','Active')";
		if($qsql = mysqli_query($con,$sql))
		{
			echo "<script>alert('payment record inserted successfully...');</script>";
		}
		else
		{
			echo mysqli_error($con);
		}
		
		if($_POST[discountamount] != '0')
		{
			 $sql ="UPDATE billing SET discount=$_POST[discountamount]+ discount, discountreason=CONCAT('$_POST[discountreason] , ', discountreason) WHERE appointmentid='$_GET[appointmentid]'";
			$qsql = mysqli_query($con,$sql);
			echo mysqli_error($con);
			
		}
}
if(isset($_SESSION[patientid]))
{
	$sql="SELECT * FROM payment WHERE paymentid='$_GET[editid]' ";
	$qsql = mysqli_query($con,$sql);
	$rsedit = mysqli_fetch_array($qsql);
	
}
$billappointmentid = $_GET[appointmentid];
?>

<div class="wrapper col2">
  <div id="breadcrumb">
    <ul>
      <li class="first">Payment Detail</li></ul>
  </div>
</div>
<div class="wrapper col4">
  <div id="container">
     <form method="post" action="" name="frmpatprfl" onSubmit="return validateform()">     
    <table width="515" border="3">
     <thead>
        <tr>
          <th colspan="2">&nbsp;Add payment details.. </th>
          </tr>
          </thead>
           <tbody>
        <tr>
          <td width="34%">Paid Date</td>
          <td width="66%"><input type="date" value="<?php echo date("Y-m-d"); ?>" name="date" id="date"></td>
        </tr>
        <tr>
          <td>Paid Time</td>
          <td><input type="text" readonly="readonly" value="<?php echo date("H:i:s"); ?>" name="time" id="time"></td>
        </tr>
        <tr>
          <td>Paid Amount</td>
          <td><input name="paidamount" type="text" id="paidamount" value="0"></td>
        </tr>
        <tr>
          <td colspan="2" align="center"><input type="submit" name="submit" id="submit" value="Submit" /></td>
        </tr>
      </tbody>
    </table>
    
    <p>&nbsp;</p>
    </form>
    
<?php
include("viewpaymentreport.php");
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
<div class="clear"></div>
  </div>
</div>
<?php
include("footer.php");
?>
