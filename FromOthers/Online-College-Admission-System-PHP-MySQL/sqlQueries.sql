            CREATE TABLE student_data(
                ID INT(3) NOT NULL PRIMARY KEY AUTO_INCREMENT,
                FULLNAME VARCHAR(70),
                GENDER VARCHAR(8),
                BGROUP VARCHAR(10),
                ADDRESS VARCHAR(100),
                CITY VARCHAR(20),
                STATE VARCHAR(50),
                ZIP VARCHAR(10),
                PNUMBER VARCHAR(11),
                EMAIL VARCHAR(60),
                PASSWORD VARCHAR(100),
                register_date datetime,
                UNIQUE(EMAIL)
            )
            INSERT INTO education_information (ID,ssc_board,ssc_school,ssc_per,ssc_attempt,
                                                hsc_board,hsc_school,hsc_per,hsc_attempt,
                                                grad_deg,grad_board,grad_school,grad_year,grad_attempt,
                                                pgrad_deg,pgrad_board,pgrad_school,pgrad_year,pgrad_attempt)
                                                VALUES
                                                ()

                CREATE TABLE education_information(
                    E_ID INT(3) NOT NULL PRIMARY KEY AUTO_INCREMENT,
                    ID INT(3) NOT NULL REFERENCES student_data(ID) ,
                    ssc_board VARCHAR(70) NOT NULL,
                    ssc_school VARCHAR(70) NOT NULL,
                    ssc_per VARCHAR(3) NOT NULL,
                    ssc_year VARCHAR(5) NOT NULL,
                    ssc_attempt VARCHAR(7) NOT NULL,
                    
                    hsc_board VARCHAR(70) NOT NULL,
                    hsc_school VARCHAR(70) NOT NULL,
                    hsc_per VARCHAR(3) NOT NULL,
                    hsc_year VARCHAR(5) NOT NULL,
                    hsc_attempt VARCHAR(7) NOT NULL,

                    grad_deg VARCHAR(70),
                    grad_board VARCHAR(70),
                    grad_school VARCHAR(70),
                    grad_per VARCHAR(3),
                    grad_year VARCHAR(5),
                    grad_attempt VARCHAR(7),

                    pgrad_deg VARCHAR(70),
                    pgrad_board VARCHAR(70),
                    pgrad_school VARCHAR(70),
                    pgrad_per VARCHAR(3),
                    pgrad_year VARCHAR(5),
                    pgrad_attempt VARCHAR(7)
                )

            CREATE TABLE courses(
                ID INT(2) PRIMARY KEY AUTO_INCREMENT,
                coursename VARCHAR(20) UNIQUE
            )

            
CREATE TABLE selected_courses(
     S_ID INT(2) PRIMARY KEY AUTO_INCREMENT, 
     ID INT(3) NOT NULL REFERENCES student_data(ID), 
     coursename VARCHAR(20) NOT NULL, 
     isAvailable BOOLEAN )


create Table educational_details_be (
    BE_ID int PRIMARY KEY AUTO_INCREMENT,
	ID 	int NOT NULL REFERENCES student_data(ID),
	ssc_school varchar(10) not null,
	ssc_year varchar(10) not null,
	ssc_percentage varchar(10) not null,
	ssc_class varchar(10) not null,
	hsc_school varchar(10) not null,
	hsc_year varchar(10) not null,
	hsc_pcm	varchar(10) not null,
	hsc_percentage varchar(10) not null,
	hsc_class varchar(10) not null,
	roll_no	varchar(10) not null,
	physics	int not null,
	chemistry int not null,
	maths int not null,
	total int not null,
	jee_main_rank varchar(10) not null,
	contact_01 varchar(10) not null,
	contact_02 varchar(10),
	acpc_no	varchar(10),
	acpc_merit varchar(10),
	p1 varchar(10) not null,
	p2 varchar(10) not null,
	p3 varchar(10) not null,
	p4 varchar(10) not null,
    isAvailable BOOLEAN NOT NULL
	);