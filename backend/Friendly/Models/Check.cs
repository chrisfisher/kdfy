using System.Collections.Generic;

namespace Friendly.Models
{
    public class Check
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual Tag Tag { get; set; }
    }
}