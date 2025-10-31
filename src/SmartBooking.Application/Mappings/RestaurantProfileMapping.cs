using AutoMapper;
using SmartBooking.Application.Dtos;
using SmartBooking.Core.Entities;

namespace SmartBooking.Application.Mapping
{
    public class RestaurantProfileMapping : Profile
    {
        public RestaurantProfileMapping()
        {
            CreateMap<RestaurantDto, Restaurant>();
            CreateMap<Restaurant, RestaurantReadDto>();


        }
    }
}
