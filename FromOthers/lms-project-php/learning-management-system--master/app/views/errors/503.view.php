<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Open Source | 503</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="<?php asset('css/bootstrap.min.css')?>">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="<?php asset('css/font-awesome.min.css')?>">
    <!-- Ionicons -->
    <link rel="stylesheet" href="<?php asset('css/ionicons.min.css')?>">
    <!-- Theme style -->
    <link rel="stylesheet" href="<?php asset('css/AdminLTE.min.css')?>">
    <!-- iCheck -->
    <link rel="stylesheet" href="<?php asset('js/plugins/iCheck/square/blue.css')?>">

</head>
<body>
<section class="content">

    <div class="error-page">
        <h2 class="headline text-red">500</h2>

        <div class="error-content">
            <h3><i class="fa fa-warning text-red"></i> Oops! Something went wrong.</h3>

            <p>
               <?=$message?>
                Meanwhile, you may <a href="/">return to home page</a> or try using the search form.
            </p>

            <form class="search-form" action="/search" method="get">
                <div class="input-group">
                    <input type="text" name="q" class="form-control" placeholder="Search">

                    <div class="input-group-btn">
                        <button type="submit" class="btn btn-danger btn-flat"><i class="fa fa-search"></i>
                        </button>
                    </div>
                </div>
                <!-- /.input-group -->
            </form>
        </div>
    </div>
    <!-- /.error-page -->

</section>
<!-- jQuery 2.2.0 -->
<script src="<?php asset('js/jquery-3.1.1.min.js')?>"></script>
<!-- Bootstrap 3.3.5 -->
<script src="<?php asset('js/bootstrap.min.js')?>"></script>
</body>
</html>