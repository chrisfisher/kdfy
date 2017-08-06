
angular.module('friendlyApp').controller('checksController', ['$scope', '$http', 'authenticationService', 'checkDataService',
    function ($scope, $http, authenticationService, checkDataService) {

        angular.extend($scope, {
            checks: [],
            getChecks: function () {
                checkDataService.getChecks().then(function (checks) {
                    $scope.checks = checks;
                });
            },
            updateLeftNav: function () {
                $('.nav-sidebar li').removeClass('active');
                $('#checks-nav-link').addClass('active');
            }
        });

        if (authenticationService.isAuthenticated())
            $scope.getChecks();
        
    }]);