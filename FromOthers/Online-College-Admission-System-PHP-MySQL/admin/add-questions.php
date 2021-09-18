<html lang="en">
<head>
   <meta charset="utf-8">
   <meta http-equiv="X-UA-Compatible" content="IE=edge">
   <meta name="viewport" content="width=device-width, initial-scale=1">
   <title>Admin Section | Dashboard</title>
   <!-- Bootstrap core CSS -->
   <link href="css/bootstrap.min.css" rel="stylesheet">
   <link href="css/style.css" rel="stylesheet">
   <script src="http://cdn.ckeditor.com/4.6.1/standard/ckeditor.js"></script>
</head>
<body>
   <!-- Almost Common for Every Page -->
   <?php include'adminMenu.php' ?>
   <header id="header">
      <div class="container">
         <div class="row">
         
            <div class="col-md-10">

               <h1><span class="glyphicon glyphicon-cog" aria-hidden="true"></span> Entrance Test </h1>
               
            </div>

            <div class="col-md-2">
            <div class="dropdown create">
              <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Test Options
                <span class="caret"></span>
              </button>
              <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
<!--                <li><a type="button" data-toggle="modal" data-target="#addPage">Add Questions</a></li> -->
                <li><a href="#">Add Questions</a></li>

              </ul>
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
      <form>
      
                        <div class="form-group">
                          <label>Question Body</label>
                          <textarea name="editor1" class="form-control" placeholder="Page Body">
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                          </textarea>
                        </div>
                        <div class="checkbox">
                          <label>
                            <input type="checkbox" checked> Online
                          </label>
                        </div>
                        <div class="form-group col-lg-3">
      
                          <label>Answer 1 </label>
                          <input type="text" class="form-control " value="ans 1">
      
                          
                          <label>Answer 2</label>
                          <input type="text" class="form-control " value="ans 2">
                          
                          
                          <label>Answer 3</label>
                          <input type="text" class="form-control " value="ans 3">
                          
                          
                          <label>Answer 4</label>
                          <input type="text" class="form-control " value="ans 4">
      
                          <label>
                            <!-- Add Correct Answer Logic Soon -->
                          </label>
                              <input type="submit" class="form-control btn btn-default" value="Submit">
                        </div>
      
                      </form>
                    </div>
                    </div>
      
                </div>
              </div>
            </div>
      

   </section>
   <script>
      CKEDITOR.replace( 'editor1' );
   </script>
   <!-- Bootstrap core JavaScript
      ================================================== -->
   <!-- Placed at the end of the document so the pages load faster -->
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
   <script src="js/bootstrap.min.js"></script>