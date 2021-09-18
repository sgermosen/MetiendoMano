<?php
    class Connection
    {
        function mkConnection()
        {
            $con=new mysqli("localhost","root","","attendance");
            return $con;
        }
    }
?>