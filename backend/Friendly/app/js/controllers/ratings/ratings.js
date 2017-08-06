
angular.module('friendlyApp').controller('ratingsController', ['$scope', '$http', 'authenticationService', 'ratingDataService',
    function ($scope, $http, authenticationService, ratingDataService) {

        angular.extend($scope, {
            ratings: [],
            getRatings: function () {
                ratingDataService.getRatings().then(function (ratings) {
                    $scope.ratings = ratings;
                });
            },
            updateLeftNav: function () {
                $('.nav-sidebar li').removeClass('active');
                $('#ratings-nav-link').addClass('active');
            }
        });

        if (authenticationService.isAuthenticated())
            $scope.getRatings();
        
    }]);