using System;
using GalaSoft.MvvmLight.Views;

namespace BernieApp.Common.Design
{
    public class DesignNavigationService : INavigationService
    {
        public string CurrentPageKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            //throw new NotImplementedException();
        }
    }
}
