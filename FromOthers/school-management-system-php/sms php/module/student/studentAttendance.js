function ajaxRequestToGetAttendancePresentThisMonth()
{
	$.ajax({
		url: 'myattendancethismonth.php',
		success: function(response){
			$('#mypresent').html(response);
			ajaxRequestToGetAttendanceAbsentThisMonth();
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetAttendancePresentAll()
{
	
	$.ajax({
		url: 'myattendanceall.php',
		success: function(response){
			$('#mypresent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}
function ajaxRequestToGetAttendanceAbsentThisMonth()
{
	$.ajax({
		url: 'myattendanceabsentthismonth.php',
		success: function(response){
			$('#myabsent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetAttendanceAbsentAll()
{
	
	$.ajax({
		url: 'myattendanceabsentall.php',
		success: function(response){
			$('#myabsent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}