<div class="box box-widget widget-user-2">
    <!-- Add the bg color to the header using any of the bg-* classes -->
    <div class="widget-user-header bg-yellow">
        <div class="widget-user-image">
            <img class="img-circle" src="<?=$course->image?>" alt="User Avatar">
        </div>
        <!-- /.widget-user-image -->
        <h3 class="widget-user-username"><?=$course->title?></h3>
        <h5 class="widget-user-desc"><?=$course->category()->name?></h5>
    </div>
    <div class="box-footer no-padding">
        <ul class="nav nav-stacked">
            <li><a>Materials <span class="pull-right badge bg-blue"><?=count($course->materials())?></span></a></li>
            <li><a>Rate <span class="pull-right badge bg-red"><?=$course->rate?></span></a></li>
            <li><a>Added At: <span class="pull-right badge bg-red"><?=$course->created_at?></span></a></li>
            <li><a>Last Update: <span class="pull-right badge bg-red"><?=$course->created_at?></span></a></li>
        </ul>
        <a href="/courses/<?=$course->id?>" class="btn btn-primary btn-block"><b>View Course</b></a>
    </div>
</div>
