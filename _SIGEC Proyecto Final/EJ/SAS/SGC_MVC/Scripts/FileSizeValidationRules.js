jQuery.validator.unobtrusive.adapters.add(
    'filesize', ['maxsize'], function (options) {
        options.rules['filesize'] = options.params;
        if (options.message) {
            options.messages['filesize'] = options.message;
        }
    }
);

jQuery.validator.addMethod("filesize",
    function (value, element, params) {
         
        if (element.files.length < 1) {
            // Ningun archivo seleccionado
            return true;
        }

        if (!element.files || !element.files[0].size) {
            // Este navegador no soporta el API de HTML5
            return true;
        }

        if (element.files[0].size < 0 || element.files[0].size == undefined)
            return true;

        return element.files[0].size < params.maxsize;
    }
);