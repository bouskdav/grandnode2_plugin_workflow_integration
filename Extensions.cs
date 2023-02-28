using Grand.Domain.Localization;
using Grand.Infrastructure.Mapper;
using Grand.Web.Common.Models;
using Misc.WorkflowIntegration.Domain;
using Misc.WorkflowIntegration.Models;
using System.Reflection;

namespace Misc.WorkflowIntegration
{
    public static class MyExtensions
    {
        public static InvoiceSeriesModel ToModel(this OrderInvoiceSerie entity)
        {
            return entity.MapTo<OrderInvoiceSerie, InvoiceSeriesModel>();
        }

        public static OrderInvoiceSerie ToEntity(this InvoiceSeriesModel model)
        {
            return model.MapTo<InvoiceSeriesModel, OrderInvoiceSerie>();
        }

        public static List<TranslationEntity> ToLocalizedProperty<T>(this IList<T> list) where T : ILocalizedModelLocal
        {
            var local = new List<TranslationEntity>();
            foreach (var item in list)
            {
                Type[] interfaces = item.GetType().GetInterfaces();
                PropertyInfo[] props = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                foreach (var prop in props)
                {
                    bool insert = true;

                    foreach (var i in interfaces)
                    {
                        if (i.HasProperty(prop.Name))
                        {
                            insert = false;
                        }
                    }

                    if (insert && prop.GetValue(item) != null)
                        local.Add(new TranslationEntity()
                        {
                            LanguageId = item.LanguageId,
                            LocaleKey = prop.Name,
                            LocaleValue = prop.GetValue(item).ToString(),
                        });
                }
            }
            return local;
        }

        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }
    }
}