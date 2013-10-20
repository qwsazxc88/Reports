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
function TerraGraphicsLevel1IDChange() {
        GetChilds('Level2ID', $('#Level1ID').val(), 2);
}
function GetChilds(controlName, parentId, level) {
    addTerraSelError();
    var url = actionTerraPointChildUrl + '?parentId=' + parentId + '&level=' + level;
        $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addTerraSelError(result.Error);
            }
            else {
                setValuesToDropdown(controlName, result.Children);
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
}
function clearTerraSelErrors() {
    $("#SetShortNameError").text("");
    $("#SetShortNameError").hide();
}