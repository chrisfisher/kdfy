
angular.module('friendlyApp').controller('logoutController', ['$scope', '$http', '$location',
    function ($scope, $http, $location) {

        angular.extend($scope, {
            logout: function () {
                sessionStorage.removeItem('friendlyTokenKey');
                $location.path('/login');
            }
        });

        $scope.logout();
        
    }]);