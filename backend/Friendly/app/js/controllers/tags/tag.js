
angular.module('friendlyApp').controller('tagController', ['$scope', '$route', 'authenticationService', 'tagDataService',
    function ($scope, $route, authenticationService, tagDataService) {

        angular.extend($scope, {
            tag: [],
            getTag: function () {
                tagDataService.getTag($route.current.params.id).then(function (tag) {
                    $scope.tag = tag;
                });
            },            
            deleteTag: function () {
                tagDataService.deleteTag($route.current.params.id);
            },
            updateLeftNav: function () {
                $('.nav-sidebar li').removeClass('active');
                $('#checks-nav-link').addClass('active');
            }
        });

        if (authenticationService.isAuthenticated()) {
            $scope.getTag();
        }

    }]);