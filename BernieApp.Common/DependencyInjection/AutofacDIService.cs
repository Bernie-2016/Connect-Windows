using Autofac;
using BernieApp.Common.Models;
using BernieApp.Common.ViewModels;
using GalaSoft.MvvmLight.Views;
using System;

namespace BernieApp.Common.DependencyInjection
{
    public class AutofacDIService : IDependencyInjectionService
    {
        private IContainer _container;
        private Func<INavigationService> _navConfigDelegate;
        private bool _initialized;

        private void Init()
        {
            _initialized = true;

            var builder = new ContainerBuilder();

            RegisterViewModels(builder);
            builder.RegisterInstance(CreateNavigationService());

            _container = builder.Build();
        }

        private INavigationService CreateNavigationService()
        {
            if (_navConfigDelegate == null)
            {
                throw new Exception("Call IOC.Default.ConfigureNavigationService to the configure the NavigationService");
            }
            var navService = _navConfigDelegate();

            return navService;
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(MainViewModel));
        } 

        public T Resolve<T>()
        {
            if (!_initialized)
            {
                Init();
            }

            return _container.Resolve<T>();
        }

        // todo : Improve this
        public void ConfigureNavigationService(Func<IConfigurableNavigationService> navConfigDelegate)
        {           
            _navConfigDelegate = navConfigDelegate;
        }
    }
}