using Grand.Infrastructure.ModelBinding;
using Grand.Infrastructure.Models;
using Grand.Web.Common.Link;
using Grand.Web.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Misc.WorkflowIntegration.Models
{
    public partial class InvoiceSeriesModel : BaseEntityModel, IStoreLinkModel
    {
        [GrandResourceDisplayName("Misc.WorkflowIntegration.From")]
        public DateTime From { get; set; }

        [GrandResourceDisplayName("Misc.WorkflowIntegration.NextNumber")]
        public int NextNumber { get; set; }

        [GrandResourceDisplayName("Misc.WorkflowIntegration.Pattern")]
        public string Pattern { get; set; }

        //Store acl
        [GrandResourceDisplayName("Misc.WorkflowIntegration.LimitedToStores")]
        [UIHint("Stores")]
        public string[] Stores { get; set; }
    }
}
