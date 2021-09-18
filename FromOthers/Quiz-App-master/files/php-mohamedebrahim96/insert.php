<?php

     include "connection.php";

    if (!empty($_POST)) {
    try {
    // set the PDO error mode to exception
    $conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
     $sql  = "INSERT INTO `student2` (
      `cardnumber`,
       `email`,
        `password`,
         `fname`,
          `lname`,
           `phone`) VALUES ( '".$_POST["cardnumber"]."',
                             '".$_POST["email"]."',
                              '".$_POST["password"]."',
                                 '".$_POST["fname"]."',
                                  '".$_POST["lname"]."',
                                 '".$_POST["phone"]."')";




    $conn->exec($sql);
    echo "New record created successfully";
    }
catch(PDOException $e)
    {
    echo $sql . "<br>" . $e->getMessage();
    }
$conn = null;
   
}
?>