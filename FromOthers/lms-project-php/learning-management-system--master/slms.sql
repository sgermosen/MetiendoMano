

CREATE TABLE `category` (
  `id` int(5) NOT NULL,
  `name` varchar(75) NOT NULL
);



CREATE TABLE `comments` (
  `id` int(11) NOT NULL,
  `uid` int(11) NOT NULL,
  `mid` varchar(50) NOT NULL,
  `body` longtext NOT NULL,
  `created_at` timestamp NOT NULL,
  `updated_at` timestamp NOT NULL 
);

-- --------------------------------------------------------

--
-- Table structure for table `course`
--

CREATE TABLE `course` (
  `id` int(10) NOT NULL,
  `cid` int(5) NOT NULL,
  `image` varchar(255) NOT NULL,
  `start` timestamp NULL DEFAULT NULL,
  `end` timestamp NULL DEFAULT NULL,
  `title` varchar(255) NOT NULL,
  `description` text NOT NULL,
  `rate` int(5) NOT NULL,
  `created_at` timestamp NOT NULL,
  `updated_at` timestamp NOT NULL 
);

-- --------------------------------------------------------

--
-- Table structure for table `material`
--

CREATE TABLE `material` (
  `id` int(10) NOT NULL,
  `cid` int(10) NOT NULL,
  `downloaded` int(11) NOT NULL DEFAULT '0',
  `link` varchar(255) NOT NULL,
  `status` varchar(50) NOT NULL DEFAULT 'show',
  `name` varchar(255) NOT NULL,
  `type` enum('pdf','ppt','doc','video') NOT NULL,
  `description` text NOT NULL,
  `created_at` timestamp NOT NULL ,
  `updated_at` timestamp NOT NULL 
);

-- --------------------------------------------------------

--
-- Table structure for table `requests`
--

CREATE TABLE `requests` (
  `id` int(11) NOT NULL,
  `uid` int(11) NOT NULL,
  `title` varchar(255) DEFAULT NULL,
  `body` longtext,
  `created_at` timestamp NOT NULL ,
  `updated_at` timestamp NOT NULL 
);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(10) NOT NULL,
  `username` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `firstname` varchar(255) DEFAULT NULL,
  `image` varchar(255) DEFAULT NULL,
  `lastname` varchar(255) DEFAULT NULL,
  `gender` enum('male','female') DEFAULT NULL,
  `state` varchar(75) DEFAULT NULL,
  `country` varchar(75) DEFAULT NULL,
  `online` tinyint(4) DEFAULT NULL,
  `isbaned` tinyint(4) DEFAULT NULL,
  `role` enum('student','teacher','admin') DEFAULT NULL,
  `code` varchar(50) DEFAULT NULL,
  `created_at` timestamp NOT NULL,
  `updated_at` timestamp NOT NULL
);


ALTER TABLE `category`
  ADD PRIMARY KEY (`id`),
  ADD KEY `name` (`name`);


ALTER TABLE `comments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_comments_1_idx` (`uid`);


ALTER TABLE `course`
  ADD PRIMARY KEY (`id`),
  ADD KEY `cat_id` (`cid`),
  ADD KEY `cat_id_2` (`cid`),
  ADD KEY `cat_id_3` (`cid`);


ALTER TABLE `material`
  ADD PRIMARY KEY (`id`),
  ADD KEY `course_id` (`cid`),
  ADD KEY `course_id_2` (`cid`);


ALTER TABLE `requests`
  ADD PRIMARY KEY (`id`),
  ADD KEY `fk_requests_1_idx` (`uid`);


ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);


ALTER TABLE `category`
  MODIFY `id` int(5) NOT NULL AUTO_INCREMENT;

ALTER TABLE `comments`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `course`
--
ALTER TABLE `course`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `material`
--
ALTER TABLE `material`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `requests`
--
ALTER TABLE `requests`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `comments`
--
ALTER TABLE `comments`
  ADD CONSTRAINT `fk_comments_1` FOREIGN KEY (`uid`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Constraints for table `course`
--
ALTER TABLE `course`
  ADD CONSTRAINT `course_ibfk_1` FOREIGN KEY (`cid`) REFERENCES `category` (`id`);

--
-- Constraints for table `material`
--
ALTER TABLE `material`
  ADD CONSTRAINT `material_ibfk_1` FOREIGN KEY (`cid`) REFERENCES `course` (`id`);

--
-- Constraints for table `requests`
--
ALTER TABLE `requests`
  ADD CONSTRAINT `fk_requests_1` FOREIGN KEY (`uid`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

