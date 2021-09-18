<?php partial('admin/header',['title'=>$course->title])?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        <?=$course->title?>
        <small><?= $course->category()->name?></small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/courses"><i class="ion ion-ios-book"></i> Courses</a></li>
        <li><a href="/cats/<?=$course->category()->id?>"><i class="ion ion-ios-list"></i><?=$course->category()->name?></a></li>
        <li><a href="/courses/<?=$course->id?>"><i class="ion ion-ios-book"></i><?=$course->title?></a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <div class="nav-tabs-custom">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#info" data-toggle="tab" aria-expanded="true">Course Info</a></li>
                <li class=""><a href="#materials" data-toggle="tab" aria-expanded="false">Materials</a></li>
            </ul>
            <div class="tab-content">
                <div class="tab-pane active" id="info">
                    <div class="center-block">
                        <img src="<?=$course->image?>" alt="" class="img-responsive center-block" width="400">
                        <p class="text-center">Course Rate: <i class="fa fa-star"></i> <?=$course->rate?> </p>
                        <hr>
                    </div>
                    <?=$course->description?>
                </div>
                <!-- /.tab-pane -->
                <div class="tab-pane" id="materials">
                    <!-- The timeline -->
                    <ul class="timeline timeline-inverse">
                        <!-- timeline time label -->
                        <li class="time-label">
                            <span class="bg-red">Materials</span>
                        </li>
                        <?php foreach ($course->materials() as $material):?>
                            <?php partial('admin/material',['material'=>$material])?>
                        <?php endforeach;?>
                        <!-- END timeline item -->
                    </ul>
                </div>
                <!-- /.tab-pane -->
            </div>
            <!-- /.tab-content -->
        </div>
</section>
    <!-- /.row -->
<?php partial('admin/footer')?>

