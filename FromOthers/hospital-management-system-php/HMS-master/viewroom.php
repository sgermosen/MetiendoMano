<?php
include("header.php");
include("dbconnection.php");
if(isset($_GET[delid]))
{
	$sql ="DELETE FROM room WHERE roomid='$_GET[delid]'";
	$qsql=mysqli_query($con,$sql);
	if(mysqli_affected_rows($con) == 1)
	{
		echo "<script>alert('room deleted successfully..');</script>";
	}
}
?>

<div class="wrapper col2">
  <div id="breadcrumb">
    <ul>
      <li class="first">View Room</li></ul>
  </div>
</div>
<div class="wrapper col4">
  <div id="container">
    <h1>View Room details record</h1>
    <table width="200" border="3">
      <tbody>
        <tr>
          <td width="21%">Room Type</td>
          <td width="21%">Room Number</td>
          <td width="30%">Number of beds</td>
            <td width="30%">Room Tariff</td>
          <td width="14%">Status</td>
          <td width="14%">Action</td>
        </tr>
          <?php
		$sql ="SELECT * FROM room";
		$qsql = mysqli_query($con,$sql);
		while($rs = mysqli_fetch_array($qsql))
		{
        echo "<tr>
          <td>&nbsp;$rs[roomtype]</td>
          <td>&nbsp;$rs[roomno]</td>
		   <td>&nbsp;$rs[noofbeds]</td>
		   <td>&nbsp;$rs[room_tariff]</td>
		<td>&nbsp;$rs[status]</td>
		 <td>&nbsp;<a href='room.php?editid=$rs[roomid]'>Edit</a> | <a href='viewroom.php?delid=$rs[roomid]'>Delete</a> </td>
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