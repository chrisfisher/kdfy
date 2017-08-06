using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Friendly.Context;
using Friendly.Controllers.Requests;
using Friendly.Models;

namespace Friendly.Services
{
    public interface ILocationService
    {
        IEnumerable<Location> GetLocations(GetLocationsRequest request);
        Location GetLocation(int id);
    }

    public class LocationService : ILocationService
    {
        private readonly FriendlyContext _friendlyContext;
        private readonly ILocationBoundsService _locationBoundsService;

        public LocationService(
            FriendlyContext friendlyContext,
            ILocationBoundsService locationBoundsService)
        {
            _friendlyContext = friendlyContext;
            _locationBoundsService = locationBoundsService;
        }

        public IEnumerable<Location> GetLocations(GetLocationsRequest request)
        {
            var locations = _friendlyContext.Locations
                .Include(l => l.LocationType)
                .Include(l => l.LocationType.Ratings)
                .Include(l => l.LocationType.Ratings.Select(x => x.Tag))
                .Include(l => l.LocationType.Checks)
                .Include(l => l.LocationType.Checks.Select(x => x.Tag))
                .Include(l => l.ImageLinks)
                .ToList();

            if (request != null && request.Latitude.HasValue && request.Longitude.HasValue && request.Radius.HasValue)
            {
                var point = new GeoPoint { Latitude = (double)request.Latitude.Value, Longitude = (double)request.Longitude.Value };
                var bounds = _locationBoundsService.GetBoundsForPoint(point, request.Radius.Value);
                locations = locations.Where(l => _locationBoundsService.IsPointInBounds(new GeoPoint { Latitude = (double)l.Latitude, Longitude = (double)l.Longitude }, bounds)).ToList();
            }

            return locations;
        }

        public Location GetLocation(int id)
        {
            return _friendlyContext.Locations
                .Include(l => l.LocationType)
                .Include(l => l.LocationType.Ratings)
                .Include(l => l.LocationType.Ratings.Select(r => r.Tag))
                .Include(l => l.LocationType.Checks)
                .Include(l => l.LocationType.Checks.Select(r => r.Tag))
                .Include(l => l.Reviews)
                .Include(l => l.Reviews.Select(x => x.CheckScores))
                .Include(l => l.Reviews.Select(x => x.RatingScores))
                .FirstOrDefault(l => l.Id == id);
        }
    }
}