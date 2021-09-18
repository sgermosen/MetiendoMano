<?php partial('admin/header',['title'=>'Course Edit'])?>
<?php
$request=\App\Core\Session::get('request');
$errors=$request['errors'];
$fields=$request['fields'];
$mfields=[];
if(isset($fields['cid'])){
    $mfields=$fields;
}
\App\Core\Session::delete('request');
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Courses
        <small>Course Edit</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/courses"><i class="ion ion-ios-book"></i> Courses</a></li>
        <li><a href="/courses/<?=$course->id?>"><i class="ion ion-ios-book"></i><?=$course->title?></a></li>
        <li><a href="/courses/<?=$course->id?>/edit"><i class="ion ion-edit"></i>Edit</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">

    <div class="nav-tabs-custom">
        <ul class="nav nav-tabs">
            <li><h4 style="padding-left: 10px"><?=$course->title?></h4></li>
            <li><a href="#details" data-toggle="tab">Course Info</a></li>
            <li><a href="#materials" data-toggle="tab">Materials</a></li>
            <li class="dropdown pull-right">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                    <i class="fa fa-gear"></i> Options <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#" data-toggle="modal" data-target="#editdetails" ><i class="fa fa-edit"></i>Edit Details</a></li>
                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#" data-toggle="modal" data-target="#addmaterial" ><i class="fa fa-plus"></i>Add Material</a></li>
                </ul>
            </li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="details">
                <div class="row">
                    <div class="col-sm-3">
                        <img src="<?=$course->image?>" alt="" class="img-responsive">
                    </div>
                    <div class="col-sm-3">
                        <h5>Title: <span class="text-green"><?=$course->title?></span></h5>
                        <h5>Category: <span class="text-green"><?=$course->category()->name?></span></h5>
                        <h5>Start Date: <span class="text-green"><?=$course->start?></span></h5>
                        <h5>End Date: <span class="text-green"><?=$course->end?></span></h5>
                        <hr>
                    </div>
                    <div class="col-sm-3">
                        <h5>Materials: <span class="text-green"><?=count($course->materials())?></span></h5>
                        <h5>Rate: <span class="text-green"><?=$course->rate?></span></h5>
                        <h5>Added At: <span class="text-green"><?=$course->created_at?></span></h5>
                        <h5>Last Update: <span class="text-green"><?=$course->updated_at?></span></h5>
                        <hr>
                    </div>
                </div>
                <div class="row">
                    <?= $course->description?>
                </div>
            </div>
            <!-- /.tab-pane -->
            <div class="tab-pane" id="materials">
                <table id="indextable" class="table table-bordered table-striped">
                    <thead>
                    <tr>
                        <th>Name</th>
                        <th>Link</th>
                        <th>Status</th>
                        <th>Type</th>
                        <th>Download Count</th>
                        <th>Created at</th>
                        <th>Last update</th>
                        <th>Edit</th>
                        <th>View</th>
                        <th>Delete</th>
                    </tr>
                    </thead>
                    <tbody>
                    <?php if(count($course->materials())>0):?>
                        <?php foreach ($course->materials() as $material):?>
                            <tr>
                                <td><?= $material->name?></td>
                                <td><a href="<?= $material->link ?>"><?= $material->link?></a></td>
                                <td><?=$material->status?></td>
                                <td><?=$material->type?></td>
                                <td><?=$material->downloaded?></td>
                                <td><?= $material->created_at?></td>
                                <td><?= $material->updated_at?></td>
                                <td><a href="/materials/<?=$material->id?>/edit"><span class="fa fa-edit"></span></a></td>
                                <td><a href="/materials/<?=$material->id?>"><span class="fa fa-book"></span></a></td>
                                <td>
                                    <?php start_form('delete',"/materials/$material->id")?>
                                    <?php \App\Core\Session::saveBackUrl()?>
                                    <button type="submit" style="border: none;background-color: rgba(0,0,0,0); color:#9f191f">
                                        <span class="fa fa-remove"></span>
                                    </button>
                                    <?php close_form()?>
                                </td>
                            </tr>
                        <?php endforeach;?>
                    <?php endif;?>
                    </tbody>
                    <tfoot>
                    <tr>
                        <th>Name</th>
                        <th>Link</th>
                        <th>Status</th>
                        <th>Type</th>
                        <th>Download Count</th>
                        <th>Created at</th>
                        <th>Last update</th>
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
                    <h4 class="modal-title">Edit <?= $course->title?> Info</h4>
                </div>
                <?php start_form('put',"/courses/$course->id",['enctype'=>"multipart/form-data"])?>
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
                                            <input type="text" name="title" class="form-control" value="<?=$course->title?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="image">Image</label>
                                            <img src="<?=$course->image?>" alt="">
                                            <input type="file" name="image" class="form-control" >
                                        </div>
                                        <div class="form-group">
                                            <label for="desc">Description</label>
                                            <textarea id="editor" name="desc" class="form-control"><?=$course->description?></textarea>
                                        </div>
                                        <div class="form-group">
                                            <label for="date">Start Date</label>
                                            <input type="date" name="start" class="form-control" value="<?=date("Y-m-d",strtotime($course->start))?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="date">End Date</label>
                                            <input type="date" name="end" class="form-control" value="<?=date("Y-m-d",strtotime($course->end))?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="cat">Category</label>
                                            <select name="cat" id="cat" class="form-control">
                                                <?php foreach ($cats as $cat):?>
                                                    <?php if($cat->id==$course->cid):?>
                                                        <option value="<?= $cat->id ?>" selected="selected"><?= $cat->name ?></option>
                                                    <?php else:?>
                                                        <option value="<?= $cat->id ?>"><?= $cat->name ?></option>
                                                    <?php endif?>
                                                <?php endforeach?>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="rank">Rate</label>
                                            <input type="number" id="rank" name="rank" class="form-control" value="<?=$course->rate?>">
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
    <div class="modal fade" id="addmaterial" tabindex="-1" role="dialog" aria-labelledby="Edit Details" aria-hidden="true" >
        <div class="modal-dialog" style="width:60%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Add Material</h4>
                </div>
                <?php start_form('post',"/materials",['enctype'=>"multipart/form-data"])?>
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
                                        <input type="hidden" name="cid" value="<?=$course->id?>">
                                        <div class="form-group">
                                            <label for="title">Name</label>
                                            <input type="text" name="name" class="form-control" value="<?=isset($mfields['name'])?$mfields['name']:''?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="type">Type</label>
                                            <select name="type" id="type" class="form-control">
                                                <option value="pdf" <?php if(isset($mfields['type'])&&$mfields['type']=='pdf'){echo 'selected="selected"';}?>>Pdf</option>
                                                <option value="doc" <?php if(isset($mfields['type'])&&$mfields['type']=='doc'){echo 'selected="selected"';}?>>Word</option>
                                                <option value="ppt" <?php if(isset($mfields['type'])&&$mfields['type']=='ppt'){echo 'selected="selected"';}?>>PowerPoint</option>
                                                <option value="video" <?php if(isset($mfields['type'])&&$mfields['type']=='video'){echo 'selected="selected"';}?>>Video</option>
                                            </select>
                                        </div>
                                        <div class="form-group" id="file">
                                            <label for="link">File</label>
                                            <input type="file"  name="link" class="form-control" value="<?php if(isset($mfields['type'])&&$mfields['type']!='video'){echo $mfields['link'];}?>">
                                        </div>
                                        <div class="form-group" hidden id="video">
                                            <label for="vlink">Video Link</label>
                                            <input type="url" name="vlink"  class="form-control" value="<?php if(isset($mfields['type'])&&$mfields['type']=='video'){echo $mfields['link'];}?>" >
                                        </div>
                                        <div class="form-group">
                                            <label for="desc">Status</label>
                                            <select name="status" id="status" class="form-control">
                                                <option value="show" <?php if(isset($mfields['status'])&&$mfields['status']=='show'){echo 'selected="selected"';}?>>Show</option>
                                                <option value="hide" <?php if(isset($mfields['status'])&&$mfields['status']=='hide'){echo 'selected="selected"';}?>>Hide</option>
                                                <option value="lock" <?php if(isset($mfields['status'])&&$mfields['status']=='lock'){echo 'selected="selected"';}?>>Lock</option>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="date">Description</label>
                                            <textarea name="description" id="editor" cols="30" rows="10"><?=isset($mfields['description'])?$mfields['description']:''?></textarea>
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

            </div>
            <!-- /.modal-dialog -->
        </div>
    </div>

</section>
    <!-- /.content -->
<?php partial('admin/footer')?>
<script>
    $(document).ready(function () {
        if($('#type').value=="video"){
            $('#video').show();
            $('#file').hide();
        }else{
            $('#video').hide();
            $('#file').show();
        };
       $('#type').on('change',function (e) {
            if(this.value=="video"){
                $('#video').show();
                $('#file').hide();
            }else{
                $('#video').hide();
                $('#file').show();
            }
       });
    });
</script>
