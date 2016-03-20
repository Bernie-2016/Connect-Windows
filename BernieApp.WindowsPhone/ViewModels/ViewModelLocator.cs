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
using Microsoft.Practices.ServiceLocation; //Needto find this for WinRT!!!


namespace BernieApp.WindowsPhone.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = CreateNavigationService();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IBernieClient, DesignTimeBernieClient>();
                SimpleIoc.Default.Register<INavigationService>(
                    () => navigationService);
            }
            else
            {
                SimpleIoc.Default.Register(
                    () => new FeedClient<FeedEntry>(
                        Endpoints.NewsBaseUrl));
                SimpleIoc.Default.Register<IBernieClient, LiveBernieClient>();
                SimpleIoc.Default.Register<INavigationService>(
                    () => new NavigationService());
            }
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
            navigationService.Configure("HubPage", typeof(HubPage));
            navigationService.Configure("NewsDetailPage", typeof(NewsDetailPage));
            navigationService.Configure("ActionsPage", typeof(ActionsPage));
            navigationService.Configure("NearbyPage", typeof(NearbyPage));
            navigationService.Configure("SettingsPage", typeof(SettingsPage));

            return navigationService;
        }
    }
}

