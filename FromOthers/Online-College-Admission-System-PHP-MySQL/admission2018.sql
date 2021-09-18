-- phpMyAdmin SQL Dump
-- version 4.7.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 08, 2017 at 07:56 PM
-- Server version: 10.1.25-MariaDB
-- PHP Version: 7.1.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `admission2018`
--

-- --------------------------------------------------------

--
-- Table structure for table `admins`
--

CREATE TABLE `admins` (
  `id` int(10) UNSIGNED NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(125) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL,
  `isActive` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `admins`
--

INSERT INTO `admins` (`id`, `name`, `email`, `password`, `created_at`, `updated_at`, `isActive`) VALUES
(1, 'Admin1', '16564017@nuv.ac.in', '1337x', NULL, NULL, NULL),
(2, 'yash karanke', 'll@ll.com', 'random', '0000-00-00 00:00:00', '0000-00-00 00:00:00', 0),
(3, 'New Admin', 'admin@admin.com', '1234', NULL, NULL, 1);

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `ID` int(2) NOT NULL,
  `coursename` varchar(20) DEFAULT NULL,
  `isActive` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`ID`, `coursename`, `isActive`) VALUES
(27, 'MAA', NULL),
(29, 'MCA', NULL),
(30, 'MBA', NULL),
(31, 'MCQ', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `education_information`
--

CREATE TABLE `education_information` (
  `E_ID` int(3) NOT NULL,
  `ID` int(3) NOT NULL,
  `ssc_board` varchar(70) NOT NULL,
  `ssc_school` varchar(70) NOT NULL,
  `ssc_per` varchar(3) NOT NULL,
  `ssc_year` varchar(5) NOT NULL,
  `ssc_attempt` varchar(7) NOT NULL,
  `hsc_board` varchar(70) NOT NULL,
  `hsc_school` varchar(70) NOT NULL,
  `hsc_per` varchar(3) NOT NULL,
  `hsc_year` varchar(5) NOT NULL,
  `hsc_attempt` varchar(7) NOT NULL,
  `grad_deg` varchar(70) DEFAULT NULL,
  `grad_board` varchar(70) DEFAULT NULL,
  `grad_school` varchar(70) DEFAULT NULL,
  `grad_per` varchar(3) DEFAULT NULL,
  `grad_year` varchar(5) DEFAULT NULL,
  `grad_attempt` varchar(7) DEFAULT NULL,
  `pgrad_deg` varchar(70) DEFAULT NULL,
  `pgrad_board` varchar(70) DEFAULT NULL,
  `pgrad_school` varchar(70) DEFAULT NULL,
  `pgrad_per` varchar(3) DEFAULT NULL,
  `pgrad_year` varchar(5) DEFAULT NULL,
  `pgrad_attempt` varchar(7) DEFAULT NULL,
  `isActive` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `education_information`
--

INSERT INTO `education_information` (`E_ID`, `ID`, `ssc_board`, `ssc_school`, `ssc_per`, `ssc_year`, `ssc_attempt`, `hsc_board`, `hsc_school`, `hsc_per`, `hsc_year`, `hsc_attempt`, `grad_deg`, `grad_board`, `grad_school`, `grad_per`, `grad_year`, `grad_attempt`, `pgrad_deg`, `pgrad_board`, `pgrad_school`, `pgrad_per`, `pgrad_year`, `pgrad_attempt`, `isActive`) VALUES
(1, 2, 'TEST DATA', 'TEST DATA', 'a', '2010', 'First', 'TEST DATA', 'TEST DATA', 'a', '2010', 'First', 'bca', 'TEST DATA', 'TEST DATA', 'a', '2010', 'First', '', '', '', '', '', 'First', 1),
(2, 4, 'Dummy Data', 'Dummy Data', 'A', '2012', 'First', 'Dummy Data', 'Dummy Data', 'A', '2012', 'First', '', '', '', '', '', 'Select ', '', '', '', '', '', 'Select ', 1),
(3, 5, '', '', '', '', 'Select ', '', '', '', '', 'Select ', '', '', '', '', '', 'Select ', '', '', '', '', '', 'Select ', 1),
(4, 18, 'fgdfgd', 'gdfgdfg', '55', '2013', 'Second', 'nbb', 'ghgjh', '56', '2014', 'First', '', 'gjhg', 'jhkjh', '56', '2014', 'Select ', '', '', '', '', '', 'Select ', 1);

-- --------------------------------------------------------

--
-- Table structure for table `education_information_be`
--

CREATE TABLE `education_information_be` (
  `BE_ID` int(11) NOT NULL,
  `ID` int(11) NOT NULL,
  `ssc_school` varchar(10) NOT NULL,
  `ssc_year` varchar(10) NOT NULL,
  `ssc_percentage` varchar(10) NOT NULL,
  `ssc_class` varchar(10) NOT NULL,
  `hsc_school` varchar(10) NOT NULL,
  `hsc_year` varchar(10) NOT NULL,
  `hsc_pcm` varchar(10) NOT NULL,
  `hsc_percentage` varchar(10) NOT NULL,
  `hsc_class` varchar(10) NOT NULL,
  `roll_no` varchar(10) NOT NULL,
  `physics` int(11) NOT NULL,
  `chemistry` int(11) NOT NULL,
  `maths` int(11) NOT NULL,
  `total` int(11) NOT NULL,
  `jee_main_rank` varchar(10) NOT NULL,
  `contact_01` varchar(10) NOT NULL,
  `contact_02` varchar(10) DEFAULT NULL,
  `acpc_no` varchar(10) DEFAULT NULL,
  `acpc_merit` varchar(10) DEFAULT NULL,
  `p1` varchar(10) NOT NULL,
  `p2` varchar(10) NOT NULL,
  `p3` varchar(10) NOT NULL,
  `p4` varchar(10) NOT NULL,
  `isAvailable` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `education_information_be`
--

INSERT INTO `education_information_be` (`BE_ID`, `ID`, `ssc_school`, `ssc_year`, `ssc_percentage`, `ssc_class`, `hsc_school`, `hsc_year`, `hsc_pcm`, `hsc_percentage`, `hsc_class`, `roll_no`, `physics`, `chemistry`, `maths`, `total`, `jee_main_rank`, `contact_01`, `contact_02`, `acpc_no`, `acpc_merit`, `p1`, `p2`, `p3`, `p4`, `isAvailable`) VALUES
(1, 1, 'acpcmeribe', '2016', '55', 'A', 'acpcmeribe', '201', '55', '55', 'A', 'ACPC1', 50, 50, 50, 150, 'JEE2', '9033655920', '', '', '', 'ME', 'EE', 'CSE', 'CE', 1);

-- --------------------------------------------------------

--
-- Table structure for table `selected_courses`
--

CREATE TABLE `selected_courses` (
  `S_ID` int(2) NOT NULL,
  `ID` int(3) NOT NULL,
  `coursename` varchar(20) NOT NULL,
  `isAvailable` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `selected_courses`
--

INSERT INTO `selected_courses` (`S_ID`, `ID`, `coursename`, `isAvailable`) VALUES
(1, 2, 'MHRM', 1),
(2, 0, 'BCA', 1),
(3, 0, 'BCA', 1),
(4, 2, 'MMM', 1),
(5, 2, 'MscIT', 1),
(6, 2, 'MscIT', 1),
(7, 4, 'MHRM', 1),
(8, 1, 'MBA', 1),
(9, 1, 'MBA', 1),
(10, 1, 'MBA', 1),
(11, 1, 'MBA', 1),
(12, 1, 'MBA', 1),
(13, 1, 'BBA', 1),
(14, 1, 'BCA', 1),
(15, 1, 'MHRM', 1),
(16, 1, 'MXA', 1),
(17, 1, 'NEW', 1),
(18, 1, 'MMM', 1),
(19, 15, 'MAA', 1),
(20, 12, 'MAA', 1),
(21, 12, 'BBA', 1),
(22, 18, 'BBA', 1),
(23, 18, 'MAA', 1),
(24, 18, 'TTT', 1),
(25, 1, 'B.E', 1),
(26, 2, 'MBA', 1),
(27, 12, 'MBA', 1),
(28, 12, 'MCA', 1);

-- --------------------------------------------------------

--
-- Table structure for table `student_data`
--

CREATE TABLE `student_data` (
  `ID` int(3) NOT NULL,
  `FULLNAME` varchar(70) DEFAULT NULL,
  `GENDER` varchar(8) DEFAULT NULL,
  `BGROUP` varchar(10) DEFAULT NULL,
  `ADDRESS` varchar(100) DEFAULT NULL,
  `CITY` varchar(20) DEFAULT NULL,
  `STATE` varchar(50) DEFAULT NULL,
  `ZIP` varchar(10) DEFAULT NULL,
  `PNUMBER` varchar(11) DEFAULT NULL,
  `EMAIL` varchar(60) DEFAULT NULL,
  `PASSWORD` varchar(100) DEFAULT NULL,
  `register_date` date DEFAULT NULL,
  `dob` varchar(10) DEFAULT NULL,
  `profile_image` varchar(100) DEFAULT NULL,
  `isActive` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `student_data`
--

INSERT INTO `student_data` (`ID`, `FULLNAME`, `GENDER`, `BGROUP`, `ADDRESS`, `CITY`, `STATE`, `ZIP`, `PNUMBER`, `EMAIL`, `PASSWORD`, `register_date`, `dob`, `profile_image`, `isActive`) VALUES
(1, 'YASH KARANKE', 'MALE', 'B+VE', 'VADODARA							', 'VADODARA', 'GUJARAT', '390001', '9714574465', 'dex.papa@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', NULL, NULL, '', NULL),
(2, 'YASH KARANKE', 'MALE', 'B+VE', 'VADODARA							', 'VADAODARA', 'GJ', '390001', '9714574465', 'dex@dex.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-08', NULL, '', NULL),
(3, 'Shashi Karanke', 'Male', 'B+VE', 'Vadodara							', 'Vadodara', 'Vadodara', '390007', '9998290920', 'shashi.karanke@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-12', '20-08-1997', '', NULL),
(4, 'Dummy Man', 'Male', 'B+VE', 'VADODARA', 'VADODARA', 'VADODARA', '390011', '9714574665', 'dex@google.om', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-15', '01-10-1997', '', 1),
(5, 'Shashi Karanke', 'Male', 'b+ve', 'hogwards', 'Vadodara', 'Gujarat', '390015', '8154036295', '15102009@nuv.ac.in', '8af76326814e1566929ad87a80435880', '2017-10-15', '19-08-1997', '', 1),
(6, 'Yash Karanke', 'Male', 'B+VE', 'Vadodara', 'Vadodara', 'Gujarat', '390001', '9714574465', 'dex@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '20-12-1995', '', 1),
(7, 'Shane Paryan', 'Male', 'B+E', 'Vadodara', 'Vadodara', 'Gujarat', '390001', '9715744655', 'shane@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '15-10-1981', 'Shane.png', 1),
(8, 'Shane Paryan', 'Male', 'B+VE', 'VADODARA', 'VADODARA', 'Gujarat', '390001', '9714574665', 'shane.p@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '15-10-1997', 'L', 1),
(9, 'Shane Paryan', 'Male', 'B+VE', 'Vadodara', 'Vadodara', 'Gujarat', '39001', '9714574465', 'sha@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '05-10-1999', '', 1),
(10, 'Shane Paryan', 'Male', 'B+VE', 'VADODARA', 'VADODARA', 'VADODARA', '390001', '9714574465', 'sha2@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '07-10-1981', 'D:xampp	mpphp9972.tmp', 1),
(11, 'Shane', 'Male', 'B+VE', 'VADODARA', 'VADODARA', 'VADODARA', '3900011', '9714574465', 'sss@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '11-10-1995', '', 1),
(12, 'Shane', 'Male', 'B+VE', 'VADODARA', 'VADODARA', 'Gujarat', '390001', '9714574465', '999@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '09-10-1996', 'D:xampp	mpphpFE61.tmp', 1),
(13, 'Shane', 'Male', 'B+VE', 'VADODARA', 'VADODARA', 'VADODARA', '390001', '9714574465', 'dexxx@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '05-03-1986', '/profile_imagesShane.png', 1),
(14, 'Shane', 'Male', 'B+VE', 'Vadodara', 'Vadodara', 'Gujarat', '3900011', '9714574465', 'de@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '20-12-1992', 'profile_images/Shane.png', 1),
(15, 'Shane', 'Male', 'B+VE', 'VADODARA', 'vadodara', 'Gujarat', '3900011', '9714574465', 'dee@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-16', '20-12-1995', 'profile_images/Shane.png', 1),
(16, 'Shane', 'Male', 'B+VE', 'VADODARA', 'Vadodara', 'Gujarat', '390001', '9714574465', 'aa@gmail.com', '81dc9bdb52d04dc20036dbd8313ed055', '2017-10-22', '12-10-2017', 'profile_images/Shane.png', 1),
(17, 'Yash Karanke', 'Male', '', 'VADODARA', 'VADODARA', 'VADODARA', '390001', '9714574465', 'a@a.com', '74b87337454200d4d33f80c4663dc5e5', '2017-10-25', '11-10-2017', 'profile_images/Shane.png', 1),
(18, 'Sagar Soni', 'Male', 'A+VE', 'Padra', 'bARODA', 'gUJARAT', '391410', '9033186403', 'SAGAR@gmail.com', 'b4078c14fbcb7b3ef69a5f915a753d5b', '2017-10-31', '06-10-1994', 'profile_images/P7A.png', 1),
(19, 'Yash Karanke', 'Male', 'A+VE', 'Vadodara', 'Vadodara', 'Vadodara', '390001', '9714574465', 'Vadodara@gmail.com', '2395404a9438f1483548ab1179671f95', '2017-11-07', '08-11-2017', 'profile_images/minimalistic-dc-comics-joker-pictures-for-desktop.jpg', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admins`
--
ALTER TABLE `admins`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `admins_email_unique` (`email`);

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `coursename` (`coursename`);

--
-- Indexes for table `education_information`
--
ALTER TABLE `education_information`
  ADD PRIMARY KEY (`E_ID`);

--
-- Indexes for table `education_information_be`
--
ALTER TABLE `education_information_be`
  ADD PRIMARY KEY (`BE_ID`);

--
-- Indexes for table `selected_courses`
--
ALTER TABLE `selected_courses`
  ADD PRIMARY KEY (`S_ID`),
  ADD KEY `bba_index_coursename` (`coursename`);

--
-- Indexes for table `student_data`
--
ALTER TABLE `student_data`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `EMAIL` (`EMAIL`),
  ADD KEY `stud_name_index` (`FULLNAME`),
  ADD KEY `gender_stud` (`GENDER`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admins`
--
ALTER TABLE `admins`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `ID` int(2) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;
--
-- AUTO_INCREMENT for table `education_information`
--
ALTER TABLE `education_information`
  MODIFY `E_ID` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
--
-- AUTO_INCREMENT for table `education_information_be`
--
ALTER TABLE `education_information_be`
  MODIFY `BE_ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- AUTO_INCREMENT for table `selected_courses`
--
ALTER TABLE `selected_courses`
  MODIFY `S_ID` int(2) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;
--
-- AUTO_INCREMENT for table `student_data`
--
ALTER TABLE `student_data`
  MODIFY `ID` int(3) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
