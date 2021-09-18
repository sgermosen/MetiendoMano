<?php
  include_once 'Loan.php';
  // include_once 'AccountNumber.php';

  // class Account extends AccountNumber
  class Account extends Loan
  {
  	private $accNumber;
  	private $balance;
    private $branchName;

  	public function openAccount($brName){  //public function opeAccount($number)
  		$this->balance = 500;
        
      $res = mysql_query("insert into ac set name = 'Asif'");
   		$sql = mysql_query("select * from ac");
   		while ($row=mysql_fetch_array($sql)) {
   			$x = $row[0];
   		}

   		$sql2 = mysql_query("select concat('AC-', id) as id from ac where id = '$x'");
      $row2 = mysql_fetch_row($sql2);

      $this->accNumber = $row2[0];
         
      $this->branchName = $brName;

      $res = mysql_query("INSERT into account(Account_number, Branch_name, Balance) VALUES('$this->accNumber', '$this->branchName', '$this->balance')");

        return $res;
  	}

    public function getAcNumber(){
      return $this->accNumber;
    }

    public function updateBalance($aNumber, $value){    
      $this->accNumber = $aNumber;
      $this->balance = $value;

      $res = mysql_query("UPDATE account SET Balance='$this->balance' WHERE Account_number='$this->accNumber'");

      return $res;
    }

    public function showBalance($acNo){
      $this->accNumber = $acNo;

      $res = mysql_query("select d.Customer_name, d.Account_number, a.Balance from depositor d, account a where d.Account_number = '$this->accNumber' and a.Account_number = '$this->accNumber'");

      return $res;
    }

    public function showAccountInfo($acNo){
      $this->accNumber = $acNo;

      $res = mysql_query("select d.Customer_name, d.Account_number, a.Balance, b.Branch_name, b.Branch_city, c.Customer_street, c.Customer_city from depositor d, account a, branch b, customer c where d.Account_number = '$this->accNumber' and a.Account_number = '$this->accNumber' and a.Branch_name=b.Branch_name and d.Customer_name=c.Customer_name");

      return $res;
    }

    public function showAllAccountInfo(){
      $res = mysql_query("select d.Customer_name, d.Account_number, c.Customer_street, c.Customer_city, a.Balance, b.Branch_name, b.Branch_city from depositor d, account a, branch b, customer c where d.Account_number = a.Account_number and d.Customer_name = c.Customer_name and a.Branch_name=b.Branch_name order by d.Account_number");

      return $res;      
    }

    public function closeAccount($acNo){
      $this->accNumber = $acNo;

      $res = mysql_query("DELETE FROM account WHERE Account_number = '$this->accNumber'");

      return $res;
    }

  }

?>