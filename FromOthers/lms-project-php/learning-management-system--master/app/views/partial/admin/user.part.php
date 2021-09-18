<div class="box box-primary">
    <div class="box-body box-profile">
        <img class="profile-user-img img-responsive img-circle" src="<?= $user->image?>" alt="User profile picture">
        <h3 class="profile-username text-center"><?=$user->firstname." ".$user->lastname?></h3>
        <p class="text-muted text-center"><?=$user->email?></p>
        <a href="/users/<?=$user->id?>" class="btn btn-primary btn-block"><b>View Profile</b></a>
    </div>
    <!-- /.box-body -->
</div>