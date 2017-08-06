using System.Collections.Generic;

namespace Friendly.Models.ApiModels
{
    public class LocationApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string GooglePlaceId { get; set; }
        public string GooglePlaceName { get; set; }
        public string GooglePlaceAddress { get; set; }
        public decimal AverageScore { get; set; }
        public string LocalSecrets { get; set; }

        public LocationTypeApiModel LocationType { get; set; }
        public IEnumerable<LocationReviewApiModel> Reviews { get; set; }
        public IEnumerable<ImageLink> ImageLinks { get; set; }
    }
}