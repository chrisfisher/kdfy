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
    [RoutePrefix("api/checks")]
    public class CheckController : ApiController
    {
        private readonly FriendlyContext _friendlyContext;

        public CheckController(FriendlyContext friendlyContext)
        {
            _friendlyContext = friendlyContext;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Check> GetChecks()
        {
            return _friendlyContext.Checks.Include(c => c.Tag).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Check GetCheck(int id)
        {
            return _friendlyContext.Checks
                    .Include(c => c.Tag)
                    .FirstOrDefault(c => c.Id == id);
        }


        [HttpPost]
        [Route("add")]
        public int AddCheck(CheckRequest request)
        {
            if (request == null) return -1;

            var check = new Check
            {
                Description = request.CheckDescription,
                Tag = _friendlyContext.Tags.Find(request.TagId)
            };
            _friendlyContext.Checks.Add(check);
            _friendlyContext.SaveChanges();
            return check.Id;
        }

        [HttpPost]
        [Route("edit")]
        public bool EditCheck(CheckRequest request)
        {
            if (request == null) return false;

            var check = _friendlyContext.Checks.Find(request.CheckId);
            if (check == null) return false;
            check.Description = request.CheckDescription;
            check.Tag = _friendlyContext.Tags.Find(request.TagId);
            _friendlyContext.SaveChanges();
            return true;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public bool DeleteLocation(int id)
        {
            var check = _friendlyContext.Checks.Find(id);
            if (check == null) return false;
            _friendlyContext.Checks.Remove(check);
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