<?php

$nonavbar = '' ;

 include 'init.php' ;
 $output = '';
	
 if(isset($_POST['search'])) {
	 
	  $query = "
	  SELECT * FROM tbl_customer 
	  WHERE CustomerName LIKE '%".$_POST['search']."%'";

	

 }else {
	 
	  $query = " SELECT * FROM tbl_customer ORDER BY CustomerID";
	 
 }

 	 $stmt = $conn->prepare($query);
	 $stmt->execute();

	  $rows = $stmt->fetchAll();
	 if($stmt->rowCount() > 0) {
		 
		 $output .= '
					  <div class="table-responsive">
					   <table class="table table bordered">
						<tr>
						 <th>Customer Name</th>
						 <th>Address</th>
						 <th>City</th>
						 <th>Postal Code</th>
						 <th>Country</th>
						</tr> ' ;
		 foreach($rows as $row) {
			 
			 $output .= '
					   <tr>
						<td>'.$row["CustomerName"].'</td>
						<td>'.$row["Address"].'</td>
						<td>'.$row["City"].'</td>
						<td>'.$row["PostalCode"].'</td>
						<td>'.$row["Country"].'</td>
					   </tr>
					  ';
			 
		 }
		 
		 echo $output;


	 }else {
		 echo 'Data Not Found';
	 }


?>