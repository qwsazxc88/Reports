function onSetShortNamesClick() {
    createSetShortNameDialog();
}
function createSetShortNameDialog() {
    var elem = document.createElement('div');
    elem.id = "divSetShortNameDialog";
    var newDiv = $(elem);
    //var departmentId = $("#DepartmentId").val();
    //var typeId = $("#RequestTypeId").val();
    var title = "Установка коротких названий";
    $(newDiv).text('Подождите, идет загрузка данных ...');
    $.ajaxSetup({ cache: false });
    //var url = actionDepDialogUrl + '?parentId='+ parentId+'&level='+level;
    $(newDiv).load(actionSetShortNameDialogUrl + " #SetShortNameTable"
    , function (response, status, xhr) {
        if (status == "error") {
            var msg = "Произошла ошибка: ";
            $(newDiv).html("<div style='color:Red'>" + msg + xhr.status + " " + xhr.statusText + "</div>");
        }
    }
    );
//    if ($('#IsShortNamesEditable').val() != 'True')
//        disableSaveButton();
        
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
                $(this).dialog("close");
//                if (typeof ChangeMonth == 'function') {
//                    ChangeMonth(); //call function
//                }
            },
            "Отмена": function () {
                $(this).dialog("close");
            }
        }
    });
}
function ValidateShortName() {
    return true;
}
function SaveShortName() {
    return;
}
function TerraGraphicsLevel3IDChange() {
    addTerraSelError();
    var url = actionTerraPointShortNameUrl + '?pointId=' + $('#Level2ID').val();
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraSelError(result.Error);
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
    addTerraSelError();
    var url = actionTerraPointChildUrl + '?parentId=' + parentId + '&level=' + level;
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraSelError(result.Error);
            }
            else {
                setValuesToDropdown(controlName, result.Children);
                if (level == 2) {
                    setValuesToDropdown('Level3ID', result.Level3Children);
                }
                $('#ShortName').val(result.ShortName)
            }
        });
}
function setValuesToDropdown(controlName, data) {
    var optionsValues = '<select style = "width:95%" onchange = TerraGraphics"' + controlName + 'Change();" id="' + controlName + '" name="' + controlName + '">';
    optionsValues += '<option value="0"></option>';
    $.each(data, function (item, data) {
        optionsValues += '<option value="' + data.Id + '">' + data.Name + '</option>';
    })
    optionsValues += '</select>';
    var options = $('#' + controlName);
    options.replaceWith(optionsValues);
}
function addTerraSelError(value) {
    $("#SetShortNameError").text(value);
    $("#SetShortNameError").show();
    disableSaveButton() 
}
function clearTerraSelErrors() {
    $("#SetShortNameError").text("");
    $("#SetShortNameError").hide();
}
function disableSaveButton() {
    $(".ui-dialog-buttonpane button:contains('Установить')").button("disable");
}
