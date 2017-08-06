
namespace Friendly.Controllers.Requests
{
    public class LocationTypeRequest
    {
        public int LocationTypeId { get; set; }
        public string LocationTypeName { get; set; }
        public int[] CheckIds { get; set; }
        public int[] RatingIds { get; set; }
    }
}