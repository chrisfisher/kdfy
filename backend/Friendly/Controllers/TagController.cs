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
    [RoutePrefix("api/tags")]
    public class TagController : ApiController
    {
        private readonly FriendlyContext _friendlyContext;

        public TagController(FriendlyContext friendlyContext)
        {
            _friendlyContext = friendlyContext;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Tag> GetTags()
        {
            return _friendlyContext.Tags.ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public Tag GetTag(int id)
        {
            return _friendlyContext.Tags.FirstOrDefault(t => t.Id == id);
        }


        [HttpPost]
        [Route("add")]
        public int AddTag(TagRequest request)
        {
            if (request == null) return -1;

            var tag = new Tag
            {
                Description = request.TagDescription
            };
            _friendlyContext.Tags.Add(tag);
            _friendlyContext.SaveChanges();
            return tag.Id;
        }

        [HttpPost]
        [Route("edit")]
        public bool EditTag(TagRequest request)
        {
            if (request == null) return false;

            var tag = _friendlyContext.Tags.Find(request.TagId);
            if (tag == null) return false;
            tag.Description = request.TagDescription;
            _friendlyContext.SaveChanges();
            return true;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public bool DeleteTag(int id)
        {
            var tag = _friendlyContext.Tags.Find(id);
            if (tag == null) return false;
            _friendlyContext.Tags.Remove(tag);
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