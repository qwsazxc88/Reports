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
        data.onRegisterApi = function (gridApi) {
            if (gridApi != undefined) {
                $scope.gridApi = gridApi;
                $scope.gridApi.core.handleWindowResize();
                console.log($scope.gridApi);
                $('#changes-height').height('auto');
            }
        };
        $scope.GridOptions = data;
        if ($scope.gridApi != undefined) {
            $scope.gridApi.core.handleWindowResize(); $('#changes-height').height('auto');
            console.log($scope.gridApi);
        }
    }
    $scope.OnError = function (message) { alert(message); $scope.IsWorking = false; }
    $scope.IsWorking = false;
    $scope.SetDocuments({});
    $scope.GridOptions.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
        $scope.gridApi.expandable.expandAllRows();
    };
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