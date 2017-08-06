
namespace Friendly.Controllers.Requests
{
    public class LocationRequest
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }              
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string GooglePlaceId { get; set; }
        public string GooglePlaceName { get; set; }
        public string GooglePlaceAddress { get; set; }
        public int LocationTypeId { get; set; }
        public string LocalSecrets { get; set; }
        public ImageLink[] ImageLinks { get; set; }
    }

    public class ImageLink
    {
        public string Id { get; set; }
        public string FileType { get; set; }
    }
}