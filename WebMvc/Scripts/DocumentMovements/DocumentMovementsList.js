console.log('DocumentMovementsList module loading...');
var GridApi=undefined;
var Application = angular.module('DocumentMovementsList', ['ServiceModule', 'ui.grid', 'ui.grid.edit', 'ui.grid.autoResize', 'ui.grid.expandable', 'ui.grid.grouping', 'ui.grid.resizeColumns']);
Application.controller('DocumentMovementsListController', function ($scope, $http, $filter, dataService, i18nService, $timeout, $window) {
    this.scope = $scope;
    //i18nService.setCurrentLang('ru');
    $scope.Documents = [];
    $scope.RowFilter = {};
    $scope.RowFilter.DocumentName = "";
    $scope.SetModel = function (data) { $scope.Model = data; $scope.IsWorking = false; }
    $scope.SetDocuments = function (data) {
        if (data.data) {
            $('#changes-height').height((data.data.length + 1) * 30);
        }
        $scope.IsWorking = false;
        //data.expandableRowTemplate = '<div ui-grid="row.entity.subGridOptions" ui-grid-edit style="height:auto;"></div>';
        data.expandableRowTemplate = '<div class="expandable-wrap"><table class="grid"><tr><th>Наименование</th><th>Отправлено</th><th>Получено</th></tr><tr ng-repeat="tablerow in row.entity.subGridOptions.data | filter:DocTypeFilter"><td>{{tablerow.DocumentName}}</td><td><input type= "checkbox" ng-model="tablerow.DocumentSended" disabled="disabled"/></td><td><input type= "checkbox" ng-model="tablerow.DocumentReceived" disabled="disabled" ng-disabled="!row.entity.subGridOptions.IsGridEditable"/></td></tr></table><br><div class="field-wrap">Отметка о получении:<input type="checkbox" disabled="disabled" ng-model="row.entity.ReceiverAccept" ng-disabled="!row.entity.subGridOptions.IsGridEditable" ></div></div>';
        data.enableVerticalScrollbar = 0;
        data.expandableRowHeight = 'auto';
        data.enableExpandableRowHeader = false;
        data.expandableRowScope = {
            IsGridEditable: data.IsGridEditable,
            DocTypeFilter: $scope.RowFilter
        };
        $scope.CustomFilter = function (renderableRows) {
            var value = $scope.RowFilter.DocumentName ? $scope.RowFilter.DocumentName.toLowerCase() : "";

            renderableRows.forEach(function (row) {
                var match = false;
                row.entity.subGridOptions.data.forEach(function (field) {
                    var val = field.DocumentName;

                    if (val.toLowerCase().indexOf(value) > -1) {
                        match = true;
                    }
                });
                if (!match) {
                    row.visible = false;
                }
            });
            return renderableRows;
        };
        $scope.$watch(
        // This function returns the value being watched. It is called for each turn of the $digest loop
            function () { return $scope.RowFilter.DocumentName; },
        // This is the change listener, called when the value returned from the above function changes
            function (newValue, oldValue) {
                if (newValue != oldValue) {
                    // Only increment the counter if the value changed
                    if ($scope.gridApi) {
                        if (data.data) {
                            $('#changes-height').height((data.data.length + 1) * 30); $scope.gridApi.core.handleWindowResize();
                        }
                        $scope.gridApi.grid.refresh(); $scope.gridApi.core.handleWindowResize(); $('#changes-height').height('auto');
                    }
                }
            }
            );
        data.onRegisterApi = function (gridApi) {
            if (gridApi != undefined) {
                $scope.gridApi = gridApi;
                $scope.gridApi.core.handleWindowResize();
                $scope.gridApi.grid.registerRowsProcessor($scope.CustomFilter, 110);
            }
        };
        $scope.GridOptions = data;
        if ($scope.gridApi != undefined) {
            $scope.gridApi.core.handleWindowResize(); $('#changes-height').height('auto');
        }
    }
    $scope.OnError = function (message) { alert(message); $scope.IsWorking = false; }
    $scope.IsWorking = false;
    $scope.SetDocuments({});

    $scope.toggleRow = function (row) {
        $scope.gridApi.expandable.toggleRowExpansion(row);
    }
    $scope.expandAllRows = function () {
        $scope.gridApi.expandable.expandAllRows();
    }

    $scope.collapseAllRows = function () {
        $scope.gridApi.expandable.collapseAllRows();
    }
    $scope.GetData = function () {
        $scope.IsWorking = true;
        $scope.Model.DepartmentId = $('#DepartmentId').val();
        var promise = dataService.PostPromise('/DocumentMovements/DocumentMovementsListJson', $scope.Model);
        promise.then($scope.SetDocuments);
    }
    $scope.Model = {};
    $scope.SaveData = function () {
        $scope.IsWorking = true;
        dataService.Post('/DocumentMovements/DocumentMovementsList', $scope.Model, $scope.SetModel, $scope.OnError);
    }
    $scope.SaveEdited = function () {
        var elements = [];
        console.log($scope.GridOptions.data);
        for (var i = 0; i < $scope.GridOptions.data.length; i++) {
            var el = $scope.GridOptions.data[i];
            console.log('TEST!');
            console.log(el);
            if (el.subGridOptions.IsGridEditable) {
                var newelement = { Id: el.Id, SelectedDocs: [], ReceiverAccept: el.ReceiverAccept };
                for (var n = 0; n < el.subGridOptions.data.length; n++) {
                    var elementdocs = el.subGridOptions.data[n];
                    var d = { Type: elementdocs.DocType, RecieverCheck: elementdocs.DocumentReceived };
                    newelement.SelectedDocs.push(d);
                }
                elements.push(newelement);
            }
        }
        dataService.Post('/DocumentMovements/SaveModelsFromList', elements, $scope.GetData, $scope.OnError)
    }
});
console.log('DocumentMovementsList module loaded...');
$(document).ready(function () {
    setActiveMenuItem("documentMovements"); 
});