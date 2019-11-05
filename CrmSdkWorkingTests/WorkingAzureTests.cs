using System;
using System.Threading;
using CrmSdkIssue;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrmSdkWorkingTests
{
    [TestClass]
    public class WorkingAzureTests
    {
        // NOTE: If use version greater than 2.3.2 fails with
        //             System.IO.FileLoadException: Could not load file or assembly 'Newtonsoft.Json, Version=6.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed' or one of its dependencies. The located assembly's manifest definition does not match the assembly reference. (Exception from HRESULT: 0x80131040)
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
                ApplicationId = "<azure ad app id>",
                Authority = "https://login.microsoftonline.com/<domain>.onmicrosoft.com",
                Password = "<password>",
                RedirectUri = "http://<domain>.onmicrosoft.com:5000/signin-oidc"
            };

            string username = "<email>";

            var graph = Graph.GetConnection(connection);
            var user = graph.GetUser(username);

            Assert.IsNotNull(user);
        }

        // Using 'OLD' OrganizationServiceProxy
        // Packages:
        // Microsoft.CrmSdk.CoreAssemblies:9.0.2.19
        [TestMethod]
        public void CRMTest()
        {
            string serviceurl = "https://<instance name>.api.crm6.dynamics.com/XRMServices/2011/Organization.svc";
            string username = "<user>";
            string password = "<password>";

            var crm = CRM.GetConnection(serviceurl, username, password);
            var version = crm.GetVersion();

            Assert.IsNotNull(version);
        }
    }
}
