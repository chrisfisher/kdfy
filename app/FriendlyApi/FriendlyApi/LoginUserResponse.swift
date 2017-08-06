//
//  LoginResponse.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 21/12/15.
//  Copyright © 2015 Department of Architecture. All rights reserved.
//

import Foundation
import ObjectMapper

public class LoginUserResponse: Mappable {
  
  public var token: String?
  public var tokenType: String?
  public var username: String?
  public var tokenExpiry: String?
  
  required public init?(_ map: Map){
    
  }
  
  public func mapping(map: Map) {
    token         <- map["access_token"]
    tokenType     <- map["token_type"]
    username      <- map["userName"]
    tokenExpiry   <- map["expires_in"]
  }
  
}