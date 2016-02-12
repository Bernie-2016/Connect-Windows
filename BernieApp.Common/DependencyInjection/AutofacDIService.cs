using Autofac;
using BernieApp.Common.Design;
using BernieApp.Common.Http;
using BernieApp.Common.Models;
using BernieApp.Common.ViewModels;
using GalaSoft.MvvmLight;
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

            if (ViewModelBase.IsInDesignModeStatic)
            {
                builder.RegisterInstance(new DesignBernieHttpClient()).As<IBernieHttpClient>();
                builder.RegisterInstance(new DesignNavigationService()).As<INavigationService>();
            }
            else
            {
                builder.RegisterType<IssuesClient>().AsSelf();
                builder.RegisterType<NewsClient>().AsSelf();
                builder.RegisterType<BernieHttpClient>().As<IBernieHttpClient>();
                var navSvc = CreateNavigationService();
                if (navSvc != null)
                {
                    builder.RegisterInstance(CreateNavigationService()).As<INavigationService>();
                }
            }

            _container = builder.Build();
        }

        private INavigationService CreateNavigationService()
        {
            if (_navConfigDelegate == null)
            {
                return null;
            }

            return _navConfigDelegate();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(MainViewModel));
            builder.RegisterType(typeof(NewsItemViewModel));
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