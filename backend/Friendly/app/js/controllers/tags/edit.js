
angular.module('friendlyApp').controller('editTagController', ['$scope', '$http', '$location', '$route', 'authenticationService', 'tagDataService',
    function ($scope, $http, $location, $route, authenticationService, tagDataService) {

        angular.extend($scope, {
            tagRequest: {
                tagId: 0,
                tagDescription: ""
            },
            getTag: function () {
                tagDataService.getTag($route.current.params.id).then(function (tag) {
                    $scope.tagRequest.tagDescription = tag.description;
                });
            },
            saveTag: function () {
                if (!$scope.isValidRequest()) return;
                if ($route.current.params.id == 'new') {
                    tagDataService.addTag($scope.tagRequest);
                } else {
                    $scope.tagRequest.tagId = $route.current.params.id;
                    tagDataService.editTag($scope.tagRequest);
                }
            },
            isValidRequest: function () {
                if ($scope.tagRequest.tagDescription == "") return false;
                return true;
            }
        });
        
        if (authenticationService.isAuthenticated()) {
            $scope.getTag();            
        }

    }]);