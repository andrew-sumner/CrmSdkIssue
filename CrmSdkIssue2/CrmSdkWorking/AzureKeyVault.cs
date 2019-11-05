using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Threading.Tasks;

namespace CrmSdkIssue
{
    /**
     * Service-to-service authentication to Azure Key Vault 
     * https://blogs.msdn.microsoft.com/cloud_solution_architect/2017/12/22/using-managed-service-identities-in-functions-to-access-key-vault/
     * https://docs.microsoft.com/en-us/azure/key-vault/service-to-service-authentication
     */
    public static class AzureKeyVault
    {
        private static KeyVaultClient keyVaultClient = null;

        static AzureKeyVault()
        {
            // NOTE: This only works when running via visual studio or logged on 
            // Does not work from service even if assinged identity https://blog.bitscry.com/2019/02/13/using-azure-key-vault-in-a-console-application/

            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
        }

        /// <summary>
        /// Get secret value from Azure Key Vault.
        /// <param name="secret">Full secret url</param>
        /// <returns>Secret value</returns>
        public static string GetSecret(string secret)
        {
            return GetSecretAsync(secret).Result;
        }

        /// <summary>
        /// Get secret value from Azure Key Vault.
        /// <param name="secret">Full secret url</param>
        /// <returns>Secret value</returns>
        public static async Task<string> GetSecretAsync(string secretUrl)
        {
            SecretBundle result;

            try
            {
                result = await keyVaultClient.GetSecretAsync(secretUrl).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new KeyVaultErrorException($"Key Vault '{secretUrl}' secret was not found.", ex);
            }

            if (String.IsNullOrEmpty(result.Value))
            {
                throw new ArgumentException($"Key Vault '{secretUrl}' secret is not configured.");
            }

            return result.Value;
        }
    }
}
