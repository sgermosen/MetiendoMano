<?php partial('admin/header',['title'=>'Category Edit'])?>
<?php
$request=\App\Core\Session::get('request');
$errors=$request['errors'];
$fields=$request['fields'];
\App\Core\Session::delete('request');
?>
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>
            Categories
            <small>Edit</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="/cats"><i class="ion ion-ios-list"></i> Categories</a></li>
            <li><a href="/cats/<?=$cat->id?>"><i class="ion ion-plus"></i><?=$cat->name?></a></li>
            <li><a href="/cats/<?=$cat->id?>/edit"><i class="ion ion-edit"></i>Edit</a></li>
        </ol>
    </section>
    <!-- Main content -->
    <section class="content">
        <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li><h4 style="padding-left: 10px"><?=$cat->name?></h4></li>
                <li><a href="#courses" data-toggle="tab">Courses</a></li>
                <li class="dropdown pull-right">
                    <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                        <i class="fa fa-gear"></i> Options <span class="caret"></span>
                    </a>
                    <ul class="dropdown-menu">
                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#" data-toggle="modal" data-target="#editdetails" ><i class="fa fa-edit"></i>Change Name</a></li>
                        <li role="presentation"><a role="menuitem" tabindex="-1" href="#" data-toggle="modal" data-target="#addcourse" ><i class="fa fa-plus"></i>Add Course</a></li>
                    </ul>
                </li>
            </ul>
            <div class="tab-content">
                <!-- /.tab-pane -->
                <div class="tab-pane active" id="courses">
                    <table id="indextable" class="table table-bordered table-striped">
                        <thead>
                        <tr>
                            <th>Title</th>
                            <th>Rate</th>
                            <th>Start</th>
                            <th>End</th>
                            <th>Edit</th>
                            <th>View</th>
                            <th>Delete</th>
                        </tr>
                        </thead>
                        <tbody>
                        <?php foreach ($cat->courses() as $course):?>
                            <tr>
                                <td><?= $course->title?></td>
                                <td><?= $course->rate?></td>
                                <td><?= $course->start?></td>
                                <td><?= $course->end?></td>
                                <td><a href="/courses/<?=$course->id?>/edit"><span class="fa fa-edit"></span></a></td>
                                <td><a href="/courses/<?=$course->id?>"><span class="fa fa-book"></span></a></td>
                                <td>
                                    <?php start_form('delete',"/courses/$course->id")?>
                                    <?php \App\Core\Session::saveBackUrl()?>
                                    <button type="submit" class="delete" style="border: none;background-color: rgba(0,0,0,0); color:#9f191f">
                                        <span class="fa fa-remove"></span>
                                    </button>
                                    <?php close_form()?>
                                </td>
                            </tr>
                        <?php endforeach?>
                        </tbody>
                        <tfoot>
                        <tr>
                            <th>Title</th>
                            <th>Rate</th>
                            <th>Start</th>
                            <th>End</th>
                            <th>Edit</th>
                            <th>View</th>
                            <th>Delete</th>
                        </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
            <!-- /.tab-content -->
        </div>
        <div class="modal fade" id="editdetails" tabindex="-1" role="dialog" aria-labelledby="Edit Details" aria-hidden="true" >
            <div class="modal-dialog" style="width:60%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">Edit <?= $cat->name?> Info</h4>
                    </div>
                    <?php start_form('put',"/cats/$cat->id")?>
                    <div class="box box-solid">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="box-group" id="accordion">
                                <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                                <div class="panel box box-primary">
                                    <div class="box-header with-border">
                                        <h4 class="box-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#basicinfo">
                                                Basic Info
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="basicinfo" class="panel-collapse collapse in">
                                        <div class="box-body">
                                            <div class="form-group">
                                                <label for="name">Title</label>
                                                <input type="text" name="name" class="form-control" value="<?=$cat->name?>">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <?php if(count($errors)>0):?>
                                <?php partial('admin/verrors',['errors'=>$errors]);?>
                            <?php endif;?>
                        </div>
                        <!-- /.box-body -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                            <button type="submit" class="btn btn-primary">Update</button>
                        </div>

                    </div>
                    <?php close_form()?>

                </div>
                <!-- /.modal-dialog -->
            </div>
        </div>
        <div class="modal fade" id="addcourse" tabindex="-1" role="dialog" aria-labelledby="Add New Course" aria-hidden="true" >
            <div class="modal-dialog" style="width:60%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span></button>
                        <h4 class="modal-title">Add New Course</h4>
                    </div>
                    <?php start_form('post',"/courses",['enctype'=>"multipart/form-data"])?>
                    <?php \App\Core\Session::saveBackUrl()?>
                    <div class="box box-solid">
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="box-group" id="accordion">
                                <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                                <div class="panel box box-primary">
                                    <div class="box-header with-border">
                                        <h4 class="box-title">
                                            <a data-toggle="collapse" data-parent="#accordion" href="#basicinfo">
                                                Basic Info
                                            </a>
                                        </h4>
                                    </div>
                                    <div id="basicinfo" class="panel-collapse collapse in">
                                        <div class="box-body">
                                            <input type="hidden" name="cat" value="<?=$cat->id?>">
                                            <div class="form-group">
                                                <label for="title">Title</label>
                                                <input type="text" name="title" class="form-control">
                                            </div>
                                            <div class="form-group">
                                                <label for="image">Image</label>
                                                <input type="file" name="image" class="form-control">
                                            </div>
                                            <div class="form-group">
                                                <label for="desc">Description</label>
                                                <textarea id="editor" name="desc" class="form-control"></textarea>

                                            </div>
                                            <div class="form-group">
                                                <label for="date">Start Date</label>
                                                <input type="date" name="start" class="form-control">
                                            </div>
                                            <div class="form-group">
                                                <label for="date">End Date</label>
                                                <input type="date" name="end" class="form-control">
                                            </div>
                                            <div class="form-group">
                                                <label for="rank">Rate</label>
                                                <input type="number" id="rank" name="rank" class="form-control">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <?php if(count($errors)>0):?>
                                <?php partial('admin/verrors',['errors'=>$errors]);?>
                            <?php endif;?>
                        </div>
                        <!-- /.box-body -->
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                            <button type="submit" class="btn btn-primary">Add</button>
                        </div>

                    </div>
                    <?php close_form()?>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>
        </div>
    </section>
    <!-- /.content -->

<?php partial('admin/footer')?>