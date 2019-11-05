using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;

namespace CrmSdkIssue
{
    public class CRMClient
    {
        public CrmServiceClient CrmServiceClient { get; private set; }
        public IOrganizationService OrganizationService { get; private set; }

        public static CRMClient GetConnection(string crmConnectionString)
        {
            return new CRMClient(crmConnectionString);
        }

        private CRMClient(string connectionString)
        {
            Connect(connectionString);
        }

        private void Connect(string connectionString)
        {
            CrmServiceClient = new CrmServiceClient(connectionString);
            OrganizationService = CrmServiceClient.OrganizationWebProxyClient != null ? (IOrganizationService)CrmServiceClient.OrganizationWebProxyClient : (IOrganizationService)CrmServiceClient.OrganizationServiceProxy;

            if (!CrmServiceClient.IsReady)
            {
                throw new Exception(CrmServiceClient.LastCrmError, CrmServiceClient.LastCrmException);
            }
        }

        public RetrieveVersionResponse GetVersion()
        {
            RetrieveVersionRequest versionRequest = new RetrieveVersionRequest();
            return (RetrieveVersionResponse)OrganizationService.Execute(versionRequest);
        }
    }
}
