
using AutoMapper;
using BarbarBot.Common.ModelsDto;
using BarberBot.BusinessLogic.Interfaces;
using BarberBot.Model.DataBaseContext;
using BarberBot.Model.Models;

namespace BarberBot.BusinessLogic.Services
{
    public class CityService : ICityService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public CityService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }
        public IEnumerable<CityDto> Get()
        {
            var city = _applicationContext.Cities.ToList();
            var cityDto = _mapper.Map<List<CityDto>>(city);
            return cityDto;
        }
        public CityDto Get(string Name)
        {
            var city = _applicationContext.Cities.FirstOrDefault(x => x.Name == Name);
            var cityDto = _mapper.Map<CityDto>(city);
            return cityDto;
        }
        public CityDto Get(int id)
        {
            var city = _applicationContext.Cities.FirstOrDefault(x => x.Id == id);
            var cityDto = _mapper.Map<CityDto>(city);
            return cityDto;
        }
    }
}
