
angular.module('friendlyApp').service('locationDataService', ['$http', '$q', '$location', 'authenticationService',
    function ($http, $q, $location, authenticationService) {
        
        this.getLocations = function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/locations',
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data.length > 0) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch locations.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.getLocation = function (locationId) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/locations/' + locationId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch location.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.deleteLocation = function (locationId) {
            var deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: '/api/locations/delete/' + locationId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    $location.path('/locations');
                } else {
                    deferred.reject("Unable to delete location.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.addLocation = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/locations/add',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/locations');
                } else {
                    deferred.reject("Unable to add location.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.editLocation = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/locations/edit',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    deferred.resolve(result.data);                   
                } else {
                    deferred.reject("Unable to edit location.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.reviewLocation = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/locations/review',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/locations');
                } else {
                    deferred.reject("Unable to review location.");
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