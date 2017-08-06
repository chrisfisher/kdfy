
angular.module('friendlyApp').controller('locationsController', ['$scope', '$location', 'authenticationService', 'locationDataService',
    function ($scope, $location, authenticationService, locationDataService) {

        angular.extend($scope, {
            locations: [],
            getLocations: function () {
                locationDataService.getLocations().then(function (locations) {
                    $scope.locations = locations;
                });     
            },
            updateLeftNav: function () {
                $('.nav-sidebar li').removeClass('active');
                $('#locations-nav-link').addClass('active');
            }
        });

        if (!authenticationService.isAuthenticated())
            $location.path('/login');

        $scope.getLocations();
        $scope.updateLeftNav();
        
    }]);