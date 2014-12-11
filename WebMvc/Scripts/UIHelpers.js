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

// Добавить datepicker к текстовым контролам
// $inputs - jQuery-объект с выборкой контролов
// range - строка формата "диапазон_лет[;минимальная_дата[;максимальная_дата]]", пример: "-80:-14;-80Y;-14Y"
// makeReadonly - заблокировать ввод в контрол с помощью клавиатуры
var attachDatepickerToInputs = function ($inputs, range, makeReadonly) {
    var rangeArray = range.split(";");
    $inputs.datepicker({
        dateFormat: "dd.mm.yy",
        changeYear: true,
        yearRange: rangeArray[0] !== undefined ? rangeArray[0] : null,
        minDate: rangeArray[1] !== undefined ? rangeArray[1] : null,
        maxDate: rangeArray[2] !== undefined ? rangeArray[2] : null
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
};

var convertDateToShortDateString = function (date) {
    if (!date) {
        return "__.__.____"
    }
    return ('0' + date.getDate()).slice(-2) + "." + ('0' + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
};

var convertMSJsonDateToShortDateString = function (mSJsonDate) {
    if (!mSJsonDate) {
        return "__.__.____"
    }
    var date = new Date(parseInt(mSJsonDate.substr(6)));
    return ('0' + date.getDate()).slice(-2) + "." + ('0' + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
};