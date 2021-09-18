<?php
  include_once 'Branch.php';
  class Customer extends Branch
  {
  	private $customerName;
  	private $customerSreet;
  	private $customerCity;

  	public function createCustomer($name, $street, $city){
  		$this->customerName = $name;
  		$this->customerSreet = $street;
  		$this->customerCity = $city;
 
      $res1 = mysql_query("INSERT into customer(Customer_name, Customer_street, Customer_city) VALUES('$this->customerName', '$this->customerSreet', '$this->customerCity')");

      return $res1;
  	}

  }
?>