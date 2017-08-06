//
//  Review.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 28/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class Review: Mappable {
  
  public var reviewId: Int!
  public var username: String!
  public var score: Int!
  public var date: NSDate!
  
  required public init?(_ map: Map){}
  
  public func mapping(map: Map) {
    reviewId <- map["id"]
    username <- map["username"]
    score <- map["score"]
    date <- map["date"]
    
  }
  
}