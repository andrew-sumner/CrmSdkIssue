using System;
using System.Threading;
using CrmSdkIssue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrmSdkIssueTests
{
    [TestClass]
    public class IssuesAzureTests
    {
        // Fails with System.IO.FileLoadException: Could not load file or assembly 'Newtonsoft.Json, Version=6.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)
        // if update Microsoft.Azure.KeyVault to latest version then all tests fail
        //
        // Packages: 
        // Microsoft.Azure.KeyVault:2.3.2
        // Microsoft.Azure.Services.AppAuthentication:1.3.1
        [TestMethod]
        public void KeyVaultTest()
        {
            string secretUrl = "<secret url>";

            var secret = AzureKeyVault.GetSecret(secretUrl);

            Assert.IsNotNull(secret);
        }

        // Packages: 
        // Microsoft.Graph:1.19.0
        // Microsoft.Identity.Client:4.6.0
        [TestMethod]
        public void GraphTest()
        {
            ADDAppConnectionDetails connection = new ADDAppConnectionDetails
            {
                ApplicationId = "<Azure AD App Id>",
                Authority = "https://login.microsoftonline.com/<domain>.onmicrosoft.com",
                Password = "<password>",
                RedirectUri = "http://<domain>.onmicrosoft.com:5000/signin-oidc"
            };

            string username = "<email>";

            var graph = Graph.GetConnection(connection);
            var user = graph.GetUser(username);

            Assert.IsNotNull(user);
        }

        // NOTE: fails with 
        //    System.IO.FileLoadException: Could not load file or assembly 'Microsoft.IdentityModel.Clients.ActiveDirectory, Version=3.19.8.16603, Culture=neutral, PublicKeyToken=31bf3856ad364e35' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference
        // Using 'NEW' CrmServiceClient
        // Packages:
        // Microsoft.CrmSdk.XrmTooling.CoreAssembly:9.1.0.21
        [TestMethod]
        public void CRMTest()
        {
            var authType = "Office365";
            string url = "https://<instance name>.api.crm6.dynamics.com";
            string username = "<user>";
            string password = "<password>";
            
            string cs = $"ServiceUri={url};AuthType={authType};UserName={username};Password={password}";            
            var crm = CRMClient.GetConnection(cs);
            var version = crm.GetVersion();

            Assert.IsNotNull(version);
        }
    }
}
