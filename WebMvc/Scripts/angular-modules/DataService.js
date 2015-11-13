console.log('Service module loading...');
var ServiceModule = angular.module('ServiceModule', []);
ServiceModule.factory('dataService', function ($http, $q) {
    return {
        PostPromise: function (url, query) {
            var deferred = $q.defer();
            $http({ method: 'POST', url: url, data: query }).
                 success(function (data, status, headers, config) {
                     deferred.resolve(data);
                 }).
                error(function (data, status, headers, config) {
                    deferred.reject(status);
                });
            return deferred.promise;
        },
        Get: function (url, query, success, fail) {
            $http.get(url, query).
                  success(function (data, status, headers, config) {
                      success(data);                      
                  }).
                  error(function (data, status, headers, config) {
                      fail('Ошибка при передачи данных.');
                  });
        },
        Post: function (url, query, success, fail) {
            $http.post(url, query).
                  success(function (data, status, headers, config) {
                      success(data);                      
                  }).
                  error(function (data, status, headers, config) {
                      fail('Ошибка при передачи данных.');
                  });
        }
    };
      });
      console.log('Service module loaded');