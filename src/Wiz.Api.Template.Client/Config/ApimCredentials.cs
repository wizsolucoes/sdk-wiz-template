namespace Wiz.Api.Template.Client.Config
{
    public class ApimCredentials: ICredentials
    {
        public ApimCredentials(string baseAuthUrl, string subscriptionKey, string apiKey)
        {
            this.BaseAuthUrl = baseAuthUrl;
            this.SubscriptionKey = subscriptionKey;
            this.ApiKey = apiKey;

        }
        public string BaseAuthUrl { get; }
        public string SubscriptionKey { get; }
        public string ApiKey { get; }
    }
}