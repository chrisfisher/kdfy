
angular.module('friendlyApp').controller('editLocationController', ['$scope', '$http', '$location', '$route', '$q', 'authenticationService', 'locationTypeDataService', 'locationDataService', 'uiGmapGoogleMapApi', 'Upload', '$timeout', 'imageDataService',
    function ($scope, $http, $location, $route, $q, authenticationService, locationTypeDataService, locationDataService, GoogleMapApi, Upload, $timeout, imageDataService) {

        //angular.extend($scope, {
        //    locationRequest: {
        //        locationId: 0,
        //        locationName: "",
        //        locationTypeId: 0,                
        //        googlePlaceId: "",
        //        googlePlaceName: "",
        //        googlePlaceAddress: "",
        //        localSecrets: "",
        //        imageLinks: []
        //    },
        //    locationTypes: [],
        //    locationImages: [],
        //    dropdownDisplayText: "Select location type",
        //    selectedPlace: {},
        //    selectedLocationType: {},
        //    files: [],
        //    getLocationTypes: function () {
        //        var deferred = $q.defer();
        //        locationTypeDataService.getLocationTypes().then(function (locationTypes) {
        //            $scope.locationTypes = locationTypes;
        //            deferred.resolve(locationTypes);
        //        });
        //        return deferred.promise;
        //    },
        //    selectLocationType: function (locationType) {
        //        $scope.locationRequest.locationTypeId = locationType.id;
        //        $scope.dropdownDisplayText = locationType.name;
        //        $scope.selectedLocationType = locationType;
        //    },
        //    getLocation: function () {
        //        var deferred = $q.defer();
        //        locationDataService.getLocation($route.current.params.id).then(function (location) {
        //            $scope.locationRequest.locationName = location.name;
        //            $scope.locationRequest.localSecrets = location.localSecrets;
        //            $scope.locationRequest.imageLinks = location.imageLinks;
        //            $scope.updateImages(location.imageLinks);
        //            deferred.resolve(location);
        //        });
        //        return deferred.promise;
        //    },
        //    saveLocation: function () {
        //        $scope.locationRequest.latitude = $scope.map.center.latitude;
        //        $scope.locationRequest.longitude = $scope.map.center.longitude;
        //        $scope.locationRequest.googlePlaceId = $scope.selectedPlace.id;
        //        $scope.locationRequest.googlePlaceName = $scope.selectedPlace.name;
        //        $scope.locationRequest.googlePlaceAddress = $scope.selectedPlace.address;
        //        if (!$scope.isValidRequest()) return;
        //        if ($route.current.params.id == 'new') {
        //            return locationDataService.addLocation($scope.locationRequest);
        //        } else {
        //            $scope.locationRequest.locationId = $route.current.params.id;
        //            return locationDataService.editLocation($scope.locationRequest);
        //        }
        //    },
        //    isValidRequest: function () {
        //        if ($scope.locationRequest.locationTypeId <= 0) return false;
        //        if ($scope.locationRequest.locationName == "") return false;
        //        return true;
        //    },
        //    updateImages: function (imageLinks) {
        //        $scope.locationImages = _.map(imageLinks, function (imageLink) {
        //            return {
        //                id: imageLink.id,
        //                url: "https://kdfy.blob.core.windows.net/location-images/" + imageLink.id + "." + imageLink.fileType
        //            };
        //        });
        //    },
        //    uploadFiles: function (files, errFiles) {
        //        if (errFiles.length > 0) {
        //            alert('Error uploading file. Try a size less than 500kb.');
        //            return;
        //        }
        //        var btn = $('#edit-upload-button');
        //        btn.button('loading');
        //        $scope.files = files;
        //        $scope.errFiles = errFiles;
        //        angular.forEach(files, function (file) {
        //            var reader = new FileReader();
        //            reader.onload = $scope.imageIsLoaded;
        //            reader.readAsDataURL(file);
        //            file.upload = Upload.upload({
        //                url: '/api/images/add',
        //                data: { file: file }
        //            });
        //            file.upload.then(function (response) {
        //                $timeout(function () {
        //                    file.result = response.data;
        //                    $scope.locationRequest.imageLinks.push({ id: file.result.imageId, fileType: file.result.fileType });
        //                    $scope.saveLocation().then(function (result) {                                
        //                        $scope.locationImages.push({
        //                            id: file.result.imageId,
        //                            url: "https://kdfy.blob.core.windows.net/location-images/" + file.result.imageId + "." + file.result.fileType
        //                        });
        //                        btn.button('reset');
        //                    });
        //                });
        //            }, function (response) {
        //                if (response.status > 0)
        //                    $scope.errorMsg = response.status + ': ' + response.data;
        //            }, function (evt) {
        //                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
        //            });
        //        });
        //    },
        //    imageIsLoaded: function (e) {               
        //    },
        //    deleteImage: function (id) {
        //        var btn = $('#' + id);
        //        btn.button('loading');
        //        imageDataService.deleteImage(id).then(function () {
        //            _.remove($scope.locationRequest.imageIds, function (imageId) { return imageId == id; });
        //            _.remove($scope.locationImages, function (locationImage) { return locationImage.id == id; });
        //            btn.button('reset');
        //        }).catch(function (error) {
        //            alert(error);
        //        });
        //    },
        //    places: [],
        //    selected: {
        //        options: {
        //            visible: false

        //        },
        //        templateurl: 'window.tpl.html',
        //        templateparameter: {}
        //    },
        //    map: {
        //        control: {},
        //        center: {
        //            latitude: -36.8671465,
        //            longitude: 174.7687427
        //        },
        //        zoom: 12,
        //        dragging: false,
        //        bounds: {},
        //        markers: [],
        //        idkey: 'place_id',
        //        events: {
        //            idle: function (map) {

        //            },
        //            dragend: function (map) {
        //                //update the search box bounds after dragging the map
        //                var bounds = map.getBounds();
        //                var ne = bounds.getNorthEast();
        //                var sw = bounds.getSouthWest();
        //                $scope.searchbox.options.bounds = new google.maps.LatLngBounds(sw, ne);
        //                //$scope.searchbox.options.visible = true;
        //            }
        //        }
        //    },
        //    searchbox: {
        //        template: 'searchbox.tpl.html',
        //        position: 'top-left',
        //        options: {
        //            bounds: {},
        //            visible: true
        //        },
        //        events: {
        //            places_changed: function (searchBox) {

        //                places = searchBox.getPlaces()

        //                if (places.length == 0) {
        //                    return;
        //                }

        //                $scope.selectedPlace = {
        //                    id: places[0].id,
        //                    name: places[0].name,
        //                    address: places[0].formatted_address,
        //                    reviews: _.map(places[0].reviews, function (review) {
        //                        return {
        //                            author: review.author_name,
        //                            text: review.text
        //                        }
        //                    }).filter(function (review) {
        //                        return review.text != "";
        //                    })
        //                };

        //                newMarkers = [];

        //                var bounds = new google.maps.LatLngBounds();
        //                for (var i = 0, place; place = places[i]; i++) {
        //                    var marker = {
        //                        id: i,
        //                        place_id: place.place_id,
        //                        name: place.name,
        //                        latitude: place.geometry.location.lat(),
        //                        longitude: place.geometry.location.lng(),
        //                        options: {
        //                            visible: false
        //                        },
        //                        templateurl: 'window.tpl.html',
        //                        templateparameter: place
        //                    };
        //                    newMarkers.push(marker);

        //                    bounds.extend(place.geometry.location);
        //                }

        //                $scope.map.bounds = {
        //                    northeast: {
        //                        latitude: bounds.getNorthEast().lat(),
        //                        longitude: bounds.getNorthEast().lng()
        //                    },
        //                    southwest: {
        //                        latitude: bounds.getSouthWest().lat(),
        //                        longitude: bounds.getSouthWest().lng()
        //                    }
        //                }

        //                _.each(newMarkers, function (marker) {
        //                    marker.closeClick = function () {
        //                        $scope.selected.options.visible = false;
        //                        marker.options.visble = false;
        //                        return $scope.$apply();
        //                    };
        //                    marker.onClicked = function () {
        //                        $scope.selected.options.visible = false;
        //                        $scope.selected = marker;
        //                        $scope.selected.options.visible = true;
        //                    };
        //                });

        //                $scope.map.markers = newMarkers;
        //            }
        //        }
        //    }

        //});
        
        //if (authenticationService.isAuthenticated()) {
        //    $scope.getLocationTypes();
        //    if ($route.current.params.id != 'new') {
        //        $scope.getLocation().then(function (location) {
        //            $scope.selectLocationType(location.locationType);
        //            $scope.map.center.latitude = location.latitude;
        //            $scope.map.center.longitude = location.longitude;
        //            $scope.selectedPlace.id = location.googlePlaceId;
        //            $scope.selectedPlace.name = location.googlePlaceName;
        //            $scope.selectedPlace.address = location.googlePlaceAddress;
        //        });
        //    }
        //}
               
        //GoogleMapApi.then(function (maps) {
        //    maps.visualRefresh = true;
        //    $scope.defaultBounds = new google.maps.LatLngBounds(
        //      new google.maps.LatLng(-36.8671465, 174.7687427),
        //      new google.maps.LatLng(-36.8671465, 174.7687427));
            
        //    $scope.map.bounds = {
        //        northeast: {
        //            latitude: $scope.defaultBounds.getNorthEast().lat(),
        //            longitude: $scope.defaultBounds.getNorthEast().lng()
        //        },
        //        southwest: {
        //            latitude: $scope.defaultBounds.getSouthWest().lat(),
        //            longitude: -$scope.defaultBounds.getSouthWest().lng()
        //        }
        //    }
        //    $scope.searchbox.options.bounds = new google.maps.LatLngBounds($scope.defaultBounds.getNorthEast(), $scope.defaultBounds.getSouthWest());
        //});
        
    }]);

angular.module('friendlyApp').controller('windowController', function ($scope) {
    $scope.place = {};
    $scope.showPlaceDetails = function (param) {
        $scope.place = param;
    }
});