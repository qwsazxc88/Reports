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

function showInputActionResult(inputs, isOk) {
    var color = isOk ? "lightgreen" : "pink";
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].css("background-color", color);
    }
    setTimeout(function () {
        for (var i = 0; i < inputs.length; i++) {
            inputs[i].css("background-color", "white");
        }
    }, 700);
}