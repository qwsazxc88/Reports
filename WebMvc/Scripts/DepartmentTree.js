function changeDepartment() {
    createDepartmentDialog();
}
function createDepartmentDialog() 
{
    var elem = document.createElement('div');
    elem.id = "divDepartmentDialog";
    var newDiv = $(elem);
    var departmentId = $("#DepartmentId").val();
    //var typeId = $("#RequestTypeId").val();

    var title =  "Выбор структурного подразделения";
    $(newDiv).text('Подождите, идет загрузка данных ...'); 
    $.ajaxSetup({cache: false});
    //var url = actionDepDialogUrl + '?parentId='+ parentId+'&level='+level;
    $(newDiv).load(actionDepDialogUrl+"?id=" + departmentId +" #DialogTable"
    , function(response, status, xhr) {
                if (status == "error") {
                    var msg = "Произошла ошибка: ";
                    $(newDiv).html("<div style='color:Red'>"+msg + xhr.status + " " + xhr.statusText+"</div>");
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
        width: 550,
	    height: 260,
        close: function (event, ui) {
            $(this).dialog("destroy").remove();
        },
        buttons:
        {
            "Сохранить": function () {
                if (!ValidateDepartment())
                    return;
                SaveDepartment();
                $(this).dialog("close");
            },
            "Отмена": function () {
                $(this).dialog("close");
            }
        }
    });
}
function ValidateDepartment() {
    if (($('#Level4ID').val() == 0) &&
    (($('#Level3ID').val() != 0) || ($('#Level2ID').val() != 0))) {
        addDepSelError("Необходимо выбрать структурное подразделение не менее 3 уровня");
        return false;
    }
    return true;
}
function SaveDepartment() {
    if ($('#Level7ID').val() != 0) {
        setDepartmentValues('Level7ID');
//        $('#DepartmentId').val($('#Level7ID').val());
//        $('#DepartmentName').val($("#Level7ID option:selected").text());
//        $('#DepartmentNameLabel').text($("#Level7ID option:selected").text());
        return;
    }
    if ($('#Level6ID').val() != 0) {
        setDepartmentValues('Level6ID');
//        $('#DepartmentId').val($('#Level6ID').val());
//        $('#DepartmentName').val($("#Level6ID option:selected").text());
//        $('#DepartmentNameLabel').text($("#Level6ID option:selected").text());
//        return;
    }
    if ($('#Level5ID').val() != 0) {
        setDepartmentValues('Level5ID');
//        $('#DepartmentId').val($('#Level5ID').val());
//        $('#DepartmentName').val($("#Level5ID option:selected").text());
//        $('#DepartmentNameLabel').text($("#Level5ID option:selected").text());
        return;
    }
    if ($('#Level4ID').val() != 0) {
        setDepartmentValues('Level4ID');
//        $('#DepartmentId').val($('#Level4ID').val());
//        $('#DepartmentName').val($("#Level4ID option:selected").text());
//        $('#DepartmentNameLabel').text($("#Level4ID option:selected").text());
        return;
    }
    $('#DepartmentId').val("0");
    $('#DepartmentName').val("");
    $('#DepartmentNameLabel').text("");
    return;
}
function setDepartmentValues(control) {
    $('#DepartmentId').val($('#'+control).val());
    $('#DepartmentName').val($('#' + control + ' option:selected').text());
    $('#DepartmentNameLabel').text($('#' + control + ' option:selected').text());
}
function Level2IDChange() {
    if($('#Level2ID').val() == 0)
        setEmptyToDropdown('Level3ID');
    else
        GetChilds('Level3ID',$('#Level2ID').val(),3);
    setEmptyToDropdown('Level4ID');
    setEmptyToDropdown('Level5ID');
    setEmptyToDropdown('Level6ID');
    setEmptyToDropdown('Level7ID');
}
function Level3IDChange() {
    if ($('#Level3ID').val() == 0)
        setEmptyToDropdown('Level4ID');
    else
        GetChilds('Level4ID', $('#Level3ID').val(), 4);
//    setEmptyToDropdown('Level4ID');
    setEmptyToDropdown('Level5ID');
    setEmptyToDropdown('Level6ID');
    setEmptyToDropdown('Level7ID');

}
function Level4IDChange() {
    if ($('#Level4ID').val() == 0)
        setEmptyToDropdown('Level5ID');
    else
        GetChilds('Level5ID', $('#Level4ID').val(), 5);
    //setEmptyToDropdown('Level4ID');
    //setEmptyToDropdown('Level5ID');
    setEmptyToDropdown('Level6ID');
    setEmptyToDropdown('Level7ID');
}
function Level5IDChange() {
    if ($('#Level5ID').val() == 0)
        setEmptyToDropdown('Level6ID');
    else
        GetChilds('Level6ID', $('#Level5ID').val(), 6);
    //setEmptyToDropdown('Level4ID');
    //setEmptyToDropdown('Level5ID');
    //setEmptyToDropdown('Level6ID');
    setEmptyToDropdown('Level7ID');
}
function Level6IDChange() {
    if ($('#Level6ID').val() == 0)
        setEmptyToDropdown('Level7ID');
    else
        GetChilds('Level7ID', $('#Level6ID').val(), 6);
    //setEmptyToDropdown('Level4ID');
    //setEmptyToDropdown('Level5ID');
    //setEmptyToDropdown('Level6ID');
    //setEmptyToDropdown('Level7ID');
}
function Level7IDChange() {
}
function GetChilds(controlName,parentId,level) {
    clearDepSelErrors();
    var url = actionDepUrl + '?parentId='+ parentId+'&level='+level;
    $.getJSON(url,
        function (result) {
            if (result.Error != "") {
                addDepSelError(result.Error);
            }
            else {
                setValuesToDropdown(controlName, result.Children);
            }
        });
//        fail(function () {
//            addDepSelError("Произошла ошибка асинхронного запроса");
//                                    
//        });
}

function setValuesToDropdown(controlName,data)
{
    //var options = $('#' + controlName);
    //options.attr("disabled", "disabled");
    var optionsValues = '<select style = "min-width:300px" onchange = "' + controlName + 'Change();" id="' + controlName + '" name="' + controlName + '">';
    optionsValues += '<option value="0"></option>';
    $.each(data, function (item, data) {
        optionsValues += '<option value="' + data.Id + '">' + data.Name + '</option>';
    })
    optionsValues += '</select>';
    var options = $('#' + controlName );
    options.replaceWith(optionsValues);
    //options.removeAttr("disabled");
}
function setEmptyToDropdown(controlName) {
    var optionsValues = '<select style = "min-width:300px" onchange = "' + controlName + 'Change();" id="' + controlName + '" name="' + controlName + '">';
    optionsValues += '<option value="0"></option>';
    optionsValues += '</select>';
    var options = $('#' + controlName);
    options.replaceWith(optionsValues);
}
function addDepSelError(value) {
    $("#SelectDepartmentError").text(value);
    $("#SelectDepartmentError").show();
}
function clearDepSelErrors() {
    $("#SelectDepartmentError").text("");
    $("#SelectDepartmentError").hide();
}
