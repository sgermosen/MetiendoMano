function ajaxRequestToGetAttendancePresentThisMonth()
{
	var info = {
		id: $('#childid').val()
		
	};

	$.ajax({
		url: 'myattendancethismonth.php',
		data: info,
		success: function(response){
			$('#mypresent').html(response);
			ajaxRequestToGetAttendanceAbsentThisMonth();
		}
		
	});
}

function ajaxRequestToGetAttendancePresentAll()
{
	var info = {
		id: $('#childid').val()
		
	};
	
	$.ajax({
		url: 'myattendanceall.php',
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
		id: $('#childid').val(),
		
	};
	
	$.ajax({
		url: 'myattendanceabsentthismonth.php',
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
		id: $('#childid').val(),
		
	};
	
	$.ajax({
		url: 'myattendanceabsentall.php',
		data: info,
		success: function(response){
			$('#myabsent').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}



function ajaxRequestToGetChildInfo()
{
	var info = {
		id: $('#childid').val()
		
	};
	
	$.ajax({
		url: 'mychildinfo.php',
		data: info,
		success: function(response){
			$('#mychild').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}

function ajaxRequestToGetChildPaymentInfo()
{
	var info = {
		id: $('#childid').val()
		
	};
	
	$.ajax({
		url: 'mychildpaymentinfo.php',
		data: info,
		success: function(response){
			$('#mychildpayment').html(response);
			
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
         alert(errorThrown);
    }
	});
}