<?php
	session_start();
    if(isset($_SESSION['email']))
    {
      $email = $_SESSION['email'];
      include 'variables.php';
    ?>
    <html lang="en">

    <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <title>Search Results</title>
        <!-- Bootstrap core CSS -->
        <link href="css/bootstrap.min.css" rel="stylesheet">
        <link href="css/style.css" rel="stylesheet">
        <script src="http://cdn.ckeditor.com/4.6.1/standard/ckeditor.js"></script>
        <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
        <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
        <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css">
        <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.4.2/css/buttons.dataTables.min.css">
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
        <script type="text/javascript" src="https://cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
        <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.4.2/js/dataTables.buttons.min.js"></script>
        <script type="text/javascript" src="//cdn.datatables.net/buttons/1.4.2/js/buttons.print.min.js"></script>

        <!-- Date picker JS-->
        <script>
            $(function() {
                var dateFormat = "dd-mm-yy",
                    from = $("#from")
                    .datepicker({
                        defaultDate: "+1w",
                        changeMonth: true,
                        numberOfMonths: 3,
                        changeMonth: true,
                        changeYear: true

                    })
                    .on("change", function() {
                        to.datepicker("option", "minDate", getDate(this));
                    }),
                    to = $("#to").datepicker({
                        defaultDate: "+1w",
                        changeMonth: true,
                        numberOfMonths: 3,
                        changeMonth: true,
                        changeYear: true
                    })
                    .on("change", function() {
                        from.datepicker("option", "maxDate", getDate(this));
                    });

                function getDate(element) {
                    var date;
                    try {
                        date = $.datepicker.parseDate(dateFormat, element.value);
                    } catch (error) {
                        date = null;
                    }
                    return date;
                }
            });
        </script>

        <script type="text/javascript">
            $(document).ready(function() {
                $('#record').DataTable({
                    dom: 'Bfrtip',
                    buttons: [
                        'print'
                    ]
                });
            });
        </script>
    </head>

    <body>
        <!-- Almost Common for Every Page -->
        <?php include'adminMenu.php' ?>
        <header id="header">
            <div class="container">
                <div class="row">
                    <div class="col-md-10">
                        <h1><span class="glyphicon glyphicon-cog" aria-hidden="true"></span> Search Records </h1>
                    </div>
                </div>
            </div>
            </div>
            </div>
        </header>
        <section id="breadcrumb">
            <div class="container">
                <ol class="breadcrumb">
                    <li class="active">Records</li>
                </ol>
            </div>
        </section>
        <?php include 'side-menu.php' ?>
        <div class="col-md-9">
            <!-- Website Overview -->
            <div class="panel panel-default">
                <div class="panel-heading main-color-bg">
                    <h3 class="panel-title">Admissions Record</h3>
                </div>
                <div class="panel-body">
                    <div class="row">
                        <form action="records.php" method="post">
                            <!--            <div class="col-md-2">
                                <div class="form-group">
                                    <label for="exampleSelect1">Select Course</label>
                                    <select class='form-control' id='exampleSelect1' name='cname'>
								                     <?php
								                          $mysqli = mysqli_connect($databaseHost,$databaseUsername,$databasePassword,$databaseName);
								                          $query = "SELECT * FROM courses";
								                          $result = mysqli_query($mysqli,$query);
								                          while($addrow = mysqli_fetch_array($result)){
								                            echo "<option>$addrow[1]</option>";
								                          }
								                     ?>
                    						</select>
                              </div>
                            </div> -->
                    <br/>
                    <table id="record" class="table table-striped table-hover table-bordered">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>PCM</th>
                                <th>ACPC No.</th>
                                <th>ACPC Merit</th>
                                <th>JEE Rank</th>
                                <th>P1</th>
                                <th>P2</th>
                                <th>P3</th>
                                <th>P4</th>
                                <th>Contact</th>
                            </tr>
                        </thead>
                        <?php
                $mysqli = mysqli_connect($databaseHost,$databaseUsername,$databasePassword,$databaseName);
                  $query = "select student_data.FULLNAME,education_information_be.hsc_pcm,education_information_be.acpc_no,education_information_be.acpc_merit,education_information_be.jee_main_rank,
                  education_information_be.p1,
                  education_information_be.p2,
                  education_information_be.p3,
                  education_information_be.p4,
                  student_data.pnumber,
                  student_data.id
                  from education_information_be
                  join student_data ON
                  student_data.id = education_information_be.ID";
                  $result = mysqli_query($mysqli,$query);
                  while($addrow = mysqli_fetch_array($result)){
                    $newDate = date('Y-m-d',strtotime($addrow[4]));
                    echo "<tr>";
                    echo "<td><a href='view.php?id=$addrow[10]' target='_blank'>" .$addrow[0] . "</a></td>";                    
                    echo "<td>$addrow[1]</td>";
                    echo "<td>$addrow[2]</td>";
                    echo "<td>$addrow[3]</td>";
                    echo "<td>$addrow[4]</td>";
                    echo "<td>$addrow[5]</td>";
                    echo "<td>$addrow[6]</td>";
                    echo "<td>$addrow[7]</td>";
                    echo "<td>$addrow[8]</td>";
                    echo "<td>$addrow[9]</td>";
                    echo "</tr>";

                }
            ?>
                    </table>
                    </form>
                </div>
            </div>
        </div>
        </div>
        </div>
        </section>

        <?php
  }
else{
?>
<?php 
			echo "<script language='javascript'>alert('Login to continue');
			window.location.href='/Online-College-Admission-System-PHP-MySQL/admin';
			 </script>";
       
        } ?>