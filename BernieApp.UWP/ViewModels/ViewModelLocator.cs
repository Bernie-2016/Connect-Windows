using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using BernieApp.Common.Http;

namespace BernieApp.UWP.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //Create Design Time clients to use here instead of the runtime ones
                SimpleIoc.Default.Register<NewsClient>();
                SimpleIoc.Default.Register<IssuesClient>();
                //Add Actions client
                //Add Nearby client
            }
            else
            {
                SimpleIoc.Default.Register<NewsClient>();
                SimpleIoc.Default.Register<IssuesClient>();
                //Add Actions client
                //Add Nearby client
            }
            SimpleIoc.Default.Register<NewsViewModel>();
            SimpleIoc.Default.Register<IssuesViewModel>();
            SimpleIoc.Default.Register<ActionsViewModel>();
            SimpleIoc.Default.Register<NearbyViewModel>();
        }

        public NewsViewModel NewsViewModel => SimpleIoc.Default.GetInstance<NewsViewModel>();
        public IssuesViewModel IssuesViewModel => SimpleIoc.Default.GetInstance<IssuesViewModel>();
        public ActionsViewModel ActionsViewModel => SimpleIoc.Default.GetInstance<ActionsViewModel>();
        public NearbyViewModel NearbyViewModel => SimpleIoc.Default.GetInstance<NearbyViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();
    }
}
