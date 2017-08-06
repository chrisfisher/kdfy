
angular.module('friendlyApp').service('locationTypeDataService', ['$http', '$q', '$location', 'authenticationService',
    function ($http, $q, $location, authenticationService) {

        this.getLocationTypes = function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/locationTypes',
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data.length > 0) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch locations types.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.getLocationType = function (locationTypeId) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/locationTypes/' + locationTypeId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch location type.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.deleteLocationType = function (locationTypeId) {
            var deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: '/api/locationTypes/delete/' + locationTypeId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    $location.path('/locationTypes');
                } else {
                    alert("Unable to delete location type. Make sure no locations exist with this type.");
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

        this.addLocationType = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/locationTypes/add',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/locationTypes');
                } else {
                    deferred.reject("Unable to add location type.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.editLocationType = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/locationTypes/edit',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/locationTypes');
                } else {
                    deferred.reject("Unable to edit location type.");
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