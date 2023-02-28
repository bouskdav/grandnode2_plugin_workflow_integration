using Grand.Business.Core.Events.Checkout.Orders;
using Grand.Business.Core.Interfaces.Common.Directory;
using Grand.Domain.Common;
using Grand.Domain.Orders;
using MediatR;
using Misc.WorkflowIntegration.Domain;
using Misc.WorkflowIntegration.Services;

namespace Misc.WorkflowIntegration.Events
{
    public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly PdfSettings _pdfSettings;
        private readonly IUserFieldService _userFieldService;
        private readonly IInvoiceSeriesService _invoiceSeriesService;

        public OrderPlacedEventHandler(
            PdfSettings pdfSettings,
            IUserFieldService userFieldService,
            IInvoiceSeriesService invoiceSeriesService)
        {
            _pdfSettings = pdfSettings;
            _userFieldService = userFieldService;
            _invoiceSeriesService = invoiceSeriesService;
        }

        public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            DateTime invoiceEffectiveDate = DateTime.Today;

            bool isServiceAvailableForStore = await _invoiceSeriesService.IsServiceAvailableForStore(notification.Order.StoreId, invoiceEffectiveDate);

            if (!isServiceAvailableForStore)
                return;

            // don't create invoice for pending order if not explicitly set
            bool disableInvoiceForPendingOrders = _pdfSettings.DisablePdfInvoicesForPendingOrders && notification.Order.OrderStatusId == (int)OrderStatusSystem.Pending;

            if (!disableInvoiceForPendingOrders || true)
            {
                // check, if order already have invoice record
                var invoiceNumber = await _userFieldService.GetFieldsForEntity<string>(notification.Order, InvoiceConstants.INVOICE_NUMBER_FIELD_KEY);

                // skip
                if (!String.IsNullOrEmpty(invoiceNumber))
                    return;

                _ = await _invoiceSeriesService.SetNextAvailableNumberForOrder(notification.Order, invoiceEffectiveDate);
            }
        }
    }
}
