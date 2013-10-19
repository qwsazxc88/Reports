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