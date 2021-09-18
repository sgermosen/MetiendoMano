<?php
        include './Database/Controler.php';
        include 'role.php';
?>

                     
<h2 style="color:blue;">
    <b>Welcome <?php echo $_SESSION["fac_name"]; ?></b></h2>
    <br> 
<script language="javascript">
var i=0;
var path=new Array();

//list of images

path[0]="img/2.jpg";
path[1]="img/3.jpg";
path[2]="img/4.jpg";
path[3]="img/5.jpg";


function swapImage()
{
            document.slide.src=path[i];
            if(i<path.length-1)
	i++;
            else 
	i = 0;
    setTimeout("swapImage()",2700);
}

window.onload=swapImage;
</script>
       
<center>
    <img name="slide" src="img/1.jpeg" height="400"  width="900" style="border: 5px double black"></img>
</center>


<?php
        include 'footer.php';
?>