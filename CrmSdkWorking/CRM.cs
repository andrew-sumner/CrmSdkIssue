using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;


namespace CrmSdkIssue
{
    public class CRM
    {
        public OrganizationServiceProxy OrganizationService { get; private set; }

        public static CRM GetConnection(string serviceUrl, string username, string password)
        {
            return new CRM(serviceUrl, username, password);
        }

        private CRM(string serviceUrl, string username, string password)
        {
            Connect(serviceUrl, username, password);
        }

        private void Connect(string serviceUrl, string username, string password)
        { 
            Console.WriteLine($"Establishing new CRM connection to {serviceUrl} for {username}...");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ClientCredentials credentials = new ClientCredentials();
            credentials.UserName.UserName = username;
            credentials.UserName.Password = password;

            IServiceManagement<IOrganizationService> orgServiceManagement = ServiceConfigurationFactory.CreateManagement<IOrganizationService>(new Uri(serviceUrl));

            OrganizationService = new OrganizationServiceProxy(orgServiceManagement, credentials);
            OrganizationService.Authenticate();
        }

        public RetrieveVersionResponse GetVersion()
        {
            RetrieveVersionRequest versionRequest = new RetrieveVersionRequest();
            return (RetrieveVersionResponse)OrganizationService.Execute(versionRequest);
        }
    }
}
