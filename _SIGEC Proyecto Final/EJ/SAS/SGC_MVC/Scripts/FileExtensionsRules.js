$(function () {
    jQuery.validator.unobtrusive.adapters.add('fileextensions', ['fileextensions'], function (options) {
        // Set up test parameters
        var params = {
            fileextensions: options.params.fileextensions.split(',')
        };

        // Match parameters to the method to execute
        options.rules['fileextensions'] = params;
        if (options.message) {
            // If there is a message, set it for the rule
            options.messages['fileextensions'] = options.message;
        }
    });

    jQuery.validator.addMethod("fileextensions", function (value, element, param) {
        var extension = getFileExtension(value);
        if (extension == undefined) return true;
        var validExtension = $.inArray(extension, param.fileextensions) !== -1;
        return validExtension;
    });

    function getFileExtension(fileName) {
        var extension = (/[.]/.exec(fileName)) ? /[^.]+$/.exec(fileName) : undefined;
        if (extension != undefined) {
            return extension[0];
        }
        return extension;
    };
}(jQuery));

