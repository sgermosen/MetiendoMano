$(document).ready(function () {

    $('#RoleId').on('change', function () {
        $.ajax({
            type: 'GET',
            url: $('#actionUrl').val(),
            data: { role_id: $('#RoleId option:selected').val() },
            success: function (data) {
                $("#partial").html("");
                $(data).appendTo('#partial');
                $('.accordion').accordion({ heightStyle: 'content', collapsible: true, active : false });
                $('.accordion').accordion("refresh");
                $('.acc').accordion({collapsible : true, active : false});
                $('.acc').accordion("refresh");
            }
        });
    });

    if ($('#RoleId option').length > 0) {
        $('#RoleId').trigger("change");
    }
});