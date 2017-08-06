using Friendly.Controllers.Requests;
using Friendly.Models;
using Friendly.Models.ApiModels;

namespace Friendly
{
    public static class FriendlyMapperProfile
    {
        public static void RegisterMappings()
        {
            // Request mappings

            AutoMapper.Mapper.CreateMap<LocationRequest, Location>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.LocationId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.LocationName))
                .ForMember(dest => dest.LocationType, opts => opts.Ignore());

            AutoMapper.Mapper.CreateMap<LocationTypeRequest, LocationType>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.LocationTypeId))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.LocationTypeName))
                .ForMember(dest => dest.Checks, opts => opts.Ignore())
                .ForMember(dest => dest.Ratings, opts => opts.Ignore());

            AutoMapper.Mapper.CreateMap<ReviewLocationRequest, LocationReview>();

            // Api model mappings

            AutoMapper.Mapper.CreateMap<Location, LocationApiModel>();
            AutoMapper.Mapper.CreateMap<LocationType, LocationTypeApiModel>();
            AutoMapper.Mapper.CreateMap<LocationReview, LocationReviewApiModel>();
            AutoMapper.Mapper.CreateMap<Check, CheckApiModel>();
            AutoMapper.Mapper.CreateMap<Rating, RatingApiModel>();
            AutoMapper.Mapper.CreateMap<Tag, TagApiModel>();
            AutoMapper.Mapper.CreateMap<CheckScore, CheckScoreApiModel>();
            AutoMapper.Mapper.CreateMap<RatingScore, RatingScoreApiModel>();
        }

    }
}