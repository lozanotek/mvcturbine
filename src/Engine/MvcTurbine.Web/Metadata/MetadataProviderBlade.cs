using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MvcTurbine.Blades;

namespace MvcTurbine.Web.Metadata
{
    public class MetadataProviderBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            var retriever = new MetadataAttributeRetriever();

            var list = retriever.GetTypesOfAllMetadataAttributeHandlers()
                .Select(type => new Mapping
                {
                    AttributeType = GetTheGenericValidatorType(type),
                    HandlerType = type
                }).ToList();

            ModelMetadataProviders.Current = new CustomMetadataProvider(context.ServiceLocator, list);
        }

        private static Type GetTheGenericValidatorType(Type validatorType)
        {
            return validatorType.GetInterfaces()
                .Where(x => x.IsGenericType &&
                            x.FullName.StartsWith("MvcTurbine.Metadata.IMetadataAttributeHandler`1"))
                .First()
                .GetGenericArguments()[0];
        }
    }
}
