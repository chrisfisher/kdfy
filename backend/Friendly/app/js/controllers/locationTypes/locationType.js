
angular.module('friendlyApp').controller('locationTypeController', ['$scope', '$route', 'authenticationService', 'locationTypeDataService',
    function ($scope, $route, authenticationService, locationTypeDataService) {
        
        angular.extend($scope, {
            locationType: {},
            getLocationType: function () {
                locationTypeDataService.getLocationType($route.current.params.id).then(function (locationType) {
                    $scope.locationType = locationType;
                });
            },
            deleteLocationType: function () {
                locationTypeDataService.deleteLocationType($route.current.params.id);
            }
        });
        
        if (authenticationService.isAuthenticated())
            $scope.getLocationType();

    }]);