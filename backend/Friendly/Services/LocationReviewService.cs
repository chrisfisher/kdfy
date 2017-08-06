using Friendly.Models;
using System.Collections.Generic;
using System.Linq;

namespace Friendly.Services
{
    public interface ILocationReviewService
    {
        decimal CalculateTotalScore(IEnumerable<CheckScore> checkScores, IEnumerable<RatingScore> ratingScores);
    }

    public class LocationReviewService : ILocationReviewService
    {
        public decimal CalculateTotalScore(IEnumerable<CheckScore> checkScores, IEnumerable<RatingScore> ratingScores)
        {
            var checkScoreTotal = checkScores.Select(x => new { Score = x.Value ? 5 : 1 }).Sum(x => x.Score);
            var ratingScoreTotal = ratingScores.Sum(x => x.Value);            
            decimal total = (checkScoreTotal + ratingScoreTotal) / (checkScores.Count() + ratingScores.Count());
            return total;
        }
    }
}