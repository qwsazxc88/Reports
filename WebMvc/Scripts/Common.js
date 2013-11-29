function trimSpacesLeft(str) {
    if (str == undefined) str = "";
    var ptrn = /\s*((\S+\s*)*)/;
    return str.replace(ptrn, "$1");
}

function trimSpacesRight(str) {
    if (str == undefined) str = "";
    var ptrn = /((\s*\S+)*)\s*/;
    return str.replace(ptrn, "$1");
}

function trimSpaces(str) {
    if (str == undefined) str = "";
    return trimSpacesLeft(trimSpacesRight(str));
}
function GetTwoDigitValue(value) {
    return parseInt(value * 100, 10) / 100;
}
function addError(value) {
    $("#Error").text(value);
    $("#Error").show();
}
function clearErrors() {
    $("#Error").text("");
    $("#Error").hide();
}
function escapeJson(value) {
    return escape(value); //value.replace('"','\x22');
}
function setActiveMenuItem(menuName) {
    $(".on").each(function () {
        jQuery(this).attr("class", "menuitem");
    });
    $("#" + menuName).attr("class", "on");
}
function setMainActiveMenuItem(menuName) {
//    $(".mainOn").each(function () {
//        $("#" + this).attr("class", "mainMenuItem");
    //    });
    clearMainActiveMenuItem();
    $("#" + menuName).attr("class", "mainOn");
}
function clearMainActiveMenuItem() {
    $(".mainOn").each(function () {
        jQuery(this).attr("class", "mainMenuItem");
    });
}

function ValidateFloat(control) {
    var hours = $(control).val();
    var hourValue = getFloatValueC(hours);
    if (isNaN(hourValue) || !/^[0-9\.,]+$/.test(hours)) {
          return undefined;
    }
    hourValue = GetTwoDigitValue(hourValue);
    hours = ReplaceToRussianDecimalPointC(hourValue.toString());
    control.val(hours);
    return hourValue;
}
function getFloatValueC(textValue) {
    var value = trimSpaces(textValue);
    value = ReplaceDecimalPointC(value);
    return parseFloat(value);
}
function ReplaceDecimalPointC(value) {
    return value.replace(/,/g, '.');

}
function ReplaceToRussianDecimalPointC(value) {
    return value.replace('.', ',');

}