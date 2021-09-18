<?php
 $dbhost = 'localhost';
 $dbusername = 'root';
 $dbpassword = '';
 $dbname =     'quizes';

 $conn = new PDO("mysql:host=$dbhost;dbname=$dbname", $dbusername, $dbpassword);

 try{
 	 $conn = new PDO("mysql:host=$dbhost;dbname=$dbname", $dbusername, $dbpassword,
 			 array(PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION));
    //die(json_encode(array('outcome' => true)));
}
catch(PDOException $ex){
   // die(json_encode(array('outcome' => false, 'message' => 'Unable to connect')));
}

?>