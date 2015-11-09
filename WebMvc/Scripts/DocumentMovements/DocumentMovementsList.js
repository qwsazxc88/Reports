console.log('DocumentMovementsList module loading...');
var Application = angular.module('DocumentMovementsList', ['ServiceModule', 'ui.grid', 'ui.grid.autoResize']);
Application.controller('DocumentMovementsListController', function ($scope, $http, $filter, dataService) {
    this.scope = $scope;
    $scope.SetModel = function (data) { $scope.Model = data; $scope.IsWorking = false; }
    $scope.OnError = function (message) { alert(message); $scope.IsWorking = false; }
    $scope.IsWorking = false;
    
    $scope.Model = {};
    $scope.Model.Documents = [];
    $scope.Model.GridOptions =
    {
        enableSorting: true,
        columnDefs: $scope.columns,
        data: $scope.Model.Documents
    };
    $scope.SaveData = function () {
        $scope.IsWorking = true;
        dataService.Post('DocumentMovements/DocumentMovementsList', $scope.Model, $scope.SetModel, alert);
    }
});
console.log('DocumentMovementsList module loaded...');