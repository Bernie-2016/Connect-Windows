using GalaSoft.MvvmLight.Views;
using System;

namespace BernieApp.Common.DependencyInjection
{
    public interface IConfigurableNavigationService : INavigationService
    {
        Func<Type, object, bool> NavigateDelegate { get; set; }
        void Configure(string pageKey, Type pageType);
    }
}
