using Grand.Business.Core.Interfaces.Cms;
using Grand.Business.Core.Interfaces.Common.Localization;

namespace Misc.WorkflowIntegration
{
    public class WorkflowIntegrationProvider : IWidgetProvider
    {
        private readonly ITranslationService _translationService;
        private readonly WorkflowIntegrationSettings _czechInvoiceGeneratorSettings;

        public WorkflowIntegrationProvider(
            ITranslationService translationService, 
            WorkflowIntegrationSettings czechInvoiceGeneratorSettings)
        {
            _translationService = translationService;
            _czechInvoiceGeneratorSettings = czechInvoiceGeneratorSettings;
        }

        public string ConfigurationUrl => WorkflowIntegrationDefaults.ConfigurationUrl;

        public string SystemName => WorkflowIntegrationDefaults.ProviderSystemName;

        public string FriendlyName => _translationService.GetResource(WorkflowIntegrationDefaults.FriendlyName);

        public int Priority => _czechInvoiceGeneratorSettings.DisplayOrder;

        public IList<string> LimitedToStores => _czechInvoiceGeneratorSettings.LimitedToStores;

        public IList<string> LimitedToGroups => _czechInvoiceGeneratorSettings.LimitedToGroups;

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public async Task<IList<string>> GetWidgetZones()
        {
            //return await Task.FromResult(new List<string>
            //{
            //    WorkflowIntegrationDefaults.WidgetZoneHomePage,
            //    WorkflowIntegrationDefaults.WidgetZoneCategoryPage,
            //    WorkflowIntegrationDefaults.WidgetZoneCollectionPage,
            //    WorkflowIntegrationDefaults.WidgetZoneBrandPage,
            //});

            return new List<string>();
        }

        public Task<string> GetPublicViewComponentName(string widgetZone)
        {
            return Task.FromResult("WorkflowIntegration");
        }

    }
}
