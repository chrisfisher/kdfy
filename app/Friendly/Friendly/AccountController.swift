//
//  AccountController.swift
//  Friendly
//
//  Created by Chris Fisher on 24/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import UIKit
import FriendlyApi

class AccountController: UIViewController {

  var userManager: UserManagerProtocol!
  
  @IBOutlet weak var welcomeLabel: UILabel!
  
  override func viewWillAppear(animated: Bool) {
    super.viewWillAppear(animated)
    guard let user = userManager.loadActiveUser() else {
      performSegueWithIdentifier("signInSegue", sender: self)
      return
    }
    welcomeLabel.text = "Welcome, " + user.username + "!"
  }
  
  @IBAction func unwindToAccount(sender: UIStoryboardSegue) { }
  
  @IBAction func didTapSignOutButton(sender: LoadingButton) {
    userManager.clearActiveUser()
    performSegueWithIdentifier("signInSegue", sender: self)
  }
}
