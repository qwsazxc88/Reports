console.log('DocumentMovementsList module loading...');
var Application = angular.module('DocumentMovementsList', ['ServiceModule', 'ui.grid', 'ui.grid.edit', 'ui.grid.autoResize', 'ui.grid.expandable', 'ui.grid.grouping', 'ui.grid.resizeColumns']);
Application.controller('DocumentMovementsListController', function ($scope, $http, $filter, dataService, i18nService) {
    this.scope = $scope;
    //i18nService.setCurrentLang('ru');
    $scope.Documents = [];
    $scope.SetModel = function (data) { $scope.Model = data; $scope.IsWorking = false; }
    $scope.some="kuku";
    $scope.SetDocuments = function (data) {
        $scope.IsWorking = false;
        data.expandableRowTemplate = '<div ui-grid="row.entity.subGridOptions" ui-grid-edit style="height:150px;"></div>';
        data.enableVerticalScrollbar = 0;
        data.expandableRowHeight = 150;
        data.expandableRowScope = {
            IsGridEditable: data.IsGridEditable
        };
        $scope.GridOptions = data;
        console.log($scope.GridOptions);
        $('.ui-grid').height('auto'); $('.ui-grid-viewport').height('auto');
    }
    $scope.OnError = function (message) { alert(message); $scope.IsWorking = false; }
    $scope.IsWorking = false;
    $scope.SetDocuments({}); /*{ "enableColumnResizing": true,
        "enableVerticalScrollbar": 0,
        "enableFiltering": true,
        "columnDefs":
            [
                { "width": 50, "field": "Id", "displayName": "Номер", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "number", "cellTemplate": "<a href='/DocumentMovements/DocumentMovementsEdit/{{row.entity[col.field]}}'>{{row.entity[col.field]}}</a>" },
                { "width": 200, "field": "User", "displayName": "ФИО сотрудника", "enableSorting": true, "enableFiltering": true, "enableCellEdit": true, "type": "string" }, { "width": 200, "field": "UserDep3", "displayName": "Дирекция 3 ур.", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "string" }, 
                { "width": 200, "field": "UserDep7", "displayName": "Подразделение 7 ур.", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "string", "cellTemplate": null, "cellEditTemplate": null }, { "width": 0, "field": "Direction", "displayName": "Направление", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "string", "cellTemplate": null, "cellEditTemplate": null }, { "width": 0, "field": "Sender", "displayName": "Отправитель", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "string", "cellTemplate": null, "cellEditTemplate": null }, { "width": 0, "field": "Receiver", "displayName": "Получатель", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "string", "cellTemplate": null, "cellEditTemplate": null }, { "width": 0, "field": "CreateDate", "displayName": "Дата создания", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "date", "cellTemplate": "<div>{{row.entity[col.field]| date: 'dd.MM.yyyy'}}</div>", "cellEditTemplate": null }, { "width": 0, "field": "SendDate", "displayName": "Дата отправки", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "date", "cellTemplate": "<div>{{row.entity[col.field]| date: 'dd.MM.yyyy'}}</div>", "cellEditTemplate": null }, { "width": 0, "field": "ReceiveDate", "displayName": "Дата получения", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "string", "cellTemplate": null, "cellEditTemplate": null }, { "width": 0, "field": "Descript", "displayName": "Описание", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "string", "cellTemplate": null, "cellEditTemplate": null }, { "width": 0, "field": "Статус", "displayName": "Статус", "enableSorting": true, "enableFiltering": true, "enableCellEdit": false, "type": "string", "cellTemplate": null, "cellEditTemplate": null}], "data": [{ "Id": 1, "User": "", "UserDep3": "", "UserDep7": "", "Direction": "В банк", "Sender": "Аутсорсинг", "Receiver": "Поляк Валентина Николаевна", "CreateDate": "2015-11-10T12:21:33", "SendDate": "2015-11-10T12:21:33", "ReceiveDate": null, "Descript": null, "Status": "Отправлено", "subGridOptions": { "enableColumnResizing": true, "enableVerticalScrollbar": 0, "enableFiltering": true, "columnDefs": [], "data": []} }, { "Id": 2, "User": "", "UserDep3": "", "UserDep7": "", "Direction": "В банк", "Sender": "Аутсорсинг", "Receiver": "Поляк Валентина Николаевна", "CreateDate": "2015-11-10T12:31:04", "SendDate": "2015-11-10T12:31:30", "ReceiveDate": null, "Descript": null, "Status": "Отправлено", "subGridOptions": { "enableColumnResizing": true, "enableVerticalScrollbar": 0, "enableFiltering": true, "columnDefs": [], "data": []} }, { "Id": 3, "User": "Балабаева Юлия Сергеевна", "UserDep3": "Объединенный департамент №3", "UserDep7": "Управление продаж г. Бердск", "Direction": "В банк", "Sender": "Аутсорсинг", "Receiver": "Поляк Валентина Николаевна", "CreateDate": "2015-11-10T13:18:01", "SendDate": "2015-11-10T13:18:21", "ReceiveDate": null, "Descript": "тест", "Status": "Отправлено", "subGridOptions": { "enableColumnResizing": true, "enableVerticalScrollbar": 0, "enableFiltering": true, "columnDefs": [], "data": []} }, { "Id": 7, "User": "Абаев Валерий Ринатович", "UserDep3": "Объединенный департамент №2", "UserDep7": "Управление общей безопасности г. Краснодар", "Direction": "В банк", "Sender": "Аутсорсинг", "Receiver": "Поляк Валентина Николаевна", "CreateDate": "2015-11-10T13:37:16", "SendDate": "2015-11-10T13:37:16", "ReceiveDate": null, "Descript": "тест", "Status": "Отправлено", "subGridOptions": { "enableColumnResizing": true, "enableVerticalScrollbar": 0, "enableFiltering": true, "columnDefs": [], "data": [{ "DocumentName": "Тестовый документ", "DocumentSended": true, "DocumentReceived": false}]} }, { "Id": 8, "User": "Абасов Низам Джавидович", "UserDep3": "Дирекция ЮЖНАЯ", "UserDep7": "Отдел технического сопровождения г. Краснодар", "Direction": "В банк", "Sender": "Аутсорсинг", "Receiver": "Поляк Валентина Николаевна", "CreateDate": "2015-11-10T16:14:45", "SendDate": "2015-11-10T16:14:45", "ReceiveDate": null, "Descript": "тест", "Status": "Отправлено", "subGridOptions": { "enableColumnResizing": true, "enableVerticalScrollbar": 0, "enableFiltering": true, "columnDefs": [], "data": [{ "DocumentName": "Тестовый документ", "DocumentSended": true, "DocumentReceived": false }, { "DocumentName": "Тестовый приказ", "DocumentSended": true, "DocumentReceived": false }, { "DocumentName": "Договор", "DocumentSended": true, "DocumentReceived": false}]} }, { "Id": 9, "User": "Кутлина Елена Николаевна", "UserDep3": "Объединенный департамент №3", "UserDep7": "Отдел по работе с клиентами г. Бердск", "Direction": "В банк", "Sender": "Аутсорсинг", "Receiver": "Рогова Алина Евгеньевна", "CreateDate": "2015-11-10T16:36:31", "SendDate": "2015-11-10T16:36:31", "ReceiveDate": null, "Descript": "еееее", "Status": "Отправлено", "subGridOptions": { "enableColumnResizing": true, "enableVerticalScrollbar": 0, "enableFiltering": true, "columnDefs": [], "data": [{ "DocumentName": "Тестовый документ", "DocumentSended": true, "DocumentReceived": false }, { "DocumentName": "Тестовый приказ", "DocumentSended": true, "DocumentReceived": false }, { "DocumentName": "Договор", "DocumentSended": true, "DocumentReceived": false}]} }, { "Id": 10, "User": "Поляк Валентина Николаевна", "UserDep3": "Объединенный департамент №5", "UserDep7": "Управление контроля и оплаты АХР г. Москва", "Direction": "В банк", "Sender": "Аутсорсинг", "Receiver": "Рогозина Ирина Егоровна", "CreateDate": "2015-11-11T13:50:54", "SendDate": "2015-11-11T13:50:54", "ReceiveDate": null, "Descript": "тест", "Status": "Отправлено", "subGridOptions": { "enableColumnResizing": true, "enableVerticalScrollbar": 0, "enableFiltering": true, "columnDefs": [], "data": [{ "DocumentName": "Тестовый документ", "DocumentSended": true, "DocumentReceived": false }, { "DocumentName": "Тестовый приказ", "DocumentSended": true, "DocumentReceived": false }, { "DocumentName": "Договор", "DocumentSended": true, "DocumentReceived": false}]}}] });
   */
    $scope.GridOptions.onRegisterApi = function (gridApi) {
        $scope.gridApi = gridApi;
        $scope.gridApi.expandable.expandAllRows();
    };

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
});
console.log('DocumentMovementsList module loaded...');
$(document).ready(function () {
    $('.ui-grid').height('auto'); $('.ui-grid-viewport').height('auto');
    setActiveMenuItem("documentMovements"); 
});