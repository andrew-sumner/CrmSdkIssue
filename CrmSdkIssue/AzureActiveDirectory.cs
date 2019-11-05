using Microsoft.Graph;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CrmSdkIssue
{
    public static class AzureActiveDirectory
    {
        // Use scopes defined by the Azure AD App we're connecting to
        private static readonly string[] scopes = { "https://graph.microsoft.com/.default" };
        private static IConfidentialClientApplication confidentialClientApp;

        public static GraphServiceClient GetGraphServiceClient(ADDAppConnectionDetails connection)
        {
            if (confidentialClientApp == null)
            {
                confidentialClientApp = ConfidentialClientApplicationBuilder
                    .Create(connection.ApplicationId)
                    .WithAuthority(connection.Authority)
                    .WithClientSecret(connection.Password)
                    .Build();
            }

            GraphServiceClient graphClient = new GraphServiceClient(
                "https://graph.microsoft.com/v1.0",
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                        var token = await GetTokenForApplicationAsync();
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                    }));

            return graphClient;
        }

        private static async Task<string> GetTokenForApplicationAsync()
        {
            AuthenticationResult authResult = await confidentialClientApp.AcquireTokenForClient(scopes).ExecuteAsync();
            return authResult.AccessToken;
        }
    }

    public class ADDAppConnectionDetails
    {
        public string ApplicationId { get; set; }
        public string Password { get; set; }
        public string RedirectUri { get; set; }
        public string Authority { get; set; }
    }
}
