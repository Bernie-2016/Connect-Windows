using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BernieApp.Portable.Client;
using BernieApp.Portable.Client.ES4BS;
using BernieApp.Portable.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace BernieApp.UWP.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Changed to MainViewModel from ViewModelBase. May not change or help at all
            if (MainViewModel.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IBernieClient, DesignTimeBernieClient>();
            }
            else
            {
                SimpleIoc.Default.Register(
                    () => new FeedClient<FeedEntry>(
                        Endpoints.NewsBaseUrl));

                SimpleIoc.Default.Register(
                    () => new ActionClient<ActionAlert>(
                        Endpoints.ActionAlertsUrl));

                SimpleIoc.Default.Register<IBernieClient, LiveBernieClient>();
            }
            SimpleIoc.Default.Register<NewsViewModel>();
            SimpleIoc.Default.Register<NewsDetailViewModel>(true);
            SimpleIoc.Default.Register<ActionsViewModel>();
            SimpleIoc.Default.Register<EventsViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<RegisterViewModel>();
        }

        public NewsViewModel NewsViewModel => SimpleIoc.Default.GetInstance<NewsViewModel>();
        public NewsDetailViewModel NewsDetailViewModel => SimpleIoc.Default.GetInstance<NewsDetailViewModel>();
        public ActionsViewModel ActionsViewModel => SimpleIoc.Default.GetInstance<ActionsViewModel>();
        public EventsViewModel NearbyViewModel => SimpleIoc.Default.GetInstance<EventsViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();
        public RegisterViewModel RegisterViewModel => SimpleIoc.Default.GetInstance<RegisterViewModel>();
    }
}
