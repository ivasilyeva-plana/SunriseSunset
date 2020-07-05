using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SunriseSunset.Models
{
    public class SunriseSunsetApiResponseModel : ApiRequestError
    {
        [JsonProperty("results")]
        public SunriseSunsetModel SunriseSunset { get; set; }

    }

    public class SunriseSunsetModel
    {
        [JsonProperty("sunrise")]
        public DateTime Sunrise { get; set; }

        [JsonProperty("sunset")]
        public DateTime Sunset { get; set; }
    }

    public class ApiRequestError
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}