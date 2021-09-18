<?php

     include "connection.php";

    if (!empty($_POST)) {
      // Innitialize Variable
      $result='';
        $cardnumber = $_POST['cardnumber'];
          $password = $_POST['password'];
      
      // Query database for row exist or not
          $sql = 'SELECT * FROM student2 WHERE  cardnumber = :cardnumber AND password = :password';
          $stmt = $conn->prepare($sql);
          $stmt->bindParam(':cardnumber', $cardnumber, PDO::PARAM_STR);
          $stmt->bindParam(':password', $password, PDO::PARAM_STR);
          $stmt->execute();
          if($stmt->rowCount())
          {
            $result="sccuss";  
          }  
          elseif(!$stmt->rowCount())
          {
            $result="error";
          }
      
      // send result back to android
        echo $result;
    }
?>