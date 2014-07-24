var handleCheckboxState = function ($checkbox, checkedActionObject, uncheckedActionObject, actOnPageLoad) {

    var handler = function () {
        if ($checkbox.attr("checked") === true) {
            checkedActionObject.act();
        }
        else {
            uncheckedActionObject.act();
        }
    }

    $checkbox.click(handler);

    if (actOnPageLoad === true) {
        handler();
    }
};

var attachDatepickerToInputs = function ($inputs, range, makeReadonly) {
    $inputs.datepicker({
        dateFormat: "dd.mm.yy",
        changeYear: true,
        yearRange: range
    });
    if (makeReadonly) {
        $inputs.attr("readOnly", "true");
    }
};