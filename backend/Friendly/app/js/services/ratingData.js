
angular.module('friendlyApp').service('ratingDataService', ['$http', '$q', '$location', 'authenticationService',
    function ($http, $q, $location, authenticationService) {

        this.getRatings = function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/ratings',
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data.length > 0) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch ratings.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }
               
        this.getRating = function (ratingId) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/ratings/' + ratingId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch rating.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.deleteRating = function (ratingId) {
            var deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: '/api/ratings/delete/' + ratingId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    $location.path('/ratings');
                } else {
                    alert("Unable to delete rating. Make sure it is not being used.");
                    deferred.reject();
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.addRating = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/ratings/add',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/ratings');
                } else {
                    deferred.reject("Unable to add rating.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.editRating = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/ratings/edit',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/ratings');
                } else {
                    deferred.reject("Unable to edit rating.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

    }]);