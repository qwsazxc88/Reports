﻿function createEditPointDialog(id, day, userId) {
    var elem = document.createElement('div');
    elem.id = "divEditPointDialog";
    var newDiv = $(elem);
    var title = "Выбор точки ";
    $(newDiv).text('Подождите, идет загрузка данных ...');
    $.ajaxSetup({ cache: false });
    $(newDiv).load(actionEditPointDialogUrl + '?id=' + id + '&day=' + day + '&userId=' + userId + " #EditPointTable"
    , function (response, status, xhr) {
        if (status == "error") {
            var msg = "Произошла ошибка: ";
            $(newDiv).html("<div style='color:Red'>" + msg + xhr.status + " " + xhr.statusText + "</div>");
            disableEditSaveButton();
            disableEditClearButton();
        } else if (status == "success") {
            if ($('#EditPointTableLoadError').val() != undefined) {
                disableEditSaveButton();
                disableEditClearButton();
            }
            if ($('#IsDeleteEnable').val() == 'False') {
                disableEditClearButton();
            }
            if ($('#IsPlanEditable').val() == 'False') {
                $('#SetDefaultPoint').attr("disabled", "disabled");
                disablePlan(false);
            }
            else {
                enablePlan();
                if($('#IsPlanHoliday').val() == 'True')
                    disablePlanHours();
                else
                    enablePlanHours();
            }
              if($('#IsFactHoliday').val() == 'True')
                    disableFactHours();
                else
                    enableFactHours();
        }
    }
    );
    $(newDiv).dialog(
    { // initialize dialog box
        autoOpen: true,
        modal: true,
        title: title,
        // fix IE6  
        bgiframe: true,
        draggable: false,
        resizable: false,
        width: 750,
        height: 520,
        close: function (event, ui) {
            $(this).dialog("destroy").remove();
        },
        buttons:
        {
            "Сохранить": function () {
                if (!ValidateEditPoint())
                    return;
                SaveEditPoint();
            },
            "Удалить": function () {
                deletePoint();
                //$(this).dialog("close");
            },
            "Отмена": function () {
                $(this).dialog("close");
            }
        }
    });
}
function deletePoint() {
    var id = $('#Id').val();
    clearTerraEditErrors();
    var url = actionEditPointDeleteUrl + "?id=" + $('#Id').val();
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraEditError(result.Error);
            }
            else {
                //$("#divEditPointDialog").dialog("close");
                refreshTableBtn(true);
            }
        });
}
function ValidateEditPoint() {
    //clearTerraEditErrors();
    clearDlgErrors($("#EditPointTable"));
    var result = true;
    if (($('#IsPlanEditable').val() == 'False') && ($('#FactHours').val() == '')) {
        addDlgError($('#FactHours'), "Необходимо указать часы");
        result = false;
        return result;
    }
    if (($('#IsFactVisible').val() == 'False') && ($('#Hours').val() == '')) {
        addDlgError($('#Hours'), "Необходимо указать часы");
        result = false;
        return result;
    }
    if (($('#IsFactVisible').val() == 'True') && ($('#Hours').val() == '') &&
        ($('#IsPlanEditable').val() == 'True') && ($('#FactHours').val() == '')) {
        addDlgError($('#Hours'), "Необходимо указать часы (плановые или фактические)");
        result = false;
        return result;
    }

    if ($('#Hours').val() != '') {
        var hours = ValidateFloat($("#Hours"));
        if (hours == undefined) {
            //addTerraEditError("Поле 'План' должно быть числом от 0 до 24");
            addDlgError($('#Hours'), "Часы должны быть числом от 0 до 24");
            result = false;
        } else if ((hours < 0) || (hours > 24)) {
            //addTerraEditError("Поле 'План' должно быть числом от 0 до 24");
            addDlgError($('#Hours'), "Часы должны быть числом от 0 до 24");
            result = false;
        }
    }
    if ($('#FactHours').val() != '') {
        var hours = ValidateFloat($("#FactHours"));
        if (hours == undefined) {
            addDlgError($('#FactHours'), "Часы должны должно быть числом от 0 до 24");
            //addTerraEditError("Поле 'Факт' должно быть числом от 0 до 24");
            result = false;
        } else if ((hours < 0) || (hours > 24)) {
            addDlgError($('#FactHours'), "Часы должны быть числом от 0 до 24");
            //addTerraEditError("Поле 'Факт' должно быть числом от 0 до 24");
            result = false;
        }
    }
    return result;

    /*var hours = parseInt($("#Hours").val(), 10);
    if (isNaN(hours) || (hours < 0) || (hours > 24)) {
        addTerraEditError("Поле 'План' должно быть целым числом от 0 до 24");
        return false;
    }*/
   // return true;
}
function SaveEditPoint() {
    clearTerraEditErrors();
    var pointId = 0;
    if ($('#EpLevel3ID').val() != null)
        pointId = $('#EpLevel3ID').val();
    var url = actionEditPointSaveUrl + '?pointId=' + pointId + "&id=" + $('#Id').val() + "&userId=" + $('#UserId').val() + "&day=" + $('#Day').val()
                + "&hours=" + $('#Hours').val() + "&credits=" + $('#Credit').val() + "&factCredits=" + $('#FactCredit').val() + '&factPointId=' + $('#FactEpLevel3ID').val()
                + "&factHours=" + $('#FactHours').val();
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraEditError(result.Error);
            }
            else {
                //TerraGraphicsLevel2IDChange();
                refreshTableBtn(true);
                //$("#divEditPointDialog").dialog("close");
                
            }
        });
    }

    function disableFact() {
        setEmptyValuesToDropdown('FactEpLevel2ID');
        //$('#FactEpLevel2ID').val(0);
        $('#FactEpLevel2ID').attr("disabled", "disabled");
        setEmptyValuesToDropdown('FactEpLevel3ID');
        //$('#FactEpLevel3ID').val(0);
        $('#FactEpLevel3ID').attr("disabled", "disabled");
        disableFactHours();
    }

    function enableFact() {
        $('#FactEpLevel2ID').removeAttr("disabled");
        $('#FactEpLevel3ID').removeAttr("disabled");
        enableFactHours();
    }
    function disableFactHours() {
        $('#FactHours').attr("disabled", "disabled");
        $('#FactHours').val(0);
    }
    function enableFactHours() {
        $('#FactHours').removeAttr("disabled");
    }

    function disablePlan(setHours) {
        if (!setHours)
            $('#EpLevel1ID').attr("disabled", "disabled");
        $('#EpLevel2ID').attr("disabled", "disabled");
        $('#EpLevel3ID').attr("disabled", "disabled");
        $('#Hours').attr("disabled", "disabled");
        if(setHours)
            $('#Hours').val(0);
        $('#Credit').attr("disabled", "disabled");
        $('#Credit').val(0);
    }
    function enablePlan() {
        $('#EpLevel2ID').removeAttr("disabled");
        $('#EpLevel3ID').removeAttr("disabled");
        enablePlanHours();
//        $('#Hours').removeAttr("disabled");
//        $('#Credit').removeAttr("disabled");
    }
    function disablePlanHours() {
        $('#Hours').attr("disabled", "disabled");
        $('#Hours').val(0);
        $('#Credit').attr("disabled", "disabled");
        $('#Credit').val(0);
    }
    function enablePlanHours() {
        $('#Hours').removeAttr("disabled");
        $('#Credit').removeAttr("disabled");
    }
    function TerraGraphicsFactEpLevel1IDChange() {
        if ($('#FactEpLevel1ID').val() == -1) {
            disableFact(); 
        }
        else {
            enableFact();
            GetEditTgChilds('FactEpLevel2ID', $('#FactEpLevel1ID').val(), 2);
        }
}
function TerraGraphicsFactEpLevel2IDChange() {
    GetEditTgChilds('FactEpLevel3ID', $('#FactEpLevel2ID').val(), 3);
}
function TerraGraphicsFactEpLevel3IDChange() {
}

function TerraGraphicsEpLevel1IDChange() {
//    if ($('#EpLevel1ID').val() == -1) {
//        disablePlan();
//    }
//    else {
//        enablePlan();
        GetEditTgChilds('EpLevel2ID', $('#EpLevel1ID').val(), 2);
    //}
}
function TerraGraphicsEpLevel2IDChange() {
    GetEditTgChilds('EpLevel3ID', $('#EpLevel2ID').val(), 3);
}
function TerraGraphicsEpLevel3IDChange() {
}
function GetEditTgChilds(controlName, parentId, level) {
clearTerraEditErrors();
var url = actionTerraPointChildUrl + '?parentId=' + parentId + '&level=' + level;
$.getJSON(url,
    function (result) {
        if (result.Error != "") {
            addTerraEditError(result.Error, true);
        }
        else {
            if(result.IsHoliday)
            {
                if (controlName.indexOf('Fact') == 0)
                    disableFactHours();
                else
                    disablePlanHours();
            }
            else
            {
                if (controlName.indexOf('Fact') == 0)
                    enableFactHours();
                else
                    enablePlanHours();
            }
            setTgValuesToDropdown(controlName, result.Children);
            if (level == 2) {
                if (controlName.indexOf('Fact') == 0)
                    setTgValuesToDropdown('FactEpLevel3ID', result.Level3Children);
                else
                    setTgValuesToDropdown('EpLevel3ID', result.Level3Children);
            }
        }
    });
}
function SetDefaultPoint() {
    clearTerraEditErrors();
    var url = actionTerraPointSetDefaultUrl + '?pointId=' + $('#EpLevel3ID').val() + "&userId=" + $('#UserId').val();
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraEditError(result.Error);
            }
            else {
                //TerraGraphicsLevel2IDChange();
                //$("#divSetShortNameDialog").dialog("close");
            }
        });
}

function addTerraEditError(value) {
    addTerraEditError(value, false);
}
function addTerraEditError(value, disableButton) {
    $("#EditPointError").text(value);
    $("#EditPointError").show();
    if (disableButton)
        disableEditSaveButton()
}
function clearTerraEditErrors() {
    $("#EditPointError").text("");
    $("#EditPointError").hide();
}
function disableEditSaveButton() {
    $(".ui-dialog-buttonpane button:contains('Сохранить')").button("disable");
}
function disableEditClearButton() {
    $(".ui-dialog-buttonpane button:contains('Удалить')").button("disable");
}




function onSetShortNamesClick() {
    createSetShortNameDialog();
}
function createSetShortNameDialog() {
    var elem = document.createElement('div');
    elem.id = "divSetShortNameDialog";
    var newDiv = $(elem);
    var title = "Установка коротких названий";
    $(newDiv).text('Подождите, идет загрузка данных ...');
    $.ajaxSetup({ cache: false });
    $(newDiv).load(actionSetShortNameDialogUrl + " #SetShortNameTable"
    , function (response, status, xhr) {
        if (status == "error") {
            var msg = "Произошла ошибка: ";
            $(newDiv).html("<div style='color:Red'>" + msg + xhr.status + " " + xhr.statusText + "</div>");
        } else if (status == "success") {
            if ($('#ShortNameTableLoadError').val() != undefined)
                disableSaveButton();
        }
    }
    );
    $(newDiv).dialog(
    { // initialize dialog box
        autoOpen: true,
        modal: true,
        title: title,
        // fix IE6  
        bgiframe: true,
        draggable: false,
        resizable: false,
        width: 750,
        height: 340,
        close: function (event, ui) {
            $(this).dialog("destroy").remove();
        },
        open: function (event, ui) {
            if ($('#IsShortNamesEditable').val() != 'True')
                disableSaveButton();

        },
        buttons:
        {
            "Установить": function () {
                if (!ValidateShortName())
                    return;
                SaveShortName();
            },
            "Отмена": function () {
                $(this).dialog("close");
            }
        }
    });
}
function ValidateShortName() {
    if ($('#ShortName').val() == '') {
        addTerraSelError("Необходимо указать короткое название");
        return false;
    }
    if ($('#ShortName').val().length > 5 ) {
        addTerraSelError("Длина короткого названия не может превышать  5 символов");
        return false;
    }
    return true;
}
function SaveShortName() {
    clearTerraSelErrors();
    var url = actionTerraPointSaveUrl + '?pointId=' + $('#Level3ID').val() + "&shortName=" + escapeJson($('#ShortName').val());
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraSelError(result.Error);
            }
            else {
                TerraGraphicsLevel2IDChange();
                //$("#divSetShortNameDialog").dialog("close");
            }
        });
   
}
function TerraGraphicsLevel3IDChange() {
    clearTerraSelErrors();
    var url = actionTerraPointShortNameUrl + '?pointId=' + $('#Level3ID').val();
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraSelError(result.Error,true);
            }
            else {
                $('#ShortName').val(result.ShortName)
            }
        });
}
function TerraGraphicsLevel2IDChange() {
        GetTgChilds('Level3ID', $('#Level2ID').val(), 3);
}
function TerraGraphicsLevel1IDChange() {
    GetTgChilds('Level2ID', $('#Level1ID').val(), 2);
}
function GetTgChilds(controlName, parentId, level) {
    clearTerraSelErrors();
    var url = actionTerraPointChildUrl + '?parentId=' + parentId + '&level=' + level;
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraSelError(result.Error, true);
            }
            else {
                var selection;
                if (level == 3) {
                    var selection = $('#Level3ID').val();
                }
                setTgValuesToDropdown(controlName, result.Children);
                if (level == 2) {
                    setTgValuesToDropdown('Level3ID', result.Level3Children);
                }
                else {
                    $('#Level3ID').val(selection);
                }
                TerraGraphicsLevel3IDChange();
                //$('#ShortName').val(result.ShortName)
            }
        });
}
function setTgValuesToDropdown(controlName, data) {
    var optionsValues = '<select style = "width:95%" onchange = TerraGraphics' + controlName + 'Change(); id="' + controlName + '" name="' + controlName + '">';
//    optionsValues += '<option value="0"></option>';
    $.each(data, function (item, data) {
        optionsValues += '<option value="' + data.Id + '">' + data.Name + '</option>';
    })
    optionsValues += '</select>';
    var options = $('#' + controlName);
    options.replaceWith(optionsValues);
}
function setEmptyValuesToDropdown(controlName) {
    var optionsValues = '<select style = "width:95%" id="' + controlName + '" name="' + controlName + '">';
    optionsValues += '<option value="0"></option>';
    optionsValues += '</select>';
    var options = $('#' + controlName);
    options.replaceWith(optionsValues);
}
function addTerraSelError(value) {
    addTerraSelError(value, false);
}
function addTerraSelError(value,disableButton) {
    $("#SetShortNameError").text(value);
    $("#SetShortNameError").show();
    if(disableButton)
        disableSaveButton() 
}
function clearTerraSelErrors() {
    $("#SetShortNameError").text("");
    $("#SetShortNameError").hide();
}
function disableSaveButton() {
    $(".ui-dialog-buttonpane button:contains('Установить')").button("disable");
}
