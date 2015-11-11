console.log('DocumentMovementsEdit module loading...');
var Application = angular.module('DocumentMovementsEdit', ['ServiceModule','oi.select']);
Application.controller('DocumentMovementsEditController', function ($scope, $http, $filter, $q, dataService) {
    this.scope = $scope;
    $scope.SetModel = function (data) {
        $location.path('/DocumentMovements/DocumentMovementsEdit/' + data.Id);
    }
    $scope.OnError = function (message) { alert(message); $scope.IsWorking = false; }
    $scope.IsWorking = false;
    $scope.$watch('IsWorking', function (newValue, oldValue) {
        if (newValue) {
        }
        else {
        }
    });
    $scope.$watch(function (scope) { return scope.Model.Receiver }, function (newValue, oldValue) {
        console.log(newValue);
        if (newValue.Id > 0) {
            $('#Receiver_Id').val(newValue.Id);
        }
    });
    $scope.$watch(function (scope) { return scope.Model.User }, function (newValue, oldValue) {
        console.log(newValue);
        if (newValue.Id > 0) {
            $('#User_Id').val(newValue.Id);
        }
    });
    $scope.Model = {};
    $scope.SaveModel = function () {
        console.log('test');
        $scope.IsWorking = true;
        dataService.Post('DocumentMovements/DocumentMovementsEdit', $scope.Model, $scope.SetModel, $scope.OnError);
    }
    $scope.SearchUsers = function (query) {
        return dataService.PostPromise('/AutoComplete/GetUsers', { name: query });
    }
    $scope.SearchReceiverBank = function (query) {
        return dataService.PostPromise('/AutoComplete/GetUsers', { name: query });
    }
    $scope.SearchReceiverRuscount = function (query) {
        return dataService.PostPromise('/DocumentMovements/GetRuscountUsers', { name: query });
    }
});
console.log('DocumentMovementsEdit module loaded...');