using System;

namespace BernieApp.Common.DependencyInjection
{
    public interface IDependencyInjectionService
    {
        T Resolve<T>();
        void ConfigureNavigationService(Func<IConfigurableNavigationService> navConfigDelegate);
    }
}
