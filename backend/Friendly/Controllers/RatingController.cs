using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using Friendly.Context;
using Friendly.Models;
using Friendly.Controllers.Requests;
using System;

namespace Friendly.Controllers
{
    [Authorize]
    [RoutePrefix("api/ratings")]
    public class RatingController : ApiController
    {
        private readonly FriendlyContext _friendlyContext;

        public RatingController(FriendlyContext friendlyContext)
        {
            _friendlyContext = friendlyContext;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Rating> GetRatings()
        {
            return _friendlyContext.Ratings.Include(r => r.Tag).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Rating GetRating(int id)
        {
            return _friendlyContext.Ratings
                   .Include(r => r.Tag)
                   .FirstOrDefault(r => r.Id == id);
        }

        [HttpPost]
        [Route("add")]
        public int AddRating(RatingRequest request)
        {
            if (request == null) return -1;

            var rating = new Rating
            {
                Description = request.RatingDescription,
                Tag = _friendlyContext.Tags.Find(request.TagId)
            };
            _friendlyContext.Ratings.Add(rating);
            _friendlyContext.SaveChanges();
            return rating.Id;
        }

        [HttpPost]
        [Route("edit")]
        public bool EditRating(RatingRequest request)
        {
            if (request == null) return false;

            var rating = _friendlyContext.Ratings.Find(request.RatingId);
            if (rating == null) return false;
            rating.Description = request.RatingDescription;
            rating.Tag = _friendlyContext.Tags.Find(request.TagId);
            _friendlyContext.SaveChanges();
            return true;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public bool DeleteLocation(int id)
        {
            var rating = _friendlyContext.Ratings.Find(id);
            if (rating == null) return false;
            _friendlyContext.Ratings.Remove(rating);
            try
            {
                _friendlyContext.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}