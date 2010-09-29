using System;
using System.Collections.Generic;

namespace MvcTurbine.Web.Metadata
{
    public class CreateMetadataArguments
    {
        public IEnumerable<Attribute> Attributes { get; set; }
        public Type ContainerType { get; set; }
        public Func<object> ModelAccessor { get; set; }
        public Type ModelType { get; set; }
        public string PropertyName { get; set; }
    }
}