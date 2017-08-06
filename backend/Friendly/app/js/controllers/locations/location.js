
angular.module('friendlyApp').controller('locationController', ['$scope', '$route', '$location', 'authenticationService', 'locationDataService',
    function ($scope, $route, $location, authenticationService, locationDataService) {

        angular.extend($scope, {
            location: {},
            getLocation: function () {
                locationDataService.getLocation($route.current.params.id).then(function (location) {
                    $scope.location = location;
                    $scope.updateImages(location.imageLinks)
                });
            },
            deleteLocation: function () {
                locationDataService.deleteLocation($route.current.params.id);
            },
            updateImages: function (imageLinks) {
                $scope.locationImages = _.map(imageLinks, function (imageLink) {
                    return {
                        id: imageLink.id,
                        url: "https://kdfy.blob.core.windows.net/location-images/" + imageLink.id + "." + imageLink.fileType
                    };
                });
            }
        });

        if (authenticationService.isAuthenticated()) {
            $scope.getLocation();
        }

    }]);