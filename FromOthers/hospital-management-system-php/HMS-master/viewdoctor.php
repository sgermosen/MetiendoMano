<?php
include("adformheader.php");
include("dbconnection.php");
if(isset($_GET[delid]))
{
	$sql ="DELETE FROM doctor WHERE doctorid='$_GET[delid]'";
	$qsql=mysqli_query($con,$sql);
	if(mysqli_affected_rows($con) == 1)
	{
		echo "<script>alert('doctor record deleted successfully..');</script>";
	}
}
?>
<div class="container-fluid">
	<div class="block-header">
		<h2>View  Doctor</h2>

	</div>

<div class="card">

	<section class="container">
		<table class="table table-bordered table-striped table-hover js-basic-example dataTable">
			<thead>
				<tr>
					<td>Doctor Name</td>
					<td>Mobile Number</td>
					<td>Department</td>
					<td>Login ID</td>
					<td>Consultancy Charge</td>
					<td>Education</td>
					<td>Experience</td>
					<td>Status</td>
					<td>Action</td>
				</tr>
			</thead>
			<tbody>
				
				<?php
				$sql ="SELECT * FROM doctor";
				$qsql = mysqli_query($con,$sql);
				while($rs = mysqli_fetch_array($qsql))
				{

					$sqldept = "SELECT * FROM department WHERE departmentid='$rs[departmentid]'";
					$qsqldept = mysqli_query($con,$sqldept);
					$rsdept = mysqli_fetch_array($qsqldept);
					echo "<tr>
					<td>&nbsp;$rs[doctorname]</td>
					<td>&nbsp;$rs[mobileno]</td>
					<td>&nbsp;$rsdept[departmentname]</td>
					<td>&nbsp;$rs[loginid]</td>
					<td>&nbsp;$rs[consultancy_charge]</td>
					<td>&nbsp;$rs[education]</td>
					<td>&nbsp;$rs[experience] year</td>
					<td>$rs[status]</td>
					<td>&nbsp;
					<a href='doctor.php?editid=$rs[doctorid]' class='btn btn-sm btn-raised g-bg-cyan'>Edit</a> <a href='viewdoctor.php?delid=$rs[doctorid]' class='btn btn-sm btn-raised g-bg-blush2'>Delete</a> </td>
					</tr>";
				}
				?>      </tbody>
			</table>
		</section>
	</div>
</div>
	<?php
	include("adformfooter.php");
	?>