console.log('Service module loading...');
var ServiceModule = angular.module('ServiceModule', []);
ServiceModule.factory('dataService', function ($http, $q) {
    return {
        
        Get: function (url, data, success, fail) {
            $http.get(url, data).
                  success(function (data, status, headers, config) {
                      if (data.Success == 'Success') {
                          MainLogStorage += 'Get data from ' + url + ' success!';
                          success(data.Result);
                      }
                      else {
                          MainLogStorage += 'Post data to ' + url + ' fails! Error: ' + data.Message;
                          fail(data.Message);
                      }
                  }).
                  error(function (data, status, headers, config) {
                      MainLogStorage += 'Post data to ' + url + ' fails! Data transfer error. ';
                      fail('Ошибка при передачи данных.');
                  });
        },
        Post: function (url, data, success, fail) {
            $http.post(url, data).
                  success(function (data, status, headers, config) {
                      if (data.Success == 'Success') {
                          MainLogStorage += 'Post data to ' + url + ' success!';
                          success(data.Result);
                      }
                      else {
                          MainLogStorage += 'Post data to ' + url + ' fails! Error: ' + data.Message;
                          fail(data.Message);
                      }
                  }).
                  error(function (data, status, headers, config) {
                      MainLogStorage += 'Post data to ' + url + ' fails! Data transfer error. ';
                      fail('Ошибка при передачи данных.');
                  });
        }
    };
      });
      console.log('Service module loaded');