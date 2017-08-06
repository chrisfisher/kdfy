
angular.module('friendlyApp').controller('tagsController', ['$scope', '$http', 'authenticationService', 'tagDataService',
    function ($scope, $http, authenticationService, tagDataService) {

        angular.extend($scope, {
            tags: [],
            getTags: function () {
                tagDataService.getTags().then(function (tags) {
                    $scope.tags = tags;
                });
            },
            updateLeftNav: function () {
                $('.nav-sidebar li').removeClass('active');
                $('#tags-nav-link').addClass('active');
            }
        });

        if (authenticationService.isAuthenticated()) {
            $scope.getTags();
        }
        
    }]);