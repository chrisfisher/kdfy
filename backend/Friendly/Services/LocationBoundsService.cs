using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Friendly.Services
{
    public interface ILocationBoundsService
    {
        bool IsPointInBounds(GeoPoint point, Bounds bounds);
        Bounds GetBoundsForPoint(GeoPoint point, int radiusInMetres);
    }

    public class LocationBoundsService : ILocationBoundsService
    {
        private const double WGS84_a = 6378137.0;
        private const double WGS84_b = 6356752.3;

        public bool IsPointInBounds(GeoPoint point, Bounds bounds)
        {
            return point.Latitude <= bounds.MaxPoint.Latitude 
                && point.Longitude <= bounds.MaxPoint.Longitude
                && point.Latitude >= bounds.MinPoint.Latitude
                && point.Longitude >= bounds.MinPoint.Longitude;
        }

        public Bounds GetBoundsForPoint(GeoPoint point, int radiusInMetres)
        {
            var lat = DegreesToRadians(point.Latitude);
            var lon = DegreesToRadians(point.Longitude);
            var halfSide = radiusInMetres;
            var radius = WGS84EarthRadius(lat);
            var pradius = radius * Math.Cos(lat);
            var latMin = lat - halfSide / radius;
            var latMax = lat + halfSide / radius;
            var lonMin = lon - halfSide / pradius;
            var lonMax = lon + halfSide / pradius;

            return new Bounds
            {
                MinPoint = new GeoPoint { Latitude = RadiansToDegrees(latMin), Longitude = RadiansToDegrees(lonMin) },
                MaxPoint = new GeoPoint { Latitude = RadiansToDegrees(latMax), Longitude = RadiansToDegrees(lonMax) }
            };
        }

        private static double DegreesToRadians(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        private static double RadiansToDegrees(double radians)
        {
            return 180.0 * radians / Math.PI;
        }

        private static double WGS84EarthRadius(double lat)
        {
            var An = WGS84_a * WGS84_a * Math.Cos(lat);
            var Bn = WGS84_b * WGS84_b * Math.Sin(lat);
            var Ad = WGS84_a * Math.Cos(lat);
            var Bd = WGS84_b * Math.Sin(lat);
            return Math.Sqrt((An*An + Bn*Bn) / (Ad*Ad + Bd*Bd));
        }
    }
    
    public class Bounds
    {
        public GeoPoint MinPoint { get; set; }
        public GeoPoint MaxPoint { get; set; }
    }

    public class GeoPoint
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }    
}