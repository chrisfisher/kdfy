//
//  CreateUserResponse.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 16/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation

public class CreateUserResponse {
  
  public var success: Bool
  public var errorMessage: String?
  
  init(success: Bool, errorMessage: String? = nil) {
    self.success = success
    self.errorMessage = errorMessage
  }
  
}