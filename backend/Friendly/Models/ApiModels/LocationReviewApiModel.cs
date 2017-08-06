using System;
using System.Collections.Generic;

namespace Friendly.Models.ApiModels
{
    public class LocationReviewApiModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal Score { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public LocationApiModel Location { get; set; }
        public IEnumerable<CheckScoreApiModel> CheckScores { get; set; }
        public IEnumerable<RatingScoreApiModel> RatingScores { get; set; }
    }
}