function ajaxRequestToGetAttendanceTeacherPresentThisMonth()
{
	var info = {
		id: $('#teaid').val()
		
	};
	
	$.ajax({
		url: 'myattendanceteacherthismonth.php',
		data: info,
		success: function(response){
			$('#myteapresent').html(response);
			ajaxRequestToGetAttendanceTeacherAbsentThisMonth();
			ajaxRequestToGetAttendanceStaffPresentThisMonth();
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetAttendanceTeacherPresentAll()
{
	var info = {
		id: $('#teaid').val()
		
	};
	$.ajax({
		url: 'myattendanceteacherall.php',
		data: info,
		success: function(response){
			$('#myteapresent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}
function ajaxRequestToGetAttendanceTeacherAbsentThisMonth()
{
	var info = {
		id: $('#teaid').val()
		
	};
	$.ajax({
		url: 'myattendanceteacherabsentthismonth.php',
		data: info,
		success: function(response){
			$('#myteaabsent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetAttendanceTeacherAbsentAll()
{
	var info = {
		id: $('#teaid').val()
		
	};
	
	$.ajax({
		url: 'myattendanceteacherabsentall.php',
		data: info,
		success: function(response){
			$('#myteaabsent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetAttendanceStaffPresentThisMonth()
{
	var info = {
		id: $('#staffid').val()
		
	};
	
	$.ajax({
		url: 'myattendancestaffthismonth.php',
		data: info,
		success: function(response){
			$('#mystaffpresent').html(response);
			ajaxRequestToGetAttendanceStaffAbsentThisMonth();
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetAttendanceStaffPresentAll()
{
	var info = {
		id: $('#staffid').val()
		
	};
	$.ajax({
		url: 'myattendancestaffall.php',
		data: info,
		success: function(response){
			$('#mystaffpresent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}
function ajaxRequestToGetAttendanceStaffAbsentThisMonth()
{
	var info = {
		id: $('#staffid').val()
		
	};
	$.ajax({
		url: 'myattendancestaffabsentthismonth.php',
		data: info,
		success: function(response){
			$('#mystaffabsent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetAttendanceStaffAbsentAll()
{
	var info = {
		id: $('#staffid').val()
		
	};
	
	$.ajax({
		url: 'myattendancestaffabsentall.php',
		data: info,
		success: function(response){
			$('#mystaffabsent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}