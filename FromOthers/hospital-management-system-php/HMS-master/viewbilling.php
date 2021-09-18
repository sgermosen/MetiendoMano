<?php
session_start();
include("dbconnection.php");
if(isset($_GET[delid]))
{
	$sql ="DELETE FROM billing_records WHERE billingid='$_GET[delid]'";
	$qsql=mysqli_query($con,$sql);
	if(mysqli_affected_rows($con) == 1)
	{
		echo "<script>alert('billing record deleted successfully..');</script>";
	}
}
?>
 <section class="container">


<?php
$sqlbilling_records ="SELECT * FROM billing WHERE appointmentid='$billappointmentid'";
$qsqlbilling_records = mysqli_query($con,$sqlbilling_records);
$rsbilling_records = mysqli_fetch_array($qsqlbilling_records);
?>
 	<table class="table table-bordered table-striped">
      <tbody>
        <tr>
          <th scope="col"><div align="right">Bill number &nbsp; </div></th>
          <td> <?php echo $rsbilling_records[billingid]; ?></td>
        </tr>
        <tr>
          <th width="124" scope="col"><div align="right">Appointment Number &nbsp; </div></th>
          	<td width="413"> <?php echo $rsbilling_records[appointmentid]; ?>
            </td>
        </tr>
         
		<tr>
		  <th scope="col"><div align="right">Billing Date &nbsp; </div></th>
		  <td>&nbsp;<?php echo $rsbilling_records[billingdate]; ?></td>
	    </tr>
        
		<tr>
		  <th scope="col"><div align="right">Billing time&nbsp; </div></th>
		  <td>&nbsp;<?php echo $rsbilling_records[billingtime] ; ?></td>
	    </tr>
      </tbody>
    </table>
	<table class="table table-bordered table-striped">
      <thead>
       <tr>
          <th width="97" scope="col">Date</th>
          <th width="245" scope="col">Decription</th>
          <th width="86" scope="col">Bill Amount</th>
        </tr>
        </thead>
        <tbody>
         <?php
		$sql ="SELECT * FROM billing_records where billingid='$rsbilling_records[billingid]'";
		$qsql = mysqli_query($con,$sql);
		$billamt= 0;
		while($rs = mysqli_fetch_array($qsql))
		{
        echo "<tr>
          	<td>&nbsp;$rs[bill_date]</td>
		 	<td>&nbsp; $rs[bill_type]";

if($rs[bill_type] == "Service Charge")
{
 	 $sqlservice_type1 = "SELECT * FROM service_type WHERE service_type_id='$rs[bill_type_id]'";
	$qsqlservice_type1 = mysqli_query($con,$sqlservice_type1);
	$rsservice_type1 = mysqli_fetch_array($qsqlservice_type1);
	echo " - " . $rsservice_type1[service_type];
}
			

if($rs[bill_type]== "Room Rent")
{
		$sqlroomtariff = "SELECT * FROM room WHERE roomid='$rs[bill_type_id]'";
		$qsqlroomtariff = mysqli_query($con,$sqlroomtariff);
		$rsroomtariff = mysqli_fetch_array($qsqlroomtariff);
		echo " : ". $rsroomtariff[roomtype] .  "- Room No." . $rsroomtariff[roomno];
}

if($rs[bill_type] == "Consultancy Charge")
{
	//Consultancy Charge
	$sqldoctor = "SELECT * FROM doctor WHERE doctorid='$rs[bill_type_id]'";
	$qsqldoctor = mysqli_query($con,$sqldoctor);
	$rsdoctor = mysqli_fetch_array($qsqldoctor);
		echo " - Mr.".$rsdoctor[doctorname];
}

if($rs[bill_type] =="Treatment Cost")
{	
	//Treatment Cost
	$sqltreatment = "SELECT * FROM treatment WHERE treatmentid='$rs[bill_type_id]'";
	$qsqltreatment = mysqli_query($con,$sqltreatment);
	$rstreatment = mysqli_fetch_array($qsqltreatment);
	echo " - ".$rstreatment[treatmenttype];
}

if($rs[bill_type]  == "Prescription charge")
{
	$sqltreatment = "SELECT * FROM prescription WHERE treatmentid='$rs[bill_type_id]'";
	$qsqltreatment = mysqli_query($con,$sqltreatment);
	$rstreatment = mysqli_fetch_array($qsqltreatment);
		
	$sqltreatment1 = "SELECT * FROM treatment_records WHERE treatmentid='$rstreatment[treatment_records_id]'";
	$qsqltreatment1 = mysqli_query($con,$sqltreatment1);
	$rstreatment1 = mysqli_fetch_array($qsqltreatment1);

	$sqltreatment2 = "SELECT * FROM treatment WHERE treatmentid='$rstreatment1[treatmentid]'";
	$qsqltreatment2 = mysqli_query($con,$sqltreatment2);
	$rstreatment2 = mysqli_fetch_array($qsqltreatment2);
	echo 	" - " . $rstreatment2[treatmenttype];
} 

	echo " </td><td>&nbsp;Tk. $rs[bill_amount]</td></tr>";
		$billamt = $billamt +  $rs[bill_amount];
		}
		?>
      </tbody>
    </table>

    <table class="table table-bordered table-striped">
      <tbody>
        <tr>
          <th scope="col"><div align="right">Bill Amount &nbsp; </div></th>
          <td>&nbsp;Tk. <?php echo $billamt; ?></td>
        </tr>
        <tr>
          <th width="442" scope="col"><div align="right">Tax Amount (5%) &nbsp; </div></th>
          	<td width="95">&nbsp;Tk. <?php echo $taxamt = 5 * ($billamt / 100); ?>
            </td>
        </tr>
         
		<tr>
		  <th scope="col"><div align="right">Discount &nbsp; </div></th>
		  <td>&nbsp;Tk. <?php echo $rsbilling_records[discount]; ?></td>
	    </tr>
        
		<tr>
		  <th scope="col"><div align="right">Grand Total &nbsp; </div></th>
		  <td>&nbsp;Tk. <?php echo ($billamt + $taxamt)  - $rsbilling_records[discount] ; ?></td>
	    </tr>
      </tbody>
    </table>
  
    </section>