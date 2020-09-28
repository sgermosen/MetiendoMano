//$(document).ready(function () {
//    $('#dataTable').dataTable({ "bJQueryUI": true, "bFilter": false, "bInfo": false, "bLengthChange": false }).rowReordering({ sURL: $('#updateUrl').val() });
//});



$(document).ready(function () {
    $('.dndTable').dataTable({
        "oLanguage": {
            "sLengthMenu": "Mostrar _MENU_ registros por páginas",
            "sZeroRecords": "No hay registros entontrados.",
            "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Monstrando 0 a 0 de 0 records",
            "sInfoFiltered": "(filtrado desde _MAX_ total registros)",
            "sSearch": "Buscar",
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        }
        
    }).rowReordering({ sURL: $('#updateUrl').val() });
    $('#dataTableHome').dataTable({
        "oLanguage": {
            "sLengthMenu": "Mostrar _MENU_ registros por páginas",
            "sZeroRecords": "No hay registros entontrados.",
            "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Monstrando 0 a 0 de 0 records",
            "sInfoFiltered": "(filtrado desde _MAX_ total registros)",
            "sSearch": "Buscar",
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        }
    });
    $('.dataTable2').dataTable({"bFilter": false, "bInfo": false, "bLengthChange": false });
    $('.normalTable').dataTable({
        "oLanguage": {
            "sLengthMenu": "Mostrar _MENU_ registros por páginas",
            "sZeroRecords": "No hay registros entontrados.",
            "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Monstrando 0 a 0 de 0 records",
            "sInfoFiltered": "(filtrado desde _MAX_ total registros)",
            "sSearch": "Buscar",
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        }
    });

    $('.partialTable').dataTable({
        "oLanguage": {
            "sLengthMenu": "Mostrar _MENU_ registros por páginas",
            "sZeroRecords": "No hay registros entontrados.",
            "sInfo": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Monstrando 0 a 0 de 0 records",
            "sInfoFiltered": "(filtrado desde _MAX_ total registros)",
            "sSearch": "Buscar",
            "oPaginate": {
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            }
        },
        bProcessing: true
    });

});

