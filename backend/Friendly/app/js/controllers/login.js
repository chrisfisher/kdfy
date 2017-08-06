
angular.module('friendlyApp').controller('loginController', ['$scope', '$http', '$location', 'authenticationService',
    function ($scope, $http, $location, authenticationService) {

        angular.extend($scope, {
            userCredentials: {
                username: "",
                password: "",
                grant_type: "password"
            },            
            login: function ($event) {
                if (!$scope.validateLogin())
                    return;
                var btn = $('#friendly-login-button');
                btn.button('loading');
                authenticationService.login($scope.userCredentials).then(function () {
                    $location.path('/locations');
                });                
            },
            validateLogin: function () {
                if (!$scope.userCredentials.username || $scope.userCredentials.username == "")
                    return false;
                if (!$scope.userCredentials.password || $scope.userCredentials.password == "")
                    return false;
                return true;
            },
            hideNavButtons: function () {
                $('.nav-sidebar li').hide();
                $('.navbar-form').hide();                
            }
        });

        $scope.hideNavButtons();
        
    }]);