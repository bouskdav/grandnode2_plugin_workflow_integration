using Grand.Business.Core.Events.Checkout.Orders;
using Grand.Business.Core.Interfaces.Common.Directory;
using Grand.Domain.Data;
using MediatR;
using Misc.WorkflowIntegration.Domain;
using Misc.WorkflowIntegration.Services;

namespace Misc.WorkflowIntegration.Events
{
    public class OrderPaidEventHandler : INotificationHandler<OrderPaidEvent>
    {
        private readonly IRepository<OrderInvoiceExtension> _orderInvoiceRepository;
        private readonly IUserFieldService _userFieldService;
        private readonly IInvoiceSeriesService _invoiceSeriesService;
        private readonly WorkflowIntegrationSettings _czechInvoiceGeneratorSettings;

        public OrderPaidEventHandler(
            IRepository<OrderInvoiceExtension> orderInvoiceRepository,
            IUserFieldService userFieldService,
            IInvoiceSeriesService invoiceSeriesService,
            WorkflowIntegrationSettings czechInvoiceGeneratorSettings)
        {
            _orderInvoiceRepository = orderInvoiceRepository;
            _userFieldService = userFieldService;
            _invoiceSeriesService = invoiceSeriesService;
            _czechInvoiceGeneratorSettings = czechInvoiceGeneratorSettings;
        }

        public async Task Handle(OrderPaidEvent notification, CancellationToken cancellationToken)
        {
            //DateTime invoiceEffectiveDate = DateTime.Today;

            //bool isServiceAvailableForStore = await _invoiceSeriesService.IsServiceAvailableForStore(notification.Order.StoreId, invoiceEffectiveDate);

            //if (!isServiceAvailableForStore)
            //    return;

            //// check, if order already have invoice record
            //var invoiceNumber = await _userFieldService.GetFieldsForEntity<string>(notification.Order, InvoiceConstants.INVOICE_NUMBER_FIELD_KEY);

            //// skip
            //if (!String.IsNullOrEmpty(invoiceNumber))
            //    return;

            //_ = await _invoiceSeriesService.SetNextAvailableNumberForOrder(notification.Order, invoiceEffectiveDate);
        }
    }
}
