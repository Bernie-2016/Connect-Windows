using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BernieApp.UWP.ViewModels
{
    public class ViewModelLocator
    {
        private NewsViewModel _NewsViewModel;
        public NewsViewModel NewsViewModel =>
            _NewsViewModel ?? (_NewsViewModel = new NewsViewModel());

        private ActionsViewModel _ActionsViewModel;
        public ActionsViewModel ActionsViewModel =>
            _ActionsViewModel ?? (_ActionsViewModel = new ActionsViewModel());

        private NearbyViewModel _NearbyViewModel;
        public NearbyViewModel NearbyViewModel =>
            _NearbyViewModel ?? (_NearbyViewModel = new NearbyViewModel());

        private IssuesViewModel _IssuesViewModel;
        public IssuesViewModel IssuesViewModel =>
            _IssuesViewModel ?? (_IssuesViewModel = new IssuesViewModel());

        private SettingsViewModel _SettingsViewModel;
        public SettingsViewModel SettingsViewModel =>
            _SettingsViewModel ?? (_SettingsViewModel = new SettingsViewModel());
    }
}
