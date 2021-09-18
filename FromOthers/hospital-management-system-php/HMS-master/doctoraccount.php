
<?php

include("adheader.php");
include 'dbconnection.php';

if(!isset($_SESSION[doctorid]))
{
	echo "<script>window.location='doctorlogin.php';</script>";
}

?>
<div class="container-fluid">
  <div class="block-header">
    <h1>Welcome <?php  $sql="SELECT * FROM `doctor` WHERE doctorid='$_SESSION[doctorid]' ";
    $doctortable = mysqli_query($con,$sql);
    $doc = mysqli_fetch_array($doctortable);

    echo 'Dr. '. $doc[doctorname]; ?>

  </h1>
</div>
</div>





<div class="card">
  <section class="container">
    <div class="row clearfix" style="margin-top: 10px">
      <div class="col-lg-3 col-md-3 col-sm-6">
        <div class="info-box-4 hover-zoom-effect">
          <div class="icon"> <i class="zmdi zmdi-account col-blue"></i> </div>
          <div class="content">
            <div class="text">New Appoiment</div>
            <div class="number"><?php
            $sql = "SELECT * FROM appointment WHERE `doctorid`=1 AND appointmentdate=' ".date("Y-m-d")."'";
            $qsql = mysqli_query($con,$sql);
            echo mysqli_num_rows($qsql);
            ?></div>
          </div>
        </div>
      </div>
      <div class="col-lg-3 col-md-3 col-sm-6">
        <div class="info-box-4 hover-zoom-effect">
          <div class="icon"> <i class="zmdi zmdi-account col-green"></i> </div>
          <div class="content">
            <div class="text">Number of Patient</div>
            <div class="number"><?php
            $sql = "SELECT * FROM patient WHERE status='Active'";
            $qsql = mysqli_query($con,$sql);
            echo mysqli_num_rows($qsql);
            ?></div>
          </div>
        </div>
      </div>
      <div class="col-lg-3 col-md-3 col-sm-6">
        <div class="info-box-4 hover-zoom-effect">
          <div class="icon"> <i class="zmdi zmdi-bug col-blush"></i> </div>
          <div class="content">
            <div class="text">Today's Appoinment</div>
            <div class="number">
              <?php
              $sql = "SELECT * FROM appointment WHERE status='Active' AND `doctorid`=1 AND appointmentdate=' ".date("Y-m-d")."'" ;
            $qsql = mysqli_query($con,$sql);
            echo mysqli_num_rows($qsql);
            ?>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-3 col-md-3 col-sm-6">
        <div class="info-box-4 hover-zoom-effect">
          <div class="icon"> <i class="zmdi zmdi-balance col-cyan"></i> </div>
          <div class="content">
            <div class="text">Total Earning Earning</div>
            <div class="number">৳ 
              <?php 
              $sql = "SELECT sum(bill_amount) as total  FROM `billing_records` WHERE `bill_type` = 'Consultancy Charge'" ;
              $qsql = mysqli_query($con,$sql);
              while ($row = mysqli_fetch_assoc($qsql))
              { 
               echo $row['total'];
             }
              ?>

            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</div>



<?php
include("adfooter.php");
?>