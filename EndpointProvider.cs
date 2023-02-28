using Grand.Infrastructure.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Misc.WorkflowIntegration
{
    public partial class EndpointProvider : IEndpointProvider
    {
        public void RegisterEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
        {
            ////PDT
            //endpointRouteBuilder.MapControllerRoute("Plugin.Payments.MembersOnly",
            //     "Plugins/PaymentPayPalStandard/PDTHandler",
            //     new { controller = "PaymentPayPalStandard", action = "PDTHandler" }
            //);
        }

        public int Priority => 0;
    }
}
