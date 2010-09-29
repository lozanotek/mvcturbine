using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTurbine.Blades;

namespace MvcTurbine.Web.Metadata
{
    public class MetadataProviderBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            var list = CreateAListOfMappingsOfHandlersAndTheTypesTheyHandle();

            ModelMetadataProviders.Current = new CustomMetadataProvider(context.ServiceLocator, list);
        }

        private static List<MetadataAttributeMapping> CreateAListOfMappingsOfHandlersAndTheTypesTheyHandle()
        {
            var retriever = new MetadataAttributeRetriever();

            return retriever.GetTypesOfAllMetadataAttributeHandlers()
                .Select(type => new MetadataAttributeMapping
                                    {
                                        AttributeType = GetTheTypeThatThisHandlerHandles(type),
                                        HandlerType = type
                                    }).ToList();
        }

        private static Type GetTheTypeThatThisHandlerHandles(Type validatorType)
        {
            return validatorType.GetInterfaces()
                .Where(ThisIsAMetadataAttributeHandler)
                .First()
                .GetGenericArguments()[0];
        }

        private static bool ThisIsAMetadataAttributeHandler(Type x)
        {
            return x.IsGenericType &&
                   x.FullName != null &&
                   x.FullName.StartsWith("MvcTurbine.Metadata.IMetadataAttributeHandler`1");
        }
    }
}