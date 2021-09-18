<?php partial('admin/header',['title'=>'Material Edit'])?>
<?php
$request=\App\Core\Session::get('request');
$errors=$request['errors'];
$fields=$request['fields'];
\App\Core\Session::delete('request');
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Materials
        <small>Edit <?=$material->title?></small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/materials"><i class="fa fa-book"></i>Materials</a></li>
        <li><a href="/materials/<?=$material->id?>"><i class="fa fa-book"></i><?=$material->name?></a></li>
        <li><a href="/materials/<?=$material->id?>/edit"><i class="fa fa-edit"></i>Edit</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">

    <div class="nav-tabs-custom">
        <ul class="nav nav-tabs">
            <li><h4 style="padding-left: 10px"><?=$material->name?></h4></li>
            <li><a href="#details" data-toggle="tab">Material Info</a></li>
            <li><a href="#comments" data-toggle="tab">Comments</a></li>
            <li class="dropdown pull-right">
                <a class="dropdown-toggle" data-toggle="dropdown" href="#" aria-expanded="false">
                    <i class="fa fa-gear"></i> Options <span class="caret"></span>
                </a>
                <ul class="dropdown-menu">
                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#" data-toggle="modal" data-target="#editdetails" ><i class="fa fa-edit"></i>Edit Details</a></li>
                </ul>
            </li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="details">
                <p>Name: <?=$material->name?></p>
                <p>Course: <?=$material->course()->title?></p>
                <p>Link: <a href="<?=$material->link?>"><?=$material->link?></a></p>
                <p>Status: <?=$material->status?></p>
                <p>Type: <?=$material->type?></p>
                <p>Comments: <?=count($material->comments())?></p>
                <p>Description: <?=$material->description?></p>
            </div>
            <div class="tab-pane" id="comments">
                <table id="indextable" class="table table-bordered table-striped">
                    <thead>
                    <tr>
                        <th>User</th>
                        <th>Content</th>
                        <th>Created at</th>
                        <th>Last update</th>
                        <th>Delete</th>
                    </tr>
                    </thead>
                    <tbody>
                    <?php if(count($material->comments())>0):?>
                        <?php foreach ($material->comments() as $comment):?>
                            <tr>
                                <td><?= $comment->user()->username?></td>
                                <td><?= $comment->body?></td>
                                <td><?= $comment->created_at?></td>
                                <td><?= $comment->updated_at?></td>
                                <td>
                                    <?php start_form('delete',"/comments/$comment->id")?>
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
                        <th>User</th>
                        <th>Content</th>
                        <th>Created at</th>
                        <th>Last update</th>
                        <th>Delete</th>
                    </tr>
                    </tfoot>
                </table>
            </div>
            <!-- /.tab-pane -->
        </div>
        <!-- /.tab-content -->
    </div>


    <div class="modal fade" id="editdetails" tabindex="-1" role="dialog" aria-labelledby="Edit Details" aria-hidden="true" >
        <div class="modal-dialog" style="width:60%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Edit <?= $material->title?> Info</h4>
                </div>
                <?php start_form('put',"/materials/$material->id",['enctype'=>"multipart/form-data"])?>
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
                                            <label for="type">Course</label>
                                            <select name="cid" id="course" class="form-control">
                                                <?php foreach ($courses as $course):?>
                                                    <option value="<?=$course->id?>" <?php if($material->cid==$course->id){echo 'selected="selected"';}?>><?=$course->title?></option>
                                                <?php endforeach?>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="title">Name</label>
                                            <input type="text" name="name" class="form-control" value="<?=$material->name?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="type">Type</label>
                                            <select name="type" id="type" class="form-control">
                                                <option value="pdf" <?php if($material->type=='pdf'){echo 'selected="selected"';}?>>Pdf</option>
                                                <option value="doc" <?php if($material->type=='doc'){echo 'selected="selected"';}?>>Word</option>
                                                <option value="ppt" <?php if($material->type=='ppt'){echo 'selected="selected"';}?>>PowerPoint</option>
                                                <option value="video" <?php if($material->type=='video'){echo 'selected="selected"';}?>>Video</option>
                                            </select>
                                        </div>
                                        <div class="form-group" id="file">
                                            <label for="link">File</label>
                                            <input type="file"  name="link" class="form-control" value="<?php if($material->type!='video'){echo $material->link;}?>">
                                        </div>
                                        <div class="form-group" hidden id="video">
                                            <label for="vlink">Video Link</label>
                                            <input type="url" name="vlink"  class="form-control" value="<?php if($material->type=='video'){echo $material->link;}?>" >
                                        </div>
                                        <div class="form-group">
                                            <label for="desc">Status</label>
                                            <select name="status" id="status" class="form-control">
                                                <option value="show" <?php if($material->status=='show'){echo 'selected="selected"';}?>>Show</option>
                                                <option value="hide" <?php if($material->status=='hide'){echo 'selected="selected"';}?>>Hide</option>
                                                <option value="lock" <?php if($material->status=='lock'){echo 'selected="selected"';}?>>Lock</option>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="date">Description</label>
                                            <textarea name="description" id="editor" cols="30" rows="10"><?=$material->description?></textarea>
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
