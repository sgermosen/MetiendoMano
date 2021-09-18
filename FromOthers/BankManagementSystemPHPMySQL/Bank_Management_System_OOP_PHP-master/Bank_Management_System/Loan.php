<?php
   
   include("Customer.php");
   class Loan extends Customer 
   {
   	private $loanNumber;
   	private $branchName;
   	private $amount;

    public function createLoan($brName){  //public function opeAccount($number)
  		$this->amount = 0;
        
      $res = mysql_query("insert into lc set name = 'Asif'");
   	  $sql = mysql_query("select * from lc");
   	  while ($row=mysql_fetch_array($sql)) {
   		$x = $row[0];
   	  }

   	  $sql2 = mysql_query("select concat('LN-', id) as id from ac where id = '$x'");
      $row2 = mysql_fetch_row($sql2);

      $this->loanNumber = $row2[0];
         
      $this->branchName = $branchName;

      $res = mysql_query("INSERT into loan(Loan_number, Branch_name, Amount) VALUES('$this->loanNumber', '$this->branchName', '$this->amount')");

        return $res;
  	}
   }
?>