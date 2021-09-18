<?php partial('admin/header',['title'=>'All Materials'])?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Materials
        <small>List</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/materials"><i class="fa fa-book"></i> All Materials</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title">Materials</h3>
        </div>
        <div class="box-body">
            <ul class="timeline timeline-inverse">
                <!-- timeline time label -->
                <?php foreach ($courses as $course):?>
                    <li class="time-label">
                        <span class="bg-red"><?=$course->title?></span>
                    </li>
                    <?php foreach ( $course->materials() as $material):?>
                        <?php partial('admin/material',['material'=>$material]);?>
                    <?php endforeach;?>
                <?php endforeach;?>
                <!-- END timeline item -->
            </ul>
        </div>
        <!-- /.box-body -->
        <div class="box-footer">
           All Materials
        </div>
        <!-- /.box-footer-->
    </div>
</section>

<?php partial('admin/footer')?>


