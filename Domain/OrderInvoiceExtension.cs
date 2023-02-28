using Grand.Domain;

namespace Misc.WorkflowIntegration.Domain
{
    public partial class OrderInvoiceExtension : BaseEntity
    {
        public Guid OrderGuid { get; set; }

        public int InvoiceOrder { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime InvoiceEffectiveDate { get; set; }
    }
}
