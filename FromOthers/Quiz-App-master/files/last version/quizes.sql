-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jun 06, 2018 at 10:42 AM
-- Server version: 10.1.10-MariaDB
-- PHP Version: 7.0.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `quizes`
--

-- --------------------------------------------------------

--
-- Table structure for table `answers`
--

CREATE TABLE `answers` (
  `id` int(11) NOT NULL,
  `ans1` varchar(100) NOT NULL,
  `ans2` varchar(100) NOT NULL,
  `ans3` varchar(100) NOT NULL,
  `ans4` varchar(100) NOT NULL,
  `correct_ans` varchar(100) NOT NULL,
  `questions_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `answers`
--

INSERT INTO `answers` (`id`, `ans1`, `ans2`, `ans3`, `ans4`, `correct_ans`, `questions_id`) VALUES
(1, '17', '18', '19', '20', '17', 1),
(3, '35', '13', '12', '15', '35', 3);

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `course_id` int(11) NOT NULL,
  `grade_id` int(11) NOT NULL,
  `division_id` int(11) NOT NULL,
  `course_name` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`course_id`, `grade_id`, `division_id`, `course_name`) VALUES
(1, 3, 1, 'Software Engineering'),
(3, 1, 1, 'English');

-- --------------------------------------------------------

--
-- Table structure for table `courses_has_exam`
--

CREATE TABLE `courses_has_exam` (
  `courses_id` int(11) NOT NULL,
  `exam_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `courses_has_exam`
--

INSERT INTO `courses_has_exam` (`courses_id`, `exam_id`) VALUES
(1, 2),
(3, 3);

-- --------------------------------------------------------

--
-- Table structure for table `courses_has_student`
--

CREATE TABLE `courses_has_student` (
  `course_id` int(11) NOT NULL,
  `student_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `division`
--

CREATE TABLE `division` (
  `division_id` int(11) NOT NULL,
  `division_name` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `division`
--

INSERT INTO `division` (`division_id`, `division_name`) VALUES
(1, 'علوم الحاسب'),
(2, 'نظم معلومات'),
(3, 'محاسبه'),
(4, 'ادارة');

-- --------------------------------------------------------

--
-- Table structure for table `exam`
--

CREATE TABLE `exam` (
  `exam_id` int(11) NOT NULL,
  `exam_name` varchar(45) NOT NULL,
  `exam_start_date` date NOT NULL,
  `exam_duration` int(11) NOT NULL,
  `exam_status` int(11) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `exam`
--

INSERT INTO `exam` (`exam_id`, `exam_name`, `exam_start_date`, `exam_duration`, `exam_status`) VALUES
(1, 'Software', '2018-06-11', 10, 1),
(2, 'ex1', '2018-06-11', 15, 1),
(3, 'ex15', '2018-04-17', 0, 1);

-- --------------------------------------------------------

--
-- Table structure for table `exam_has_questions`
--

CREATE TABLE `exam_has_questions` (
  `exam_id` int(11) NOT NULL,
  `questions_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `exam_has_questions`
--

INSERT INTO `exam_has_questions` (`exam_id`, `questions_id`) VALUES
(1, 1),
(1, 3),
(2, 3);

-- --------------------------------------------------------

--
-- Table structure for table `exam_results`
--

CREATE TABLE `exam_results` (
  `id` int(11) NOT NULL,
  `total_points` int(11) NOT NULL,
  `correct_ans` int(11) NOT NULL,
  `wrong_ans` int(11) NOT NULL,
  `student_id` int(11) NOT NULL,
  `exam_id` int(11) NOT NULL,
  `division_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `exam_results`
--

INSERT INTO `exam_results` (`id`, `total_points`, `correct_ans`, `wrong_ans`, `student_id`, `exam_id`, `division_id`) VALUES
(1, 100, 50, 0, 1, 1, 4);

-- --------------------------------------------------------

--
-- Table structure for table `grade`
--

CREATE TABLE `grade` (
  `grade_id` int(11) NOT NULL,
  `grade_name` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `grade`
--

INSERT INTO `grade` (`grade_id`, `grade_name`) VALUES
(1, 'الفرقة الأولي'),
(2, 'الفرقة الثانيه'),
(3, 'الفرقة الثالثه'),
(4, 'الفرقة الرابعه');

-- --------------------------------------------------------

--
-- Table structure for table `professor`
--

CREATE TABLE `professor` (
  `id` int(11) NOT NULL,
  `email` varchar(45) NOT NULL,
  `password` varchar(255) NOT NULL,
  `fname` varchar(45) NOT NULL,
  `lname` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `professor`
--

INSERT INTO `professor` (`id`, `email`, `password`, `fname`, `lname`) VALUES
(2, 'hesham.elalawy@gmail.com', '7c4a8d09ca3762af61e59520943dc26494f8941b', 'Alaa', 'Elalawy'),
(3, 'heshamzz@info.com', '7c4a8d09ca3762af61e59520943dc26494f8941b', 'Ahmed', 'Elalawy');

-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE `questions` (
  `id` int(11) NOT NULL,
  `question` varchar(200) NOT NULL,
  `question_points` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `questions`
--

INSERT INTO `questions` (`id`, `question`, `question_points`) VALUES
(1, '5 + 12 ', 5),
(3, '15 + 20', 10);

-- --------------------------------------------------------

--
-- Table structure for table `student`
--

CREATE TABLE `student` (
  `student_id` int(11) NOT NULL,
  `card_id` int(32) DEFAULT NULL,
  `email` varchar(45) NOT NULL,
  `password` varchar(255) NOT NULL,
  `fname` varchar(45) NOT NULL,
  `lname` varchar(45) NOT NULL,
  `dob` date NOT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `last_login_date` date DEFAULT NULL,
  `division_id` int(11) NOT NULL,
  `grade_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `student`
--

INSERT INTO `student` (`student_id`, `card_id`, `email`, `password`, `fname`, `lname`, `dob`, `phone`, `last_login_date`, `division_id`, `grade_id`) VALUES
(1, 1235, 'hesham.elalawy@gmail.com', '94ae0a96d83a445d72a93417b63ac90d79db5eca', 'Hesham', 'Elalawy', '0000-00-00', NULL, NULL, 1, 4),
(2, 1237, 'm.ebrahim@gmail.com', '7c4a8d09ca3762af61e59520943dc26494f8941b', 'Mohamed', 'Ebrahim', '0000-00-00', NULL, NULL, 1, 4),
(3, 12389, 'dd@met.com', '7c4a8d09ca3762af61e59520943dc26494f8941b', 'do', 'da', '0000-00-00', NULL, NULL, 1, 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `answers`
--
ALTER TABLE `answers`
  ADD PRIMARY KEY (`id`,`questions_id`),
  ADD KEY `fk_answers_questions1_idx` (`questions_id`);

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`course_id`),
  ADD KEY `fk_courses_grade_idx` (`grade_id`),
  ADD KEY `fk_courses_division1_idx` (`division_id`);

--
-- Indexes for table `courses_has_exam`
--
ALTER TABLE `courses_has_exam`
  ADD PRIMARY KEY (`courses_id`,`exam_id`),
  ADD KEY `fk_courses_has_exam_exam1_idx` (`exam_id`),
  ADD KEY `fk_courses_has_exam_courses1_idx` (`courses_id`);

--
-- Indexes for table `courses_has_student`
--
ALTER TABLE `courses_has_student`
  ADD PRIMARY KEY (`course_id`,`student_id`),
  ADD KEY `fk_courses_has_student_student1_idx` (`student_id`),
  ADD KEY `fk_courses_has_student_courses1_idx` (`course_id`);

--
-- Indexes for table `division`
--
ALTER TABLE `division`
  ADD PRIMARY KEY (`division_id`);

--
-- Indexes for table `exam`
--
ALTER TABLE `exam`
  ADD PRIMARY KEY (`exam_id`);

--
-- Indexes for table `exam_has_questions`
--
ALTER TABLE `exam_has_questions`
  ADD PRIMARY KEY (`exam_id`,`questions_id`),
  ADD KEY `fk_exam_has_questions_questions1_idx` (`questions_id`),
  ADD KEY `fk_exam_has_questions_exam1_idx` (`exam_id`);

--
-- Indexes for table `exam_results`
--
ALTER TABLE `exam_results`
  ADD PRIMARY KEY (`id`,`student_id`,`exam_id`),
  ADD KEY `fk_exam_results_student1_idx` (`student_id`),
  ADD KEY `fk_exam_results_exam1_idx` (`exam_id`),
  ADD KEY `fk_exam_results_division1_idx` (`division_id`);

--
-- Indexes for table `grade`
--
ALTER TABLE `grade`
  ADD PRIMARY KEY (`grade_id`);

--
-- Indexes for table `professor`
--
ALTER TABLE `professor`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `student`
--
ALTER TABLE `student`
  ADD PRIMARY KEY (`student_id`),
  ADD KEY `fk_student_division1_idx` (`division_id`),
  ADD KEY `fk_student_grade1_idx` (`grade_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `answers`
--
ALTER TABLE `answers`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `course_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `division`
--
ALTER TABLE `division`
  MODIFY `division_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `exam`
--
ALTER TABLE `exam`
  MODIFY `exam_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `exam_results`
--
ALTER TABLE `exam_results`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `grade`
--
ALTER TABLE `grade`
  MODIFY `grade_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `professor`
--
ALTER TABLE `professor`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `student`
--
ALTER TABLE `student`
  MODIFY `student_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `answers`
--
ALTER TABLE `answers`
  ADD CONSTRAINT `fk_answers_questions1` FOREIGN KEY (`questions_id`) REFERENCES `questions` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `courses`
--
ALTER TABLE `courses`
  ADD CONSTRAINT `fk_courses_division1` FOREIGN KEY (`division_id`) REFERENCES `division` (`division_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_courses_grade` FOREIGN KEY (`grade_id`) REFERENCES `grade` (`grade_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `courses_has_exam`
--
ALTER TABLE `courses_has_exam`
  ADD CONSTRAINT `fk_courses_has_exam_courses1` FOREIGN KEY (`courses_id`) REFERENCES `courses` (`course_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_courses_has_exam_exam1` FOREIGN KEY (`exam_id`) REFERENCES `exam` (`exam_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `courses_has_student`
--
ALTER TABLE `courses_has_student`
  ADD CONSTRAINT `fk_courses_has_student_courses1` FOREIGN KEY (`course_id`) REFERENCES `courses` (`course_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_courses_has_student_student1` FOREIGN KEY (`student_id`) REFERENCES `student` (`student_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `exam_has_questions`
--
ALTER TABLE `exam_has_questions`
  ADD CONSTRAINT `fk_exam_has_questions_exam1` FOREIGN KEY (`exam_id`) REFERENCES `exam` (`exam_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_exam_has_questions_questions1` FOREIGN KEY (`questions_id`) REFERENCES `questions` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `exam_results`
--
ALTER TABLE `exam_results`
  ADD CONSTRAINT `fk_exam_results_division1` FOREIGN KEY (`division_id`) REFERENCES `division` (`division_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_exam_results_exam1` FOREIGN KEY (`exam_id`) REFERENCES `exam` (`exam_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_exam_results_student1` FOREIGN KEY (`student_id`) REFERENCES `student` (`student_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `student`
--
ALTER TABLE `student`
  ADD CONSTRAINT `fk_student_division1` FOREIGN KEY (`division_id`) REFERENCES `division` (`division_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_student_grade1` FOREIGN KEY (`grade_id`) REFERENCES `grade` (`grade_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
