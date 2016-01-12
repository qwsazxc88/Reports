
var Application = angular.module('StaffMovementsFactList', ['ServiceModule', 'ui.grid', 'ui.grid.edit', 'ui.grid.autoResize', 'ui.grid.expandable', 'ui.grid.grouping', 'ui.grid.resizeColumns']);
Application.controller('StaffMovementsFactListController', function ($scope, $http, $filter, dataService, i18nService, $timeout, $window) {
    this.scope = $scope;
    $scope.Documents = [];
    $scope.SetModel = function (data) { $scope.Model = data; $scope.IsWorking = false; }
    $scope.SetDocuments = function (data) {
        if (data.data) {
            $('#changes-height').height((data.data.length + 1) * 30);
        }
        $scope.IsWorking = false;
        data.enableVerticalScrollbar = 0;

        data.onRegisterApi = function (gridApi) {
            if (gridApi != undefined) {
                $scope.gridApi = gridApi;
                $scope.gridApi.core.handleWindowResize();
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
        var promise = dataService.PostPromise('/StaffMovements/GetFactDocuments', $scope.Model);
        promise.then($scope.SetDocuments);
    }
    $scope.Model = {};

});
$(document).ready(function () {
    setActiveMenuItem("StaffMovementsFacts");
});