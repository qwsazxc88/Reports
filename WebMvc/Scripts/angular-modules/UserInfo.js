console.log('UserInfo module loading...');
var UserInfoModule = angular.module('UserInfoModule', []);
/*UserInfoModule.directive("userInfo", function () {
    return {
        link: function (scope, element, attrs) {
            scope.data = scope[attrs["userInfo"]];
        },
        restrict: "A",
        template: "<fieldset class='bordered doc-wrap-blue panel'>" +
                    "<legend>{{data.Label}}</legend>" +
                    "<div class='field-wrap'><label>ФИО сотрудника:</label><span>{{data.Name}}</span></div>" +
                    "<div class='field-wrap'><label>Организация:</label><span>{{data.Organization}}</span></div>" +
                    "<div class='field-wrap'><label>Должность:</label><span>{{data.PositionName}}</span></div>" +
                    "<div class='field-wrap'><label>Департамент 3 ур.:</label><span>{{data.Dep3Name}}</span></div>" +
                    "<div class='field-wrap'><label>Подразделение 7 ур.:</label><span>{{data.Dep7Name}}</span></div>" +
                    "<div class='field-wrap'><label>Руководители:</label><span ng-repeat='chief in data.Chiefs'>{{chief}}, </span></div>" +
                    "<div class='field-wrap'><label>Руководители:</label><span ng-repeat='personnel in data.Personnels'>{{personnel}}, </span></div>" +
                    "</fieldset>"
    }
});*/

UserInfoModule.directive("userInfo", function ($compile) {
    return function (scope, element, attrs) {
        var content = "<fieldset class='bordered doc-wrap-blue panel'>" +
                    "<legend>{{Label}}</legend>" +
                    "<div class='field-wrap'><label>ФИО сотрудника:</label><span>{{Name}}</span></div>" +
                    "<div class='field-wrap'><label>Организация:</label><span>{{Organization}}</span></div>" +
                    "<div class='field-wrap'><label>Должность:</label><span>{{PositionName}}</span></div>" +
                    "<div class='field-wrap'><label>Департамент 3 ур.:</label><span>{{Dep3Name}}</span></div>" +
                    "<div class='field-wrap'><label>Подразделение 7 ур.:</label><span>{{Dep7Name}}</span></div>" +
                    "<div class='field-wrap'><label>Руководители:</label><span ng-repeat='chief in Chiefs'>{{chief}}, </span></div>" +
                    "<div class='field-wrap'><label>Руководители:</label><span ng-repeat='personnel in Personnels'>{{personnel}}, </span></div>" +
                    "</fieldset>";
        var answersElem = angular.element(content);
        var compileFn = $compile(answersElem);
        compileFn(scope);
        element.append(answersElem);
    }
});