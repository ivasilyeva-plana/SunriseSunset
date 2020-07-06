using SunriseSunset.HandleError;
using SunriseSunset.Models;
using SunriseSunset.Network;
using SunriseSunset.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SunriseSunset.Controllers
{
    [CustomHandleError]
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

        public async Task<ActionResult> Index(string city, int? selectionType)
        {
            var cityList = (await _cityRepository.ListAsync()).ToList();

            var cityModels = string.IsNullOrEmpty(city) 
                ? cityList
                : cityList.Where(c => string.Equals(city, c.Key)).ToList();
            
           var tasks = cityModels.Select(GetCitySunriseSunSetInfo);

           var citySunriseSunsetInfoModel = await Task.WhenAll(tasks);
           
            var operationTypes = 
                from SelectionType d in Enum.GetValues(typeof(SelectionType))
                select new { ID = (int)d, Name = d.ToString() };
            

            return View(new CitiesListViewModel
            {
                Cities = citySunriseSunsetInfoModel,
                CitySelectList = new SelectList(cityList, "Key", "Name"),
                SelectionColumn = new SelectList(operationTypes, "ID", "Name"),
                SelectionColumnValue = selectionType
            });
        }


        private async Task<CitySunriseSunsetInfoModel> GetCitySunriseSunSetInfo(CityModel city)
        {
            var sunriseSunsetData = await _sunriseSunsetApi.GetSunriseSunsetMessageAsync(city.Latitude, city.Longitude);
            return new CitySunriseSunsetInfoModel
            {
                CityName = city.Name,
                Sunrise = sunriseSunsetData.Sunrise.ToLocalTime(),
                Sunset = sunriseSunsetData.Sunset.ToLocalTime()
            };
        }

    }
}