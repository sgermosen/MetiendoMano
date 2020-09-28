$(document).ready(function () {
    $('#DataTable').dataTable({
        "bJQueryUI": true,
        "bServerSide": true,
        "sAjaxSource": $('#serverSideProcessing').val(),
        "bProcessing": true,
        "aoColumns": [
                        {
                        //    "sName": "ID",
                        //    "bSearchable": false,
                        //    "bSortable": false,
                        //    "fnRender": function (oObj) {
                        //        return '<a href=\"Details/' +
                        //        oObj.aData[0] + '\">View</a>';
                        //    }
                            //},{ 
                        "sName": "code"},
                        { "sName": "name" }
        ]
    });
});