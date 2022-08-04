

using BarbarBot.Common.ModelsDto;

namespace BarberBot.BusinessLogic.Interfaces
{
    public interface IAmenitiesService
    {
        AmenitiesDto Get(int id);
        IEnumerable<AmenitiesDto> Get();
    }
}
