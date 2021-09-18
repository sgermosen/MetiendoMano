<?php 
	session_start();
	if(isset($_SESSION['email']))
	{
    ?>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Admin Area | Dashboard</title>
    <!-- Bootstrap core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <script src="http://cdn.ckeditor.com/4.6.1/standard/ckeditor.js"></script>
</head>

<body>
    <!-- Almost Common for Every Page -->
    <?php include 'adminMenu.php' ?>
    <header id="header">
        <div class="container">
            <div class="row">

                <div class="col-md-10">

                    <h1><span class="glyphicon glyphicon-cog" aria-hidden="true"></span> Entrance Test </h1>

                </div>

                <div class="col-md-2">
                    <div class="dropdown create">
                        <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                            Questions
                            <span class="caret"></span>
                        </button>
                        <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
                            <li><a type="button" data-toggle="modal" data-target="#addPage">Add Questions</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        </div>
        </div>
        </div>
        </div>
    </header>
    <section id="breadcrumb">
        <div class="container">
            <ol class="breadcrumb">
                <li class="active">Test Questions</li>
            </ol>
        </div>
    </section>
    <?php include 'side-menu.php' ?>
    <div class="col-md-9">
        <!-- Website Overview -->
        <div class="panel panel-default">
            <div class="panel-heading main-color-bg">
                <h3 class="panel-title">Questions</h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <input class="form-control" type="text" placeholder="Filter Posts...">
                    </div>
                </div>
                <br>
                <table class="table table-striped table-hover table-bordered">
                    <tr>
                        <th>ID</th>
                        <th>Question</th>
                        <th>Created</th>
                        <th>Actions</th>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td>Which of the following operating systems do you choose to implement a client server network?</td>
                        <td>Sep 27, 2017</td>
                        <td><a class="btn btn-default" href="#">Edit</a> <a class="btn btn-danger" href="#">Delete</a>
                        </td>
                    </tr>

                    <tr>
                        <td>2</td>
                        <td>Which of the following operating systems do you choose to implement a client server network?</td>
                        <td>Sep 28, 2017</td>
                        <td><a class="btn btn-default" href="#">Edit</a> <a class="btn btn-danger" href="#">Delete</a>
                        </td>
                    </tr>

                    <tr>
                        <td>3</td>
                        <td>Which of the following operating systems do you choose to implement a client server network?</td>
                        <td>Sep 28, 2017</td>
                        <td><a class="btn btn-default" href="#">Edit</a> <a class="btn btn-danger" href="#">Delete</a>
                        </td>
                    </tr>
                </table>
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
 header("Location: ../admin/");
}
?>
