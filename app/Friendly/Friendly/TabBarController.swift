//
//  TabBarController.swift
//  Friendly
//
//  Created by Chris Fisher on 31/01/16.
//  Copyright © 2016 Department of Architecture. All rights reserved.
//

import UIKit

class TabBarController: UITabBarController, UITabBarControllerDelegate {
  
  private let homeTabIndex = 0
  private let accountTabIndex = 1
  
  override func viewDidLoad() {
    super.viewDidLoad()
    //self.delegate = self
    //styleTabBar()
  }
  
  func tabBarController(tabBarController: UITabBarController, shouldSelectViewController viewController: UIViewController) -> Bool {
    guard let tabBarVCs = tabBarController.viewControllers else { return false }
    if selectedIndex == accountTabIndex && viewController == tabBarVCs[accountTabIndex] {
      return false
    } else if selectedIndex == homeTabIndex && viewController == tabBarVCs[homeTabIndex] {
      return false
    } else {
      return true
    }
  }
    
  private func styleTabBar() {
    UITabBarItem.appearance().setTitleTextAttributes([NSForegroundColorAttributeName: UIColor.whiteColor()], forState: .Normal)
    UITabBarItem.appearance().setTitleTextAttributes([NSForegroundColorAttributeName: UIColor.whiteColor()], forState: .Selected)
  }
  
}