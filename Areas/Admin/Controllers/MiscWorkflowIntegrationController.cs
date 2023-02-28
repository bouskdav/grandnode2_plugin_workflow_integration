using Grand.Business.Core.Extensions;
using Grand.Business.Core.Interfaces.Common.Configuration;
using Grand.Business.Core.Interfaces.Common.Localization;
using Grand.Business.Core.Utilities.Common.Security;
using Grand.Business.Core.Interfaces.Storage;
using Grand.Web.Common.Controllers;
using Grand.Web.Common.DataSource;
using Grand.Web.Common.Filters;
using Grand.Web.Common.Security.Authorization;
using Microsoft.AspNetCore.Mvc;
using Misc.WorkflowIntegration.Models;
using Misc.WorkflowIntegration.Services;

namespace Misc.WorkflowIntegration.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    [PermissionAuthorize(PermissionSystemName.PaymentMethods)]
    public class MiscWorkflowIntegrationController : BasePluginController
    {
        private readonly ITranslationService _translationService;
        private readonly WorkflowIntegrationSettings _czechInvoiceGeneratorSettings;
        private readonly ISettingService _settingService;
        private readonly IInvoiceSeriesService _invoiceSeriesService;

        public MiscWorkflowIntegrationController(
            ITranslationService translationService,
            ISettingService settingService,
            IInvoiceSeriesService invoiceSeriesService,
            WorkflowIntegrationSettings czechInvoiceGeneratorSettings)
        {
            _translationService = translationService;
            _settingService = settingService;
            _invoiceSeriesService = invoiceSeriesService;
            _czechInvoiceGeneratorSettings = czechInvoiceGeneratorSettings;
        }

        public IActionResult Configure()
        {
            var model = new ConfigurationModel();
            
            model.DisplayOrder = _czechInvoiceGeneratorSettings.DisplayOrder;
            model.CustomerGroups = _czechInvoiceGeneratorSettings.LimitedToGroups?.ToArray();
            model.Stores = _czechInvoiceGeneratorSettings.LimitedToStores?.ToArray();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            _czechInvoiceGeneratorSettings.DisplayOrder = model.DisplayOrder;
            _czechInvoiceGeneratorSettings.LimitedToGroups = model.CustomerGroups == null ? new List<string>() : model.CustomerGroups.ToList();
            _czechInvoiceGeneratorSettings.LimitedToStores = model.Stores == null ? new List<string>() : model.Stores.ToList();

            _settingService.SaveSetting(_czechInvoiceGeneratorSettings);
            
            return Json("Ok");
        }

        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest command)
        {
            var sliders = await _invoiceSeriesService.GetInvoiceSeries();

            var items = new List<InvoiceSeriesModel>();

            foreach (var x in sliders)
            {
                var model = x.ToModel();

                items.Add(model);
            }

            var gridModel = new DataSourceResult {
                Data = items,
                Total = sliders.Count
            };

            return Json(gridModel);
        }

        public async Task<IActionResult> Create()
        {
            var model = new InvoiceSeriesModel();

            model.NextNumber = 1;

            return View(model);
        }

        [HttpPost, ArgumentNameFilter(KeyName = "save-continue", Argument = "continueEditing")]
        public async Task<IActionResult> Create(InvoiceSeriesModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var invoiceSerie = model.ToEntity();

                await _invoiceSeriesService.InsertInvoiceSerie(invoiceSerie);

                Success(_translationService.GetResource("Misc.WorkflowIntegration.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = invoiceSerie.Id }) : RedirectToAction("Configure");

            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var invoiceSerie = await _invoiceSeriesService.GetById(id);

            if (invoiceSerie == null)
                return RedirectToAction("Configure");

            var model = invoiceSerie.ToModel();

            return View(model);
        }

        [HttpPost, ArgumentNameFilter(KeyName = "save-continue", Argument = "continueEditing")]
        public async Task<IActionResult> Edit(InvoiceSeriesModel model, bool continueEditing)
        {
            var invoiceSerie = await _invoiceSeriesService.GetById(model.Id);

            if (invoiceSerie == null)
                return RedirectToAction("Configure");

            if (ModelState.IsValid)
            {
                invoiceSerie = model.ToEntity();

                await _invoiceSeriesService.UpdateInvoiceSerie(invoiceSerie);

                Success(_translationService.GetResource("Misc.WorkflowIntegration.Edited"));

                return continueEditing ? RedirectToAction("Edit", new { id = invoiceSerie.Id }) : RedirectToAction("Configure");

            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var invoiceSerie = await _invoiceSeriesService.GetById(id);

            if (invoiceSerie == null)
                return Json(new DataSourceResult { Errors = "This record not exists" });

            await _invoiceSeriesService.DeleteInvoiceSerie(invoiceSerie);

            return new JsonResult("");
        }
    }
}
