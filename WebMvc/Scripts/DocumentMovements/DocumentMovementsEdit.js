console.log('DocumentMovementsEdit module loading...');

var Application = angular.module('DocumentMovementsEdit', ['ServiceModule','oi.select']);
Application.controller('DocumentMovementsEditController', function ($scope, $http, $filter, $q, dataService) {
    $scope.IsModelExists = false;
    $scope.IsWorking = true;
    $scope.SetModel = function (data) {
        $scope.Model = data;
        $scope.IsWorking = false;
        $scope.IsModelExists = true;
    }
    $scope.SetModel({ Receiver: { Id: 0, Name: '' }, User: {Id:0,Name:''} });
    $scope.OnError = function (message) { alert(message); $scope.IsWorking = false; };
    $scope.$watch(function (scope) { return scope.Model.Receiver }, function (newValue, oldValue) {
        if (newValue && newValue.Id > 0) {
            $('#Receiver_Id').val(newValue.Id);
        }
    });
    $scope.$watch(function (scope) { return scope.Model.User }, function (newValue, oldValue) {
        if (newValue && newValue.Id > 0) {
            $('#User_Id').val(newValue.Id);
        }
    });
    $scope.SaveModel = function () {
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
$(document).ready(function () {    
    setActiveMenuItem("documentMovements");
});