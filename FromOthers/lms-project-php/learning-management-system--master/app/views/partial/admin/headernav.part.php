
<header class="main-header">

    <!-- Logo -->
    <a href="/" class="logo">
        <!-- mini logo for sidebar mini 50x50 pixels -->
        <span class="logo-mini"><b>O</b>S</span>
        <!-- logo for regular state and mobile devices -->
        <span class="logo-lg"><b>Open</b>Source</span>
    </a>

    <!-- Header Navbar -->
    <nav class="navbar navbar-static-top" role="navigation">
        <!-- Sidebar toggle button-->
        <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
            <span class="sr-only">Toggle navigation</span>
        </a>
        <!-- Navbar Right Menu -->

        <div class="navbar-custom-menu">
            <ul class="nav navbar-nav">
                <!-- User Account Menu -->
                <?php if(\App\Core\Session::isLogin()):?>
                    <li class="dropdown user user-menu">
                    <!-- Menu Toggle Button -->
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                        <!-- The user image in the navbar-->
                        <img src="<?= \App\Core\Session::getLoginUser()->image?>" alt="" class="user-image">
                        <span class="hidden-xs"><?php \App\Core\Session::getLoginUser()->username?></span>
                        <!-- hidden-xs hides the username on small devices so only the image appears. -->
                    </a>
                    <ul class="dropdown-menu">
                        <!-- The user image in the menu -->
                        <li class="user-header">
                            <img src="<?= \App\Core\Session::getLoginUser()->image?>" alt="" class="img-circle">
                            <p>
                                <?=\App\Core\Session::getLoginUser()->email?>
                                <small>Member since <?=\App\Core\Session::getLoginUser()->created_at?></small>
                            </p>
                        </li>
                        <!-- Menu Body -->
                        <!-- Menu Footer-->
                        <li class="user-footer">
                            <div class="pull-left">
                                <a href="/users/<?=\App\Core\Session::getLoginUser()->id?>" class="btn btn-default btn-flat">Profile</a>
                            </div>
                            <div class="pull-right">
                                <a href="/logout" class="btn btn-default btn-flat">Sign out</a>
                            </div>
                        </li>
                    </ul>
                </li>

                <?php else:?>
                    <li>
                        <a href="/login"><i class="fa fa-sign-in"></i> Login</a>
                    </li>
                    <li>
                        <a href="/register"><i class="fa fa-user-plus"></i> Register</a>
                    </li>
                <?php endif;?>
            </ul>
        </div>
        <form action="/search" method="get" style="margin-top:.6em">
            <div class="input-group input-group-sm">
                <input type="text" name="q" class="form-control" placeholder="Search...">
                <span class="input-group-btn">
                <button type="submit"  id="search-btn" class="btn btn-primary btn-flat"><i class="fa fa-search"></i>
                </button>
              </span>
            </div>
        </form>
    </nav>

</header>