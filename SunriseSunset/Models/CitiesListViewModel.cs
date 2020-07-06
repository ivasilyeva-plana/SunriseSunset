using System.Collections.Generic;
using System.Web.Mvc;

namespace SunriseSunset.Models
{
    public class CitiesListViewModel
    {
        public ICollection<CitySunriseSunsetInfoModel> Cities { get; set; }
        public SelectList CitySelectList { get; set; }
        public SelectList SelectionColumn { get; set; }
        public int? SelectionColumnValue { get; set; }
    }
}