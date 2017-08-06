using System.Collections.Generic;

namespace Friendly.Models
{
    public class LocationType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Check> Checks { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}