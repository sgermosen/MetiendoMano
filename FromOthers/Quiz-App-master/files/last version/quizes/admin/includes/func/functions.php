<?php


	/*
	** Get All Function v2.0
	** Function To Get All Records From Any Database Table
	*/

	function getAllFrom($field, $table, $where = NULL, $and = NULL, $orderfield, $ordering = "DESC") {

		global $conn;

		$getAll = $conn->prepare("SELECT $field FROM $table $where $and ORDER BY $orderfield $ordering");

		$getAll->execute();

		$all = $getAll->fetchAll();

		return $all;

	}





   /*
	** Title Function v1.0
	** Title Function That Echo The Page Title In Case The Page
	** Has The Variable $pageTitle And Echo Defult Title For Other Pages
	*/

    
    

    function getTitle() {
        
        global $pageTitle ;
        
        if(isset($pageTitle)) {
            
            echo $pageTitle ;
            
        }
        
        else {
            echo 'Default' ;
        }
    }
	
	
	
	 /*
	** Redirect Function v1.0
	** Redirecting To Home Page
	** echo Msg Error
	** Params $errMsg The Msg To Show
	** $seconds : number od seconds to redirect
	** header("refresh:$seconds;url=index.php")
	*/
	
	function redirectHome($errMsg , $seconds = 2) {
		
		echo "<div class='alert alert-danger'>$errMsg</div>" ;
		echo "<div class='alert alert-info'>You Will Be Redirected to Home After $seconds Seconds.</div>";
		header("refresh:$seconds;url=index.php") ;
		exit() ;
		
		
	}
	
	/*
	 ** Redirect Function V 2.0
	 ** Redirecting To Previous Page
	 ** Params $msg echo The Msg [Error , Warning , Success]
	 ** $url : Link To Redirect To ==> Go to Previous Page By $_SERVER['HTTP_REFERER']
	 ** 
	 ** 
	 */
	
	function redirect($msg , $url = null ,$seconds = 2) {
		
		if($url === null) {
			
			$url = 'index.php' ;
			$link = 'Home' ;
			
		}elseif($url == 'back') {
			
			if(isset($_SERVER['HTTP_REFERER']) && !empty($_SERVER['HTTP_REFERER']) ) {
				
				$url = $_SERVER['HTTP_REFERER'] ;
				$link = 'Previous Page' ;
				
			}else {
				$url = 'index.php' ;
			
			}
			
			
			
		}
		
		
		
		
		
		echo $msg ;
		echo "<div class='alert alert-info'>You Will Be Redirected  After $seconds Seconds.</div>";
		header("refresh:$seconds;url=$url");
		exit();
		
	}
	
	/*
	 ** Check Item Function  V 1.0
	 ** Function To Check Item In DB [Accespts Params]
	 ** $select : The Items To Select [Examples : Username :ItemName]
	 ** $table : The Table To search In
	 ** $value : The $select Value
	 */
	
	
	function checkItem($select , $table , $value) {
		
		global $conn ;
		$statement = $conn->prepare("SELECT $select FROM $table WHERE $select = ?") ;
		$statement->execute(array($value)) ;
		$count = $statement->rowCount() ;
				
		return $count ;
	}
	
	/*
	 ** Count Number Of Items V 1.0
	 ** Function To Check Number Of Itema In DB [Accespts Params]
	 ** $item = Item To Count
	 ** $table = Table To Select
	 * 
	 */
	
	function countItems($item , $table) {
		
		 global $conn ;
		 
		 $stmt2 = $conn->prepare("SELECT COUNT($item) FROM $table");
		 $stmt2->execute() ;
		 
		 return $stmt2->fetchColumn();
	}
	
	/*
	 ** Get Latest Records Function V 1.0
	 ** Function To Get Latest Items From DB [usersm items , comments ,etc ..]
	 ** $select = Field To Select
	 ** $table  = Table To Choose From
	 ** $order  = The Column to get latest items
	 ** $limit  = Number Of Records
	 ** Desc Mean : Tnazolian (ar)
	 ** 
	 */
	
	
	function getLatest($select , $table , $order , $limit = 5) {
		
		global $conn ;
		
		$stmt = $conn->prepare("SELECT $select FROM $table ORDER BY $order DESC LIMIT $limit") ;
		$stmt->execute() ;
		$rows = $stmt->fetchAll();
		
		return $rows ;
	}
	
	
	function msg ($type = "success" , $body = "Give Me Your Message") {
		
		$msg = '' ;
		
		$msgTypes = array("success" , "danger") ;
		
		if(in_array($type , $msgTypes)) {
			
			$msg = "<div class='alert alert-$type'>$body</div>" ;
			return $msg ;
			
		}else {
			
			$msg =  "<div class='alert alert-danger'>$body</div>" ;
			
			return $msg ;
			
		}
		
		
		
	}

	function correct_ans($question_id = 1) {

		global $conn ;

		$stmt3 = $conn->prepare("SELECT correct_ans FROM answers WHERE questions_id = ?");
		$stmt3->execute(array($question_id));
		$correct_ans = $stmt3->fetch();

		return $correct_ans ;

	}



	function insertCourse($grade_id , $division_id , $course_name) {
		
		global $conn ;
		
		$query = "INSERT INTO courses(grade_id , division_id , course_name)
				  VALUES(?,?,?)" ;
		$stmt = $conn->prepare($query);
		$stmt->execute(array($grade_id ,$division_id,$course_name )) ;
		
		$theMsg = "<div class='alert alert-success'>" . $stmt->rowCount() . ' Record Inserted</div>';
		redirect($theMsg, 'courses.php');
		
		
	}


	function getCourses() {
		
		global $conn ;
		
		$query = "
		
		SELECT  courses.course_id , courses.course_name ,  grade.grade_name  ,  		division.division_name  
		FROM courses 			  
		INNER JOIN grade
		ON courses.grade_id = grade.grade_id 
	    INNER JOIN division 
	    ON courses.division_id = division.division_id

					" ; 
		$stmt = $conn->prepare($query) ; 
		$stmt->execute() ;
		
		
		
		return $rows = $stmt->fetchAll();
		
		
		
	}

	function getCoursesInfo($course_id) {
		
		global $conn ;
		
		$query = "
		
		SELECT  courses.course_id , courses.course_name ,  grade.grade_name  , grade.grade_id,		division.division_name  ,  division.division_id
		FROM courses 			  
		INNER JOIN grade
		ON courses.grade_id = grade.grade_id 
	    INNER JOIN division 
	    ON courses.division_id = division.division_id
		WHERE courses.course_id = ?

					" ; 
		$stmt = $conn->prepare($query) ; 
		$stmt->execute(array($course_id)) ;
		
		
		
		return $rows = $stmt->fetch();
		
		
		
	}


	function getStudents(){
		
		global $conn ;
		
		$query = " SELECT  student.student_id , student.fname , student.card_id , student.email , 						grade.grade_name , division.division_name , student.lname 				
		
					FROM student INNER JOIN grade
					ON student.grade_id = grade.grade_id 
					INNER JOIN division 
					ON student.division_id = division.division_id


 				  " ;
		
		$stmt = $conn->prepare($query) ; 
		$stmt->execute() ;
		
		
		
		return $rows = $stmt->fetchAll();
		
		
		
	}



	function getProfessors(){
		
		global $conn ;
		
		$query = " SELECT  professor.id , professor.fname  , professor.lname  , professor.email
				 
					FROM professor  

 				  " ;
		
		$stmt = $conn->prepare($query) ; 
		$stmt->execute() ;
		
		
		
		return $rows = $stmt->fetchAll();
		
		
		
	}


	


	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	