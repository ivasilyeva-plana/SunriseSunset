using SunriseSunset.Models;
using System.Threading.Tasks;

namespace SunriseSunset.Network
{
    public interface ISunriseSunsetApi
    {
        Task<SunriseSunsetModel> GetSunriseSunsetMessage(double latitude, double longitude);
    }
}
