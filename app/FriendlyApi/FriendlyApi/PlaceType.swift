//
//  PlaceType.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 28/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class PlaceType: Mappable {

  public var placeTypeId: Int!
  public var name: String!
  public var checks: [Check]!
  public var ratings: [Rating]!

  required public init?(_ map: Map){ }
  
  public func mapping(map: Map) {
    placeTypeId <- map["id"]
    name <- map["name"]
    checks <- map["checks"]
    ratings <- map["ratings"]
  }
  
}
