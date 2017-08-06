//
//  SwinjectStoryboard.swift
//  Friendly
//
//  Created by Chris Fisher on 2/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import Swinject
import FriendlyApi

extension SwinjectStoryboard {

  class func setup() {
    
    defaultContainer.register(UserManagerProtocol.self) { _ in UserManager() }.inObjectScope(.Container)
    
    defaultContainer.register(FriendlyApiProtocol.self) { _ in FriendlyApi() }.inObjectScope(.Container)
    
    defaultContainer.register(FriendlyImageApiProtocol.self) { _ in FriendlyImageApi() }.inObjectScope(.Container)
    
    defaultContainer.register(LocationServiceProtocol.self) { _ in LocationService() }.inObjectScope(.Container)
    
    // View controllers
    defaultContainer.registerForStoryboard(PlacesController.self) { r, c in
      c.userManager = r.resolve(UserManagerProtocol.self)
      c.friendlyApi = r.resolve(FriendlyApiProtocol.self)
      c.locationService = r.resolve(LocationServiceProtocol.self)
    }
    defaultContainer.registerForStoryboard(PlaceController.self) { r, c in
      c.friendlyImageApi = r.resolve(FriendlyImageApiProtocol.self)
    }
    defaultContainer.registerForStoryboard(AccountController.self) { r, c in
      c.userManager = r.resolve(UserManagerProtocol.self)
    }
    defaultContainer.registerForStoryboard(SignInController.self) { r, c in
      c.userManager = r.resolve(UserManagerProtocol.self)
      c.friendlyApi = r.resolve(FriendlyApiProtocol.self)
    }
    defaultContainer.registerForStoryboard(SignUpController.self) { r, c in
      c.userManager = r.resolve(UserManagerProtocol.self)
      c.friendlyApi = r.resolve(FriendlyApiProtocol.self)
    }
    
  }
  
}
