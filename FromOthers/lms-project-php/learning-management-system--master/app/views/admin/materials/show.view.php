<?php partial('admin/header',['title'=>$material->name])?>
<!-- Content Header (Page header) -->
<section class="content-header">
    <h1>
        Materials
        <small>List</small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="/materials"><i class="fa fa-book"></i>Materials</a></li>
        <li><a href="/materials/<?=$material->id?>"><i class="fa fa-book"></i><?=$material->name?></a></li>
    </ol>
</section>
<!-- Main content -->
<section class="content">
    <div class="box">
        <div class="box-header with-border">
            <h3 class="box-title"><?=$material->name?></h3>
            <div class="text-center pull-right">
                <?php if($material->type=="video"):?>
                    <a class="btn btn-social-icon btn-facebook" href="https://www.facebook.com/sharer/sharer.php?u=<?=$material->link?>"><i class="fa fa-facebook"></i></a>
                    <a class="btn btn-social-icon btn-google"><i class="fa fa-google-plus" href="https://plus.google.com/share?url=<?=$material->link?>"></i></a>
                    <a class="btn btn-social-icon btn-twitter"><i class="fa fa-twitter" href="https://twitter.com/home?status=<?=$material->link?>"></i></a>
                <?php else:?>
                    <a class="btn btn-social-icon btn-facebook" href="https://www.facebook.com/sharer/sharer.php?u=<?=$_SERVER['HTTP_HOST'].$material->link?>"><i class="fa fa-facebook"></i></a>
                    <a class="btn btn-social-icon btn-google"><i class="fa fa-google-plus" href="https://plus.google.com/share?url=<?=$_SERVER['HTTP_HOST'].$material->link?>" ></i></a>
                    <a class="btn btn-social-icon btn-twitter"><i class="fa fa-twitter" href="https://twitter.com/home?status=<?=$_SERVER['HTTP_HOST'].$material->link?>"></i></a>
                <?php endif?>
                <a class="btn btn-primary" href="/materials/download?mat=<?=$material->id?>"><span class="badge bg-red"><?=$material->downloaded?></span> Download Material</a>
            </div>

        </div>
        <div class="box-body">
            <?=$material->description?>
            <br>
                <object width="100%" height="600" data="<?=$material->link?>"  title="<?=$material->name?>">
                    <div class="center-block text-center">
                        <p class="lead">Sorry can't view this Material you can download it</p>
                        <a class="btn btn-primary" href="/materials/download?mat=<?=$material->id?>">Download Material</a>
                    </div>
                </object>
            <span class="pull-right text-muted"><?=count($material->comments())?> Comments</span>
        </div>
        <!-- /.box-body -->
        <div class="box-footer box-comments">
            <?php foreach ($material->comments() as $comment):?>
                <div class="box-comment">
                    <!-- User image -->
                    <img class="img-circle img-sm" src="<?=$comment->user()->image?>" alt="User Image">
                    <?php if(\App\Core\Session::getLoginUser()->id==$comment->uid||\App\Core\Session::getLoginUser()->role=='admin'):?>
                        <?php \App\Core\Session::saveBackUrl()?>
                        <?php start_form('delete',"/comments/$comment->id")?>
                        <button type="submit" style="border: none;background-color: rgba(0,0,0,0); color:#9f191f" class="pull-right">
                            <span class="fa fa-remove"></span>
                        </button>
                        <?php close_form()?>
                    <?php endif?>
                    <div class="comment-text">
                      <span class="username">
                        <a href="/users/<?= $comment->user()->id?>"><?= $comment->user()->username?></a>
                        <span class="text-muted pull-right"><?= $comment->updated_at?></span>
                      </span><!-- /.username -->
                        <?= $comment->body?>
                    </div>
                    <!-- /.comment-text -->
                </div>
                <!-- /.box-comment -->
            <?php endforeach;?>
        </div>
        <!-- /.box-footer -->
        <div class="box-footer">
            <?php start_form('post','/comments/')?>
            <form class="form-horizontal">
                <input type="hidden" name="mid" value="<?="Material:$material->id"?>">
                <?php \App\Core\Session::saveBackUrl()?>
                <div class="form-group margin-bottom-none">
                    <div class="col-sm-10">
                        <input class="form-control input-sm" placeholder="Comment" name="body">
                    </div>
                    <div class="col-sm-2">
                        <button type="submit" class="btn btn-primary pull-right btn-block btn-sm">Comment</button>
                    </div>
                </div>
            </form>
            <?php close_form()?>
        </div>
        <!-- /.box-footer-->
    </div>
</section>

<?php partial('admin/footer')?>


