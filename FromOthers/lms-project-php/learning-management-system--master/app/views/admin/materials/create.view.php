<?php partial('admin/header',['title'=>'Materials Admin'])?>
<?php
$request=\App\Core\Session::get('request');
\App\Core\Session::delete('request');
$errors=$request['errors'];
$fields=$request['fields'];
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Materials
        <small>Admin</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/materials"><i class="fa fa-book"></i>Materials</a></li>
        <li><a href="/materials/create"><i class="fa fa-plus"></i>Create</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">All Courses</h3>
            <button class="btn btn-primary pull-right" id="addBt" data-toggle="modal" data-target="#addmaterial">
                Add New Material
            </button>
        </div>
        <div class="box-body">
            <table id="indextable" class="table table-bordered table-striped">
                <thead>
                <tr>
                    <th>Name</th>
                    <th>Course</th>
                    <th>Link</th>
                    <th>Status</th>
                    <th>Type</th>
                    <th>Comments</th>
                    <th>Downloads</th>
                    <th>Last update</th>
                    <th>Edit</th>
                    <th>View</th>
                    <th>Delete</th>
                </tr>
                </thead>
                <tbody>
                <?php if($materials):?>
                    <?php foreach ($materials as $material):?>
                        <tr>
                            <td><?= $material->name?></td>
                            <td><?= $material->course()->title?></td>
                            <td><a href="<?= $material->link ?>"><?= $material->link?></a></td>
                            <td><?=$material->status?></td>
                            <td><?=$material->type?></td>
                            <td> <?=count($material->comments())?></td>
                            <td><?=$material->downloaded?></td>
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
                    <th>Course</th>
                    <th>Link</th>
                    <th>Status</th>
                    <th>Type</th>
                    <th>Comments</th>
                    <th>Downloads</th>
                    <th>Last update</th>
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
    <div class="modal fade" id="addmaterial" tabindex="-1" role="dialog" aria-labelledby="Add New Material" aria-hidden="true" >
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
                                        <div class="form-group">
                                            <label for="type">Course</label>
                                            <select name="cid" id="course" class="form-control">
                                                <?php foreach ($courses as $course):?>
                                                    <option value="<?=$course->id?>" <?php if(isset($mfields['cid'])&&$mfields['cid']==$course->id){echo 'selected="selected"';}?>><?=$course->title?></option>
                                                <?php endforeach?>
                                            </select>
                                        </div>
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
                        <button type="submit" class="btn btn-primary">Create</button>
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

