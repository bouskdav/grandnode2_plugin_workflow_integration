using Grand.Business.Core.Utilities.Common.Security;
using Grand.Domain.Admin;
using Grand.Web.Common.Menu;

namespace Misc.WorkflowIntegration
{
    public class WorkflowIntegrationMenuProvider : IAdminMenuProvider
    {
        private readonly WorkflowIntegrationSettings _czechInvoiceGeneratorSettings;

        public WorkflowIntegrationMenuProvider(
            WorkflowIntegrationSettings czechInvoiceGeneratorSettings) 
        {
            _czechInvoiceGeneratorSettings = czechInvoiceGeneratorSettings;
        }

        public string ConfigurationUrl => WorkflowIntegrationDefaults.ConfigurationUrl;

        public string SystemName => WorkflowIntegrationDefaults.ProviderSystemName;

        public string FriendlyName => WorkflowIntegrationDefaults.FriendlyName;

        public int Priority => _czechInvoiceGeneratorSettings.DisplayOrder;

        public IList<string> LimitedToStores => _czechInvoiceGeneratorSettings.LimitedToStores;

        public IList<string> LimitedToGroups => _czechInvoiceGeneratorSettings.LimitedToGroups;

        public Task ManageSiteMap(SiteMapNode rootNode)
        {
            var salesNode = rootNode.ChildNodes.FirstOrDefault(i => i.SystemName == "Sales");

            if (salesNode == null)
                return Task.CompletedTask;

            salesNode.ChildNodes.Add(new SiteMapNode() {
                SystemName = "Invoices",
                ResourceName = "Plugins.Misc.WorkflowIntegration.Fields.Invoices",
                IconClass = "fa fa-dot-circle-o",
                Visible = true,
                ChildNodes = new List<SiteMapNode> {
                    new () {
                        SystemName = "Invoices.Settings",
                        ResourceName = "Plugins.Misc.WorkflowIntegration.Fields.InvoiceSettings",
                        ControllerName = "MiscWorkflowIntegration",
                        ActionName = "Configure",
                        Visible = true,
                    },
                    new () {
                        SystemName = "Invoices.List",
                        ResourceName = "Plugins.Misc.WorkflowIntegration.Fields.InvoiceList",
                        ControllerName = "MiscWorkflowIntegration",
                        ActionName = "Configure",
                        Visible = true,
                    },
                }
            });

            return Task.CompletedTask;
        }
    }
}
