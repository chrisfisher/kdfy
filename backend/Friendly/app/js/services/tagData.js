
angular.module('friendlyApp').service('tagDataService', ['$http', '$q', '$location', 'authenticationService',
    function ($http, $q, $location, authenticationService) {

        this.getTags = function () {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/tags',
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data.length > 0) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch tags.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.getTag = function (tagId) {
            var deferred = $q.defer();
            $http({
                method: 'GET',
                url: '/api/tags/' + tagId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    deferred.resolve(result.data);
                } else {
                    deferred.reject("Unable to fetch tag.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }
        
        this.deleteTag = function (tagId) {
            var deferred = $q.defer();
            $http({
                method: 'DELETE',
                url: '/api/tags/delete/' + tagId,
                headers: { 'Authorization': 'Bearer ' + authenticationService.getAuthToken() }
            }).then(function (result) {
                if (result.data) {
                    $location.path('/tags');
                } else {
                    alert("Unable to delete tag. Make sure it is not being used.");
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

        this.addTag = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/tags/add',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/tags');
                } else {
                    deferred.reject("Unable to add tag.");
                }
            }, function (error) {
                if (error.status == 401) {
                    $location.path('/login');
                }
                deferred.reject();
            });
            return deferred.promise;
        }

        this.editTag = function (request) {
            var deferred = $q.defer();
            $http({
                method: 'POST',
                url: '/api/tags/edit',
                headers: {
                    'Authorization': 'Bearer ' + authenticationService.getAuthToken()
                },
                data: request
            }).then(function (result) {
                if (result.data) {
                    $location.path('/tags');
                } else {
                    deferred.reject("Unable to edit tag.");
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