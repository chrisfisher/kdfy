//
//  UserManager.swift
//  Friendly
//
//  Created by Chris Fisher on 2/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation

protocol UserManagerProtocol {

  func saveActiveUser(user: User)
  
  func loadActiveUser() -> User?
  
  func clearActiveUser()
  
}

class UserManager: UserManagerProtocol {

  private let userKey = "user"
  
  func saveActiveUser(user: User) {
    let data = NSKeyedArchiver.archivedDataWithRootObject(user)
    NSUserDefaults.standardUserDefaults().setObject(data, forKey: userKey)
  }
  
  func loadActiveUser() -> User? {
    if let data = NSUserDefaults.standardUserDefaults().objectForKey(userKey) as? NSData {
      return NSKeyedUnarchiver.unarchiveObjectWithData(data) as? User
    } else {
      return nil
    }
  }
  
  func clearActiveUser() {
    NSUserDefaults.standardUserDefaults().removeObjectForKey(userKey)
  }
  
}
