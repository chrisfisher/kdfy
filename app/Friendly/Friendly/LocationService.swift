//
//  LocationService.swift
//  Friendly
//
//  Created by Chris Fisher on 31/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import CoreLocation

protocol LocationServiceProtocol {

  func start()
  
  func stop()
  
  func getLocation() -> LocationCoordinates
  
}

class LocationService: NSObject, LocationServiceProtocol, CLLocationManagerDelegate {
  
  private var currentLocation: LocationCoordinates?
  
  private lazy var defaultLocation: LocationCoordinates = {
    let defaultLatitude = -36.848461
    let defaultLongitude = 174.763336
    return LocationCoordinates(latitude: defaultLatitude, longitude: defaultLongitude)
  }()
  
  private lazy var locationManager: CLLocationManager = {
    let lm = CLLocationManager()
    lm.delegate = self
    lm.distanceFilter = 500
    return lm
  }()
  
  func start() {
    locationManager.requestWhenInUseAuthorization()
    locationManager.startUpdatingLocation()
  }
  
  func stop() {
    locationManager.stopUpdatingLocation()
  }
  
  func getLocation() -> LocationCoordinates {
    return currentLocation != nil ? currentLocation! : defaultLocation
  }
  
  func locationManager(manager: CLLocationManager, didUpdateLocations locations: [CLLocation]) {
    guard let lastLocation = locations.last else { return }
    currentLocation = LocationCoordinates(latitude: lastLocation.coordinate.latitude, longitude: lastLocation.coordinate.longitude)
  }
  
}

class LocationCoordinates {

  var latitude: Double
  var longitude: Double
  
  init(latitude: Double, longitude: Double) {
    self.latitude = latitude
    self.longitude = longitude
  }

}
