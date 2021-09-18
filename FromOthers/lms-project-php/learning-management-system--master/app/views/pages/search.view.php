<?php partial('admin/header',['title'=>'Search Results'])?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Search Results
        <small>List</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="#"><i class="ion ion-ios-search"></i> Search</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title">Courses</h3>
            <a class="btn btn-primary btn-xs pull-right" href="/courses">All Courses</a>
        </div>
        <div class="box-body">
            <?php foreach (array_chunk($courses,4) as $coursechunk):?>
                <?php foreach ($coursechunk as $course):?>
                    <div class="col-md-3 col-sm-2">
                        <?php partial('admin/course',['course'=>$course])?>
                    </div>
                <?php endforeach;?>
            <?php endforeach;?>
        </div>
        <div class="box-footer">
            Found <?=count($courses)?> Course
        </div>
    </div>
    <hr>
    <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title">Users</h3>
            <a class="btn btn-primary btn-xs pull-right" href="/users">All Users</a>
        </div>
        <div class="box-body">
            <?php foreach (array_chunk($users,4) as $userchunk):?>
                <?php foreach ($userchunk as $user):?>
                    <div class="col-md-3 col-sm-2">
                        <?php partial('admin/user',['user'=>$user])?>
                    </div>
                <?php endforeach;?>
            <?php endforeach;?>
        </div>
        <div class="box-footer">
            Found <?=count($user)?> User
        </div>
    </div>
    <hr>
    <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title">Materials</h3>
            <a class="btn btn-primary btn-xs pull-right" href="/courses">All Courses</a>
        </div>
        <div class="box-body">
            <?php foreach ($materials as $material):?>
                <?php partial('admin/material',['material'=>$material])?>
            <?php endforeach;?>
        </div>
        <div class="box-footer">
            Found <?=count($materials)?> Materials
        </div>
    </div>
    <hr>
    <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title">Requests</h3>
            <a class="btn btn-primary btn-xs pull-right" href="/requests">All Requests</a>
        </div>
        <div class="box-body">
                <?php foreach ($reqs as $req):?>
                    <?php partial('admin/request',['req'=>$req])?>
                <?php endforeach;?>
        </div>
        <div class="box-footer">
            Found <?=count($reqs)?> Requests
        </div>
    </div>
</section>

<?php partial('admin/footer')?>


