using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcTurbine.Web.Metadata
{
    internal class MetadataAttributeRetriever
    {
        public IEnumerable<Type> GetTypesOfAllMetadataAttributeHandlers()
        {
            var list = new List<Type>();

            GetAllAssemblies()
                .ForEach(assembly => list.AddRange(GetAllMetadataAttributeHandlers(assembly)));

            return list;
        }

        private static IEnumerable<Type> GetAllMetadataAttributeHandlers(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(ThisTypeIsAMetadataAttributeHandler);
        }

        private static bool ThisTypeIsAMetadataAttributeHandler(Type x)
        {
            return x.GetInterfaces()
                .Any(i => (i.FullName ?? string.Empty)
                              .StartsWith("MvcTurbine.Web.Metadata.IMetadataAttributeHandler`1"));
        }

        private static IEnumerable<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.StartsWith("MvcTurbine.Web.Metadata.,") == false)
                .ToList();
        }
    }
}