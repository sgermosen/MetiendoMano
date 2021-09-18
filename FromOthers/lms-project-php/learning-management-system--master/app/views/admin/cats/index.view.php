
<?php partial('admin/header',['title'=>'All Categories'])?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Categories
        <small>List</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/cats"><i class="ion ion-ios-list"></i> All Categories</a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <?php foreach ($cats as $cat):?>
        <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title"><?=$cat->name?></h3>
            <div class="box-tools pull-right">
                <a class="btn btn-primary btn-xs" href="/cats/<?=$cat->id?>">View Category</a>
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i>
                </button>
            </div>
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
        <div class="box-footer">
            <span class="badge bg-red"><?=count($cat->courses())?></span> Courses
        </div>
    </div>
    <?php endforeach;?>
</section>

<?php partial('admin/footer')?>

