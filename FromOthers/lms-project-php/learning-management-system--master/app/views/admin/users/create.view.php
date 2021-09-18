<?php partial('admin/header',['title'=>'Users Admin'])?>
<?php
$request=\App\Core\Session::get('request');
$errors=$request['errors'];
$fields=$request['fields'];
\App\Core\Session::delete('request');
?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Users
        <small>Admin</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/users"><i class="fa fa-users"></i>Users</a></li>
        <li><a href="/users/create"><i class="fa fa-plus"></i>Create</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">

    <div class="box box-primary">
        <div class="box-header with-border">
            <h3 class="box-title">All Users</h3>
            <button class="btn btn-primary pull-right" data-toggle="modal" data-target="#AddUser">
                Add new
            </button>
        </div>
        <div class="box-body">
            <table id="indextable" class="table table-bordered table-striped">
                <thead>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>User Name</th>
                    <th>Email</th>
                    <th>Gender</th>
                    <th>State</th>
                    <th>Edit</th>
                    <th>View</th>
                    <th>Delete</th>
                </tr>
                </thead>
                <tbody>
                <?php foreach ($users as $user):?>
                <tr>
                    <td><?= $user->firstname?></td>
                    <td><?= $user->lastname?></td>
                    <td><?= $user->username?></td>
                    <td><?= $user->email?></td>
                    <td><?= $user->gender?></td>
                    <td><?= $user->state?></td>
                    <td><a href="/users/<?=$user->id?>/edit"><span class="fa fa-edit"></span></a></td>
                    <td><a href="/users/<?=$user->id?>"><span class="fa fa-book"></span></a></td>
                    <td>
                        <?php start_form('delete',"/users/$user->id")?>
                        <?php \App\Core\Session::saveBackUrl()?>
                        <button type="submit" style="border: none;background-color: rgba(0,0,0,0); color:#9f191f">
                            <span class="fa fa-remove"></span>
                        </button>
                        <?php close_form()?>
                    </td>
                </tr>
                <?php endforeach?>
                </tbody>
                <tfoot>
                <tr>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>User Name</th>
                    <th>Email</th>
                    <th>Gender</th>
                    <th>State</th>
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
    <div class="modal fade" id="AddUser" tabindex="-1" role="dialog" aria-labelledby="Add New User" aria-hidden="true" >
        <div class="modal-dialog" style="width:60%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">Add New User</h4>
                </div>
                <?php start_form('post',"/users",['enctype'=>"multipart/form-data"])?>
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
                                            <input type="text" name="firstname" class="form-control" value="<?= isset($fields['firstname'])?$fields['firstname']:''?>" >
                                        </div>
                                        <div class="form-group">
                                            <label for="lastname">Last Name</label>
                                            <input type="text" name="lastname" class="form-control" value="<?= isset($fields['lastname'])?$fields['lastname']:''?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="gender">Gender</label>
                                            <select name="gender" id="" class="form-control">
                                                <option value="male" <?php if(isset($fields['gender'])&&$fields['gender']=='male'){echo 'selected="selected"';}?>>Male</option>
                                                <option value="female" <?php if(isset($fields['gender'])&&$fields['gender']=='female'){echo 'selected="selected"';}?>>Female</option>
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label for="country">Country</label>
                                            <select name="country" id="" class="form-control">
                                                <option value="egypt" <?php if(isset($fields['country'])&&$fields['country']=='egypt'){echo 'selected="selected"';}?>>Egypt</option>
                                                <option value="other" <?php if(isset($fields['country'])&&$fields['country']=='others'){echo 'selected="selected"';}?>>Other</option>
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
                                            <input type="text" name="username" class="form-control" value="<?= isset($fields['username'])?$fields['username']:''?>">
                                        </div>
                                        <div class="form-group">
                                            <label for="email">Email</label>
                                            <input type="text" name="email" class="form-control" value="<?= isset($fields['email'])?$fields['email']:''?>">
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
                                            <input type="file" name="image" class="form-control">
                                        </div>
                                        <div class="form-group">
                                            <label for="signature">Signature</label>
                                            <textarea  name="signature" class="form-control" id="editor"><?= isset($fields['signature'])?$fields['signature']:''?></textarea>
                                        </div>
                                        <div class="form-group">
                                            <label for="role">Role</label>
                                            <select name="role" id="" class="form-control">
                                                <option value="student" <?php if(isset($fields['role'])&&$fields['role']=='student'){echo 'selected="selected"';}?>>Student</option>
                                                <option value="admin" <?php if(isset($fields['role'])&&$fields['role']=='admin'){echo 'selected="selected"';}?>>Admin</option>
                                            </select>
                                        </div>
                                        <select name="state" id="" class="form-control">
                                            <option value="active" <?php if(isset($fields['state'])&&$fields['state']=='active'){echo 'selected="selected"';}?>>Active</option>
                                            <option value="baned" <?php if(isset($fields['state'])&&$fields['state']=='banned'){echo 'selected="selected"';}?>>Baned</option>
                                            <option value="disabled" <?php if(isset($fields['state'])&&$fields['state']=='disabled'){echo 'selected="selected"';}?>>Disable</option>
                                        </select>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <?php if(count($errors)>0):?>
                            <?php partial('admin/verrors',['errors'=>$errors])?>
                        <?php endif;?>
                    </div>
                    <!-- /.box-body -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                        <button type="submit" class="btn btn-primary">Update</button>
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


