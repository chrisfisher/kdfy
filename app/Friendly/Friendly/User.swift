//
//  User.swift
//  Friendly
//
//  Created by Chris Fisher on 30/12/15.
//  Copyright © 2015 Department of Architecture. All rights reserved.
//

import Foundation

class User: NSObject, NSCoding {
  
  var username: String
  var token: String
    
  // MARK: Initialization
  
  init(username: String, token: String) {
    self.username = username
    self.token = token
    super.init()
  }
  
  // MARK: NSCoding
  
  func encodeWithCoder(aCoder: NSCoder) {
    aCoder.encodeObject(username, forKey: "username")
    aCoder.encodeObject(token, forKey: "token")
  }
  
  required convenience init?(coder aDecoder: NSCoder) {
    let username = aDecoder.decodeObjectForKey("username") as! String
    let token = aDecoder.decodeObjectForKey("token") as! String
    self.init(username: username, token: token)
  }
  
}