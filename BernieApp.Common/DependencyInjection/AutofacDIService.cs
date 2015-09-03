using Autofac;
using BernieApp.Common.ViewModels;

namespace BernieApp.Common.DependencyInjection
{
    public class AutofacDIService : IDependencyInjectionService
    {
        private IContainer _container;

        public AutofacDIService()
        {
            Init();
        }

        private void Init()
        {
            var builder = new ContainerBuilder();

            RegisterViewModels(builder);

            _container = builder.Build();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterInstance(new MainViewModel());
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}