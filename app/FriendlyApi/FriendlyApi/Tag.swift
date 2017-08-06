//
//  Tag.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 28/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class Tag: Mappable {
  
  public var tagId: Int!
  public var description: String!
  
  required public init?(_ map: Map){}
  
  public func mapping(map: Map) {
    tagId <- map["id"]
    description <- map["description"]
  }
  
}
