//
//  PlaceController.swift
//  Friendly
//
//  Created by Chris Fisher on 10/12/15.
//  Copyright © 2015 Department of Architecture. All rights reserved.
//

import UIKit
import FriendlyApi
import GoogleMaps

class PlacesController: UIViewController, GMSMapViewDelegate {
    
  var userManager: UserManagerProtocol!
  var friendlyApi: FriendlyApiProtocol!
  var locationService: LocationServiceProtocol!
  var places: [Place]!
  var selectedPlace: Place!
    
  // MARK: Setup
  
  override func viewDidLoad() {
    super.viewDidLoad()
    navigationController?.navigationBar.setBackgroundImage(UIImage(), forBarMetrics: .Default)
    navigationController?.navigationBar.shadowImage = UIImage()
    navigationController?.navigationBar.translucent = true
    navigationController?.view.backgroundColor = UIColor.clearColor()
    startLocationService()
    loadPlaceData()
  }
  
  override func prepareForSegue(segue: UIStoryboardSegue, sender: AnyObject?) {
    if (segue.identifier == "placeSegue") {
      let placeVC = segue.destinationViewController as! PlaceController
      placeVC.place = selectedPlace
    }
  }
  
  // MARK: GMSMapViewDelegate
  
  func mapView(mapView: GMSMapView!, didTapMarker marker: GMSMarker!) -> Bool {
    selectedPlace = places.filter {$0.name == marker.title}[0]
    self.performSegueWithIdentifier("placeSegue", sender: self)
    return true
  }
  
  // MARK: Private implementation
  
  private func startLocationService() {
    locationService.start()
  }
  
  private func loadPlaceData() {
    let request = PlacesRequest()
    if let user = userManager.loadActiveUser() {
      request.token = user.token
    }
    friendlyApi.getPlaces(request).then { places -> Void in
      self.places = places
      self.loadMap()
      }.error { error in
        let alert = UIAlertView(title: "Error", message: "Unable to retrieve location data", delegate: nil, cancelButtonTitle: "Ok")
        alert.show()
    }
  }
  
  private func loadMap() {
    
    let location = locationService.getLocation()
    
    let camera = GMSCameraPosition.cameraWithLatitude(location.latitude, longitude:location.longitude, zoom:12)
    let mapView = GMSMapView.mapWithFrame(CGRectZero, camera:camera)
    mapView.delegate = self
    
    for place in places {
      let marker = GMSMarker()
      let markerColor = getMarkerColorForPlaceType(place.type)
      marker.icon = GMSMarker.markerImageWithColor(markerColor)
      marker.position = CLLocationCoordinate2DMake(place.latitude, place.longitude)
      marker.title = place.name
      marker.snippet = place.name
      marker.appearAnimation = kGMSMarkerAnimationPop
      marker.map = mapView
    }
    
    self.view = mapView
  }
  
  private func getMarkerColorForPlaceType(placeType: PlaceType) -> UIColor {
    switch (placeType.name) {
    case "Playground":
      return FriendlyColors.midGreen()
    case "Cafe":
      return FriendlyColors.midBlue()
    case "Attractions":
      return FriendlyColors.pink()
    default:
      return FriendlyColors.midGrey()
    }
  }
  
}

