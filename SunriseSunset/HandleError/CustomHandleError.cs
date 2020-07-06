using System;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace SunriseSunset.HandleError
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        private static readonly ILog Log = LogManager
            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                Log.Error(filterContext.Exception.Message);
            }
            base.OnException(filterContext);
        }
    }
}