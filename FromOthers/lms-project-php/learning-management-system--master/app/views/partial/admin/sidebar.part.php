<aside class="main-sidebar">

    <!-- sidebar: style can be found in sidebar.less -->
    <section class="sidebar">

        <!-- Sidebar user panel (optional) -->
        <div class="logo-lg" style="margin: 5% auto;width: 60%">
            <img src="<?= asset('img/logo.png')?>" alt="" class='img-responsive'>
        </div>

        <!-- search form (Optional) -->

        <!-- /.search form -->

        <!-- Sidebar Menu -->
        <ul class="sidebar-menu">
            <!-- Optionally, you can add icons to the links -->
            <?php if(\App\Core\Session::isLogin()&&\App\Core\Session::getLoginUser()->role=="admin"):?>
                <li><a href="/admin"><i class="fa fa-dashboard"></i> <span>Dashboard</span></a></li>
                <li class="treeview">
                    <a href="#"><i class="fa fa-edit"></i> <span>Manage</span> <i class="fa fa-angle-left pull-right"></i></a>
                    <ul class="treeview-menu">
                        <li><a href="/cats/create">Categories</a></li>
                        <li><a href="/courses/create">Courses</a></li>
                        <li><a href="/materials/create">Material</a></li>
                        <li><a href="/requests/create">Request</a></li>
                        <li><a href="/users/create">Users</a></li>
                    </ul>
                </li>
            <?php endif;?>
            <li><a href="/"><i class="fa fa-home"></i> <span>Home</span></a></li>
            <li><a href="/cats"><i class="ion ion-ios-list"></i> <span>Categories</span></a></li>
            <li><a href="/courses"><i class="ion ion-ios-book"></i> <span>Courses</span></a></li>
            <li><a href="/materials"><i class="fa fa-book"></i> <span>Materials</span></a></li>
            <li><a href="/requests"><i class="fa fa-inbox"></i> <span>Requests</span></a></li>
            <li><a href="/users"><i class="fa fa-users"></i> <span>Students</span></a></li>
            <?php if(\App\Core\Session::isLogin()):?>
                <li><a href="/users/<?=\App\Core\Session::getLoginUser()->id?>"><i class="fa fa-user"></i> <span>Profile</span></a></li>
            <?php endif;?>
            <li><a href="/about"><i class="fa fa-info"></i> <span>About Us</span></a></li>
            <li><a href="/contact"><i class="fa fa-phone"></i> <span>Contact Us</span></a></li>
        </ul>
        <!-- /.sidebar-menu -->
    </section>
    <!-- /.sidebar -->
</aside>