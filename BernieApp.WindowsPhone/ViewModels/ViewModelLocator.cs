using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BernieApp.Portable.Client;
using BernieApp.Portable.Client.ES4BS;
using BernieApp.Portable.Models;
using BernieApp.WindowsPhone.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;


namespace BernieApp.WindowsPhone.ViewModels
{
    public class ViewModelLocator
    {
        #region Constants
        public const string NewsDetailPageKey = "NewsDetailPage";
        public const string HubPageKey = "HubPage";
        public const string ActionsPageKey = "ActionsPage";
        public const string NearbyPageKey = "NearbyPage";
        public const string SettingsPageKey = "SettingsPage";
        #endregion

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Navigation
            SimpleIoc.Default.Unregister<INavigationService>();
            var navigationService = CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(
                    () => navigationService);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IBernieClient, DesignTimeBernieClient>();
            }
            else
            {
                SimpleIoc.Default.Register(
                    () => new FeedClient<FeedEntry>(
                        Endpoints.NewsBaseUrl));
                SimpleIoc.Default.Register<IBernieClient, LiveBernieClient>();

            }

            //Register ViewModels
            SimpleIoc.Default.Register<HubPageViewModel>();
            SimpleIoc.Default.Register<NewsDetailViewModel>();
            SimpleIoc.Default.Register<ActionsViewModel>();
            SimpleIoc.Default.Register<NearbyViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        public HubPageViewModel HubPageViewModel => SimpleIoc.Default.GetInstance<HubPageViewModel>();
        public NewsDetailViewModel NewsDetailViewModel => SimpleIoc.Default.GetInstance<NewsDetailViewModel>();
        public ActionsViewModel ActionsViewModel => SimpleIoc.Default.GetInstance<ActionsViewModel>();
        public NearbyViewModel NearbyViewModel => SimpleIoc.Default.GetInstance<NearbyViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();


        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(HubPageKey, typeof(HubPage));
            navigationService.Configure(NewsDetailPageKey, typeof(NewsDetailPage));
            navigationService.Configure(ActionsPageKey, typeof(ActionsPage));
            navigationService.Configure(NearbyPageKey, typeof(NearbyPage));
            navigationService.Configure(SettingsPageKey, typeof(SettingsPage));

            return navigationService;
        }
    }
}

