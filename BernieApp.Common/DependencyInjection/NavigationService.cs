using BernieApp.Common.Models;
using System;
using System.Collections.Generic;

namespace BernieApp.Common.DependencyInjection
{
    public class NavigationService : IConfigurableNavigationService
    {
        private Dictionary<string, Type> _pageDict;

        public NavigationService()
        {
            _pageDict = new Dictionary<string, Type>();
        }

        public string CurrentPageKey
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        // todo : rename
        public Func<Type, object, bool> NavigateDelegate { get; set; }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey)
        {
            throw new NotImplementedException();
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            if (NavigateDelegate == null)
            {
                throw new Exception("NavigateDelegate not assigned");
            }

            Type pageType;
            if (!_pageDict.TryGetValue(pageKey, out pageType))
            {
                throw new Exception("Something went wrong :(");
            }                  
            
            if (!NavigateDelegate(pageType, parameter))
            {
                // todo : Implement WS/WP way of getting navigation vaild exception message

                //throw new Exception(string.Format("Navigation Failed: Key={0}, Type={1}", pageKey, pageType.Name));
            }
        }     

        public void Configure(string pageKey, Type pageType)
        {
            if (_pageDict.ContainsKey(pageKey))
            {
                throw new Exception(string.Format("Page for {0} already configured", pageKey));
            }

            _pageDict.Add(pageKey, pageType);
        }
    }
}
