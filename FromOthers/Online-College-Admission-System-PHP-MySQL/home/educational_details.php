<?php
    session_start();
    $a="";
    $msg="";
    if(isset($_SESSION['email']))
	{

        //function this
        include 'variables.php';
        $mysqli = new mysqli($databaseHost,$databaseUsername,$databasePassword,$databaseName);
        //
        $query = "SELECT * FROM student_data WHERE email = '{$_SESSION['email']}'"; 
        $result = $mysqli->query($query) or die($mysqli->error);
        if($result->num_rows > 0) {
            while($row = $result->fetch_assoc()) {
                foreach($row as $val) {
                    $details[] = $val;
                }
            }   
        }

        if(isset($_POST['submitEducationDetails'])){
            // SSC
            $sU = $_POST['sscuniv'];
            $sC = $_POST['ssccoll'];
            $sG = $_POST['sscgrade'];
            $sY = $_POST['sscyearp'];
            $sA =$_POST['sscattemp'];
            //hsc
            $hU = $_POST['hscuniv'];
            $hC = $_POST['hsccoll'];
            $hG = $_POST['hscgrade'];
            $hY = $_POST['hscyearp'];
            $hA = $_POST['hscattemp'];
            //grad
            $gD = $_POST['graddegree'];
            $gU = $_POST['graduniv'];
            $gC = $_POST['gradcoll'];
            $gG = $_POST['gradgrade'];
            $gY = $_POST['gradyearp'];
            $gA = $_POST['gradattempt'];
            //pgrad
            $pD = $_POST['pdegree'];
            $pU = $_POST['puniv'];
            $pC = $_POST['pcoll'];
            $pG = $_POST['pgrade'];
            $pY = $_POST['pyearp'];
            $pA = $_POST['pattemp'];
            //queries
            $id = $details[0];
            $conn = new PDO("mysql:host=$databaseHost;dbname=$databaseName;", $databaseUsername, $databasePassword);
            $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

            $eQuery = $conn->prepare( "SELECT `ID` FROM `education_information` WHERE `ID` = ?" );			
			$eQuery->bindValue( 1, $details[0]);
            $eQuery->execute();
            if($eQuery->rowCount() > 0 )
			{	
                $msg = "We Already Have Your Details";
                $a = "readonly";

			}
			else{
			try{
            $insertEducationQry = "INSERT INTO 
                                   education_information 
                                        VALUES                 
                                        (NULL,'$id',
                                         '$sU','$sC','$sG','$sY','$sA',
                                         '$hU','$hC','$hG','$hY','$hA',
                                         '$gD','$gU','$gC','$gG','$gY','$gA',
                                         '$pD','$pU','$pC','$pG','$pY','$pA',
                                         1)";
                if ($conn->query($insertEducationQry))
                {
                            $msg = "Data Collected Successful. You Can Apply now";
                }
                else
                {
                            $msg = "An Error Occured Contact SysAdmin";
                }
                    }
                    catch(PDOException $e){
                        echo $e;
                    }

            }}
     ?>

<?php include 'home-menu.php'; ?>
    <?php include 'user-side-menu.php'; ?>
              <!-- Educational Details -->
        <div class="container">
            <div class="col-lg-9">
              <div class="panel panel-default">
                    <div class="panel-heading main-color-bg"> <h3 class="panel-title"> <b>Educational Information</b> </h3> </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div class="row">
                                <form action="educational_details.php" method="post">
                                    <table class="table">
                                        <thead class="thead-inverse" style="background-color:#000;color:#fff;">
                                            <tr>
                                                <th>Examination</th>
                                                <th>Name of the University/ Board</th>
                                                <th>School/ College Name</th>
                                                <th>Aggreate Percentage / Grade</th>
                                                <th>Year of Passing</th>
                                                <th>No. of Attempt </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <!-- SSC -->
                                            <tr align="center">
                                                <td>S.S.C</td>
                                                <td>
                                                    <input type="text" name="sscuniv" class="form-control" required <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" name="ssccoll" class="form-control" required <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" size="5" name="sscgrade" style="width:50px;" class="form-control" required <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="number" name="sscyearp" class="form-control" required <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <select class="form-control" name="sscattemp" required <?php echo $a?>>
                                                        <option value="First">First</option>
                                                        <option value="Second">Second</option>
                                                    </select>
                                                </td>
                                            </tr>

                                            <!-- HSC -->
                                            <tr align="center">
                                                <td>H.S.C</td>
                                                <td>
                                                    <input type="text" name="hscuniv" class="form-control" required <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" name="hsccoll" class="form-control" required<?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" size="5" name="hscgrade" style="width:50px;" class="form-control" required <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="number" name="hscyearp" class="form-control" required <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <select class="form-control" name="hscattemp" required <?php echo $a?>>
                                                        <option value="First">First</option>
                                                        <option value="Second">Second</option>
                                                    </select>
                                                </td>
                                            </tr>

                                            <!-- Graduation -->
                                            <tr align="center">
                                                <td>Graduation
                                                    <br>
                                                    <font size="-2">Degree(optional)</font>
                                                    <input type="text" size="10" class="form-control" name="graddegree" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" name="graduniv" class="form-control" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" name="gradcoll" class="form-control" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" size="5" name="gradgrade" style="width:50px;" class="form-control" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="number" name="gradyearp" class="form-control" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <select class="form-control" name="gradattempt">
                                                        <option value="Select Attempt">Select..</option>
                                                        <option value="First">First</option>
                                                        <option value="Second">Second</option>
                                                    </select>
                                                </td>
                                            </tr>

                                            <!-- P. Graduation -->
                                            <tr align="center">
                                                <td>Post Graduation
                                                    <br>
                                                    <font size="-2" face="Verdana">Degree(optional)</font>
                                                    <input type="text" size="10" class="form-control" tabindex="24" name="pdegree" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" name="puniv" class="form-control" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="text" name="pcoll" class="form-control">
                                                </td>
                                                <td>
                                                    <input type="text" size="5" name="pgrade" style="width:50px;" class="form-control" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <input type="number" name="pyearp" class="form-control" <?php echo $a?>>
                                                </td>
                                                <td>
                                                    <select class="form-control" name="pattemp" <?php echo $a?>>
                                                        <option value="Select Attempt">Select..</option>
                                                        <option value="First">First</option>
                                                        <option value="Second">Second</option>
                                                    </select>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>                         
                            <input type="submit" style="color:#BD0006;" name="submitEducationDetails" class="form-control btn btn-info">
                            <?php echo $msg;?>
							</form>
                    </div>
            </div>        
<?php } else { ?>
<?php 			echo "<script language='javascript'>alert('You are not logged in');
			window.location.href='/Admission/';
			 </script>"; } ?>