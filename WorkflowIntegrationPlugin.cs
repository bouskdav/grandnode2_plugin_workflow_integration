using Grand.Business.Core.Extensions;
using Grand.Business.Core.Interfaces.Common.Localization;
using Grand.Business.Core.Interfaces.Storage;
using Grand.Domain.Data;
using Grand.Infrastructure.Plugins;
using Misc.WorkflowIntegration.Domain;

namespace Misc.WorkflowIntegration
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class WorkflowIntegrationPlugin : BasePlugin, IPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;
        private readonly IDatabaseContext _databaseContext;
        private readonly IRepository<OrderInvoiceExtension> _orderInvoiceRepository;

        public WorkflowIntegrationPlugin(IPictureService pictureService,
            ITranslationService translationService,
            ILanguageService languageService,
            IDatabaseContext databaseContext,
            IRepository<OrderInvoiceExtension> orderInvoiceRepository)
        {
            _pictureService = pictureService;
            _translationService = translationService;
            _languageService = languageService;
            _databaseContext = databaseContext;
            _orderInvoiceRepository = orderInvoiceRepository;
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override async Task Install()
        {
            //Create index
            await _databaseContext.CreateIndex(_orderInvoiceRepository, OrderBuilder<OrderInvoiceExtension>.Create().Ascending(x => x.OrderGuid), "OrderInvoice_OrderGuid");

            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "PDFInvoice.UnitPrice", "Unit price");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "PDFInvoice.UnitTaxRate", "Tax");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "PDFInvoice.PriceTaxExcl", "Tax excl.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "PDFInvoice.PriceTaxExcl", "Tax");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "PDFInvoice.TaxTotal", "Tax sum");

            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Added", "Record added");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Edited", "Record edited");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Info", "Details");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Manage", "Manage");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.AddNew", "Add new");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Edit", "Edit");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.BackToList", "Back to list");

            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.From", "Valid from");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Fields.From", "Valid from");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.NextNumber", "Next number");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Fields.NextNumber", "Next number");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Pattern", "Pattern");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Fields.Pattern", "Pattern");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.LimitedToStores", "Limited to stores");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.DisplayOrder", "Display order");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.CustomerGroups", "Customer groups");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Misc.WorkflowIntegration.Stores", "Stores");
            
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.Misc.WorkflowIntegration.Fields.Invoices", "Invoices");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.Misc.WorkflowIntegration.Fields.InvoiceSettings", "Invoice settings");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Plugins.Misc.WorkflowIntegration.Fields.InvoiceList", "List of invoices");

            await base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override async Task Uninstall()
        {
            await this.DeletePluginTranslationResource(_translationService, _languageService, "PDFInvoice.UnitPrice");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "PDFInvoice.UnitTaxRate");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "PDFInvoice.PriceTaxExcl");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "PDFInvoice.PriceTaxExcl");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "PDFInvoice.TaxTotal");

            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Added");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Edited");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Info");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Manage");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.AddNew");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Edit");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.BackToList");

            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.From");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Fields.From");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.NextNumber");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Fields.NextNumber");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Pattern");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Fields.Pattern");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.LimitedToStores");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.DisplayOrder");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.CustomerGroups");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Misc.WorkflowIntegration.Stores");

            await base.Uninstall();
        }

        public override string ConfigurationUrl()
        {
            return WorkflowIntegrationDefaults.ConfigurationUrl;
        }
    }
}
