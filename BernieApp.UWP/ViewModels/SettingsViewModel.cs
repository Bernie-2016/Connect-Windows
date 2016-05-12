using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Services.SettingsService;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace BernieApp.UWP.ViewModels
{
    public class SettingsViewModel : MainViewModel
    {
        public Services.SettingsService _settings;
        private RelayCommand _slackButtonCommand;
        private RelayCommand _githubButtonCommand;
        

        public Uri SlackUrl => new Uri(Portable.Client.Endpoints.SlackUrl);

        public Uri GithubUrl => new Uri(Portable.Client.Endpoints.GithubUrl);

        public Uri FeedbackUrl => new Uri(Portable.Client.Endpoints.FeedbackUrl);

        public Uri PrivacyUrl => new Uri(Portable.Client.Endpoints.PrivacyUrl);

        public SettingsViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime
            }
            else
            {
                _settings = Services.SettingsService.Instance;
            }
        }

        //General
        public bool UseShellBackButton
        {
            get { return _settings.UseShellBackButton; }
            set { _settings.UseShellBackButton = value; base.RaisePropertyChanged(); }
        }

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set { _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; base.RaisePropertyChanged(); }
        }

        //Analytics
        public bool UseAnalytics
        {
            get { return _settings.UseAnalytics; }
            set { _settings.UseAnalytics = value;  base.RaisePropertyChanged(); }
        }

        //About
        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

        public RelayCommand SlackButtonCommand
        {
            get
            {
                if (_slackButtonCommand == null)
                {
                    _slackButtonCommand = new RelayCommand(async () =>
                    {
                        var success = await Launcher.LaunchUriAsync(SlackUrl);
                        if (success)
                        {
                            Debug.WriteLine("url successfully opened in browser");
                        }
                        else
                        {
                            var dialog = new MessageDialog("Unable to open the webpage, please try again later.", "Oops...");
                            await dialog.ShowAsync();
                        }
                    });
                }
                return _slackButtonCommand;
            }
        }

        public RelayCommand GithubButtonCommand
        {
            get
            {
                if (_githubButtonCommand == null)
                {
                    _githubButtonCommand = new RelayCommand(async () =>
                    {
                        var success = await Launcher.LaunchUriAsync(GithubUrl);
                        if (success)
                        {
                            Debug.WriteLine("url successfully opened in browser");
                        }
                        else
                        {
                            var dialog = new MessageDialog("Unable to open the webpage, please try again later.", "Oops...");
                            await dialog.ShowAsync();
                        }
                    });
                }
                return _githubButtonCommand;
            }
        }
    }
}
