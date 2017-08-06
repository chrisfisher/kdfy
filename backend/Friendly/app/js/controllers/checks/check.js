
angular.module('friendlyApp').controller('checkController', ['$scope', '$route', 'authenticationService', 'checkDataService',
    function ($scope, $route, authenticationService, checkDataService) {

        angular.extend($scope, {
            check: {},
            getCheck: function () {
                checkDataService.getCheck($route.current.params.id).then(function (check) {
                    $scope.check = check;
                });
            },
            deleteCheck: function () {
                checkDataService.deleteCheck($route.current.params.id);
            },
            updateLeftNav: function () {
                $('.nav-sidebar li').removeClass('active');
                $('#checks-nav-link').addClass('active');
            }
        });

        if (authenticationService.isAuthenticated()) {
            $scope.getCheck();
        }

    }]);