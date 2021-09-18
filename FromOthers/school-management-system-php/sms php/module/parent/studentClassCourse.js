function ajaxRequestToGetChild()
{
	var info = {
		childid: $('#childid').val()
		
	};

	$.ajax({
		url: 'mychildclass.php',
		data: info,
		success: function(response){
			$('#myclass').html(response);
			ajaxRequestToGetCourse();
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}


function ajaxRequestToGetCourse()
{
	var info = {
		classid: $('#myclass').val(),
		childid: $('#childid').val()
		
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
		id: $('#mycourse').val(),
		childid: $('#childid').val()
		
	};

	$.ajax({
		url: 'mycourseinfo.php',
		data: info,
		success: function(response){
			$('#mycourseinfo').html(response);
			ajaxRequestToGetCourseTeacher();
			ajaxRequestToGetCourseReportInfo();
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetCourseReportInfo()
{
	var info = {
		id: $('#mycourse').val(),
		childid: $('#childid').val()
		
	};

	$.ajax({
		url: 'mycoursereportinfo.php',
		data: info,
		success: function(response){
			$('#mycoursereportinfo').html(response);
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
		cid: $('#mycourse').val(),
		childid: $('#childid').val()
		
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
