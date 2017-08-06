
angular.module('friendlyApp').controller('ratingController', ['$scope', '$route', 'authenticationService', 'ratingDataService',
    function ($scope, $route, authenticationService, ratingDataService) {

        angular.extend($scope, {
            rating: {},
            getRating: function () {
                ratingDataService.getRating($route.current.params.id).then(function (rating) {
                    $scope.rating = rating;
                });
            },
            deleteRating: function () {
                ratingDataService.deleteRating($route.current.params.id);
            },
            updateLeftNav: function () {
                $('.nav-sidebar li').removeClass('active');
                $('#checks-nav-link').addClass('active');
            }
        });

        if (authenticationService.isAuthenticated()) {
            $scope.getRating();
        }

    }]);