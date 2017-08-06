
namespace Friendly.Controllers.Requests
{
    public class RatingRequest
    {
        public int RatingId { get; set; }
        public string RatingDescription { get; set; }
        public int TagId { get; set; }
    }
}