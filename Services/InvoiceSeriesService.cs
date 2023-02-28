using Grand.Business.Core.Interfaces.Common.Directory;
using Grand.Business.Core.Interfaces.Common.Security;
using Grand.Domain.Data;
using Grand.Domain.Orders;
using Grand.Infrastructure;
using Grand.Infrastructure.Caching;
using Misc.WorkflowIntegration.Domain;

namespace Misc.WorkflowIntegration.Services
{
    public partial class InvoiceSeriesService : IInvoiceSeriesService
    {
        #region Fields

        private readonly IRepository<OrderInvoiceSerie> _invoiceSeriesRepository;
        private readonly IAclService _aclService;
        private readonly IWorkContext _workContext;
        private readonly ICacheBase _cacheBase;
        private readonly IUserFieldService _userFieldService;

        /// <summary>
        /// Key for sliders
        /// </summary>
        /// <remarks>
        /// {0} : Store id
        /// {1} : Slider type
        /// {2} : Object entry / categoryId || collectionId
        /// </remarks>
        public const string INVOICE_SERIES_MODEL_KEY = "Grand.InvoiceSeries-{0}-{1}-{2}";
        public const string INVOICE_SERIES_PATTERN_KEY = "Grand.InvoiceSeries";

        #endregion
        
        public InvoiceSeriesService(
            IRepository<OrderInvoiceSerie> invoiceSeriesRepository,
            IWorkContext workContext, 
            IAclService aclService,
            ICacheBase cacheManager,
            IUserFieldService userFieldService)
        {
            _invoiceSeriesRepository = invoiceSeriesRepository;
            _workContext = workContext;
            _aclService = aclService;
            _cacheBase = cacheManager;
            _userFieldService = userFieldService;
        }

        /// <summary>
        /// Gets all 
        /// </summary>
        public virtual async Task<IList<OrderInvoiceSerie>> GetInvoiceSeries()
        {
            return await Task.FromResult(_invoiceSeriesRepository.Table.OrderBy(x => x.From).ToList());
        }

        ///// <summary>
        ///// Gets by type 
        ///// </summary>
        //public virtual async Task<IList<OrderInvoiceSerie>> GetPictureSliders(SliderType sliderType, string objectEntry = "")
        //{
        //    string cacheKey = string.Format(INVOICE_SERIES_MODEL_KEY, _workContext.CurrentStore.Id, sliderType.ToString(), objectEntry);
        //    return await _cacheBase.GetAsync(cacheKey, async () =>
        //    {
        //        var query = from s in _invoiceSeriesRepository.Table
        //                    where s.SliderTypeId == sliderType && s.Published
        //                    select s;

        //        if (!string.IsNullOrEmpty(objectEntry))
        //            query = query.Where(x => x.ObjectEntry == objectEntry);

        //        var items = query.ToList();
        //        return await Task.FromResult(items.Where(c => _aclService.Authorize(c, _workContext.CurrentStore.Id)).ToList());
        //    });
        //}


        /// <summary>
        /// Gets a tax rate
        /// </summary>
        public virtual Task<OrderInvoiceSerie> GetById(string slideId)
        {
            return _invoiceSeriesRepository.GetByIdAsync(slideId);
        }

        /// <summary>
        /// Inserts a slide
        /// </summary>
        public virtual async Task InsertInvoiceSerie(OrderInvoiceSerie invoiceSerie)
        {
            if (invoiceSerie == null)
                throw new ArgumentNullException(nameof(invoiceSerie));

            //clear cache
            await _cacheBase.RemoveByPrefix(INVOICE_SERIES_PATTERN_KEY);

            await _invoiceSeriesRepository.InsertAsync(invoiceSerie);
        }

        /// <summary>
        /// Updates slide
        /// </summary>
        public virtual async Task UpdateInvoiceSerie(OrderInvoiceSerie invoiceSerie)
        {
            if (invoiceSerie == null)
                throw new ArgumentNullException(nameof(invoiceSerie));

            //clear cache
            await _cacheBase.RemoveByPrefix(INVOICE_SERIES_PATTERN_KEY);

            await _invoiceSeriesRepository.UpdateAsync(invoiceSerie);
        }

        /// <summary>
        /// Delete slide
        /// </summary>
        public virtual async Task DeleteInvoiceSerie(OrderInvoiceSerie invoiceSerie)
        {
            if (invoiceSerie == null)
                throw new ArgumentNullException(nameof(invoiceSerie));

            //clear cache
            await _cacheBase.RemoveByPrefix(INVOICE_SERIES_PATTERN_KEY);

            await _invoiceSeriesRepository.DeleteAsync(invoiceSerie);
        }

        public async Task<Order> SetNextAvailableNumberForOrder(Order order, DateTime effectiveDate)
        {
            OrderInvoiceSerie latestEffectiveInvoiceSerie = _invoiceSeriesRepository.Table
                .Where(i => 
                    i.From <= effectiveDate &&
                    (!i.LimitedToStores || i.Stores.Contains(order.StoreId)))
                .OrderByDescending(i => i.From)
                .FirstOrDefault();

            if (latestEffectiveInvoiceSerie == null)
                return order;

            string invoiceNumber = latestEffectiveInvoiceSerie.Pattern;

            invoiceNumber = invoiceNumber.Replace("{yyyy}", effectiveDate.ToString("yyyy"));
            invoiceNumber = invoiceNumber.Replace("{yy}", effectiveDate.ToString("yy"));
            invoiceNumber = invoiceNumber.Replace("{dd}", effectiveDate.ToString("dd"));
            invoiceNumber = invoiceNumber.Replace("{MM}", effectiveDate.ToString("MM"));

            invoiceNumber = invoiceNumber.Replace("{####}", latestEffectiveInvoiceSerie.NextNumber.ToString("0000"));
            invoiceNumber = invoiceNumber.Replace("{###}", latestEffectiveInvoiceSerie.NextNumber.ToString("000"));
            invoiceNumber = invoiceNumber.Replace("{##}", latestEffectiveInvoiceSerie.NextNumber.ToString("00"));
            invoiceNumber = invoiceNumber.Replace("{#}", latestEffectiveInvoiceSerie.NextNumber.ToString("0"));

            await _userFieldService.SaveField(order, InvoiceConstants.INVOICE_NUMBER_FIELD_KEY, invoiceNumber);
            await _userFieldService.SaveField(order, InvoiceConstants.INVOICE_EFFECTIVE_DATE_FIELD_KEY, effectiveDate);

            latestEffectiveInvoiceSerie.NextNumber++;

            await _invoiceSeriesRepository.UpdateAsync(latestEffectiveInvoiceSerie);

            return order;
        }

        public Task<bool> IsServiceAvailableForStore(string storeId, DateTime effectiveDate)
        {
            OrderInvoiceSerie latestEffectiveInvoiceSerie = _invoiceSeriesRepository.Table
                .Where(i =>
                    i.From <= effectiveDate &&
                    (!i.LimitedToStores || i.Stores.Contains(storeId)))
                .OrderByDescending(i => i.From)
                .FirstOrDefault();

            return Task.FromResult(latestEffectiveInvoiceSerie != null);
        }
    }
}
