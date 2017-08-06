
angular.module('friendlyApp').service('checkDataService', ['$http', '$q', '$location', 'authenticationService',
function ($http, $q, $location, authenticationService) {

        this.getChecks = function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/checks',
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data.length > 0) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch checks.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.getCheck = function (checkId) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/checks/' + checkId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch check.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.deleteCheck = function (checkId) {
            var deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: '/api/checks/delete/' + checkId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    $location.path('/checks');
                } else {
                    alert("Unable to delete check. Make sure it is not being used.");
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

        this.addCheck = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/checks/add',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/checks');
                } else {
                    deferred.reject("Unable to add check.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.editCheck = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/checks/edit',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/checks');
                } else {
                    deferred.reject("Unable to edit check.");
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