namespace BernieApp.Common.Http
{
    public static class Endpoints 
    {
        public const string SharknadoBaseUrl = "https://elasticsearch.movementapp.io/articles_en_v1/berniesanders_com/_search";
        public const string IssuesBaseUrl = SharknadoBaseUrl;
        public const string NewsBaseUrl = SharknadoBaseUrl;

        // specifics unknown
        public const string ConnectBaseUrl = "https://sanders-connect-staging.herokuapp.com/";
        public const string ActionAlertsUrl = ConnectBaseUrl;
    }
}
