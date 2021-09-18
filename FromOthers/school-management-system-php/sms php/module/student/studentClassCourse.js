function ajaxRequestToGetCourse()
{
	var info = {
		classname: $('#myclass').val()
		
	};

	$.ajax({
		url: 'mycourse.php',
		data: info,
		success: function(response){
			$('#mycourse').html(response);
			ajaxRequestToGetCourseInfo();
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetCourseInfo()
{
	var info = {
		id: $('#mycourse').val()
		
	};

	$.ajax({
		url: 'mycourseinfo.php',
		data: info,
		success: function(response){
			$('#mycourseinfo').html(response);
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