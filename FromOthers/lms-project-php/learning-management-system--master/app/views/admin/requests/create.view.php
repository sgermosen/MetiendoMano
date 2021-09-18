<?php partial('admin/header',['title'=>'Request Admin'])?>
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
        <small>Admin</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/requests"><i class="fa fa-inbox"></i>Requests</a></li>
        <li><a href="/requests/create"><i class="fa fa-plus"></i>Create</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">

    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">All Users</h3>
            <button class="btn btn-primary pull-right" data-toggle="modal" data-target="#AddRequest">
                Add new
            </button>
        </div>
        <div class="box-body">
            <table id="indextable" class="table table-bordered table-striped">
                <thead>
                <tr>
                    <th>User</th>
                    <th>Title</th>
                    <th>Comments</th>
                    <th>Created at</th>
                    <th>Last update</th>
                    <th>Edit</th>
                    <th>View</th>
                    <th>Delete</th>
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
                    <td><a href="/requests/<?=$req->id?>/edit"><span class="fa fa-edit"></span></a></td>
                    <td><a href="/requests/<?=$req->id?>"><span class="fa fa-book"></span></a></td>
                    <td>
                        <?php start_form('delete',"/requests/$req->id")?>
                        <button type="submit" style="border: none;background-color: rgba(0,0,0,0); color:#9f191f">
                            <span class="fa fa-remove"></span>
                        </button>
                        <?php close_form()?>
                    </td>
                </tr>
                <?php endforeach;?>
                </tbody>
                <tfoot>
                <tr>
                    <th>User</th>
                    <th>Title</th>
                    <th>Comments</th>
                    <th>Created at</th>
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
    <div class="modal fade" id="AddRequest" tabindex="-1" role="dialog" aria-labelledby="Add New Request" aria-hidden="true" >
        <div class="modal-dialog" style="width:60%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Add New Request</h4>
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

</section>
<!-- /.content -->
<?php partial('admin/footer')?>


