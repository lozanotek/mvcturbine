using System;
using System.Collections.Generic;

namespace MvcTurbine.Web.Metadata
{
    /// <summary>
    ///   The arguments that are normally passed to the CreateMetadata 
    ///   method of the DataAnnotationsModelMetadataProvider.
    /// </summary>
    public class CreateMetadataArguments
    {
        public IEnumerable<Attribute> Attributes { get; set; }
        public Type ContainerType { get; set; }
        public Func<object> ModelAccessor { get; set; }
        public Type ModelType { get; set; }
        public string PropertyName { get; set; }
    }
}