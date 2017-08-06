//
//  Rating.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 28/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class Rating: Mappable {
  
  public var ratingId: Int!
  public var description: String!
  public var tag: Tag!
  
  required public init?(_ map: Map){}
  
  public func mapping(map: Map) {
    ratingId <- map["id"]
    description <- map["description"]
    tag <- map["tag"]
  }
  
}