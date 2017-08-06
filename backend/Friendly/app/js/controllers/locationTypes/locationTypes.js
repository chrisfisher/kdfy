
angular.module('friendlyApp').controller('locationTypesController', ['$scope', '$http', '$location', 'authenticationService', 'locationTypeDataService',
    function ($scope, $http, $location, authenticationService, locationTypeDataService) {

        angular.extend($scope, {
            locationTypes: [],
            getLocationTypes: function () {
                locationTypeDataService.getLocationTypes().then(function (locationTypes) {
                    $scope.locationTypes = locationTypes;
                });
            },
            updateLeftNav: function () {
                $('.nav-sidebar li').removeClass('active');
                $('#location-types-nav-link').addClass('active');
            }
        });
               
        if (authenticationService.isAuthenticated()) {
            $scope.getLocationTypes();
            $scope.updateLeftNav();
        }

    }]);