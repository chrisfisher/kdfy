using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friendly.Controllers.Requests
{
    public class GetLocationsRequest
    {
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? Radius { get; set; }
    }
}