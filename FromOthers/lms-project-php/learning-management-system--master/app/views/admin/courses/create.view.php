<?php partial('admin/header',['title'=>'Courses Admin'])?>
<?php
$request=\App\Core\Session::get('request');
\App\Core\Session::delete('request');
$errors=$request['errors'];
$fields=$request['fields'];
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Courses
        <small>Admin</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/courses"><i class="ion ion-ios-book"></i> Courses</a></li>
        <li><a href="/courses/create"><i class="ion ion-plus"></i>Create</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">All Courses</h3>
            <button class="btn btn-primary pull-right" id="addBt" data-toggle="modal" data-target="#AddCat">
                Add new Course
            </button>
        </div>
        <div class="box-body">
            <table id="indextable" class="table table-bordered table-striped">
                <thead>
                <tr>
                    <th>Title</th>
                    <th>Category</th>
                    <th>Rate</th>
                    <th>Start</th>
                    <th>End</th>
                    <th>Edit</th>
                    <th>View</th>
                    <th>Delete</th>
                </tr>
                </thead>
                <tbody>
                <?php foreach ($courses as $course):?>
                <tr>
                    <td><?= $course->title?></td>
                    <td><?= $course->category()->name?></td>
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
                    <th>Category</th>
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

        <div class="box-footer">
        </div>
    </div>
    <div class="modal fade" id="AddCat" tabindex="-1" role="dialog" aria-labelledby="Add New Cat" aria-hidden="true" >
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
                                        <div class="form-group">
                                            <label for="title">Title</label>
                                            <input type="text" name="title" class="form-control" value="<?=isset($fields['title'])?$fields['title']:''?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="image">Image</label>
                                            <input type="file" name="image" class="form-control" >
                                        </div>
                                        <div class="form-group">
                                            <label for="desc">Description</label>
                                            <textarea id="editor" name="desc" class="form-control"><?=isset($fields['description'])?$fields['description']:''?></textarea>

                                        </div>
                                        <div class="form-group">
                                            <label for="date">Start Date</label>
                                            <input type="date" name="start" class="form-control" value="<?=isset($fields['start'])?date("Y-m-d",strtotime($fields['title'])):date("Y-m-d")?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="date">End Date</label>
                                            <input type="date" name="end" class="form-control" value="<?=isset($fields['end'])?date("Y-m-d",strtotime($fields['title'])):date("Y-m-d")?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="cat">Category</label>
                                            <select name="cat" id="cat" class="form-control">
                                                <?php foreach ($cats as $cat):?>
                                                    <?php if($cat->id==$fields['cat']):?>
                                                        <option value="<?= $cat->id ?>" selected="selected"><?= $cat->name ?></option>
                                                    <?php else:?>
                                                        <option value="<?= $cat->id ?>"><?= $cat->name ?></option>
                                                    <?php endif?>
                                                <?php endforeach?>
                                            </select>
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
                        <button type="submit" class="btn btn-primary">Create</button>
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
