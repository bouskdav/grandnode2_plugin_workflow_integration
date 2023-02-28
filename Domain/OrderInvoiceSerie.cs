using Grand.Domain;
using Grand.Domain.Stores;

namespace Misc.WorkflowIntegration.Domain
{
    public class OrderInvoiceSerie : BaseEntity, IStoreLinkEntity
    {
        //public string StoreId { get; set; }

        public DateTime From { get; set; }

        public int NextNumber { get; set; }

        public string Pattern { get; set; }

        public bool LimitedToStores { get; set; }

        public IList<string> Stores { get; set; } = new List<string>();
    }
}
