using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Wiz.Api.Template.Client.Config;
using System;

namespace Wiz.Api.Template.Client
{
    public partial class TemplateClient
    {
        private TokenResponse _token;
        private DateTime TokenExpiration;
        public ClientCredentials Config { get; }

        public TemplateClient(
            string baseServiceUrl,
            System.Net.Http.HttpClient httpClient,
            ClientCredentials config) : this(baseServiceUrl, httpClient)
        {
            Config = config;
        }

        public async System.Threading.Tasks.Task AuthWithClientCredentials(HttpClient httpClient, ClientCredentials config)
        {
            // Less than zero t1 is earlier than t2.
            Boolean isTokenExpired = DateTime.Compare(this.TokenExpiration, DateTime.Now) < 0;

            if (this._token == null || isTokenExpired)
            {
                DiscoveryDocumentResponse disco = await httpClient.GetDiscoveryDocumentAsync(config.BaseAuthUrl);

                this._token = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    ClientId = config.ClientId,
                    ClientSecret = config.ClientSecret,
                    Address = disco.TokenEndpoint,
                    Scope = config.Scope
                });
                this.TokenExpiration = DateTime.Now.AddSeconds((this._token.ExpiresIn - 60));
            }

            string authorizationHeader = "Authorization";
            if (httpClient.DefaultRequestHeaders.Contains(authorizationHeader))
            {
                httpClient.DefaultRequestHeaders.Remove(authorizationHeader);
            }

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(authorizationHeader, $"Bearer {this._token.AccessToken}");
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url)
        {
            AuthWithClientCredentials(client, this.Config).Wait();
        }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder)
        {
            AuthWithClientCredentials(client, this.Config).Wait();
        }
    }
}