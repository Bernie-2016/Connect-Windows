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

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IBernieClient, DesignTimeBernieClient>();
            }
            else
            {
                SimpleIoc.Default.Register(
                    () => new FeedClient<FeedEntry>(
                        Endpoints.NewsBaseUrl,
                        new FeedFilter { Type = FeedItemType.News }));

                // ERROR: Can't register two FeedClients
                //SimpleIoc.Default.Register(
                //    () => new FeedClient<FeedEntry>(
                //        Endpoints.IssuesBaseUrl,
                //        new FeedFilter { Type = FeedItemType.Issues }));

                SimpleIoc.Default.Register<IBernieClient, LiveBernieClient>();
            }
            SimpleIoc.Default.Register<NewsViewModel>();
            SimpleIoc.Default.Register<IssuesViewModel>();
            SimpleIoc.Default.Register<ActionsViewModel>();
            SimpleIoc.Default.Register<NearbyViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
        }

        public NewsViewModel NewsViewModel => SimpleIoc.Default.GetInstance<NewsViewModel>();
        public IssuesViewModel IssuesViewModel => SimpleIoc.Default.GetInstance<IssuesViewModel>();
        public ActionsViewModel ActionsViewModel => SimpleIoc.Default.GetInstance<ActionsViewModel>();
        public NearbyViewModel NearbyViewModel => SimpleIoc.Default.GetInstance<NearbyViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();
    }
}
