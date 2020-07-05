using SunriseSunset.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SunriseSunset.Network
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityModel>> ListAsync();
        Task CreateAsync(CityModel city);
        Task EditAsync(CityModel city);
        Task DeleteAsync(int id);
        Task<CityModel> GetAsync(int id);
    }
}
