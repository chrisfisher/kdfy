
namespace Friendly.Models.ApiModels
{
    public class RatingApiModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public TagApiModel Tag { get; set; }
    }
}