<div class="alert alert-danger">
    <ul>
        <?php foreach ($errors as $field=>$ferrs):?>
            <?php foreach ($ferrs as $error):?>
                <p><stron><?=$field?>: </stron><?=$error?></p>
            <?php endforeach;?>
        <?php endforeach;?>
    </ul>
</div>