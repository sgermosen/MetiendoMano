<?php
   
   class Branch
   {
   	private $branchName;
   	private $branchCity;
   	private $assets;

   	public function createBranch($name, $city, $assets){
        $this->branchName = $name;
        $this->branchCity = $city;
        $this->assets = $assets;

        $res = mysql_query("INSERT into branch(Branch_name, Branch_city, Assets) VALUES('$this->branchName', '$this->branchCity', '$this->assets')");

        return $res;
   	}

   	public function getBranch(){
      $res = mysql_query("select * from branch order by Branch_name");
   		return $res;
   	}

   	public function showBranchInfo(){

   	}
   }
?>