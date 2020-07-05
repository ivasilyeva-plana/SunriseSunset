using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SunriseSunset.Models;
using SunriseSunset.Network;
using SunriseSunset.Repositories;

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
                var sunriseSunsetData = await _sunriseSunsetApi.GetSunriseSunsetMessage(item.Latitude, item.Longitude);
                citySunriseSunsetInfoMode.Add(new CitySunriseSunsetInfoModel
                {
                    CityName = item.Name,
                    Sunrise = sunriseSunsetData.Sunrise.ToLocalTime().ToString("h:mm:ss tt"),
                    Sunset = sunriseSunsetData.Sunset.ToLocalTime().ToString("h:mm:ss tt")
                });
            }

            return View(citySunriseSunsetInfoMode);
        }


    }
}