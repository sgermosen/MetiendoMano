<?php
include("header.php");
include("dbconnection.php");
if(isset($_GET[delid]))
{
	$sql ="DELETE FROM service_type WHERE service_type_id='$_GET[delid]'";
	$qsql=mysqli_query($con,$sql);
	if(mysqli_affected_rows($con) == 1)
	{
		echo "<script>alert('Service type deleted successfully..');</script>";
	}
}
?>
<div class="wrapper col2">
  <div id="breadcrumb">
    <ul>
      <li class="first">View  service type</li></ul>
  </div>
</div>
<div class="wrapper col4">
  <div id="container">
    <h1>View Service type record</h1>
    <table width="200" border="3">
      <tbody>
        <tr>
          <td>Service Type</td>
          <td>Service Charge</td>
          <td>Description</td>
          <td>Status</td>
          <td>Action</td>
        </tr>
          <?php
		$sql ="SELECT * FROM service_type";
		$qsql = mysqli_query($con,$sql);
		while($rs = mysqli_fetch_array($qsql))
		{
        echo "<tr>
          <td>&nbsp;$rs[service_type]</td>
          <td>&nbsp;$rs[servicecharge]</td>
          <td>&nbsp;$rs[description]</td>
			 <td>&nbsp;$rs[status]</td>
          <td>&nbsp; 
		 <a href='servicetype.php?editid=$rs[service_type_id]'>Edit</a> | <a href='viewservicetype.php?delid=$rs[service_type_id]'>Delete</a> </td>
        </tr>";
		}
		?>
      </tbody>
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