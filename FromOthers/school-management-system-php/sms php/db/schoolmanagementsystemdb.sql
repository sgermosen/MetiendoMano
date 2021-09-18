-- phpMyAdmin SQL Dump
-- version 4.1.6
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: May 05, 2016 at 04:26 PM
-- Server version: 5.6.16
-- PHP Version: 5.5.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `schoolmanagementsystemdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE IF NOT EXISTS `admin` (
  `id` varchar(20) NOT NULL,
  `name` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `phone` varchar(13) NOT NULL,
  `email` varchar(20) NOT NULL,
  `dob` date NOT NULL,
  `hiredate` date NOT NULL,
  `address` varchar(30) NOT NULL,
  `sex` varchar(7) NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`id`, `name`, `password`, `phone`, `email`, `dob`, `hiredate`, `address`, `sex`) VALUES
('ad-123-0', 'Prosen', '123', '01822804168', 'prosen@example.com', '1993-11-20', '2016-01-01', 'Dhaka,Cantonment', 'male'),
('ad-123-1', 'Rifat', '123', '01922000000', 'Rifat@gmail.com', '1992-05-12', '2016-04-24', 'Dhaka', 'Male'),
('ad-123-2', 'Rizvi', '123', '01922000012', 'rizvi@gmail.com', '1992-05-12', '2016-04-24', 'Dhaka', 'Male'),
('ad-123-3', 'Barid', '123', '01922012000', 'barid@gmail.com', '1992-05-12', '2016-04-24', 'Dhaka', 'Male');

-- --------------------------------------------------------

--
-- Table structure for table `attendance`
--

CREATE TABLE IF NOT EXISTS `attendance` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `date` date NOT NULL,
  `attendedid` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=29 ;

--
-- Dumping data for table `attendance`
--

INSERT INTO `attendance` (`id`, `date`, `attendedid`) VALUES
(18, '2016-05-04', 'te-123-1'),
(20, '2016-05-01', 'te-123-1'),
(21, '2016-04-12', 'te-123-1'),
(22, '2016-05-04', 'te-124-1'),
(23, '2016-04-19', 'te-124-1'),
(24, '2016-05-02', 'te-124-1'),
(25, '2016-05-04', 'sta-123-1'),
(26, '2016-05-05', 'sta-123-1'),
(27, '2016-04-04', 'sta-123-1'),
(28, '2016-04-05', 'sta-123-1');

-- --------------------------------------------------------

--
-- Table structure for table `availablecourse`
--

CREATE TABLE IF NOT EXISTS `availablecourse` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(30) NOT NULL,
  `classid` varchar(30) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=41 ;

--
-- Dumping data for table `availablecourse`
--

INSERT INTO `availablecourse` (`id`, `name`, `classid`) VALUES
(1, 'Bangla 1st', '1'),
(2, 'Bangla 1st', '2'),
(3, 'Bangla 1st', '3'),
(4, 'Bangla 1st', '4'),
(5, 'Bangla 1st', '5'),
(6, 'Bangla 1st', '6'),
(7, 'Bangla 1st', '7'),
(8, 'Bangla 1st', '8'),
(9, 'Bangla 1st', '9'),
(10, 'Bangla 1st', '10'),
(11, 'Bangla 2nd', '1'),
(12, 'Bangla 2nd', '2'),
(13, 'Bangla 2nd', '3'),
(14, 'Bangla 2nd', '4'),
(15, 'Bangla 2nd', '5'),
(16, 'Bangla 2nd', '6'),
(17, 'Bangla 2nd', '7'),
(18, 'Bangla 2nd', '8'),
(19, 'Bangla 2nd', '9'),
(20, 'Bangla 2nd', '10'),
(21, 'English 1st', '1'),
(22, 'English 1st', '2'),
(23, 'English 1st', '3'),
(24, 'English 1st', '4'),
(25, 'English 1st', '5'),
(26, 'English 1st', '6'),
(27, 'English 1st', '7'),
(28, 'English 1st', '8'),
(29, 'English 1st', '9'),
(30, 'English 1st', '10'),
(31, 'English 2nd', '1'),
(32, 'English 2nd', '2'),
(33, 'English 2nd', '3'),
(34, 'English 2nd', '4'),
(35, 'English 2nd', '5'),
(36, 'English 2nd', '6'),
(37, 'English 2nd', '7'),
(38, 'English 2nd', '8'),
(39, 'English 2nd', '9'),
(40, 'English 2nd', '10');

-- --------------------------------------------------------

--
-- Table structure for table `class`
--

CREATE TABLE IF NOT EXISTS `class` (
  `id` varchar(20) NOT NULL,
  `name` varchar(20) NOT NULL,
  `room` varchar(20) NOT NULL,
  `section` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `class`
--

INSERT INTO `class` (`id`, `name`, `room`, `section`) VALUES
('1A', 'One', '101', 'A'),
('1B', 'One', '101', 'B'),
('2A', 'Two', '201', 'A'),
('2B', 'Two', '202', 'B'),
('3A', 'Three', '301', 'A'),
('3B', 'Three', '302', 'B'),
('4A', 'Four', '401', 'A'),
('4B', 'Four', '402', 'B'),
('5A', 'Five', '501', 'A'),
('5B', 'Five', '502', 'B'),
('6A', 'Six', '601', 'A'),
('6B', 'Six', '602', 'B'),
('7A', 'Seven', '701', 'A'),
('7B', 'Seven', '702', 'B'),
('8A', 'Eight', '801', 'A'),
('8B', 'Eight', '802', 'B'),
('9A', 'Nine', '901', 'A'),
('9B', 'Nine', '902', 'B'),
('10S', 'Ten', '1001', 'Science'),
('10A', 'Ten', '1002', 'Arts'),
('10C', 'Ten', '1002', 'Commerce');

-- --------------------------------------------------------

--
-- Table structure for table `course`
--

CREATE TABLE IF NOT EXISTS `course` (
  `id` varchar(20) NOT NULL,
  `name` varchar(20) NOT NULL,
  `teacherid` varchar(20) NOT NULL,
  `studentid` varchar(20) NOT NULL,
  `classid` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `course`
--

INSERT INTO `course` (`id`, `name`, `teacherid`, `studentid`, `classid`) VALUES
('1', 'Bangla 1st', 'te-124-1', 'st-123-1', '1A'),
('1', 'Bangla 1st', 'te-124-1', 'st-124-1', '1A');

-- --------------------------------------------------------

--
-- Table structure for table `examschedule`
--

CREATE TABLE IF NOT EXISTS `examschedule` (
  `id` varchar(20) NOT NULL,
  `examdate` date NOT NULL,
  `time` varchar(20) NOT NULL,
  `courseid` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `examschedule`
--

INSERT INTO `examschedule` (`id`, `examdate`, `time`, `courseid`) VALUES
('145', '2016-05-06', '2:00-4:00', '1');

-- --------------------------------------------------------

--
-- Table structure for table `grade`
--

CREATE TABLE IF NOT EXISTS `grade` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `studentid` varchar(20) NOT NULL,
  `grade` varchar(5) NOT NULL,
  `courseid` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Dumping data for table `grade`
--

INSERT INTO `grade` (`id`, `studentid`, `grade`, `courseid`) VALUES
(1, 'st-123-1', 'C', '8'),
(2, 'st-123-1', 'F', '4'),
(3, 'st-125-1', 'D+', '1'),
(4, 'st-123-1', 'D+', '1'),
(5, 'st-124-1', 'C+', '1');

-- --------------------------------------------------------

--
-- Table structure for table `parents`
--

CREATE TABLE IF NOT EXISTS `parents` (
  `id` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `fathername` varchar(20) NOT NULL,
  `mothername` varchar(20) NOT NULL,
  `fatherphone` varchar(13) NOT NULL,
  `motherphone` varchar(13) NOT NULL,
  `address` varchar(30) NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `parents`
--

INSERT INTO `parents` (`id`, `password`, `fathername`, `mothername`, `fatherphone`, `motherphone`, `address`) VALUES
('pa-123-1', '123', 'Mr.A', 'mrs.A', '01711000000', '01711000000', 'unknown'),
('pa-124-1', '123', 'Mukles khan', 'morzina akter', '01724242424', '01924242314', 'Dhaka');

-- --------------------------------------------------------

--
-- Table structure for table `payment`
--

CREATE TABLE IF NOT EXISTS `payment` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `studentid` varchar(20) NOT NULL,
  `amount` double NOT NULL,
  `month` varchar(10) NOT NULL,
  `year` varchar(5) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `payment`
--

INSERT INTO `payment` (`id`, `studentid`, `amount`, `month`, `year`) VALUES
(1, 'st-123-1', 500, '5', '2016'),
(2, 'st-123-1', 500, '4', '2016'),
(3, 'st-124-1', 500, '5', '2016');

-- --------------------------------------------------------

--
-- Table structure for table `report`
--

CREATE TABLE IF NOT EXISTS `report` (
  `reportid` int(11) NOT NULL AUTO_INCREMENT,
  `studentid` varchar(20) NOT NULL,
  `teacherid` varchar(20) NOT NULL,
  `message` varchar(500) NOT NULL,
  `courseid` varchar(20) NOT NULL,
  PRIMARY KEY (`reportid`)
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=4 ;

--
-- Dumping data for table `report`
--

INSERT INTO `report` (`reportid`, `studentid`, `teacherid`, `message`, `courseid`) VALUES
(1, 'st-123-1', 'te-123-1', 'Good Boy', '790'),
(2, 'st-124-1', 'te-123-1', 'Good boy But not honest.', '790'),
(3, 'st-123-1', 'te-124-1', ' good', '1');

-- --------------------------------------------------------

--
-- Table structure for table `staff`
--

CREATE TABLE IF NOT EXISTS `staff` (
  `id` varchar(20) NOT NULL,
  `name` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `phone` varchar(20) NOT NULL,
  `email` varchar(20) NOT NULL,
  `sex` varchar(7) NOT NULL,
  `dob` date NOT NULL,
  `hiredate` date NOT NULL,
  `address` varchar(30) NOT NULL,
  `salary` double NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `staff`
--

INSERT INTO `staff` (`id`, `name`, `password`, `phone`, `email`, `sex`, `dob`, `hiredate`, `address`, `salary`) VALUES
('sta-123-1', 'Fogg', '123', '01913827384', 'fogg@example.com', 'male', '1985-12-18', '2016-01-01', 'dhaka', 900000),
('sta-124-1', 'Eyasin', '123', '01822369874', 'eyasin@example.com', 'Male', '1998-03-25', '2016-05-03', 'Dhaka', 60000),
('sta-125-1', 'Robin', '123', '01922584693', 'robin@gmail.com', 'Male', '1992-12-12', '2016-05-03', '', 10000),
('sta-126-1', 'Tanjil  Ahmed', '123', '016392489298', 'tanjil@yahoo.com', 'Male', '0000-00-00', '2016-05-05', 'Dhaka', 600000);

-- --------------------------------------------------------

--
-- Table structure for table `students`
--

CREATE TABLE IF NOT EXISTS `students` (
  `id` varchar(20) NOT NULL,
  `name` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `phone` varchar(13) NOT NULL,
  `email` varchar(20) NOT NULL,
  `sex` varchar(7) NOT NULL,
  `dob` date NOT NULL,
  `addmissiondate` date NOT NULL,
  `address` varchar(50) NOT NULL,
  `parentid` varchar(20) NOT NULL,
  `classid` varchar(20) NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `students`
--

INSERT INTO `students` (`id`, `name`, `password`, `phone`, `email`, `sex`, `dob`, `addmissiondate`, `address`, `parentid`, `classid`) VALUES
('st-123-1', 'mokbul', '123', '01681000000', 'mokbul@example.com', 'male', '2009-06-24', '2016-01-01', 'unknown', 'pa-123-1', '1A'),
('st-124-1', 'rashid', '123', '018204679811', 'rashid@gmail.com', 'male', '1994-01-14', '2015-12-24', 'Dhaka', 'pa-123-1', '1A'),
('st-125-1', 'Barid Hossain', '123', '01824242525', 'barid@gmail.com', 'Male', '1987-02-05', '2016-05-05', 'Dhaka', 'pa-124-1', '5A');

-- --------------------------------------------------------

--
-- Table structure for table `takencoursebyteacher`
--

CREATE TABLE IF NOT EXISTS `takencoursebyteacher` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `courseid` varchar(20) NOT NULL,
  `teacherid` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM  DEFAULT CHARSET=utf8 AUTO_INCREMENT=23 ;

--
-- Dumping data for table `takencoursebyteacher`
--

INSERT INTO `takencoursebyteacher` (`id`, `courseid`, `teacherid`) VALUES
(1, '4', 'te-123-1'),
(2, '8', 'te-123-1'),
(3, '1', 'te-124-1'),
(4, '2', 'te-124-1'),
(5, '18', 'te-125-1'),
(6, '19', 'te-125-1'),
(7, '11', 'te-125-1'),
(8, '24', 'te-126-1'),
(9, '23', 'te-126-1'),
(10, '22', 'te-126-1'),
(11, '4', 'te-124-1'),
(12, '5', 'te-123-1'),
(13, '6', 'te-125-1'),
(14, '7', 'te-127-1'),
(15, '9', 'te-127-1'),
(16, '10', 'te-127-1'),
(17, '17', 'te-125-1'),
(18, '16', 'te-125-1'),
(19, '15', 'te-125-1'),
(20, '14', 'te-126-1'),
(21, '13', 'te-126-1'),
(22, '12', 'te-126-1');

-- --------------------------------------------------------

--
-- Table structure for table `teachers`
--

CREATE TABLE IF NOT EXISTS `teachers` (
  `id` varchar(20) NOT NULL,
  `name` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `phone` varchar(13) NOT NULL,
  `email` varchar(20) NOT NULL,
  `address` varchar(30) NOT NULL,
  `sex` varchar(7) NOT NULL,
  `dob` date NOT NULL,
  `hiredate` date NOT NULL,
  `salary` double NOT NULL,
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `teachers`
--

INSERT INTO `teachers` (`id`, `name`, `password`, `phone`, `email`, `address`, `sex`, `dob`, `hiredate`, `salary`) VALUES
('te-123-1', 'Mr.X', '123', '01711000000', 'mrx@example.com', 'unknown', 'male', '1988-12-23', '2016-01-01', 200000),
('te-124-1', 'Aziz Khan', '124', '01822376277', 'aziz@gmail,com', 'dhaka', 'male', '1975-12-18', '2001-05-04', 600000),
('te-125-1', 'Rifat', '258', '01630592385', 'rifat@gmail.com', 'Dhaka', 'Male', '1992-01-26', '2016-05-04', 200000),
('te-126-1', 'Dipta', '258', '01823568956', 'dipta@gmail.com', 'Savar', 'Male', '1994-01-26', '2016-05-04', 200000),
('te-127-1', 'Abu saleh', '123', '01765439871', 'abu.saleh@gmail.com', 'Dhaka', 'Male', '1985-02-05', '2016-05-05', 200000);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `userid` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `usertype` varchar(10) NOT NULL,
  UNIQUE KEY `userid` (`userid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`userid`, `password`, `usertype`) VALUES
('ad-123-0', '123', 'admin'),
('ad-123-1', '123', 'admin'),
('ad-123-2', '123', 'admin'),
('ad-123-3', '123', 'admin'),
('pa-123-1', '123', 'parent'),
('pa-124-1', '123', 'parent'),
('st-123-1', '123', 'student'),
('st-124-1', '125', 'student'),
('st-125-1', '123', 'student'),
('sta-123-1', '123', 'staff'),
('sta-124-1', '123', 'staff'),
('sta-125-1', '123', 'staff'),
('sta-126-1', '123', 'staff'),
('te-123-1', '123', 'teacher'),
('te-124-1', '124', 'teacher'),
('te-125-1', '258', 'teacher'),
('te-126-1', '258', 'teacher'),
('te-127-1', '123', 'teacher');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
