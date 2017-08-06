//
//  PlaceController.swift
//  Friendly
//
//  Created by Chris Fisher on 12/02/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import UIKit
import FriendlyApi


class PlaceController: UIViewController {

  @IBOutlet weak var placeImageView: UIImageView!
  @IBOutlet weak var placeTitleLabel: UILabel!
  @IBOutlet weak var scrollViewTopLayoutConstraint: NSLayoutConstraint!
  
  var friendlyImageApi: FriendlyImageApiProtocol!
  var place: Place!
  
  override func viewDidLoad() {
    super.viewDidLoad()
    
    scrollViewTopLayoutConstraint.constant = -140
    
    if let imageLink = place.imageLinks.first {
      friendlyImageApi.downloadImageWithId(imageLink.imageId, fileType: imageLink.fileType, imageView: placeImageView)
    }
    
    placeTitleLabel.text = place.name
    view.bringSubviewToFront(placeTitleLabel)
   
    navigationController?.navigationBar.setBackgroundImage(UIImage(), forBarMetrics: .Default)
    navigationController?.navigationBar.shadowImage = UIImage()
    navigationController?.navigationBar.translucent = true
    navigationController?.view.backgroundColor = UIColor.clearColor()
    
  }
  
}