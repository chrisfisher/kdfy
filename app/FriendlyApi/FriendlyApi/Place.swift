//
//  Place.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 11/12/15.
//  Copyright © 2015 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class Place: Mappable {
  
  public var placeId: Int!
  public var type: PlaceType!
  public var name: String!
  public var latitude: Double!
  public var longitude: Double!
  public var googlePlaceId: String!
  public var googlePlaceName: String!
  public var googlePlaceAddress: String!
  public var averageScore: Double!
  public var localSecrets: String!
  public var imageLinks: [ImageLink]!
  
  required public init?(_ map: Map){}
  
  public func mapping(map: Map) {
    placeId <- map["id"]
    type <- map["locationType"]
    name <- map["name"]
    latitude <- map["latitude"]
    longitude <- map["longitude"]
    googlePlaceId <- map["googlePlaceId"]
    googlePlaceName <- map["googlePlaceName"]
    googlePlaceAddress <- map["googlePlaceAddress"]
    averageScore <- map["averageScore"]
    localSecrets <- map["localSecrets"]
    imageLinks <- map["imageLinks"]
  }
  
}