<?php partial('admin/header')?>
<?php
$request=\App\Core\Session::get('request');
$errors=$request['errors'];
$fields=$request['fields'];
\App\Core\Session::delete('request',['title'=>'User Edit']);
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Users
        <small>User Edit</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/users"><i class="fa fa-users"></i>Users</a></li>
        <li><a href="/users/<?=$user->id?>"><i class="fa fa-user"></i><?=$user->username?></a></li>
        <li><a href="/users/<?=$user->id?>/edit"><i class="fa fa-edit"></i>Edit</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">

    <div class="nav-tabs-custom">
        <ul class="nav nav-tabs">
            <li><h4 style="padding-left: 10px"><?=$user->username?></h4></li>
            <li><a href="#requests" data-toggle="tab">Requests</a></li>
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
                <div class="row">
                    <div class="col-sm-3">
                        <img src="<?=$user->image?>" alt="" class="img-responsive">
                    </div>
                    <div class="col-sm-3">
                        <h5>First Name: <span class="text-green"><?=$user->firstname?></span></h5>
                        <h5>Last Name: <span class="text-green"><?=$user->lastname?></span></h5>
                        <h5>User Name: <span class="text-green"><?=$user->username?></span></h5>
                        <h5>Email: <span class="text-green"><?=$user->email?></span></h5>
                        <hr>
                    </div>
                    <div class="col-sm-3">
                        <h5>Gender: <span class="text-green"><?=$user->gender?></span></h5>
                        <h5>State: <span class="text-green"><?=$user->state?></span></h5>
                        <h5>Country: <span class="text-green"><?=$user->country?></span></h5>
                        <h5>Is Baned: <span class="text-green"><?=$user->isbaned?></span></h5>
                        <hr>
                    </div>
                </div>
            </div>
            <!-- /.tab-pane -->
            <div class="tab-pane" id="requests">
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
                    <?php if(count($user->requests())>0):?>
                        <?php foreach ($user->requests() as $req):?>
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

        </div>
        <!-- /.tab-content -->
    </div>


    <div class="modal fade" id="editdetails" tabindex="-1" role="dialog" aria-labelledby="Edit Details" aria-hidden="true" >
        <div class="modal-dialog" style="width:60%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Edit <?= $user->name?> Info</h4>
                </div>
                <?php start_form('put',"/users/$user->id",['enctype'=>"multipart/form-data"])?>
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
                                            <label for="firstname">First Name</label>
                                            <input type="text" name="firstname" class="form-control" value="<?=$user->firstname?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="lastname">Last Name</label>
                                            <input type="text" name="lastname" class="form-control" value="<?=$user->lastname?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="gender">Gender</label>
                                            <select name="gender" id="" class="form-control">
                                                <option value="male" <?php if($user->gender=='male'){echo 'selected="selected"';}?>>Male</option>
                                                <option value="female" <?php if($user->gender=='female'){echo 'selected="selected"';}?>>Female</option>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="country">Country</label>
                                            <select name="country" id="" class="form-control">
                                                <option value="egypt" <?php if($user->country=='egypt'){echo 'selected="selected"';}?>>Egypt</option>
                                                <option value="other" <?php if($user->country=='others'){echo 'selected="selected"';}?>>Other</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel box box-primary">
                                <div class="box-header with-border">
                                    <h4 class="box-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#logininfo">
                                            Login Info
                                        </a>
                                    </h4>
                                </div>
                                <div id="logininfo" class="panel-collapse collapse in">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label for="username">User Name</label>
                                            <input type="text" name="username" class="form-control" value="<?=$user->username?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="email">Email</label>
                                            <input type="email" name="email" class="form-control" value="<?=$user->email?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="password">Password</label>
                                            <input type="password" name="password" class="form-control">
                                        </div>
                                        <div class="form-group">
                                            <label for="confirm">Confirm Password</label>
                                            <input type="password" name="confirm" class="form-control">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel box box-primary">
                                <div class="box-header with-border">
                                    <h4 class="box-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#profileinfo">
                                            Profile Info
                                        </a>
                                    </h4>
                                </div>
                                <div id="profileinfo" class="panel-collapse collapse in">
                                    <div class="box-body">
                                        <div class="form-group">
                                            <label for="image">Profile Photo</label>
                                            <?php uploaded_image($user->image,['width'=>'100px'])?><br>
                                            <input type="file" name="image" class="form-control">
                                        </div>
                                        <div class="form-group">
                                            <label for="signature">Signature</label>
                                            <textarea  name="signature" class="form-control" id="editor"><?= isset($fields['signature'])?$fields['signature']:''?></textarea>
                                        </div>
                                        <div class="form-group">
                                            <label for="role">Role</label>
                                            <select name="role" id="" class="form-control">
                                                <option value="student" <?php if($user->role=='student'){echo 'selected="selected"';}?>>Student</option>
                                                <option value="admin" <?php if($user->role=='admin'){echo 'selected="selected"';}?>>Admin</option>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="state">State</label>
                                            <select name="state" id="" class="form-control">
                                                <option value="active" <?php if($user->stete=='active'){echo 'selected="selected"';}?>>Active</option>
                                                <option value="baned" <?php if($user->state=='baned'){echo 'selected="selected"';}?>>Baned</option>
                                                <option value="disabled" <?php if($user->state=='disabled'){echo 'selected="selected"';}?>>Disable</option>
                                            </select>
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
                        <button type="submit" class="btn btn-primary">Edit</button>
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
    $(function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    });
</script>
