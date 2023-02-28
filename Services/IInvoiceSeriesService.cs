using Grand.Domain.Orders;
using Misc.WorkflowIntegration.Domain;

namespace Misc.WorkflowIntegration.Services
{
    public partial interface IInvoiceSeriesService
    {
        /// <summary>
        /// Gets all 
        /// </summary>
        Task<IList<OrderInvoiceSerie>> GetInvoiceSeries();

        /// <summary>
        /// Gets one by id
        /// </summary>
        Task<OrderInvoiceSerie> GetById(string serieId);

        /// <summary>
        /// Inserts an invoice serie
        /// </summary>
        Task InsertInvoiceSerie(OrderInvoiceSerie slide);

        /// <summary>
        /// Updates invoice serie
        /// </summary>
        Task UpdateInvoiceSerie(OrderInvoiceSerie slide);

        /// <summary>
        /// Delete invoice serie
        /// </summary>
        Task DeleteInvoiceSerie(OrderInvoiceSerie slide);

        Task<Order> SetNextAvailableNumberForOrder(Order order, DateTime effectiveDate);

        Task<bool> IsServiceAvailableForStore(string storeId, DateTime effectiveDate);
    }
}
