using System.Collections.Generic;
using System.Web.Mvc;

namespace SunriseSunset.Models
{
    public class CitiesListViewModel
    {
        public List<CitySunriseSunsetInfoModel> Cities { get; set; }
        public SelectList CitySelectList { get; set; }
        public SelectList OperationTypes { get; set; }
    }
}