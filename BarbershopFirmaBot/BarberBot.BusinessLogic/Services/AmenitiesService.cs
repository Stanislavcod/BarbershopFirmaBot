

using AutoMapper;
using BarbarBot.Common.ModelsDto;
using BarberBot.BusinessLogic.Interfaces;
using BarberBot.Model.DataBaseContext;

namespace BarberBot.BusinessLogic.Services
{
    public class AmenitiesService : IAmenitiesService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;
        public AmenitiesService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public IEnumerable<AmenitiesDto> Get()
        {
            var amenities = _applicationContext.Amenities.ToList();
            var amenitiesDto = _mapper.Map<List<AmenitiesDto>>(amenities);
            return amenitiesDto;
        }
        public AmenitiesDto Get(int id)
        {
            var amenities = _applicationContext.Amenities.FirstOrDefault(x => x.Id == id);
            var amenitiesDto = _mapper.Map<AmenitiesDto>(amenities);
            return amenitiesDto;    
        }
    }
}
