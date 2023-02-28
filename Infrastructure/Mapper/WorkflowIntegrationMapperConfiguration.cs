using AutoMapper;
using Grand.Infrastructure.Mapper;
using Misc.WorkflowIntegration.Domain;
using Misc.WorkflowIntegration.Models;

namespace Misc.WorkflowIntegration.Infrastructure.Mapper
{
    public class CzechInvoiceMapperConfiguration : Profile, IAutoMapperProfile
    {
        public CzechInvoiceMapperConfiguration()
        {
            CreateMap<OrderInvoiceSerie, InvoiceSeriesModel>();

            CreateMap<InvoiceSeriesModel, OrderInvoiceSerie>()
                .ForMember(dest => dest.LimitedToStores, mo => mo.Ignore());
        }

        public int Order => 0;
    }
}
