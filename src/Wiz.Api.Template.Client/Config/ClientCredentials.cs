namespace Wiz.Api.Template.Client.Config
{
    public class ClientCredentials
    {
        public ClientCredentials(string baseAuthUrl, string clientId, string clientSecret, string scope)
        {
            this.BaseAuthUrl = baseAuthUrl;
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
            this.Scope = scope;

        }
        public string BaseAuthUrl { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string Scope { get; }

    }
}