using System;

namespace BernieApp.Portable.Client
{
    public static class Endpoints 
    {
        //News
        public const string NewsBaseUrl = "https://sharknado.berniesanders.com/articles_en_v1/berniesanders_com/_search";

        //Videos
        public const string VideosBaseurl = "https://elasticsearch.movementapp.io/videos_v1/_search";

        //Events
        public const string EventsBaseUrl = "https://elasticsearch.movementapp.io/events_en_v1/berniesanders_com/_search"; //outdated

        //Actions
        public const string ActionAlertsUrl = "https://connect.berniesanders.com/api/action_alerts";
        //Staging URL: https://sanders-connect-staging.herokuapp.com/api/action_alerts 

        //Parse Keys
        //Production
        public const string APPID = "7t3fWZ0hzy65jgutvlvyexj3toCk3eiTNZlxBIcd";
        public const string NETKEY = "61r0pfuyZ9Od13ea0lXvbqYh9YDB0eHzxdRJUmfM";
        //QA
        //public const string APPID = "r1LFJIyrrqlqGpNtbax93vTeq4NSM1QVd3pwzT6O";
        //public const string NETKEY = "yJV5Mdu3xCNS9Mrvts1CM0MZfzY8TPov0CwZAtXv";

        public const string PrivacyUrl = "https://berniesanders.com/privacy-policy/";

        public const string SlackUrl = "https://connectwithbernieslack.herokuapp.com";

        public const string GithubUrl = "https://github.com/Bernie-2016/Connect-Windows";

        public const string FeedbackUrl = "https://docs.google.com/forms/d/1gE0hwL9AaUjovr4QE_0oD0A1BT1lB3GGGktHB8amHXs/viewform";
    }
}
