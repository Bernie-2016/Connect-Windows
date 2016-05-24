using System.Collections.Generic;
using System.Threading.Tasks;
using BernieApp.UWP.Messages;
using BernieApp.UWP.View;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Xaml.Navigation;
using System;

namespace BernieApp.UWP.ViewModels
{
    public class WelcomeViewModel : MainViewModel
    {
        public WelcomeViewModel()
        {
            GetStartedCommand = new RelayCommand(OnGetStartedClick);
        }

        public string WelcomeText
        {
            get
            {
                //Removed from message until EVENTS is added: The 'Events' section puts the campaign's full grassroots event list at your fingertips. Attend these events to meet fellow activists and work to canvass and GOTV. We hope you'll make some lifelong friends in the process. :)
                return
@"Connect with Bernie is designed to help you make a difference every day in our race to win the White House.

Inside, you'll find urgent social media messages from the campaign. Share them with your friends to help shape the discussion around our movement.

Remember - to win this campaign, all of us must be deply involved. Our movement needs people like you to help it succeed.

In Solidarity,
The Connect with Bernie Team";
            }
        }

        public RelayCommand GetStartedCommand { get; set; }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            Messenger.Default.Send<WelcomeMessage>(new WelcomeMessage() { WelcomeMessageType = WelcomeMessageType.Initial });
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        public void OnGetStartedClick()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["lastRunDate"] = DateTime.UtcNow.ToString();
            Messenger.Default.Send<WelcomeMessage>(new WelcomeMessage() { WelcomeMessageType = WelcomeMessageType.Accept });
            NavigationService.Navigate(typeof(NewsPage));
        }


    }
}
