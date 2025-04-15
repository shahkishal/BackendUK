using AutoMapper;
using testingUK.Model;
using testingUK.Model.Dto;

namespace testingUK.MappingsS
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Trip, TripDto>().ReverseMap();
            CreateMap<Trip, AddTripDto>().ReverseMap();

            //TravelType Mapping

            CreateMap<TravelType, TravelTypeDto>().ReverseMap();

        }
    }
}
