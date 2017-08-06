//
//  ImageLink.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 17/02/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class ImageLink: Mappable {
  
  public var imageId: String!
  public var fileType: String!
  
  required public init?(_ map: Map){}
  
  public func mapping(map: Map) {
    imageId <- map["id"]
    fileType <- map["fileType"]
  }
  
}
