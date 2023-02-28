using Grand.Domain.Configuration;

namespace Misc.WorkflowIntegration
{
    public class WorkflowIntegrationSettings : ISettings
    {
        public WorkflowIntegrationSettings()
        {
            LimitedToStores = new List<string>();
            LimitedToGroups = new List<string>();
        }
        public int DisplayOrder { get; set; }

        public IList<string> LimitedToStores { get; set; }

        public IList<string> LimitedToGroups { get; set; }
    }
}
