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
        public const string ConnectBaseUrl = "https://sanders-connect-staging.herokuapp.com/api/action_alerts";
        public const string ActionAlertsUrl = ConnectBaseUrl;
    }
}
