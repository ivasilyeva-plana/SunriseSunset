using Ninject;
using Ninject.Web.Common.WebHost;
using SunriseSunset.Init;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Extensions.Logging.Log4net;

namespace SunriseSunset
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {

            var registrations = new NinjectRegistrations();
            var kernel = new StandardKernel(registrations);
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private void RegisterServices(IKernel kernel)
        {
            // e.g. kernel.Load(Assembly.GetExecutingAssembly());
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            Database.SetInitializer(new DbInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
