<?php

	ob_start(); // Output Buffering Start

	session_start();

	if (isset($_SESSION['Admin_Username'])) {

		$pageTitle = 'Dashboard';

		include 'init.php';

		/* Start Dashboard Page */

		$numStudents = 6; // Number Of Latest Users

		$latestStudents = getLatest("*", "student", "student_id", $numStudents); // Latest Users Array

		$numCourses = 6; // Number Of Latest Items

		$latestCourses = getLatest("*", 'courses', 'course_id', $numCourses); // Latest Items Array

		$numComments = 4;

		?>

		<div class="home-stats">
			<div class="container text-center">
				<h1>Dashboard</h1>
				<div class="row">
					<div class="col-md-3">
						<div class="stat st-members">
							<i class="fa fa-users"></i>
							<div class="info">
								Total Students
								<span>
									<a href="students.php"><?php echo countItems('student_id', 'student') ?></a>
								</span>
							</div>
						</div>
					</div>
					<div class="col-md-3">
						<div class="stat st-pending">
							<i class="fa fa-user-plus"></i>
							<div class="info">
								Total Exams
								<span>
									<a href="exams.php"><?php echo countItems('courses_id', 'courses_has_exam') ?></a>
								</span>
							</div>
						</div>
					</div>
					<div class="col-md-3">
						<div class="stat st-items">
							<i class="fa fa-tag"></i>
							<div class="info">
								Total Courses
								<span>
									<a href="courses.php"><?php echo countItems('course_id', 'courses') ?></a>
								</span>
							</div>
						</div>
					</div>
					<div class="col-md-3">
						<div class="stat st-comments">
							<i class="fa fa-comments"></i>
							<div class="info">
								Total Questions
								<span>
									<a href="exams.php"><?php echo countItems('id', 'questions') ?></a>
								</span>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>

		<div class="latest">
			<div class="container">
				<div class="row">
					<div class="col-sm-6">
						<div class="panel panel-default">
							<div class="panel-heading">
								<i class="fa fa-users"></i> 
								Latest <?php echo $numStudents ?> Registerd Students 
								<span class="toggle-info pull-right">
									<i class="fa fa-plus fa-lg"></i>
								</span>
							</div>
							<div class="panel-body">
								<ul class="list-unstyled latest-users">
								<?php
									if (! empty($latestStudents)) {
										foreach ($latestStudents as $student) {
											echo '<li>';
												echo $student['fname'] . ' ' . $student['lname'];
												echo '<a href="students.php?do=Edit&stuID=' . $student['student_id'] . '">';
													echo '<span class="btn btn-success pull-right">';
														echo '<i class="fa fa-edit"></i> Edit';
														
													echo '</span>';
												echo '</a>';
											echo '</li>';
										}
									} else {
										echo 'There\'s No Members To Show';
									}
								?>
								</ul>
							</div>
						</div>
					</div>
					<div class="col-sm-6">
						<div class="panel panel-default">
							<div class="panel-heading">
								<i class="fa fa-tag"></i> Latest <?php echo $numCourses ?> Courses 
								<span class="toggle-info pull-right">
									<i class="fa fa-plus fa-lg"></i>
								</span>
							</div>
							<div class="panel-body">
								<ul class="list-unstyled latest-users">
									<?php
										if (! empty($latestCourses)) {
											foreach ($latestCourses as $course) {
												echo '<li>';
													echo $course['course_name'];
													echo '<a href="courses.php?do=Edit&courseID=' . $course['course_id'] . '">';
														echo '<span class="btn btn-success pull-right">';
															echo '<i class="fa fa-edit"></i> Edit';
															
														echo '</span>';
													echo '</a>';
												echo '</li>';
											}
										} else {
											echo 'There\'s No Courses To Show';
										}
									?>
								</ul>
							</div>
						</div>
					</div>
				</div>
				
			</div>
		</div>

		<?php

		/* End Dashboard Page */

		include $tpl.'footer.php' ;

	} else {

		header('Location: index.php');

		exit();
	}

	ob_end_flush(); // Release The Output

?>