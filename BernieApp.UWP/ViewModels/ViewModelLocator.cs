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


        private NewsViewModel _NewsViewModel;
        public NewsViewModel NewsViewModel =>
            _NewsViewModel ?? (_NewsViewModel = new NewsViewModel());

        private IssuesViewModel _IssuesViewModel;
        public IssuesViewModel IssuesViewModel =>
            _IssuesViewModel ?? (_IssuesViewModel = new IssuesViewModel());

        private ActionsViewModel _ActionsViewModel;
        public ActionsViewModel ActionsViewModel =>
            _ActionsViewModel ?? (_ActionsViewModel = new ActionsViewModel());

        private NearbyViewModel _NearbyViewModel;
        public NearbyViewModel NearbyViewModel =>
            _NearbyViewModel ?? (_NearbyViewModel = new NearbyViewModel());

        private SettingsViewModel _SettingsViewModel;
        public SettingsViewModel SettingsViewModel =>
            _SettingsViewModel ?? (_SettingsViewModel = new SettingsViewModel());
    }
}
