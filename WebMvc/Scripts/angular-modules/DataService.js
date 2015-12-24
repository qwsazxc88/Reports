function ShowFlash(IsShow)  //показываем индикатор загрузки
{
    var ind = $("#LoadingIndicator2");
    if (ind == undefined) return;
    if (!IsShow) {
        $("#LoadingIndicator2").removeClass("loading2");
        $("#DivIndicator").dialog("close");
        $("#DivIndicator").hide();
    }
    else {
        $("#LoadingIndicator2").addClass("loading2");
        $("#DivIndicator").show();
        $("#DivIndicator").dialog(
            { // initialize dialog box
                autoOpen: true,
                modal: true,
                // fix IE6  
                bgiframe: true,
                draggable: false,
                resizable: false,
                width: 300,
                height: 100,
                closeOnEscape: false,
                close: function (event, ui) {
                    //$(this).dialog("destroy").remove();
                }
            });
        $(".ui-dialog-titlebar").hide();
        $(".ui-dialog-titlebar-close").hide();
    }
}
console.log('Service module loading...');
var ServiceModule = angular.module('ServiceModule', []);
ServiceModule.factory('dataService', function ($http, $q) {
    return {
        PostPromise: function (url, query) {
            ShowFlash(true);
            var deferred = $q.defer();
            $http({ method: 'POST', url: url, data: query }).
                 success(function (data, status, headers, config) {
                     ShowFlash(false);
                     deferred.resolve(data);
                 }).
                error(function (data, status, headers, config) {
                    ShowFlash(false);
                    deferred.reject(status);
                });
            return deferred.promise;
        },
        Get: function (url, query, success, fail) {
            ShowFlash(true);
            $http.get(url, query).
                  success(function (data, status, headers, config) {
                      ShowFlash(false);
                      success(data);
                  }).
                  error(function (data, status, headers, config) {
                      ShowFlash(false);
                      fail('Ошибка при передачи данных.');
                  });
        },
              Post: function (url, query, success, fail) {
                  ShowFlash(true);
            $http.post(url, query).
                  success(function (data, status, headers, config) {
                      ShowFlash(false);
                      success(data);
                  }).
                  error(function (data, status, headers, config) {
                      ShowFlash(false);
                      fail('Ошибка при передачи данных.');
                  });
        }
    };
});
      console.log('Service module loaded');