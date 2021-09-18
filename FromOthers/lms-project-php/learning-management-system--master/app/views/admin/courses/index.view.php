<?php partial('admin/header',['title'=>'All Courses'])?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Courses
        <small>List</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/courses"><i class="ion ion-ios-book"></i> All Courses</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <?php foreach (array_chunk($courses,4) as $coursechunk):?>
        <?php foreach ($coursechunk as $course):?>
            <div class="col-md-3 col-sm-2">
                <?php partial('admin/course',['course'=>$course])?>
            </div>
        <?php endforeach;?>
    <?php endforeach;?>
</section>

<?php partial('admin/footer')?>


