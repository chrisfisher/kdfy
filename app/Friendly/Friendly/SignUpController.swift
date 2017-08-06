//
//  SignUpController.swift
//  Friendly
//
//  Created by Chris Fisher on 7/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import UIKit
import PromiseKit
import FriendlyApi

class SignUpController : UIViewController {
  
  var friendlyApi: FriendlyApiProtocol!
  var userManager: UserManagerProtocol!

  @IBOutlet weak var usernameTextField: UITextField!
  @IBOutlet weak var emailTextField: UITextField!
  @IBOutlet weak var passwordTextField: UITextField!
  @IBOutlet weak var confirmPasswordTextField: UITextField!
  @IBOutlet weak var signUpButton: LoadingButton!
  
  @IBAction func didTapView(sender: UITapGestureRecognizer) {
    self.view.endEditing(true)
  }
  
  @IBAction func didTapSignUp(sender: UIButton) {
    
    if !validatePassword() {
      showSignUpError("Passwords do not match")
      return
    }
    
    let createUserRequest = CreateUserRequest()
    createUserRequest.username = usernameTextField.text
    createUserRequest.email = emailTextField.text
    createUserRequest.password = passwordTextField.text
    
    firstly {
      self.view.endEditing(true)
      self.signUpButton.startLoading()
      return self.friendlyApi.createUser(createUserRequest)
    }.then { (createUserResponse: CreateUserResponse) in
      let loginUserRequest = LoginUserRequest()
      loginUserRequest.username = createUserRequest.username
      loginUserRequest.password = createUserRequest.password
      return self.friendlyApi.loginUser(loginUserRequest)
    }.then { (loginUserResponse: LoginUserResponse) -> Void in
      if let token = loginUserResponse.token {
        let user = User(username: createUserRequest.username, token: token)
        self.userManager.saveActiveUser(user)
        self.performSegueWithIdentifier("unwindToAccount", sender: self)
        return
      }
    }.always {
        self.signUpButton.stopLoading()
    }.error { error -> Void in
      if let friendlyApiError = error as? FriendlyApiError {
        switch friendlyApiError {
        case .CreateUserError(let message):
          self.showSignUpError(message)
          return
        }
      }
      self.showSignUpError("Unexpected error signing up")
    }
  }
  
  private func validatePassword() -> Bool {
    return passwordTextField.text != nil
      && confirmPasswordTextField.text != nil
      && passwordTextField.text == confirmPasswordTextField.text
  }
  
  private func showSignUpError(message: String) {
    let alert = UIAlertView(title: "Error", message: message, delegate: nil, cancelButtonTitle: "Ok")
    alert.show()
  }
  
}
