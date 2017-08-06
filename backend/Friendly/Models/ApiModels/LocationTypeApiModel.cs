using System.Collections.Generic;

namespace Friendly.Models.ApiModels
{
    public class LocationTypeApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<CheckApiModel> Checks { get; set; }
        public IEnumerable<RatingApiModel> Ratings { get; set; }
    }
}