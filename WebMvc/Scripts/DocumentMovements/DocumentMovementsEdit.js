console.log('DocumentMovementsEdit module loading...');
var Application = angular.module('DocumentMovementsEdit', ['ServiceModule','oi.select']);
Application.controller('DocumentMovementsEditController', function ($scope, $http, $filter, $q, dataService) {
    this.scope = $scope;
    $scope.SetModel = function (data) { $scope.Model = data; $scope.IsWorking = false; }
    $scope.OnError = function (message) { alert(message); $scope.IsWorking = false; }
    $scope.IsWorking = false;
    $scope.Model = {};
    $scope.SaveData = function () {
        $scope.IsWorking = true;
        dataService.Post('DocumentMovements/DocumentMovementsEdit', $scope.Model, $scope.SetModel, alert);
    }
});
console.log('DocumentMovementsEdit module loaded...');