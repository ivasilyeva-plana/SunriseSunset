using SunriseSunset.Models;
using System.Threading.Tasks;

namespace SunriseSunset.Network
{
    public interface ISunriseSunsetApi
    {
        Task<SunriseSunsetModel> GetSunriseSunsetMessageAsync(double latitude, double longitude);
    }
}
