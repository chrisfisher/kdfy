using System;
using System.Collections.Generic;

namespace Friendly.Models
{
    public class LocationReview
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal Score { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<CheckScore> CheckScores { get; set; }
        public virtual ICollection<RatingScore> RatingScores { get; set; }
    }
}