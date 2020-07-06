using System.Web.Mvc;
using SunriseSunset.HandleError;

namespace SunriseSunset
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
