//
//  RatingScore.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 28/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class RatingScore: Mappable {
  
  public var ratingScoreId: Int!
  public var ratingScore: Int!
  public var value: Int!
  
  required public init?(_ map: Map){}
  
  public func mapping(map: Map) {
    ratingScoreId <- map["id"]
    ratingScore <- map["ratingScore"]
    value <- map["value"]
  }
  
}
