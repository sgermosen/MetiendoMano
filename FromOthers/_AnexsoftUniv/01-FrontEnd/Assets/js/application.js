/* By Default */
$(document).ready(function () {
    // Vue Initialize
    window.$Vm = new Vue({
        el: '#app',
        components: Components
    })

    // Get current credits for current user
    App.helpers.user.getCredits();

    // Bootstrap ToolTips
    $('[data-toggle="tooltip"]').tooltip();

    // Ajax Form
    $("body").on('click', 'button', function () {
        // Si el boton no tiene el atributo ajax no hacemos nada
        if ($(this).data('ajax') === undefined)
            return;

        // El metodo .data identifica la entrada y la castea al valor más correcto
        if ($(this).data('ajax') !== true)
            return;

        var form = $(this).closest("form"),
            button = $(this),
            url = form.attr('action');

        if (button.data('confirm') !== undefined) {
            if (button.data('confirm') === '') {
                if (!confirm('¿Esta seguro de realizar esta acción?'))
                    return false;
            } else {
                if (!confirm(button.data('confirm')))
                    return false;
            }
        }

        if (button.data('delete') !== undefined) {
            if (button.data('delete') === true) {
                url = button.data('url');
            }
        }

        // Creamos un div que bloqueara todo el formulario
        var block = $('<div class="block-loading" />');
        form.prepend(block);

        // Alert container
        var alertContainer = form.find('.alert-container');
        alertContainer.html('');

        // Escondomes los errores
        form.find(".form-validation-failed").html('');

        form.ajaxSubmit({
            dataType: 'JSON',
            type: 'POST',
            url: url,
            success: function (r) {
                block.remove();

                if (r.Response) {
                    if (!button.data('reset') !== undefined) {
                        if (button.data('reset'))
                            form.reset();
                    } else {
                        form.find('input:file').val('');
                    }
                }

                // Mostrar mensaje
                if (r.Message !== null) {
                    if (r.Message.length > 0) {
                        var css = "";
                        if (r.Response) {
                            css = "alert-success";
                        } else {
                            css = "alert-danger";
                        }

                        var message = '<div class="alert ' + css + ' alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + r.Message + '</div>';

                        if (alertContainer.length > 0) {
                            alertContainer.html(message);
                        } else {
                            form.prepend(message);
                        }
                    }
                }

                // Validaciones
                if (r.Validations !== null) {
                    for (var k in r.Validations) {
                        var vmessage = r.Validations[k];
                        form.find("[data-key='" + k + "']").text(vmessage);
                    }
                }

                // Redireccionar
                if (r.Href !== null) {
                    if (r.Href === 'self') window.location.reload(true);
                    else App.helpers.url.redirect(r.Href);
                }

                // Si el servidor retorno algo
                if (button.data('endrequest') !== undefined) {
                    var resultFunction = button.data('endrequest') + '({0})';
                    resultFunction = resultFunction.format(JSON.stringify(r));
                    setTimeout(resultFunction, 0);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var message = '<div class="alert alert-warning alert-dismissable response-message"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + errorThrown + ' | <b>' + textStatus + '</b></div>';

                if (alertContainer.length > 0) {
                    alertContainer.html(message);
                }

                block.remove();
            }
        });

        return false;
    })
})

jQuery.fn.reset = function () {
    $("input", $(this)).each(function () {
        var type = $(this).attr('type');

        if (type === 'checkbox' && $(this).is('checked')) {
            $(this).click();
        } else {
            $(this).val('');
        }
    })

    $("select", $(this)).val(0);
};

/* Extensions */
if (!String.prototype.truncate) {
    String.prototype.truncate = function (max) {
        let value = this;
        value = value.trim();

        max = max || 30;

        if (value.length > max) return value.substring(0, max);
        else return value;
    }
}

if (!String.prototype.format) {
    String.prototype.format = function () {
        let text = this;
        for (let i = 0; i < arguments.length; i++) {
            text = text.replace("{" + i + "}", arguments[i]);
        }
        return text;
    }
}

if (!String.prototype.render) {
    String.prototype.render = function (obj) {
        let text = this;
        for (let k in obj) {
            text = text.replace("{" + k + "}", obj[k]);
        }
        return text;
    }
}

if (!String.prototype.ucfirst) {
    String.prototype.ucfirst = function () {
        if (this.length > 0) {
            return this.substring(0, 1).toUpperCase() + this.substring(1, this.length);
        }
    }
}

if (!Number.prototype.format) {
    Number.prototype.format = function (decimals, moneySymbol) {
        decimals = decimals || 0;
        moneySymbol = moneySymbol || false;
        moneySymbol = moneySymbol ? 'USD' : '';

        return moneySymbol + this.toFixed(decimals).replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
    };
}

/* Helpers */
App.helpers.video = {};
App.helpers.string = {};
App.helpers.number = {};
App.helpers.html = {};
App.helpers.user = {};

App.helpers.user = {
    getCredits() {
        let target = $('#userCredits');

        $.post(App.helpers.url.base() + 'user/GetCreditsByCurrentUser', function (r) {
            target.text('$ ' + parseInt(r));
        }, 'json')
    }
}

App.helpers.video = {
    getYoutubeVideo(options) {
        let id = options.id;

        options.controls = options.controls || 0,
        options.autoplay = options.autoplay || 0,
        options.showinfo = options.showinfo || 0;

        return '<iframe class="embed-responsive-item" src="https://www.youtube.com/embed/{id}?modestbranding=1&autohide=1&showinfo={showinfo}&controls={controls}&autoplay={autoplay}" frameborder="0" allowfullscreen></iframe>'.render(
            options
        );
    }
}

App.helpers.html = {
    addTableClass(id) {
        $(id).find('table').addClass('table table-striped');
    }
}