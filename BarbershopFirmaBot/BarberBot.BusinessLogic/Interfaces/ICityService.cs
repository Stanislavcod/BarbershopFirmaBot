

using BarbarBot.Common.ModelsDto;

namespace BarberBot.BusinessLogic.Interfaces
{
    public interface ICityService
    {
        CityDto Get(int id);
        IEnumerable<CityDto> Get();
    }
}
