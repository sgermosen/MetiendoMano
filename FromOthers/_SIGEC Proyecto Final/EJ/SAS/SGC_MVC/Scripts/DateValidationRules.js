jQuery.validator.unobtrusive
      .adapters.addSingleVal("dateend", "datestart");

jQuery.validator.addMethod("dateend",
    function (val, element, other) {
        var modelPrefix = element.name.substr(
                            0, element.name.lastIndexOf(".") + 1)
        var otherVal = $("[name=" + modelPrefix + other + "]").val();
        if (val && otherVal) {
            if (Date.parse(val) < Date.parse(otherVal)) {
                return false;
            }
        }
        return true;
    }
);