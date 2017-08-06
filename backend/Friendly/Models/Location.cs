using System.Collections.Generic;

namespace Friendly.Models
{
    public class Location
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
        
        public virtual LocationType LocationType { get; set; }
        public virtual ICollection<LocationReview> Reviews { get; set; }
        public virtual ICollection<ImageLink> ImageLinks { get; set; }

    }
}