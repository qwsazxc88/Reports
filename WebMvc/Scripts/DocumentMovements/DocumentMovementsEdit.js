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
    $scope.SearchUsers = function (query) {


        $http({ method: 'POST', url: '/DocumentMovements/GetUsers/', data: { query: query} }).
             success(function (data, status, headers, config) {
                 return data;
             }).
            error(function (data, status, headers, config) {
                return [];
            });

    }
});
console.log('DocumentMovementsEdit module loaded...');