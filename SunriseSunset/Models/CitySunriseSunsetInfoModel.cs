using System;

namespace SunriseSunset.Models
{
    public class CitySunriseSunsetInfoModel
    {
        public string CityName { get; set; }
        public DateTime? Sunrise { get; set; }
        public DateTime? Sunset { get; set; }
    }
}