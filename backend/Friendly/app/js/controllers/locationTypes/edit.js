
angular.module('friendlyApp').controller('editLocationTypeController', ['$scope', '$http', '$location', '$route', '$q','authenticationService', 'locationTypeDataService', 'checkDataService', 'ratingDataService',
    function ($scope, $http, $location, $route, $q, authenticationService, locationTypeDataService, checkDataService, ratingDataService) {

        angular.extend($scope, {
            locationTypeRequest: {
                locationTypeId: $route.current.params.id,
                locationTypeName: "",
                checkIds: [],
                ratingIds: []
            },
            checks: {},
            ratings: {},
            checkGroups: {},
            ratingGroups: {},
            getChecks: function () {
                var deferred = $q.defer();
                checkDataService.getChecks().then(function (checks) {
                    _.map(checks, function (check) {
                        check.isSelected = false;
                    });                    
                    var checkGroups =
                        _.chain(checks)
                        .groupBy("tag.description")
                        .pairs()
                        .map(function (item) {
                            return _.object(_.zip(["tag", "checks"], item));
                        }).value();
                    $scope.checkGroups = checkGroups;
                    $scope.checks = checks;
                    deferred.resolve(checks);
                });
                return deferred.promise;
            },
            getRatings: function () {
                var deferred = $q.defer();
                ratingDataService.getRatings().then(function (ratings) {
                    _.map(ratings, function (rating) {
                        rating.isSelected = false;
                    });                    
                    var ratingGroups =
                        _.chain(ratings)
                        .groupBy("tag.description")
                        .pairs()
                        .map(function (item) {
                            return _.object(_.zip(["tag", "ratings"], item));
                        }).value();
                    $scope.ratingGroups = ratingGroups;
                    $scope.ratings = ratings;
                    deferred.resolve(ratings);
                });
                return deferred.promise;
            },            
            getLocationType: function () {
                var deferred = $q.defer();
                locationTypeDataService.getLocationType($route.current.params.id).then(function (locationType) {
                    $scope.locationTypeRequest.locationTypeName = locationType.name;
                    deferred.resolve(locationType);
                });
                return deferred.promise;
            },
            saveLocationType: function () {
                if (!$scope.isValidRequest()) return;
                $scope.locationTypeRequest.checkIds = _.filter($scope.checks, function (check) {
                    return check.isSelected;
                }).map(function (check) {
                    return check.id;
                });
                $scope.locationTypeRequest.ratingIds = _.filter($scope.ratings, function (rating) {
                    return rating.isSelected;
                }).map(function (rating) {
                    return rating.id;
                });
                if ($scope.locationTypeRequest.locationTypeId == "new") {
                    locationTypeDataService.addLocationType($scope.locationTypeRequest);
                } else {
                    locationTypeDataService.editLocationType($scope.locationTypeRequest);
                }
                
            },
            isValidRequest: function () {
                if ($scope.locationTypeRequest.locationTypeName == "") return false;
                return true;
            }
        });

        if (authenticationService.isAuthenticated()) {
            var isNewLocationType = $route.current.params.id == "new";
            var queries = [$scope.getChecks(), $scope.getRatings()];
            if (!isNewLocationType) {
                queries.push($scope.getLocationType());
            }            
            $q.all(queries).then(function (results) {
                if (!isNewLocationType) {
                    var checks = results[0];
                    var ratings = results[1];
                    var locationType = results[2];
                    _.forEach(checks, function (check) {
                        if (_.some(locationType.checks, 'id', check.id)) {
                            check.isSelected = true;
                        }
                    });
                    _.forEach(ratings, function (rating) {
                        if (_.some(locationType.ratings, 'id', rating.id)) {
                            rating.isSelected = true;
                        }
                    });
                }                
            });
        }

    }]);