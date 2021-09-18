<?php if($material->status!='hide'):?>
<?php if($material->type=='pdf'){?>
    <li>
        <i class="fa fa-file-pdf-o bg-red"></i>
        <div class="timeline-item">
            <span class="time"><i class="fa fa-clock-o"></i> <?= $material->created_at?></span>

            <h3 class="timeline-header"><?= $material->name?></h3>

            <div class="timeline-body">
                <?= $material->description?>
            </div>
            <div class="timeline-footer">
                <a class="btn btn-primary btn-xs" href="/materials/<?=$material->id?>">View Material</a>
                <a class="btn btn-primary btn-xs" href="/materials/download?mat=<?=$material->id?>">Download Material</a>
            </div>
        </div>
    </li>
<?php }elseif($material->type=='doc'){?>
    <li>
        <i class="fa fa-file-word-o bg-blue"></i>
        <div class="timeline-item">
            <span class="time"><i class="fa fa-clock-o"></i> <?= $material->created_at?></span>

            <h3 class="timeline-header"><?= $material->name?></h3>

            <div class="timeline-body">
                <?= $material->description?>
            </div>
            <div class="timeline-footer">
                <a class="btn btn-primary btn-xs" href="/materials/<?=$material->id?>">View Material</a>
                <a class="btn btn-primary btn-xs" href="/materials/download?mat=<?=$material->id?>">Download Material</a>
            </div>
        </div>
    </li>
<?php }elseif($material->type=='ppt'){?>
    <li>
        <i class="fa fa-file-powerpoint-o bg-orange"></i>
        <div class="timeline-item">
            <span class="time"><i class="fa fa-clock-o"></i> <?= $material->created_at?></span>

            <h3 class="timeline-header"><?= $material->name?></h3>

            <div class="timeline-body">
                <?= $material->description?>
            </div>
            <div class="timeline-footer">
                <a class="btn btn-primary btn-xs" href="/materials/<?=$material->id?>">View Material</a>
                <a class="btn btn-primary btn-xs" href="/materials/download?mat=<?=$material->id?>">Download Material</a>
            </div>
        </div>
    </li>
<?php }elseif($material->type=='video'){?>
    <li>
        <i class="fa fa-file-video-o bg-blue"></i>
        <div class="timeline-item">
            <span class="time"><i class="fa fa-clock-o"></i> <?= $material->created_at?></span>

            <h3 class="timeline-header"><?= $material->name?></h3>

            <div class="timeline-body">
                <?= $material->description?>
            </div>
            <div class="timeline-footer">
                <a class="btn btn-primary btn-xs" href="/materials/<?=$material->id?>">View Material</a>
                <a class="btn btn-primary btn-xs" href="/materials/download?mat=<?=$material->id?>">Download Material</a>
            </div>
        </div>
    </li>
<?php }?>
<?php endif;?>
