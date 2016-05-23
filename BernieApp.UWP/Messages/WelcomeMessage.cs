namespace BernieApp.UWP.Messages
{
    public class WelcomeMessage
    {
        public WelcomeMessageType WelcomeMessageType { get; set; }
    }
    public enum WelcomeMessageType
    {
        Initial,
        Accept,
    }
}
