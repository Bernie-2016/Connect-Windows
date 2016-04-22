using System;

namespace BernieApp.Portable.Client
{
    public static class Endpoints 
    {
        //News
        public const string SharknadoBaseUrl = "https://elasticsearch.movementapp.io/articles_en_v1/berniesanders_com/_search";
        public const string NewsBaseUrl = SharknadoBaseUrl;

        //Videos
        public const string VideosBaseurl = "https://elasticsearch.movementapp.io/videos_v1/_search";

        //Events
        public const string EventsBaseUrl = "https://elasticsearch.movementapp.io/events_en_v1/berniesanders_com/_search";

        //Actions
        public const string ConnectBaseUrl = "https://connect.berniesanders.com/api/action_alerts";
        public const string ActionAlertsUrl = ConnectBaseUrl;
        //Staging URL: https://sanders-connect-staging.herokuapp.com/api/action_alerts 

        public const string PrivacyUrl = "https://berniesanders.com/privacy-policy/";

        public const string SlackUrl = "https://connectwithbernieslack.herokuapp.com";

        public const string GithubUrl = "https://github.com/Bernie-2016/Connect-Windows";

        public const string FeedbackUrl = "https://docs.google.com/forms/d/1gE0hwL9AaUjovr4QE_0oD0A1BT1lB3GGGktHB8amHXs/viewform";
    }
}
