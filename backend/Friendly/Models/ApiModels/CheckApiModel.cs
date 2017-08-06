
namespace Friendly.Models.ApiModels
{
    public class CheckApiModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public TagApiModel Tag { get; set; }
    }
}