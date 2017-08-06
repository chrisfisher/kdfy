
angular.module('friendlyApp').service('authenticationService', ['$location', '$q', '$http',
    function ($location, $q, $http) {
      
        this.login = function (userCredentials) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/token',
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: userCredentials
            }).then(function (result) {
                sessionStorage.setItem('friendlyUserName', result.data.userName);
                sessionStorage.setItem('friendlyTokenKey', result.data.access_token);
                deferred.resolve();
            }, function (error) {
                var btn = $('#friendly-login-button');
                btn.button('reset');
                alert(error.data.error_description);
                deferred.reject();
            });
            return deferred.promise;
        };

        this.isAuthenticated = function () {
            var tokenKey = sessionStorage.getItem('friendlyTokenKey');
            var isAuthenticated = tokenKey != null && tokenKey != 'undefined' && tokenKey != "";
            if (!isAuthenticated) {
                $('.nav-sidebar li').hide();
                $('.navbar-form').hide();
                $location.path('/login');
            } else {
                $('.nav-sidebar li').show();
                $('.navbar-form').show();
            }
            return isAuthenticated;
        };

        this.getAuthToken = function () {
            return sessionStorage.getItem('friendlyTokenKey');
        };

    }]);