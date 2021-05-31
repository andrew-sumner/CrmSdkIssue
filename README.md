# CrmSdkIssue
Demo for issue with CrmServiceClient

The solution has two project (plus test projects):

### CrmSdkIssue
Uses new CrmServiceClient and Microsoft.CrmSdk.XrmTooling.CoreAssembly:9.1.0.21

* **KeyVaultTest**: Have to use Microsoft.Azure.KeyVault:2.3.2 otherwise cannot load Newtonsoft.json (breaks all tests)
* **GraphTest**: ok
* **CRMTest**: Fails with “Could not load file or assembly 'Microsoft.IdentityModel.Clients.ActiveDirectory” – if remove KeyVault and Graph packages it works fine

### CrmSdkWorking
Uses old OrganizationServiceProxy connection and Microsoft.CrmSdk.CoreAssemblies:9.0.2.19

* **KeyVaultTest**: Have to use Microsoft.Azure.KeyVault:2.3.2 otherwise cannot load Newtonsoft.json (breaks keyvault and graph tests)
* **GraphTest**: ok
* **CRMTest**: ok


test
