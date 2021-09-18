function ajaxRequestToGetMyCourse()
{
	var info = {
		classid: $('#myclass').val()
		
	};

	$.ajax({
		url: 'mycourse.php',
		data: info,
		success: function(response){
			$('#mycourse').html(response);
			ajaxRequestToGetCourseStudent();
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}
function ajaxRequestToGetMyC()
{
	var info = {
		classid: $('#myclass').val()
		
	};

	$.ajax({
		url: 'myc.php',
		data: info,
		success: function(response){
			$('#mycourse').html(response);
			ajaxRequestToGetCourseStudent();
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}


function ajaxRequestToGetCourseStudent()
{
	var info = {
		id: $('#mycourse').val()
		
		
	};

	$.ajax({
		url: 'mystudent.php',
		data: info,
		success: function(response){
			$('#mystudent').html(response);
			ajaxRequestToGetCourseTeacher();
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}
function ajaxRequestToGetCourseTeacher()
{
	var info = {
		cid: $('#mycourse').val()
		
	};

	$.ajax({
		url: 'mycourseteacher.php',
		data: info,
		success: function(response){
			$('#mycourseteacher').html(response);
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}
function ajaxRequestToGetCourseCurrentExamSchedule()
{
	var info = {
		curid: $('#curcourse').val()
		
	};

	$.ajax({
		url: 'getcurrentexamschedule.php',
		data: info,
		success: function(response){
			$('#mycourseexamschedule').html(response);
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}