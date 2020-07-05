using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace SunriseSunset.Extensions
{
    public static class JsonExtensions
    {
        public static T To<T>(this string self) => JsonConvert.DeserializeObject<T>(self);
    }
}