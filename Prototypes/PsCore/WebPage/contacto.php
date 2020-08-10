<?php
header("Access-Control-Allow-Origin: *");
$destinatario = "sgermosen@praysoft.net"; 
$asunto = $_POST['concepto']; 
$cuerpo = ' 
<html> 
<head> 
   <title>'.$_POST['concepto'].'</title> 
</head> 
<body> 
<p> 
'.$_POST['mensaje'].'
</p> 
<h4>'.$_POST['persona'].'</h4>
<h5>'.$_POST['empresa'].'</h5>
<h5>'.$_POST['direccion'].'</h5>
<h5>'.$_POST['provincia'].'</h5>
<h5>'.$_POST['telefono'].'</h5>
</body> 
</html> 
'; 

//para el envío en formato HTML 
$headers = "MIME-Version: 1.0\r\n"; 
$headers .= "Content-type: text/html; charset=iso-8859-1\r\n"; 

//dirección del remitente 
$headers .= "From: <".$_POST['email'].">\r\n"; 

mail($destinatario,$asunto,$cuerpo,$headers) 
?>
