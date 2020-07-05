using SunriseSunset.Models;
using SunriseSunset.Network;
using SunriseSunset.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SunriseSunset.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICityRepository _cityRepository;
        private readonly ISunriseSunsetApi _sunriseSunsetApi;

        public HomeController(ICityRepository cityRepository, 
            ISunriseSunsetApi sunriseSunsetApi)
        {
            _cityRepository = cityRepository;
            _sunriseSunsetApi = sunriseSunsetApi;
        }

        public async Task<ActionResult> Index()
        {
            var cityList = await _cityRepository.ListAsync();
            var citySunriseSunsetInfoMode = new List<CitySunriseSunsetInfoModel>();

            foreach (var item in cityList)
            {
                citySunriseSunsetInfoMode.Add(await GetCitySunriseSunSetInfo(item));
            }

            return View(citySunriseSunsetInfoMode);
        }

        private async Task<CitySunriseSunsetInfoModel> GetCitySunriseSunSetInfo(CityModel city)
        {
            var sunriseSunsetData = await _sunriseSunsetApi.GetSunriseSunsetMessage(city.Latitude, city.Longitude);
            return new CitySunriseSunsetInfoModel
            {
                CityName = city.Name,
                Sunrise = sunriseSunsetData.Sunrise.ToLocalTime().ToString("h:mm:ss tt"),
                Sunset = sunriseSunsetData.Sunset.ToLocalTime().ToString("h:mm:ss tt")
            };
        }

    }
}