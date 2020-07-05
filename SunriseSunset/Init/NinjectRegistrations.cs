using Ninject.Modules;
using SunriseSunset.Models;
using SunriseSunset.Network;
using SunriseSunset.Repositories;

namespace SunriseSunset.Init
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<Context>().ToSelf();
            Bind<ICityRepository>().To<CityRepository>();
            Bind<ISunriseSunsetApi>().To<SunriseSunsetApi>()
                .WithConstructorArgument("url", "https://api.sunrise-sunset.org/json");
        }
    }
}