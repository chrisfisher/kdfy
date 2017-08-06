//
//  FriendlyImageApi.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 13/02/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import Foundation
import PromiseKit
import MapleBacon

public protocol FriendlyImageApiProtocol {

  func downloadImageWithId(imageId: String, fileType: String, imageView: UIImageView)
  
}

public class FriendlyImageApi: FriendlyImageApiProtocol {

  private let baseUrl = "https://kdfy.blob.core.windows.net/location-images/"
  
  public init() {}
  
  public func downloadImageWithId(imageId: String, fileType: String, imageView: UIImageView) {
    if let imageUrl = NSURL(string: baseUrl + imageId + "." + fileType) {
      imageView.setImageWithURL(imageUrl)
    }
  }
  
  public func clearMemoryStorage() {
    MapleBaconStorage.sharedStorage.clearMemoryStorage()
  }
  
}