<?php
include("header.php");
include("dbconnection.php");
if(isset($_GET[delid]))
{
	$sql ="DELETE FROM prescription WHERE prescriptionid='$_GET[delid]'";
	$qsql=mysqli_query($con,$sql);
	if(mysqli_affected_rows($con) == 1)
	{
		echo "<script>alert('prescription deleted successfully..');</script>";
	}
}
?>

<div class="wrapper col2">
  <div id="breadcrumb">
    <ul>
      <li class="first">View Prescription</li></ul>
  </div>
</div>
<div class="wrapper col4">
  <div id="container">
    <h1>View prescription record</h1>
<?php
$sql ="SELECT * FROM prescription";
$qsql = mysqli_query($con,$sql);
while($rs = mysqli_fetch_array($qsql))
{
	$sqlpatient = "SELECT * FROM patient WHERE patientid='$rs[patientid]'";
	$qsqlpatient = mysqli_query($con,$sqlpatient);
	$rspatient = mysqli_fetch_array($qsqlpatient);
	
	$sqldoctor = "SELECT * FROM doctor WHERE doctorid='$rs[doctorid]'";
	$qsqldoctor = mysqli_query($con,$sqldoctor);
	$rsdoctor = mysqli_fetch_array($qsqldoctor);
?>			
    <table width="200" border="3">
          <tbody>
            <tr>
              <td><strong>Doctor</strong></td>
              <td><strong>Patient</strong></td>
              <td><strong>Prescription Date</strong></td>
              <td><strong>Status</strong></td>
            </tr>
              <?php
            echo "<tr>
              <td>&nbsp;$rsdoctor[doctorname]</td>
              <td>&nbsp;$rspatient[patientname]</td>
               <td>&nbsp;$rs[prescriptiondate]</td>
            <td>&nbsp;$rs[status]</td>
            
            </tr>";
    
            ?>
          </tbody>
        </table>
        
      <h1>View Prescription record</h1>
        <table width="200" border="3">
          <tbody>
            <tr>
              <td>Medicine</td>
              <td>Cost</td>
              <td>Unit</td>
              <td>Dosage</td>
            </tr>
             <?php
            $sqlprescription_records ="SELECT * FROM prescription_records WHERE prescription_id='$_GET[prescriptionid]'";
            $qsqlprescription_records = mysqli_query($con,$sqlprescription_records);
            while($rsprescription_records = mysqli_fetch_array($qsqlprescription_records))
            {
            echo "<tr>
              <td>&nbsp;$rsprescription_records[medicine_name]</td>
              <td>&nbsp;$rsprescription_records[cost]</td>
               <td>&nbsp;$rsprescription_records[unit]</td>
                <td>&nbsp;$rsprescription_records[dosage]</td>
                  
            </tr>";
            }
            ?>
            <tr>
              <td colspan="6"><div align="center">
                <input type="submit" name="print" id="print" value="Print" onclick="myFunction()"/>
              </div></td>
              </tr>
          </tbody>
        </table>
<?php
}
?>        <p>&nbsp;</p>
  </div>
</div>
</div>
 <div class="clear"></div>
  </div>
</div>
<?php
include("footer.php");
?>