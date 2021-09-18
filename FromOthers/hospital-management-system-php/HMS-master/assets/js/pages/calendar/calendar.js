"use strict";
$('#calendar').fullCalendar({
	header: {
		left: 'prev',
		center: 'title',
		right: 'next'
	},
	defaultDate: '2015-02-12',
	editable: true,
	droppable: true, // this allows things to be dropped onto the calendar
	drop: function() {
		// is the "remove after drop" checkbox checked?
		if ($('#drop-remove').is(':checked')) {
			// if so, remove the element from the "Draggable Events" list
			$(this).remove();
		}
	},
	eventLimit: true, // allow "more" link when too many events
	events: [
		{
			title: 'All Day Event',
			start: '2015-02-01',
			className: 'b-l b-2x b-greensea'
		},
		{
			title: 'Long Event',
			start: '2015-02-07',
			end: '2015-02-10',
			className: 'bg-cyan'
		},
		{
			id: 999,
			title: 'Repeating Event',
			start: '2015-02-09T16:00:00',
			className: 'b-l b-2x b-lightred'
		},
		{
			id: 999,
			title: 'Repeating Event',
			start: '2015-02-16T16:00:00',
			className: 'b-l b-2x b-success'
		},
		{
			title: 'Conference',
			start: '2015-02-11',
			end: '2015-02-13',
			className: 'b-l b-2x b-primary'
		},
		{
			title: 'Meeting',
			start: '2015-02-12T10:30:00',
			end: '2015-02-12T12:30:00',
			className: 'b-l b-2x b-amethyst'
		},
		{
			title: 'Lunch',
			start: '2015-02-12T12:00:00',
			className: 'b-l b-2x b-primary'
		},
		{
			title: 'Meeting',
			start: '2015-02-12T14:30:00',
			className: 'b-l b-2x b-drank'
		},
		{
			title: 'Happy Hour',
			start: '2015-02-12T17:30:00',
			className: 'b-l b-2x b-lightred'
		},
		{
			title: 'Dinner',
			start: '2015-02-12T20:00:00',
			className: 'b-l b-2x b-amethyst'
		},
		{
			title: 'Birthday Party',
			start: '2015-02-13T07:00:00',
			className: 'b-l b-2x b-primary'
		},
		{
			title: 'Click for Google',
			url: 'http://google.com/',
			start: '2015-02-28',
			className: 'b-l b-2x b-greensea'
		}
	]
});

// Hide default header
//$('.fc-header').hide();



// Previous month action
$('#cal-prev').click(function(){
	$('#calendar').fullCalendar( 'prev' );
});

// Next month action
$('#cal-next').click(function(){
	$('#calendar').fullCalendar( 'next' );
});

// Change to month view
$('#change-view-month').click(function(){
	$('#calendar').fullCalendar('changeView', 'month');

	// safari fix
	$('#content .main').fadeOut(0, function() {
		setTimeout( function() {
			$('#content .main').css({'display':'table'});
		}, 0);
	});

});

// Change to week view
$('#change-view-week').click(function(){
	$('#calendar').fullCalendar( 'changeView', 'agendaWeek');

	// safari fix
	$('#content .main').fadeOut(0, function() {
		setTimeout( function() {
			$('#content .main').css({'display':'table'});
		}, 0);
	});

});

// Change to day view
$('#change-view-day').click(function(){
	$('#calendar').fullCalendar( 'changeView','agendaDay');

	// safari fix
	$('#content .main').fadeOut(0, function() {
		setTimeout( function() {
			$('#content .main').css({'display':'table'});
		}, 0);
	});

});

// Change to today view
$('#change-view-today').click(function(){
	$('#calendar').fullCalendar('today');
});

/* initialize the external events
 -----------------------------------------------------------------*/
$('#external-events .event-control').each(function() {

	// store data so the calendar knows to render an event upon drop
	$(this).data('event', {
		title: $.trim($(this).text()), // use the element's text as the event title
		stick: true // maintain when user navigates (see docs on the renderEvent method)
	});

	// make the event draggable using jQuery UI
	$(this).draggable({
		zIndex: 999,
		revert: true,      // will cause the event to go back to its
		revertDuration: 0  //  original position after the drag
	});

});

$('#external-events .event-control .event-remove').on('click', function(){
	$(this).parent().remove();
});

// Submitting new event form
$('#add-event').submit(function(e){
	e.preventDefault();
	var form = $(this);

	var newEvent = $('<div class="event-control p-10 mb-10">'+$('#event-title').val() +'<a class="pull-right text-muted event-remove"><i class="fa fa-trash-o"></i></a></div>');

	$('#external-events .event-control:last').after(newEvent);

	$('#external-events .event-control').each(function() {

		// store data so the calendar knows to render an event upon drop
		$(this).data('event', {
			title: $.trim($(this).text()), // use the element's text as the event title
			stick: true // maintain when user navigates (see docs on the renderEvent method)
		});

		// make the event draggable using jQuery UI
		$(this).draggable({
			zIndex: 999,
			revert: true,      // will cause the event to go back to its
			revertDuration: 0  //  original position after the drag
		});

	});

	$('#external-events .event-control .event-remove').on('click', function(){
		$(this).parent().remove();
	});

	form[0].reset();

	$('#cal-new-event').modal('hide');

});
