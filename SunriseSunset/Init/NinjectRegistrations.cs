using Ninject.Modules;
using SunriseSunset.Entities;
using SunriseSunset.Network;

namespace SunriseSunset.Init
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<Context>().ToSelf();
            Bind<ICityRepository>().To<CityRepository>();
        }
    }
}