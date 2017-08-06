
angular.module('friendlyApp').service('imageDataService', ['$http', '$q', '$location',
function ($http, $q, $location) {
    
    this.deleteImage = function (imageId) {
        var deferred = $q.defer();
        $http({
            method: 'DELETE',
            url: '/api/images/delete/' + imageId
        }).then(function (result) {
            if (result.data) {
                deferred.resolve(result.data);
            } else {
                alert("Unable to delete image.");
                deferred.reject();
            }
        }, function (error) {
            if (error.status == 401) {
                $location.path('/login');
            }
            deferred.reject(error);
        });
        return deferred.promise;
    }

}]);