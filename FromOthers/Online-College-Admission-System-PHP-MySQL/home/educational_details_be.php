<?php
    session_start();
    $a="";
    $msg="";
    if(isset($_SESSION['email']))
    {        
        
        //function this
        $email = $_SESSION['email'];
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

        if(isset($_POST['submitEducationDetails_BE'])){
            //start hardcoding in variables
            //ssc
            $sscboardbe = $_POST['sscboardbe'];
            $sscyearpassbe = $_POST['sscyearpassbe'];
            $sscperbe = $_POST['sscperbe'];
            $sscgradebe = $_POST['sscgradebe'];
            //hsc
            $hscboardbe = $_POST['hscboardbe'];
            $hscyearpassbe = $_POST['hscyearpassbe'];
            $hscpcm = $_POST['hscpcm'];            
            $hscperbe = $_POST['hscperbe'];
            $hscgradebe = $_POST['hscgradebe'];
            //basic
            $rollno = $_POST['rollnobe'];
            $phybe = $_POST['phybe'];
            $chembe = $_POST['chembe'];
            $matbe = $_POST['matbe'];
            $totbe = $_POST['totbe'];
            //acpc
            $rankbe = $_POST['rankbe'];
            $c1be = $_POST['c1be'];
            $c2be = $_POST['c2be'];
            $acpcregbe = $_POST['acpcregbe'];
            $acpcmeritbe = $_POST['acpcmeritbe'];
            $p1 = $_POST['p1'];
            $p2 = $_POST['p2'];
            $p3 = $_POST['p3'];
            $p4 = $_POST['p4'];
            /* End Hardcoding */
            //queries_setting user ID
            $id = $details[0];
            //Connection
            $conn = new PDO("mysql:host=$databaseHost;dbname=$databaseName;", $databaseUsername, $databasePassword);
            $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
            //Query to check education information
            $eQuery = $conn->prepare( "SELECT `ID` FROM `education_information_be` WHERE `ID` = ?" );			
			$eQuery->bindValue(1, $details[0]);
            $eQuery->execute();
            if($eQuery->rowCount() > 0 )
			{	
                $msg = "We Already Have Your Details";
                $a = "readonly";
			}
			else{
			    try{ //Inserting Details
                    $insertEducationQry_be = "INSERT INTO education_information_be (ID,ssc_school,ssc_year,ssc_percentage,ssc_class,hsc_school,hsc_year,hsc_pcm,hsc_percentage,hsc_class,roll_no,physics,chemistry,maths,total,jee_main_rank,contact_01,contact_02,acpc_no,acpc_merit,p1,p2,p3,p4,isAvailable) 
                    
                    VALUES ('$id','$sscboardbe','$sscyearpassbe','$sscperbe','$sscgradebe','$hscboardbe','$hscyearpassbe','$hscpcm','$hscperbe','$hscgradebe','$rollno','$phybe','$chembe','$matbe','$totbe','$rankbe','$c1be','$c2be','$acpcregbe','$acpcmeritbe','$p1','$p2','$p3','$p4',1)";
                if ($conn->query($insertEducationQry_be)) //Inserting (Applying) BE
                {
                    $insertQuery = "INSERT INTO selected_courses values(NULL,'$id','B.E',1)";
                    if ($conn->query($insertQuery)){
                        $msg = "<p style='text-align:center; color:green;'>Application Successful </p>";
                    }
                    else{
                    $msg = "<p style='text-align:center; color:red;'>An Error Occured Contact SysAdmin</p>";
                    }
                }
                else
                {
                            $msg = "An Error Occured Contact SysAdmin";
                }
                    }
                    catch(PDOException $e){
                        echo $e;
                    }
            }
        }
     ?>

    <?php include 'home-menu.php'; ?>
    <?php include 'user-side-menu.php'; ?>
    <script>
        function sum() {
            var w = document.getElementById('phybe').value || 0;
            var x = document.getElementById('chembe').value || 0;
            var y = document.getElementById('matbe').value || 0;

            var z = parseInt(w) + parseInt(x) + parseInt(y);

            document.getElementById('totbe').value = z;
        };

        function handleChange(input) {
            if (input.value < 0) input.value = 0;
            if (input.value > 100) input.value = null;
        };

        $(document).ready(function() {
            $('select').on('change', function(event) {
                var prevValue = $(this).data('previous');
                $('select').not(this).find('option[value="' + prevValue + '"]').show();
                var value = $(this).val();
                $(this).data('previous', value);
                $('select').not(this).find('option[value="' + value + '"]').hide();
            });
        });
    </script>
    <!-- Educational Details -->
    <div class="container">
        <div class="col-lg-9">
            <div class="panel panel-default">
                <div class="panel-heading main-color-bg">
                    <h3 class="panel-title"> <b>Educational Information</b> </h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12">
                        <div class="row">
                            <form action="educational_details_be.php" method="post">
                                <table class="table">
                                    <thead class="thead-inverse" style="background-color:#000;color:#fff;">
                                        <tr>
                                            <th>Examination</th>
                                            <th>Name of the University/ Board</th>
                                            <th>Year Of Passing</th>
                                            <th>PCM%</th>
                                            <th>Percentage/Percentile</th>
                                            <th>Class/Grade</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <!-- SSC -->
                                        <tr align="center">
                                            <td>S.S.C</td>
                                            <td>
                                                <input type="text" name="sscboardbe" class="form-control" required <?php echo $a?>>
                                            </td>

                                            <td>
                                                <input type="text" name="sscyearpassbe" class="form-control" required <?php echo $a?>>
                                            </td>

                                            <td>

                                            </td>

                                            <td>
                                                <input type="text" name="sscperbe" style="width:50px;" class="form-control" required <?php echo $a?>>
                                            </td>

                                            <td>
                                                <input type="text" name="sscgradebe" class="form-control" required <?php echo $a?>>
                                            </td>
                                        </tr>

                                        <!-- HSC -->
                                        <tr align="center">
                                            <td>H.S.C</td>
                                            <td>
                                                <input type="text" name="hscboardbe" class="form-control" required <?php echo $a?>>
                                            </td>
                                            <td>
                                                <input type="text" name="hscyearpassbe" class="form-control" required <?php echo $a?>>
                                            </td>
                                            <td>
                                                <input type="text" name="hscpcm" style="width:50px;" class="form-control" required <?php echo $a?>>
                                            </td>
                                            <td>
                                                <input type="text" name="hscperbe" style="width:50px;" class="form-control" required <?php echo $a?>>
                                            </td>
                                            <td>
                                                <input type="text" name="hscgradebe" class="form-control" required <?php echo $a?>>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                        </div>
                    </div>
                </div>
            </div>
            <!-- JEE and ACPC Information -->
            <div class="panel panel-default">
                <div class="panel-heading main-color-bg">
                    <h3 class="panel-title"> <b>JEE(Main)/ACPC-Information</b> </h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12">
                        <div class="row">
                            <table class="table">
                                <thead class="thead-inverse" style="background-color:#000;color:#fff;">
                                    <tr>
                                        <th>Roll Number</th>
                                        <th>Physics</th>
                                        <th>Chemistry</th>
                                        <th>Mathematics</th>
                                        <th>Total</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <!--JEE-->
                                    <tr align="center">
                                        <td>
                                            <input type="text" name="rollnobe" class="form-control" required <?php echo $a?>>
                                        </td>
                                        <td>
                                            <input type="text" id="phybe" name="phybe" style="width:70px;" class="form-control" required <?php echo $a?> onkeyup="sum();" onchange="handleChange(this);">
                                        </td>

                                        <td>
                                            <input type="text" id="chembe" name="chembe" style="width:70px;" class="form-control" required <?php echo $a?> onkeyup="sum();" onchange="handleChange(this);">
                                        </td>

                                        <td>
                                            <input type="text" id="matbe" name="matbe" style="width:70px;" class="form-control" required <?php echo $a?> onkeyup="sum();" onchange="handleChange(this);">
                                        </td>

                                        <td>
                                            <input type="text" id="totbe" name="totbe" style="width:70px;" class="form-control" required readonly>
                                        </td>
                                    </tr>
                                </tbody>

                                <thead class="thead-inverse" style="background-color:#000;color:#fff;">
                                    <tr>
                                        <th>JEE main Rank</th>
                                        <th>Contact No. 1</th>
                                        <th>Contact No. 2</th>
                                        <th>ACPC Registration No.(optional)</th>
                                        <th>ACPC Merit No.(optional)</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <!--JEE ACPC-->
                                    <tr align="center">
                                        <td>
                                            <input type="text" name="rankbe" class="form-control" required <?php echo $a?>>
                                        </td>

                                        <td>
                                            <input type="text" name="c1be" class="form-control" required <?php echo $a?>>
                                        </td>

                                        <td>
                                            <input type="text" name="c2be" class="form-control" <?php echo $a?>>
                                        </td>

                                        <td>
                                            <input type="text" name="acpcregbe" class="form-control" <?php echo $a?>>
                                        </td>

                                        <td>
                                            <input type="text" name="acpcmeritbe" class="form-control" <?php echo $a?>>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Priority -->
            <div class="panel panel-default">
                <div class="panel-heading main-color-bg">
                    <h3 class="panel-title"> <b>Branch Applied for</b> </h3>
                </div>
                <div class="panel-body">
                    <div class="col-lg-12">
                        <div class="row">
                            <p style="color:#BD0006;"> Branch Applied For (ME - Mechanical, CE- Civil, EE- Electrical, CSE- Computer Science and Engineering) </p>
                            <div class="row">

                                <div class="col-sm-3 form-group">
                                    <label> P1  </label>
                                    <select name="p1" id="" class="form-control" style="width:auto;" required <?php echo $a?>> 
                                        <option value="Select">Select</option>
                                        <option value="ME">ME</option>
                                        <option value="CE">CE</option>
                                        <option value="EE">EE</option>
                                        <option value="CSE">CSE</option>
                                    </select>
                                </div>

                                <div class="col-sm-3 form-group">
                                    <label> P2  </label>
                                    <select name="p2" id="" class="form-control" style="width:auto;" required <?php echo $a?> >
                            <option value="Select">Select</option>
                            <option value="ME">ME</option>
                            <option value="CE">CE</option>
                            <option value="EE">EE</option>
                            <option value="CSE">CSE</option>
                        </select>
                                </div>

                                <div class="col-sm-3 form-group">
                                    <label> P3  </label>
                                    <select name="p3" id="" class="form-control" style="width:auto;" required <?php echo $a?>>
                            <option value="Select">Select</option>
                            <option value="ME">ME</option>
                            <option value="CE">CE</option>
                            <option value="EE">EE</option>
                            <option value="CSE">CSE</option>
                        </select>
                                </div>

                                <div class="col-sm-3 form-group">
                                    <label> P4  </label>
                                    <select name="p4" id="" class="form-control" style="width:auto;" required <?php echo $a?>>
                            <option value="Select">Select</option>
                            <option value="ME">ME</option>
                            <option value="CE">CE</option>
                            <option value="EE">EE</option>
                            <option value="CSE">CSE</option>
                        </select>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <input type="submit" style="color:#BD0006;" name="submitEducationDetails_BE" class="form-control btn btn-info">
            <?php echo $msg ?>
            </form>
            <?php
        }
        else
    {
        ?>
                <?php
			echo "<script language='javascript'>alert('You are not logged in');
			window.location.href='/Admission/';
			 </script>";
    }
    ?>