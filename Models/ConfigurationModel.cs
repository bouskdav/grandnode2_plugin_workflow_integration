using Grand.Infrastructure.ModelBinding;
using Grand.Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace Misc.WorkflowIntegration.Models
{
    public class ConfigurationModel : BaseModel
    {
        
        [GrandResourceDisplayName("Misc.WorkflowIntegration.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [UIHint("CustomerGroups")]
        [GrandResourceDisplayName("Misc.WorkflowIntegration.Fields.LimitedToGroups")]
        public string[] CustomerGroups { get; set; }

        //Store acl
        [GrandResourceDisplayName("Misc.WorkflowIntegration.Fields.LimitedToStores")]
        [UIHint("Stores")]
        public string[] Stores { get; set; }

        [GrandResourceDisplayName("Misc.WorkflowIntegration.Fields.MembersOnlyAccessEnabled")]
        public bool MembersOnlyAccessEnabled { get; set; }

        [GrandResourceDisplayName("Misc.WorkflowIntegration.Fields.StorePassword")]
        public string StorePassword { get; set; }
    }
}