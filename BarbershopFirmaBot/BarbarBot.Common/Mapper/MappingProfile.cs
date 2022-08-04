

using BarbarBot.Common.ModelsDto;
using BarberBot.Model.Models;
using AutoMapper;
using System.Text;

namespace BarbarBot.Common.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
            CreateMap<Amenities, AmenitiesDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Order,OrderDto>().ReverseMap();
        }
    }
}
