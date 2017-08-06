using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using Friendly.Context;
using Friendly.Models;
using Friendly.Models.ApiModels;
using Friendly.Controllers.Requests;
using AutoMapper;
using Friendly.Services;
using System;
using System.Web.Http.Cors;
using Friendly.Infrastructure;
using Newtonsoft.Json;

namespace Friendly.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    [RoutePrefix("api/locations")]
    public class LocationController : ApiController
    {
        private readonly FriendlyContext _friendlyContext;
        private readonly ILocationReviewService _locationReviewService;
        private readonly ILocationService _locationService;
        private readonly ICache _cache;

        public LocationController(
            FriendlyContext friendlyContext,
            ILocationService locationService,
            ILocationReviewService locationReviewService,
            ICache cache)
        {
            _friendlyContext = friendlyContext;
            _locationReviewService = locationReviewService;
            _cache = cache;
            _locationService = locationService;
        }

        [OverrideAuthentication]
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IEnumerable<LocationApiModel> GetLocations([FromUri] GetLocationsRequest request)
        {
            var key = JsonConvert.SerializeObject(request);
            var locations = _cache.Get(key, () => _locationService.GetLocations(request));
            return Mapper.Map<IEnumerable<LocationApiModel>>(locations).ToList();                
        }

        [OverrideAuthentication]
        [AllowAnonymous]
        [HttpGet]
        [Route("{id}")]
        public LocationApiModel GetLocation(int id)
        {
            var key = string.Format("GetLocation.Id-{0}", id);
            var location = _cache.Get(key, () => _locationService.GetLocation(id));
            return Mapper.Map<LocationApiModel>(location);       
        }

        [HttpPost]
        [Route("add")]
        public int AddLocation(LocationRequest request)
        {
            if (request == null) return -1;

            var location = Mapper.Map<Location>(request);
            location.LocationType = _friendlyContext.LocationTypes.FirstOrDefault(x => x.Id == request.LocationTypeId);
            _friendlyContext.Locations.Add(location);

            _friendlyContext.SaveChanges();

            return location.Id;
        }

        [HttpPost]
        [Route("edit")]
        public bool EditLocation(LocationRequest request)
        {
            if (request == null) return false;

            var location = _friendlyContext.Locations.Find(request.LocationId);
            
            if (location == null) return false;

            location.Name = request.LocationName;
            location.LocationType = _friendlyContext.LocationTypes.First(x => x.Id == request.LocationTypeId);
            location.Latitude = request.Latitude;
            location.Longitude = request.Longitude;
            location.GooglePlaceAddress = request.GooglePlaceAddress;
            location.GooglePlaceId = request.GooglePlaceId;
            location.GooglePlaceName = request.GooglePlaceName;
            location.LocalSecrets = request.LocalSecrets;
            
            foreach (var imageLink in request.ImageLinks)
            {
                if (!location.ImageLinks.Any(x => x.Id == imageLink.Id)) {
                    location.ImageLinks.Add(new Models.ImageLink { Id = imageLink.Id, FileType = imageLink.FileType });
                }
            }
                        
            try
            {
                _friendlyContext.SaveChanges();
                return true;
            } catch (Exception ex)
            {
                return false;
            }            
            
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public bool DeleteLocation(int id)
        {
            var location = _friendlyContext.Locations.Find(id);
            if (location == null) return false;
            _friendlyContext.Locations.Remove(location);

            _friendlyContext.SaveChanges();

            return true;
        }

        [HttpPost]
        [Route("review")]
        public bool ReviewLocation(ReviewLocationRequest request)
        {
            if (request == null) return false;

            var location = _friendlyContext.Locations.Find(request.LocationId);
            if (location == null) return false;

            var locationReview = Mapper.Map<LocationReview>(request);
            locationReview.Score = _locationReviewService.CalculateTotalScore(request.CheckScores, request.RatingScores);
            locationReview.Date = DateTime.Now;

            foreach (var checkScore in locationReview.CheckScores)
            {
                if (_friendlyContext.CheckScores.Find(checkScore.Id) == null)
                    _friendlyContext.CheckScores.Add(checkScore);
            }

            foreach (var ratingScore in locationReview.RatingScores)
            {
                if (_friendlyContext.RatingScores.Find(ratingScore.Id) == null)
                    _friendlyContext.RatingScores.Add(ratingScore);
            }

            location.Reviews.Add(locationReview);
            _friendlyContext.LocationScores.Add(locationReview);

            _friendlyContext.SaveChanges();

            return true;
        }
    }
}
