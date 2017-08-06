//
//  CheckScore.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 28/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class CheckScore: Mappable {
  
  public var checkScoreId: Int!
  public var checkId: Int!
  public var value: Bool!
  
  required public init?(_ map: Map){}
  
  public func mapping(map: Map) {
    checkScoreId <- map["id"]
    checkId <- map["checkId"]
    value <- map["value"]
  }
  
}
