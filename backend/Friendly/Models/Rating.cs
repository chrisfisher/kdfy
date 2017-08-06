
namespace Friendly.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual Tag Tag { get; set; }
    }
}