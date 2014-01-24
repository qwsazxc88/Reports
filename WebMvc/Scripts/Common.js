﻿function trimSpacesLeft(str) {
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
function validateFieldIsNotEmptyAddError(control) {
    return validateFieldIsNotEmpty(control, true);
}
function validateFieldIsNotEmpty(control, addErrorToForm) {
    if (control.val() != '')
        return true;
    if (addErrorToForm) {
        addDlgError(control, "Обязательное поле");
    }
    return false;
}
function clearDlgErrors(form) {
    form.find(":input").removeClass("input-validation-error");
    form.find(".error").remove();
}
function addDlgError(el, value) {
    el.addClass("input-validation-error");
    var msg = value.toString();
    $("<span/>").addClass("error field-validation-error").text(msg).appendTo(el.parent());
    if (value.length > 20) $("<span/>").parent().css("width", 500);
}
function ValidateSumC(control) {
    if (control.val() == '')
        return undefined;
    var sum = ValidateFloat(control);
    if (sum == undefined) {
        addDlgError(control, "Поле должно быть неотрицательным десятичным числом");
        return undefined;
    }
    else if (sum < 0) {
        addDlgError(control, "Поле должно быть неотрицательным десятичным числом");
        return undefined;
    }
    else
        return sum;
}
function ValidateInt(control) {
    var value = control.val();
    if (value == '')
        return undefined;
    var result = parseInt(value, 10);
    if (!(/^[0-9]+$/i).test(value) || isNaN(result) || (result < 0)) {
        addDlgError(control, "Поле должно быть целым неотрицательным числом");
        return undefined;
    }
    return result;
}

  