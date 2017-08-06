
angular.module('friendlyApp', ['ngRoute', 'ui.bootstrap', 'uiGmapgoogle-maps', 'ngFileUpload']);

angular.module('friendlyApp').config(['$routeProvider', '$locationProvider', '$rootScopeProvider', 'uiGmapGoogleMapApiProvider',
    function ($routeProvider, $locationProvider, $rootScopeProvider, uiGmapGoogleMapApiProvider) {

        $routeProvider.
            when('/locations', {
                templateUrl: 'js/views/locations/locations.html',
                controller: 'locationsController'
            }).
            when('/locations/:id', {
                templateUrl: 'js/views/locations/location.html',
                controller: 'locationController'
            }).
            when('/locations/:id/edit', {
                templateUrl: 'js/views/locations/edit.html',
                controller: 'editLocationController'
            }).
            when('/locations/:id/review', {
                templateUrl: 'js/views/locations/review.html',
                controller: 'reviewLocationController'
            }).
            when('/locations/:id/reviews', {
                templateUrl: 'js/views/locations/reviews.html',
                controller: 'locationReviewsController'
            }).
            when('/locationTypes', {
                templateUrl: 'js/views/locationTypes/locationTypes.html',
                controller: 'locationTypesController'
            }).
            when('/locationTypes/:id', {
                templateUrl: 'js/views/locationTypes/locationType.html',
                controller: 'locationTypeController'
            }).
            when('/locationTypes/:id/edit', {
                templateUrl: 'js/views/locationTypes/edit.html',
                controller: 'editLocationTypeController'
            }).            
            when('/checks', {
                templateUrl: 'js/views/checks/checks.html',
                controller: 'checksController'
            }).
            when('/checks/:id', {
                templateUrl: 'js/views/checks/check.html',
                controller: 'checkController'
            }).
             when('/checks/:id/edit', {
                 templateUrl: 'js/views/checks/edit.html',
                 controller: 'editCheckController'
             }).
            when('/ratings', {
                templateUrl: 'js/views/ratings/ratings.html',
                controller: 'ratingsController'
            }).
            when('/ratings/:id', {
                templateUrl: 'js/views/ratings/rating.html',
                controller: 'ratingController'
            }).
            when('/ratings/:id/edit', {
                templateUrl: 'js/views/ratings/edit.html',
                controller: 'editRatingController'
            }).
            when('/tags', {
                templateUrl: 'js/views/tags/tags.html',
                controller: 'tagsController'
            }).
            when('/tags/:id', {
                templateUrl: 'js/views/tags/tag.html',
                controller: 'tagController'
            }).
            when('/tags/:id/edit', {
                templateUrl: 'js/views/tags/edit.html',
                controller: 'editTagController'
            }).
            when('/login', {
                templateUrl: 'js/views/login.html',
                controller: 'loginController'
            }).
            when('/logout', {
                templateUrl: 'js/views/logout.html',
                controller: 'logoutController'
            }).
        otherwise({
            redirectTo: '/login'
        });

        uiGmapGoogleMapApiProvider.configure({
            key: 'AIzaSyBZgsya5mK7SUNgWjT9Zxj0aUF7hxltQu0',
            libraries: 'places'
        });

    }]);

angular.module('friendlyApp').run(['$templateCache', function ($templateCache) {
    $templateCache.put('searchbox.tpl.html', '<input id="pac-input" class="pac-controls" type="text" placeholder="Search">');
    $templateCache.put('window.tpl.html', '<div ng-controller="windowController" ng-init="showPlaceDetails(parameter)">{{place.name}}</div>');
}])
