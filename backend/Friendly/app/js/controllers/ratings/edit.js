
angular.module('friendlyApp').controller('editRatingController', ['$scope', '$http', '$location', '$route', '$q', 'authenticationService', 'tagDataService', 'ratingDataService',
    function ($scope, $http, $location, $route, $q, authenticationService, tagDataService, ratingDataService) {

        angular.extend($scope, {
            ratingRequest: {
                ratingId: 0,
                ratingDescription: "",
                tagId: 0
            },
            tags: [],
            dropdownDisplayText: "Select tag",
            selectedTag: {},
            getTags: function () {
                tagDataService.getTags().then(function (tags) {
                    $scope.tags = tags;
                });
            },
            selectTag: function (tag) {
                $scope.ratingRequest.tagId = tag.id;
                $scope.dropdownDisplayText = tag.description;
                $scope.selectedTag = tag;
            },
            getRating: function () {
                var deferred = $q.defer();
                ratingDataService.getRating($route.current.params.id).then(function (rating) {
                    $scope.ratingRequest.ratingDescription = rating.description;
                    deferred.resolve(rating);
                });
                return deferred.promise;
            },
            saveRating: function () {
                if (!$scope.isValidRequest()) return;
                if ($route.current.params.id == 'new') {
                    ratingDataService.addRating($scope.ratingRequest);
                } else {
                    $scope.ratingRequest.ratingId = $route.current.params.id;
                    ratingDataService.editRating($scope.ratingRequest);
                }
            },
            isValidRequest: function () {
                if ($scope.ratingRequest.ratingDescription == "") return false;
                return true;
            }
        });
        
        if (authenticationService.isAuthenticated()) {
            $scope.getTags();
            if ($route.current.params.id != 'new') {
                $scope.getRating().then(function (rating) {
                    $scope.selectTag(rating.tag);
                });
            }
        }

    }]);