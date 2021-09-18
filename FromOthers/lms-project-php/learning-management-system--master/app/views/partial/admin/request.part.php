<div class="box box-widget">

    <div class="box-header with-border">
        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse" data-toggle="tooltip" title="Collapse">
                <i class="fa fa-minus"></i></button>
            <?php if(\App\Core\Session::getLoginUser()->id==$req->uid||\App\Core\Session::getLoginUser()->role=='admin'):?>
                <?php start_form('delete',"/requests/$req->id")?>
                <button type="submit" class="btn btn-box-tool"  title="" data-original-title="Remove">
                    <i class="fa fa-times"></i></button>
                <?php close_form()?>
            <?php endif;?>
        </div>
        <div class="user-block">
            <img class="img-circle" src="<?= $req->user()->image?>" alt="User Image">
            <span class="username"><a href="/users/<?= $req->user()->id?>"><?= $req->user()->username?></a></span>
            <span class="description"><?= $req->updated_at?></span>
        </div>
    </div>
    <!-- /.box-header -->
    <div class="box-body">
        <a href="/requests/<?=$req->id?>"><h3><?=$req->title?></h3></a>

        <!-- post text -->
        <p><?= $req->body?></p>
        <span class="pull-right text-muted"><?=count($req->comments())?> Comments</span>
    </div>
    <!-- /.box-body -->
    <div class="box-footer box-comments">
        <?php foreach ($req->comments() as $comment):?>
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
            <input type="hidden" name="mid" value="<?="UserRequest:$req->id"?>">
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
    <!-- /.box-footer -->
</div>