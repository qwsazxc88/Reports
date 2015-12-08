function changeDepartment(SelType) {
    //пришлось ввести переменную, так как понадобилось вызывать окно из нескольких мест на одной странице, чтобы разделить выбранные подразделения
    createDepartmentDialog(SelType);
}
function createDepartmentDialog(SelType) {

    var elem = document.createElement('div');
    elem.id = "divDepartmentDialog";
    var newDiv = $(elem);
    //var departmentId = $("#DepartmentId").val();


    var departmentId = 0;
    if (SelType == null) { SelType = 0; ; }
    //для всего остального
    if (SelType == 0) { departmentId = $("#DepartmentId").val(); }
        
    //для штатного расписания
    if (SelType == 1) { departmentId = $("#DepNextId").val(); }
    if (SelType == 2) { departmentId = $("#DepCachinId").val(); }
    if (SelType == 3) { departmentId = $("#DepATMId").val(); }
    if (SelType == 6) { departmentId = $("#ParentId").val(); }


    //Кадровые перемещения
    if (SelType == 4) { departmentId = $("#SourceDepartmentId").val(); }
    if (SelType == 5) { departmentId = $("#TargetDepartmentId").val(); }

    //прием
    if (SelType == 7) { departmentId = $("#DepartmentId").val(); }
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
        width: 750,
	    height: 340,
        close: function (event, ui) {
            $(this).dialog("destroy").remove();
        },
        buttons:
        {
            "Выбрать": function () {
                if (!ValidateDepartment())
                    return;
                SaveDepartment(SelType);
                $(this).dialog("close");
                if (typeof ChangeMonth == 'function') {
                    ChangeMonth(); //call function
                }
            },
            "Отмена": function () {
                $(this).dialog("close");
            }
        }
    });
}
function ValidateDepartment() {
    /*if (($('#Level3ID').val() == 0) && ($('#Level2ID').val() != 0)) {
        addDepSelError("Необходимо выбрать структурное подразделение не менее 2 уровня");
        return false;
    }*/
    clearDepSelErrors();
    if ($('#DepartmentRequiredLevel').val()) {
        var ctrlName = '#Level' + $('#DepartmentRequiredLevel').val() + 'ID';
        if (!$(ctrlName).val() || ($(ctrlName).val() == '0')) {
            addDepSelError("Выбор структурного подразделения уровня " + $('#DepartmentRequiredLevel').val() + " обязателен");
            return false;
        }
    }
    if ($('#Level2ID').val() == 0) {
        addDepSelError("Не выбрано подразделение");
        return false;
    }
    return true;
}
function SaveDepartment(SelType) {
    if ($('#Level7ID').val() != 0) {
        setDepartmentValues('Level7ID', SelType);
        return;
    }
    if ($('#Level6ID').val() != 0) {
        setDepartmentValues('Level6ID', SelType);
        return;
    }
    if ($('#Level5ID').val() != 0) {
        setDepartmentValues('Level5ID', SelType);
        return;
    }
    if ($('#Level4ID').val() != 0) {
        setDepartmentValues('Level4ID', SelType);
        return;
    }
    if ($('#Level3ID').val() != 0) {
        setDepartmentValues('Level3ID', SelType);
        return;
    }
    if ($('#Level2ID').val() != 0) {
        setDepartmentValues('Level2ID', SelType);
        return;
    }

    if (SelType == 0) {
        $('#DepartmentId').val("0");
        $('#DepartmentName').val("");
        $('#DepartmentNameLabel').text("");
    }

    //для штатного расписания
    if (SelType == 1) {
        $('#DepNextId').val("0");
        $('#DepNextName').val("");
        $('#DepNextNameLabel').text("");
    }

    //для штатного расписания
    if (SelType == 2) {
        $('#DepCachinId').val("0");
        $('#DepCachinName').val("");
        $('#DepCachinNameLabel').text("");
    }

    //для штатного расписания
    if (SelType == 3) {
        $('#DepATMId').val("0");
        $('#DepATMName').val("");
        $('#DepATMNameLabel').text("");
    }

    //для штатного расписания
    if (SelType == 6) {
        $('#ParentId').val("0");
        $('#DepParentName').val("");
        $('#DepParentNameLabel').text("");
    }

    //для кадровых перемещений
    if (SelType == 4) {
        $('#SourceDepartmentId').val("0");
        $('#SourceDepartmentName').val("");
        $('#SourceDepartmentNameLabel').text("");
    }
    //для кадровых перемещений
    if (SelType == 5) {
        $('#TargetDepartmentId').val("0");
        $('#TargetDepartmentName').val("");
        $('#TargetDepartmentNameLabel').text("");
    }

    //прием
    if (SelType == 7) {
        $('#DepartmentId').val("0");
        $('#DepartmentName').val("");
        $('#DepartmentNameLabel').text("");
    }
    return;
}
function setDepartmentValues(control, SelType) {
    if (SelType == 0) {
        $('#DepartmentId').val($('#' + control).val());
        $('#DepartmentName').val($('#' + control + ' option:selected').text());
        $('#DepartmentNameLabel').text($('#' + control + ' option:selected').text());
    }

    //для штатного расписания
    if (SelType == 1) {
        $('#DepNextId').val($('#' + control).val());
        $('#DepNextName').val($('#' + control + ' option:selected').text());
        $('#DepNextNameLabel').text($('#' + control + ' option:selected').text());
    }
    //для штатного расписания
    if (SelType == 2) {
        $('#DepCachinId').val($('#' + control).val());
        $('#DepCachinName').val($('#' + control + ' option:selected').text());
        $('#DepCachinNameLabel').text($('#' + control + ' option:selected').text());
    }

    //для штатного расписания
    if (SelType == 3) {
        $('#DepATMId').val($('#' + control).val());
        $('#DepATMName').val($('#' + control + ' option:selected').text());
        $('#DepATMNameLabel').text($('#' + control + ' option:selected').text());
    }

    //для штатного расписания
    if (SelType == 6) {
        $('#ParentId').val($('#' + control).val());
        $('#DepParentName').val($('#' + control + ' option:selected').text());
        $('#DepParentNameLabel').text($('#' + control + ' option:selected').text());
    }
    //для кадровых перемещений
    if (SelType == 4) {
        $('#SourceDepartmentId').val($('#' + control).val());
        $('#SourceDepartmentName').val($('#' + control + ' option:selected').text());
        $('#SourceDepartmentNameLabel').text($('#' + control + ' option:selected').text());
    }
    //для кадровых перемещений
    if (SelType == 5) {
        $('#TargetDepartmentId').val($('#' + control).val());
        $('#TargetDepartmentName').val($('#' + control + ' option:selected').text());
        $('#TargetDepartmentNameLabel').text($('#' + control + ' option:selected').text());
        $('#TargetDepartmentId').change();
    }
    //прием
    if (SelType == 7) {
        $('#DepartmentId').val($('#' + control).val());
        $('#DepartmentName').val($('#' + control + ' option:selected').text());
        $('#DepartmentNameLabel').text($('#' + control + ' option:selected').text());
        $('#DepartmentId').change();
    }
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

}

function setValuesToDropdown(controlName,data)
{
    //var options = $('#' + controlName);
    //options.attr("disabled", "disabled");
    var optionsValues = '<select style = "width:95%" onchange = "' + controlName + 'Change();" id="' + controlName + '" name="' + controlName + '">';
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
    var optionsValues = '<select style = "width:95%" onchange = "' + controlName + 'Change();" id="' + controlName + '" name="' + controlName + '">';
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
