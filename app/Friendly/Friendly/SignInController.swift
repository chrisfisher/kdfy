//
//  SignInController.swift
//  Friendly
//
//  Created by Chris Fisher on 21/12/15.
//  Copyright © 2015 Department of Architecture. All rights reserved.
//

import UIKit
import PromiseKit
import FriendlyApi

class SignInController : UIViewController {

  @IBOutlet weak var usernameTextField: UITextField!
  @IBOutlet weak var passwordTextField: UITextField!
  
  @IBOutlet weak var signInButton: LoadingButton!
  @IBOutlet weak var signUpButton: UIButton!
  
  var userManager: UserManagerProtocol!
  var friendlyApi: FriendlyApiProtocol!
  
  override func viewDidLoad() {
    super.viewDidLoad()
    setupSignUpButton()
    hideBackButton()
  }
  
  @IBAction func didTapView(sender: AnyObject) {
    self.view.endEditing(true)
  }
  
  @IBAction func didTapSignIn(sender: UIButton) {
    let loginRequest = LoginUserRequest()
    loginRequest.username = usernameTextField.text
    loginRequest.password = passwordTextField.text
    self.view.endEditing(true)
    signInButton.startLoading()
    friendlyApi.loginUser(loginRequest).then { loginResponse -> Void in
      self.signInButton.stopLoading()
      let token = loginResponse.token
      if token != nil {
        let user = User(username: loginRequest.username, token: token!)
        self.userManager.saveActiveUser(user)
        self.performSegueWithIdentifier("unwindToAccount", sender: self)
      } else {
        self.showLoginError("Invalid user credentials")
      }
    }.error { error in
        self.showLoginError("Unexpected error signing in")
    }
  }
  
  @IBAction func unwindToSignIn(sender: UIStoryboardSegue) { }
  
  private func setupSignUpButton() {
    let accountAttributes = [
      NSForegroundColorAttributeName : UIColor.whiteColor(),
      NSFontAttributeName : UIFont.systemFontOfSize(16)
    ]
    let accountString = NSMutableAttributedString(string:"Don't have an account? ", attributes: accountAttributes)
    let signUpAttributes = [
      NSForegroundColorAttributeName : UIColor.whiteColor(),
      NSFontAttributeName : UIFont.boldSystemFontOfSize(16)
    ]
    accountString.appendAttributedString(NSMutableAttributedString(string:"Sign up", attributes: signUpAttributes))
    signUpButton.setAttributedTitle(accountString, forState: .Normal)
  }
  
  private func showLoginError(message: String) {
    let alert = UIAlertView(title: "Error", message: message, delegate: nil, cancelButtonTitle: "Ok")
    alert.show()
  }
  
  private func hideBackButton() {
    self.navigationItem.setHidesBackButton(true, animated: false)
  }
  
}