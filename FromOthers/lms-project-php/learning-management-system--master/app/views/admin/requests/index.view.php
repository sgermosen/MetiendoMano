<?php partial('admin/header',['title'=>'All Requests'])?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Request
        <small>List</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/requests"><i class="fa fa-inbox"></i>All Requests</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title">Requests</h3>
            <button class="btn btn-primary pull-right" data-toggle="modal" data-target="#AddRequest">Add New Request</button>
        </div>
        <div class="box-body">
            <?php foreach ($reqs as $req):?>
                <?php partial('admin/request',['req'=>$req])?>
            <?php endforeach;?>
        </div>
        <div class="box-footer">
            <?=count($reqs)?> Requests
        </div>
    </div>


</section>
<div class="modal fade" id="AddRequest" tabindex="-1" role="dialog" aria-labelledby="Add New Request" aria-hidden="true" >
    <div class="modal-dialog" style="width:60%">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">×</span></button>
                <h4 class="modal-title">Add New User</h4>
            </div>
            <?php start_form('post',"/requests",['enctype'=>"multipart/form-data"])?>
            <?php \App\Core\Session::saveBackUrl()?>
            <div class="box box-solid">
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="box-group" id="accordion">
                        <!-- we are adding the .panel class so bootstrap.js collapse plugin detects it -->
                        <div class="panel box box-primary">
                            <div class="box-header with-border">
                                <h4 class="box-title">
                                    <a data-toggle="collapse" data-parent="#accordion" href="#request">
                                        New Request
                                    </a>
                                </h4>
                            </div>
                            <div id="request" class="panel-collapse collapse in">
                                <div class="box-body">
                                    <div class="form-group">
                                        <label for="title">Title</label>
                                        <input type="text" name="title" class="form-control" value="<?= isset($fields['title'])?$fields['title']:''?>" >
                                    </div>
                                    <div class="form-group">
                                        <label for="body">Content</label>
                                        <textarea name="body" id="editor" cols="30" rows="10" class="form-control"><?= isset($fields['body'])?$fields['body']:''?></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <?php if(count($errors)>0):?>
                        <div class="alert alert-danger">
                           <?php partial('admin/errors',['errors'=>$errors]);?>
                        </div>
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


<?php partial('admin/footer')?>

