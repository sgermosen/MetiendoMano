<?php partial('admin/header',['title'=>$cat->name])?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        <?=$cat->name?>
        <small>List</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/cats"><i class="ion ion-ios-list"></i>Categories</a></li>
        <li><a href="/cats/<?=$cat->id?>"><i class="ion ion-ios-list"></i><?=$cat->name?></a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title"><?=$cat->name?></h3>
            <span class="pull-right badge bg-red">Courses: <?=count($cat->courses())?></span> Courses
        </div>
        <div class="box-body">
            <?php foreach (array_chunk($cat->courses(),4) as $coursechunk):?>
                <?php foreach ($coursechunk as $course):?>
                    <div class="col-md-3 col-sm-2">
                        <?php partial('admin/course',['course'=>$course])?>
                    </div>
                <?php endforeach;?>
            <?php endforeach;?>
        </div>
    </div>
</section>
<?php partial('admin/footer')?>

