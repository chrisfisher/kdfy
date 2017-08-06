
angular.module('friendlyApp').controller('locationReviewsController', ['$scope', '$location', '$route', 'authenticationService', 'locationDataService',
    function ($scope, $location, $route, authenticationService, locationDataService) {

        angular.extend($scope, {
            location: {},
            getLocation: function () {
                locationDataService.getLocation($route.current.params.id).then(function (location) {
                    _.map(location.reviews, function (review) {
                        review.formattedDate = moment(review.date).format('MMMM Do YYYY, h:mm a');
                    });
                    $scope.location = location;
                });     
            },
            getCheckDescription: function (checkId) {
                var check = _.find($scope.location.locationType.checks, function (check) {
                    return check.id == checkId;
                });
                return check !== null ? check.description : "";
            },
            getRatingDescription: function (ratingId) {
                var rating = _.find($scope.location.locationType.ratings, function (rating) {
                    return rating.id == ratingId;
                });
                return rating != null && rating != "undefined" ? rating.description : "";
            }
        });

        if (authenticationService.isAuthenticated()) {
            $scope.getLocation();
        }
        
    }]);