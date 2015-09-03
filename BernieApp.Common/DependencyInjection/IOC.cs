namespace BernieApp.Common.DependencyInjection
{
    public class IOC 
    {
        private static IDependencyInjectionService _default;

        public static IDependencyInjectionService Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new AutofacDIService();
                }

                return _default;
            }
        }
    }
}
