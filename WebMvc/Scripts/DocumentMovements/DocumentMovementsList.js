console.log('DocumentMovementsList module loading...');
var Application = angular.module('DocumentMovementsList', ['ServiceModule', 'ui.grid', 'ui.grid.autoResize', 'ui.grid.grouping', 'ui.grid.resizeColumns']);
Application.controller('DocumentMovementsListController', function ($scope, $http, $filter, dataService,i18nService) {
    this.scope = $scope;
    //i18nService.setCurrentLang('ru');
    $scope.Documents = [];
    $scope.SetModel = function (data) { $scope.Model = data; $scope.IsWorking = false; }
    $scope.SetDocuments = function (data) {
        $scope.IsWorking = false;
        $scope.Model.Documents = data;
        $scope.GridOptions.data = $scope.Model.Documents;
        $('.ui-grid').height('auto'); $('.ui-grid-viewport').height('auto');
    }
    $scope.OnError = function (message) { alert(message); $scope.IsWorking = false; }
    $scope.IsWorking = false;

    $scope.GetData = function () {
        $scope.IsWorking = true;
        var promise = dataService.PostPromise('/DocumentMovements/DocumentMovementsListJson', $scope.Model);
        promise.then($scope.SetDocuments);
    }
    $scope.Model = {};
    $scope.Model.Documents = [];
    $scope.GridOptions = {
        enableColumnResizing: true,
        enableVerticalScrollbar : 0,
        data: $scope.Model.Documents,
        enableFiltering: true,
        columnDefs: [
        { field: 'Id', displayName: 'Номер', enableFiltering: true, enableSorting: true, type: 'number', width:50 },
        { field: 'User', displayName: 'ФИО сотрудника',enableCellEdit: true, enableFiltering: true, enableSorting: true, type: 'string', width:200 },
        { field: 'UserDep3', displayName: 'Дирекция 3 ур.', enableFiltering: true, enableSorting: true, type: 'string', width:200 },
        { field: 'UserDep7', displayName: 'Подразделение 7 ур.', enableFiltering: true, enableSorting: true, type: 'string', width:200 },
        { field: 'Direction', displayName: 'Направление', enableFiltering: true, enableSorting: true, type: 'string' },
        { field: 'Sender', displayName: 'Отправитель', enableFiltering: true, enableSorting: true, type: 'string' },
        { field: 'Receiver', displayName: 'Получатель', enableFiltering: true, enableSorting: true, type: 'string' },
        { field: 'CreateDate', displayName: 'Дата создания', enableFiltering: true, enableSorting: true, type: 'date', cellTemplate: '<div>{{row.entity[col.field]| date: \'dd.MM.yyyy\'}}</div>' },
        { field: 'SendDate', displayName: 'Дата отправки', enableFiltering: true, enableSorting: true, type: 'date', cellTemplate: '<div>{{row.entity[col.field]| date: \'dd.MM.yyyy\'}}</div>' },
        { field: 'Descript', displayName: 'Описание', enableFiltering: true, enableSorting: true, type: 'string' },
        { field: 'DocumentName', displayName: 'Наименование документа', enableFiltering: true, enableSorting: true, type: 'string' },
        { field: 'DocumentSended', displayName: 'Документ отправлен', enableFiltering: true, enableSorting: true, type: 'boolean', cellTemplate: '<input ng-show="row.entity[col.field]!=undefined" type="checkbox" ng-model="row.entity[col.field]" disabled="disabled"/>' },
        { field: 'DocumentReceived', displayName: 'Документ получен', enableFiltering: true, enableSorting: true, type: 'boolean', cellTemplate: '<input ng-show="row.entity[col.field]!=undefined" type="checkbox" ng-model="row.entity[col.field]" disabled="disabled"/>' }, 
        { field: 'Status', displayName: 'Статус', enableFiltering: true, enableSorting: true, type: 'string'}] };
    $scope.SaveData = function () {
        $scope.IsWorking = true;
        dataService.Post('/DocumentMovements/DocumentMovementsList', $scope.Model, $scope.SetModel, $scope.OnError);
    }
});
console.log('DocumentMovementsList module loaded...');
$(document).ready(function () { $('.ui-grid').height('auto'); $('.ui-grid-viewport').height('auto'); });