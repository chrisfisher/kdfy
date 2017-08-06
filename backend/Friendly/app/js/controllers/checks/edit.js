
angular.module('friendlyApp').controller('editCheckController', ['$scope', '$http', '$location', '$route', '$q', 'authenticationService', 'tagDataService', 'checkDataService',
    function ($scope, $http, $location, $route, $q, authenticationService, tagDataService, checkDataService) {

        angular.extend($scope, {
            checkRequest: {
                checkId: 0,
                checkDescription: "",
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
                $scope.checkRequest.tagId = tag.id;
                $scope.dropdownDisplayText = tag.description;
                $scope.selectedTag = tag;
            },
            getCheck: function () {
                var deferred = $q.defer();
                checkDataService.getCheck($route.current.params.id).then(function (check) {
                    $scope.checkRequest.checkDescription = check.description;
                    deferred.resolve(check);
                });
                return deferred.promise;
            },
            saveCheck: function () {
                if (!$scope.isValidRequest()) return;
                if ($route.current.params.id == 'new') {
                    checkDataService.addCheck($scope.checkRequest);
                } else {
                    $scope.checkRequest.checkId = $route.current.params.id;
                    checkDataService.editCheck($scope.checkRequest);
                }
            },
            isValidRequest: function () {
                if ($scope.checkRequest.checkDescription == "") return false;
                return true;
            }
        });
        
        if (authenticationService.isAuthenticated()) {
            $scope.getTags();
            if ($route.current.params.id != 'new') {
                $scope.getCheck().then(function (check) {
                    $scope.selectTag(check.tag);
                });
            }
        }

    }]);