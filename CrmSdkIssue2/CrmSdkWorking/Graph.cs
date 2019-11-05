using Microsoft.Graph;
using System;
using System.Linq;

namespace CrmSdkIssue
{
    /**
     * Utilities for access the Microsoft Graph API.
     * 
     * Useful Links:
     *  - Graph Explorer: https://developer.microsoft.com/en-us/graph/graph-explorer
     *  - Graph API: https://developer.microsoft.com/en-us/graph/docs/api-reference/v1.0/resources/group
     *  - Query Parameters: https://developer.microsoft.com/en-us/graph/docs/concepts/query_parameters
     */
    public class Graph
    {
        GraphServiceClient graphClient;

        public static Graph GetConnection(ADDAppConnectionDetails connection)
        {
            var graphClient = AzureActiveDirectory.GetGraphServiceClient(connection);
            return new Graph(graphClient);
        }

        private Graph(GraphServiceClient graphClient)
        {
            this.graphClient = graphClient;
        }
        
        public Group GetGroupFromName(string adGroup)
        {
            return graphClient.Groups.Request().Filter($"displayName eq '{adGroup}'").GetAsync().Result.FirstOrDefault();
        }

        public User GetUser(string id)
        {
            return graphClient.Users[id].Request().Select("id,displayName,mail,userPrincipalName,employeeId,onPremisesDistinguishedName,onPremisesExtensionAttributes").GetAsync().Result;
        }
    }
}
