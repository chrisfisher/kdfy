//
//  FriendlyApi.swift
//  FriendlyApi
//
//  Created by Chris Fisher on 11/12/15.
//  Copyright © 2015 Department of Architecture. All rights reserved.
//

import Foundation
import PromiseKit
import Alamofire
import AlamofireObjectMapper

public protocol FriendlyApiProtocol {
  
  func createUser(request: CreateUserRequest) -> Promise<CreateUserResponse>
  func loginUser(request: LoginUserRequest) -> Promise<LoginUserResponse>
  func getPlaces(request: PlacesRequest) -> Promise<[Place]>
  
}

public enum FriendlyApiError: ErrorType {
  case CreateUserError(message: String)
}

public class FriendlyApi: FriendlyApiProtocol {
  
  private var baseUrl: String = "https://kdfy.azurewebsites.net"
  
  public init() {}
  
  public func createUser(request: CreateUserRequest) -> Promise<CreateUserResponse> {
    
    let parameters: [String: AnyObject]? = [
      "username": request.username,
      "email": request.email,
      "password": request.password
    ]
    
    return Promise { fulfill, reject in
      
      Alamofire.request(.POST, baseUrl + "/api/account/register", parameters: parameters, encoding: .URLEncodedInURL).responseJSON(completionHandler: { response -> Void in
        
        if let error = response.result.error {
          reject(error)
          return
        }
        
        if let success = response.result.value as? Bool {
          fulfill(CreateUserResponse(success: success))
          return
        }
        
        if let errorResponse = response.result.value as? NSDictionary {
          if let modelState = errorResponse["modelState"] as? NSDictionary {
            if let modelStateError = modelState[""] as? [String] {
              let errorMessage = modelStateError[0]
              reject(FriendlyApiError.CreateUserError(message: errorMessage))
              return
            }
          }
        }
        
        reject(FriendlyApiError.CreateUserError(message: "Unexpected error signing up"))

      })
    }
  }
  
  public func loginUser(request: LoginUserRequest) -> Promise<LoginUserResponse> {
    
    let parameters: [String: AnyObject]? = [
      "username": request.username,
      "password": request.password,
      "grant_type": "password"
    ]
    
    return Promise { fulfill, reject in
      Alamofire.request(.POST, baseUrl + "/token", parameters: parameters).responseObject { (response: Response<LoginUserResponse, NSError>) -> Void in

        if let error = response.result.error {
          reject(error)
          return
        }
        
        let loginResponse = response.result.value!
                
        fulfill(loginResponse)
        
      }
    }
  }
  
  public func getPlaces(request: PlacesRequest) -> Promise<[Place]> {
  
    var parameters: [String: AnyObject]? = [String: AnyObject]()
    if let latitude = request.latitude {
      parameters!["latitude"] = latitude
    }
    if let longitude = request.longitude {
      parameters!["longitude"] = longitude
    }
    if let radius = request.radius {
      parameters!["radius"] = radius
    }
    var headers: [String: String]?
    if let token = request.token {
      headers = ["Authorization": "Bearer " + token]
    }
    
    return Promise { fulfill, reject in
      Alamofire.request(.GET, baseUrl + "/api/locations", parameters: nil, encoding: .JSON, headers: headers).responseArray { (response: Response<[Place], NSError>) -> Void in
        
        if let error = response.result.error {
          reject(error)
          return
        }
        
        let places = response.result.value!
        
        fulfill(places)
        
      }
    }
 
  }
  
}
