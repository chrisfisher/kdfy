
angular.module('friendlyApp').controller('reviewLocationController', ['$scope', '$http', '$location', '$route', 'authenticationService', 'locationDataService',
    function ($scope, $http, $location, $route, authenticationService, locationDataService) {

        angular.extend($scope, {
            location: {},
            checks: {},
            checkGroups: {},
            ratings: {},
            ratingGroups: {},
            maxRating: 5,
            activeRatingId: 0,
            activeRatingValue:0,
            reviewLocationRequest: {
                locationId: 0,
                ratingScores: [],
                checkScores: [],
                userName: sessionStorage.getItem('friendlyUserName'),
                comment: ""
            },
            getLocation: function () {
                locationDataService.getLocation($route.current.params.id).then(function (location) {
                    $scope.location = location;
                    $scope.reviewLocationRequest.locationId = location.id;
                    _.map(location.locationType.checks, function (check) {
                        check.value = false;
                    });
                    var checks = location.locationType.checks;
                    var checkGroups =
                        _.chain(checks)
                        .groupBy("tag.description")
                        .pairs()
                        .map(function (item) {
                            return _.object(_.zip(["tag", "checks"], item));
                        }).value();
                    $scope.checkGroups = checkGroups;
                    $scope.checks = checks;
                    _.map(location.locationType.ratings, function (rating) {
                        rating.value = 0;
                    });
                    var ratings = location.locationType.ratings;
                    var ratingGroups =
                        _.chain(ratings)
                        .groupBy("tag.description")
                        .pairs()
                        .map(function (item) {
                            return _.object(_.zip(["tag", "ratings"], item));
                        }).value();
                    $scope.ratingGroups = ratingGroups;
                    $scope.ratings = ratings;
                });
            },            
            onRatingHoverStart: function (value, ratingId) {
                $scope.activeRatingId = ratingId;
                $scope.activeRatingValue = value;
            },
            onRatingHoverStop: function (ratingId) {
                $scope.activeRatingId = 0;
                $scope.activeRatingValue = 0;
            },            
            ratingStates: [
                { stateOn: 'glyphicon-ok-sign', stateOff: 'glyphicon-ok-circle' },
                { stateOn: 'glyphicon-star', stateOff: 'glyphicon-star-empty' },
                { stateOn: 'glyphicon-heart', stateOff: 'glyphicon-ban-circle' },
                { stateOn: 'glyphicon-heart' },
                { stateOff: 'glyphicon-off' }
            ],
            save: function () {
                $scope.reviewLocationRequest.checkScores = _.map($scope.checks, function (check) {
                    return {
                        checkId: check.id,
                        value: check.value
                    };
                });
                $scope.reviewLocationRequest.ratingScores = _.map($scope.ratings, function (rating) {
                    return {
                        ratingId: rating.id,
                        value: rating.value
                    };
                });
                locationDataService.reviewLocation($scope.reviewLocationRequest);
            }
        });
        
        if (authenticationService.isAuthenticated()) {
            $scope.getLocation();
        }        

    }]);
