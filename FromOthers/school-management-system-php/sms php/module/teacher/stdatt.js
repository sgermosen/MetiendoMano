
function ajaxRequestToGetAttendancePresentThisMonth()
{
      var info = {
		classid: $('#mystudent').val()
		
	};
	$.ajax({
		url: 'stattendancethismonth.php',
		data: info,
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
var info = {
		classid: $('#mystudent').val()
		
	};
	
	$.ajax({
		url: 'stattendanceall.php',
		data: info,
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
var info = {
		classid: $('#mystudent').val()
		
	};
	$.ajax({
		url: 'stattendanceabsentthismonth.php',
		data: info,
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
var info = {
		classid: $('#mystudent').val()
		
	};
	
	$.ajax({
		url: 'stattendanceabsentall.php',
		data: info,
		success: function(response){
			$('#myabsent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}