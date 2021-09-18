<?php include("adheader.php");


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

<div class="container-fluid">
  <div class="block-header">
    <h2>View prescription record</h2>

  </div>


    <?php
          $sql ="SELECT * FROM prescription where patientid='$_SESSION[patientid]'";
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
    <div class="card" style="padding: 10px">

  <section class="container">
    <table class="table table-bordered table-striped table-hover ">

        <thead>
           <tr>
              <td><strong>Doctor</strong></td>
              <td><strong>Patient</strong></td>
              <td><strong>Prescription Date</strong></td>
              <td><strong>Status</strong></td>
            </tr>
        </thead>
        <tbody>
          

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
        
        
        <table class="table table-hover table-bordered table-striped">
          <thead>
             <tr>
              <td>Medicine</td>
              <td>Cost</td>
              <td>Unit</td>
              <td>Dosage</td>
            </tr>
          </thead>
          <tbody>
           
            <?php
            $sqlprescription_records ="SELECT * FROM prescription_records LEFT JOIN medicine ON prescription_records.medicine_name=medicine.medicineid WHERE prescription_records.prescription_id='$rs[0]'";
            $qsqlprescription_records = mysqli_query($con,$sqlprescription_records);
            while($rsprescription_records = mysqli_fetch_array($qsqlprescription_records))
            {
              echo "<tr>
              <td>&nbsp;$rsprescription_records[medicinename]</td>
              <td>&nbsp;$rsprescription_records[cost]</td>
              <td>&nbsp;$rsprescription_records[unit]</td>
              <td>&nbsp;$rsprescription_records[dosage]</td>

              </tr>";
            }
            ?>
            
          </tbody>
        </table>
        <input type="submit" class="btn btn-lg" name="print" id="print" value="Print" onclick="myFunction()"/>
      </div>    
      
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
include("adfooter.php");
?>
<script>
  function myFunction()
  {
   window.print();
 }
</script>