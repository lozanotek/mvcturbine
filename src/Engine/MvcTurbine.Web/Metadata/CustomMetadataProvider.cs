using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.Web.Metadata
{
    public class CustomMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        private readonly IServiceLocator serviceLocator;
        private readonly List<Mapping> mappingList;

        public CustomMetadataProvider(IServiceLocator serviceLocator, List<Mapping> mappingList)
        {
            this.serviceLocator = serviceLocator;
            this.mappingList = mappingList;
        }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
                                                        Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            AlterMetadata(metadata, new CreateMetadataArguments
                                        {
                                            Attributes = attributes,
                                            ContainerType = containerType,
                                            ModelAccessor = modelAccessor,
                                            ModelType = modelType,
                                            PropertyName = propertyName
                                        });

            return metadata;
        }

        public virtual void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args)
        {
            foreach (var handler in GetEveryHandlerForThisType(args))
                handler.AlterMetadata(metadata, args);
        }

        private IEnumerable<IMetadataAttributeHandlerBase> GetEveryHandlerForThisType(CreateMetadataArguments args)
        {
            return mappingList
                .Where(map => ThisIsAHandlerforThisType(args, map))
                .Select(CreateTheHandler);
        }

        private IMetadataAttributeHandlerBase CreateTheHandler(Mapping map)
        {
            return serviceLocator.Resolve(map.HandlerType) as IMetadataAttributeHandlerBase;
        }

        private static bool ThisIsAHandlerforThisType(CreateMetadataArguments args, Mapping map)
        {
            return args.Attributes.Any(x => x.GetType() == map.AttributeType);
        }
    }
}