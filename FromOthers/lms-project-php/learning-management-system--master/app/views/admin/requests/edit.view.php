<?php partial('admin/header',['title'=>'Request Edit'])?>
<?php
$request=\App\Core\Session::get('request');
$errors=$request['errors'];
$fields=$request['fields'];
\App\Core\Session::delete('request');
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Requests
        <small>Edit <?=$req->title?></small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/requests"><i class="fa fa-inbox"></i>Requests</a></li>
        <li><a href="/requests/<?=$req->id?>"><i class="fa fa-inbox"></i><?=$req->title?></a></li>
        <li><a href="/requests/<?=$req->id?>/Edit"><i class="fa fa-edit"></i>Edit</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">

    <div class="nav-tabs-custom">
        <ul class="nav nav-tabs">
            <li><h4 style="padding-left: 10px"><?=$req->title?></h4></li>
            <li><a href="#details" data-toggle="tab">Request Info</a></li>
            <li><a href="#requests" data-toggle="tab">Comments</a></li>
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
                <?=$req->body?>
            </div>
            <!-- /.tab-pane -->
            <div class="tab-pane" id="requests">
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
                    <?php if(count($req->comments())>0):?>
                        <?php foreach ($req->comments() as $comment):?>
                            <tr>
                                <td><?= $comment->user()->username?></td>
                                <td><?= $comment->body?></td>
                                <td><?= $comment->created_at?></td>
                                <td><?= $comment->updated_at?></td>
                                <td>
                                    <?php start_form('delete',"/comments/$comment->id")?>
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
                        <th>User</th>
                        <th>Content</th>
                        <th>Created at</th>
                        <th>Last update</th>
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
                    <h4 class="modal-title">Edit <?= $req->title?> Info</h4>
                </div>
                <?php start_form('put',"/requests/$req->id",['enctype'=>"multipart/form-data"])?>
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
                                            <input type="text" name="title" class="form-control" value="<?= $req->title?>" >
                                        </div>
                                        <div class="form-group">
                                            <label for="body">Content</label>
                                            <textarea name="body" id="editor" cols="30" rows="10" class="form-control"><?= $req->body?></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <?php if(count($errors)>0):?>
                            <div class="alert alert-danger">
                               <?php partial('admin/errors',['errors'=>$errors])?>
                            </div>
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