//
//  ScrimView.swift
//  Friendly
//
//  Created by Chris Fisher on 20/02/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import UIKit

class ScrimView: UIView {
  
  let color1 = UIColor(white: 0.2, alpha: 0.0)
  let color2 = UIColor(white: 0.2, alpha: 0.2)
  let color3 = UIColor(white: 0.2, alpha: 0.4)
  let color4 = UIColor(white: 0.2, alpha: 0.6)
  
  override func drawRect(rect: CGRect) {
    let scrimLayer = CAGradientLayer()
    scrimLayer.frame = self.bounds
    scrimLayer.colors = [
      self.color4.CGColor,
      self.color1.CGColor,
      self.color1.CGColor,
      self.color1.CGColor,
      self.color1.CGColor,
      self.color2.CGColor,
      self.color3.CGColor,
      self.color4.CGColor
    ]
    self.layer.addSublayer(scrimLayer)
  }
  
}
