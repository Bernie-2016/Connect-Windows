namespace BernieApp.Portable.Client
{
    public static class Endpoints 
    {
        //News
        public const string SharknadoBaseUrl = "https://elasticsearch.movementapp.io/articles_en_v1/berniesanders_com/_search";
        public const string NewsBaseUrl = SharknadoBaseUrl;

        //Events
        
        
        //Actions
        public const string ConnectBaseUrl = "https://sanders-connect-staging.herokuapp.com/";
        public const string ActionAlertsUrl = ConnectBaseUrl;
    }
}
