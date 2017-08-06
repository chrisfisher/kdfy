//
//  LoadingButton.swift
//  Friendly
//
//  Created by Chris Fisher on 17/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import UIKit

class LoadingButton: UIButton {

  private var titleText: String?
  
  private lazy var buttonSpinner: UIActivityIndicatorView = {
    let activityView = UIActivityIndicatorView(activityIndicatorStyle: .White)
    activityView.center = self.center
    activityView.frame = self.bounds
    activityView.startAnimating()
    return activityView
  }()
  
  required init?(coder aDecoder: NSCoder) {
    super.init(coder: aDecoder)
    layer.borderWidth = 0.0
    layer.cornerRadius = 5.0
    layer.borderColor = UIColor.whiteColor().CGColor
  }
  
  func startLoading() {
    UIApplication.sharedApplication().networkActivityIndicatorVisible = true
    buttonSpinner.startAnimating()
    addSubview(buttonSpinner)
    titleText = titleLabel?.text
    setTitle("", forState: .Normal)
  }
  
  func stopLoading() {
    UIApplication.sharedApplication().networkActivityIndicatorVisible = false
    buttonSpinner.stopAnimating()
    buttonSpinner.removeFromSuperview()
    setTitle(titleText, forState: .Normal)
  }
  
}
