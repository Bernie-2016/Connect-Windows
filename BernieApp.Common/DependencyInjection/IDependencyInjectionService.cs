namespace BernieApp.Common.DependencyInjection
{
    public interface IDependencyInjectionService
    {
        T Resolve<T>();
    }
}
