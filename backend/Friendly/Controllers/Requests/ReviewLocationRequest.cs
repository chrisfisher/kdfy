using Friendly.Models;
using System.Collections.Generic;

namespace Friendly.Controllers.Requests
{
    public class ReviewLocationRequest
    {
        public int LocationId { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }

        public IEnumerable<CheckScore> CheckScores { get; set; }
        public IEnumerable<RatingScore> RatingScores { get; set; }        
    }
}