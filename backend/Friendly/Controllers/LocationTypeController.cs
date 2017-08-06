using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using Friendly.Context;
using Friendly.Models;
using Friendly.Controllers.Requests;
using System;
using AutoMapper;

namespace Friendly.Controllers
{
    [Authorize]
    [RoutePrefix("api/locationtypes")]
    public class LocationTypeController : ApiController
    {
        private readonly FriendlyContext _friendlyContext;

        public LocationTypeController(FriendlyContext friendlyContext)
        {
            _friendlyContext = friendlyContext;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<LocationType> GetLocationTypes()
        {
            return _friendlyContext.LocationTypes
                    .Include(l => l.Ratings)
                    .Include(l => l.Ratings.Select(r => r.Tag))
                    .Include(l => l.Checks)
                    .Include(l => l.Checks.Select(r => r.Tag))
                    .ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public LocationType GetLocationType(int id)
        {
            return _friendlyContext.LocationTypes
                    .Include(l => l.Ratings)
                    .Include(l => l.Ratings.Select(r => r.Tag))
                    .Include(l => l.Checks)
                    .Include(l => l.Checks.Select(r => r.Tag))
                    .FirstOrDefault(l => l.Id == id);
        }

        [HttpPost]
        [Route("add")]
        public int AddLocationType(LocationTypeRequest request)
        {
            if (request == null) return -1;

            var locationType = Mapper.Map<LocationType>(request);
            locationType.Checks = request.CheckIds.Select(c => _friendlyContext.Checks.Find(c)).ToList();
            locationType.Ratings = request.RatingIds.Select(r => _friendlyContext.Ratings.Find(r)).ToList();
            _friendlyContext.LocationTypes.Add(locationType);
            _friendlyContext.SaveChanges();
            return locationType.Id;
        }

        [HttpPost]
        [Route("edit")]
        public bool EditLocationType(LocationTypeRequest request)
        {
            if (request == null) return false;

            var locationType = _friendlyContext.LocationTypes.Find(request.LocationTypeId);
            if (locationType == null) return false;
            foreach (var check in locationType.Checks.ToList())
                locationType.Checks.Remove(check);
            foreach (var rating in locationType.Ratings.ToList())
                locationType.Ratings.Remove(rating);
            locationType.Checks = _friendlyContext.Checks.Where(c => request.CheckIds.Contains(c.Id)).ToList();
            locationType.Ratings = _friendlyContext.Ratings.Where(c => request.RatingIds.Contains(c.Id)).ToList();
            _friendlyContext.SaveChanges();
            return true;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public bool DeleteLocationType(int id)
        {
            var locationType = _friendlyContext.LocationTypes.Find(id);
            if (locationType == null) return false;
            foreach (var check in locationType.Checks.ToList())
                locationType.Checks.Remove(check);
            foreach (var rating in locationType.Ratings.ToList())
                locationType.Ratings.Remove(rating);
            _friendlyContext.LocationTypes.Remove(locationType);
            try
            {
                _friendlyContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
