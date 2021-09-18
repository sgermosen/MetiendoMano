<?php partial('admin/header',['title'=>'Dashboard'])?>

<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        DashBord
        <small>Admin</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/admin"><i class="fa fa-dashboard"></i>Dashboard</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
     <!-- Small boxes (Stat box) -->
    <div class="row">
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-aqua">
                <div class="inner">
                    <h3><?= count($reqs)?></h3>

                    <p>Requests</p>
                </div>
                <div class="icon">
                    <i class="fa fa-book"></i>
                </div>
                <a href="/requests/create" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-green">
                <div class="inner">
                    <h3><?= count($materials)?></h3>

                    <p>Materials</p>
                </div>
                <div class="icon">
                    <i class="fa fa-inbox"></i>
                </div>
                <a href="/materials/create" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-yellow">
                <div class="inner">
                    <h3><?= count($users)?></h3>

                    <p>User Registrations</p>
                </div>
                <div class="icon">
                    <i class="ion ion-person-add"></i>
                </div>
                <a href="/users/create" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
        <div class="col-lg-3 col-xs-6">
            <!-- small box -->
            <div class="small-box bg-red">
                <div class="inner">
                    <h3><?= count($courses)?></h3>

                    <p>Courses</p>
                </div>
                <div class="icon">
                    <i class="ion ion-ios-book"></i>
                </div>
                <a href="/courses/create" class="small-box-footer">More info <i class="fa fa-arrow-circle-right"></i></a>
            </div>
        </div>
        <!-- ./col -->
    </div>
    <!-- /.row -->
    <!-- Main row -->
    <div class="row">
        <div class="col-md-8">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title">Latest Requests</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <table class="table no-margin">
                            <thead>
                            <tr>
                                <th>User</th>
                                <th>Title</th>
                                <th>Comments</th>
                                <th>Added At</th>
                                <th>Updated At</th>
                            </tr>
                            </thead>
                            <tbody>
                            <?php foreach ($reqs as $req):?>
                                <tr>
                                    <td><?= $req->user()->username?></td>
                                    <td><?= $req->title?></td>
                                    <td><?=count($req->comments())?></td>
                                    <td><?= $req->created_at?></td>
                                    <td><?= $req->updated_at?></td>
                                </tr>
                            <?php endforeach;?>
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                    <a href="/requests/create" class="btn btn-sm btn-info btn-flat pull-left">Place New Request</a>
                    <a href="/requests" class="btn btn-sm btn-default btn-flat pull-right">View All Requests</a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        <div class="col-md-4">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Latest Members</h3>

                    <div class="box-tools pull-right">
                        <span class="label label-danger"><?=count($users)?> New Members</span>
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i>
                        </button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body no-padding">
                    <ul class="users-list clearfix">
                        <?php foreach ($users as $user):?>
                        <li>
                            <img src="<?=$user->image?>" alt="User Image">
                            <a class="users-list-name" href="/users/<?=$user->id?>"><?=$user->firstname." ".$user->lastname?></a>
                            <span class="users-list-date"><?=$user->created_at?></span>
                        </li>
                        <?php endforeach;?>
                    </ul>
                    <!-- /.users-list -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer text-center">
                    <a href="/users" class="uppercase">View All Users</a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title">Materials Downloads</h3>

                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="table-responsive">
                        <table class="table no-margin">
                            <thead>
                            <tr>
                                <th>Course</th>
                                <th>Name</th>
                                <th>Status</th>
                                <th>Type</th>
                                <th>Downloads</th>
                                <th>Added At</th>
                            </tr>
                            </thead>
                            <tbody>
                            <?php foreach ($materials as $material):?>
                                <tr>
                                    <td><?= $material->course()->title?></td>
                                    <td><?= $material->name?></td>
                                    <td><?=$material->status?></td>
                                    <td><?= $material->type?></td>
                                    <td><?= $material->downloaded?></td>
                                    <td><?= $material->created_at?></td>
                                </tr>
                            <?php endforeach;?>
                            </tbody>
                        </table>
                    </div>
                    <!-- /.table-responsive -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer clearfix">
                    <a href="/materials/create" class="btn btn-sm btn-info btn-flat pull-left">Place New Material</a>
                    <a href="/materials" class="btn btn-sm btn-default btn-flat pull-right">View All Materials</a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
        <div class="col-md-4">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <h3 class="box-title">Latest Courses</h3>

                    <div class="box-tools pull-right">
                        <span class="label label-danger"><?=count($courses)?> New Courses</span>
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                        </button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-times"></i>
                        </button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body no-padding">
                    <ul class="users-list clearfix">
                        <?php foreach ($courses as $course):?>
                            <li>
                                <img src="<?=$course->image?>" alt="User Image">
                                <a class="users-list-name" href="/courses/<?=$course->id?>"><?=$course->title?></a>
                                <span class="users-list-date"><?=$course->created_at?></span>
                            </li>
                        <?php endforeach;?>
                    </ul>
                    <!-- /.users-list -->
                </div>
                <!-- /.box-body -->
                <div class="box-footer text-center">
                    <a href="/users" class="uppercase">View All Users</a>
                </div>
                <!-- /.box-footer -->
            </div>
        </div>
    </div>
    <!-- /.row (main row) -->

</section>
<!-- /.content -->
<?php partial('admin/footer')?>


