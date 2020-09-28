$.validator.setDefaults({
    ignore: '.ignore',
    onkeyup: false,
    //onfocusout: false,
    onsubmit: true
});

$(".confirmateComposite").focusout(function () {
    $(".validateComposite").removeData("previousValue");
});

$(document).ready(function () {
    $(document).tooltip({
        position: {
            my: "center bottom-20",
            at: "center top",
            using: function (position, feedback) {
                $(this).css(position);
                $("<div>")
                  .addClass("arrow")
                  .addClass(feedback.vertical)
                  .addClass(feedback.horizontal)
                  .appendTo(this);
            }
        }
    });

    $('.fieldInput').css("position", "absolute").css("left", "-9999px");

    $('.eraseInput').on("click", function (event) {
        event.preventDefault();
        resetInput($(".fieldInput"));
        $(".falseInputFile").val("");
        $(this).hide();
    });

    $('.falseInputFile').val($('.hiddenName').val());
    if ($('.falseInputFile').val() == '') {
        $('.eraseInput').hide();
    }

    $('.btSubmit').on("click", function (event) {
        if ($('.hiddenName').val() &&
            $('.hiddenName').val() != $('.falseInputFile').val()) {
            $('.fileChange').dialog("open");
            event.preventDefault();
        }
        else {
            $('.standard_form').submit();
        }
    });
    $('.fieldInput').on("change", function () {
        if ($(this).val() != '' || $('.falseInputFile').val() != '') {
            $('.eraseInput').show();
        }
    });
});

window.resetInput = function (e) {
    e.wrap('<form>').closest('form').get(0).reset();
    e.unwrap();
}

$(document).on('click', '.filetrigger', function () {
    inputfile = $(this).prevAll('input:file');

    $(inputfile).trigger('click');

    $(inputfile).change(function () {
        $('#filename').val($(this).val());
    });
});

$('.fileChange').dialog({
    autoOpen: false,
    width: 540,
    height: 200,
    resizable: false,
    modal: true,
    buttons: {
        "Guardar": function () {
            $(this).dialog("close");
            $('.standard_form').submit();
        },
        "Cancel": function () {
            $(this).dialog("close");
        }
    }
});

$('.nestedDropDown').change(function () {
    alert($("#" + this.Attr("id") + " :selected").val());
    alert($(this).data.childDropdownID);
});

var oLanguage = {
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
};

jQuery.fn.dataTableExt.oApi.fnSetFilteringDelay = function (oSettings, iDelay) { 
    var
_that = this,
iDelay = (typeof iDelay == 'undefined') ? 250 : iDelay;

    this.each(function (i) {
        $.fn.dataTableExt.iApiIndex = i;
        var
    $this = this,
    oTimerId = null,
    sPreviousSearch = null,
    anControl = $('input', _that.fnSettings().aanFeatures.f);

        anControl.unbind('keyup').bind('keyup', function () {
            var $$this = $this;

            if (sPreviousSearch === null || sPreviousSearch != anControl.val()) {
                window.clearTimeout(oTimerId);
                sPreviousSearch = anControl.val();
                oTimerId = window.setTimeout(function () {
                    $.fn.dataTableExt.iApiIndex = i;
                    _that.fnFilter(anControl.val());
                }, iDelay);
            }
        });

        return this;
    });
    return this;
}

function RefreshTable(tableId, urlData, dataValEdit, dataUrlEdit, dataValDelete, dataUrlDelete) {
    // Retrieve the new data with $.getJSON. You could use it ajax too
    $.getJSON(urlData, null, function (json) {
        table = $(tableId).dataTable();
        oSettings = table.fnSettings();

        table.fnClearTable(this);
        for (var i = 0; i < json.aaData.length; i++) {
            var links =
                '<a href="javascript:void(0);" class="edit_link button_link editAction" data-val="' + dataValEdit + json.aaData[i][2] + '" data-url="' + dataUrlEdit + '" >Editar</a>' +
                '<a href="javascript:void(0);" class="delete_link button_link deleteAction" data-val="' + dataValDelete + json.aaData[i][2] + '" data-url="' + dataUrlDelete + '" >Eliminar</a>';
            json.aaData[i][2] = links;
            table.oApi._fnAddData(oSettings, json.aaData[i]);
        }

        oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();
        table.fnDraw();

        $('.editAction').on('click', function (evt) {
            evt.preventDefault();
            $('#actionModalUrl').val($(this).data('url'));
            $.get(
                $(this).data('val'),
                function (data) {
                    $('#modalPanel').html(data);
                    $('#modalPanel').dialog("open");
                }
            );

        });

        $('.deleteAction').on('click', function (evt) {
            evt.preventDefault();
            if (confirm('¿Esta seguro de eliminar este recurso?')) {
                $.post(
                    $(this).data('val'),
                    function (data) {
                        RefreshTable(".partialTable", $('#partialUrl').val(), dataValEdit, dataUrlEdit, dataValDelete, dataUrlDelete);
                        alert(data);
                    }
                );
            }
        });
    });
}

function printObject(o) {
    var out = '';
    for (var p in o) {
        out += p + ': ' + o[p] + '\n';
    }
    alert(out);
}

function fnShowHide(datatableID,iCol,visible) {
    datatableID.fnSetColumnVis(iCol, visible);
}

/*** Function to parse dynamic loaded content with unobstrusive jQuery validation ***/
function fnValidateDynamicContent(element) {
    var currForm = element.closest("form");
    $('.standard_form')
    $('.standard_form').removeData("validator");
    $('.standard_form').removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(currForm);
    // This line is important and added for client side validation to trigger, without this it didn't fire client side errors.
    $('.standard_form').validate();
}