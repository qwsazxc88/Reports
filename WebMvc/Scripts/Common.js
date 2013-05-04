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
        $("#" + this).attr("class", "menuitem");
    });
    $("#" + menuName).attr("class", "on");
}
function setMainActiveMenuItem(menuName) {
    $(".mainOn").each(function () {
        $("#" + this).attr("class", "mainMenuItem");
    });
    $("#" + menuName).attr("class", "mainOn");
}  