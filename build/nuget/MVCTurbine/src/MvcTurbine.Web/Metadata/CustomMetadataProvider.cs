namespace MvcTurbine.Web.Metadata {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;
	using MvcTurbine.ComponentModel;

    public class CustomMetadataProvider : DataAnnotationsModelMetadataProvider {
        private readonly IServiceLocator serviceLocator;
        private readonly IList<MetadataAttributeMapping> mappingList;

        public CustomMetadataProvider(IServiceLocator serviceLocator, IList<MetadataAttributeMapping> mappingList) {
            this.serviceLocator = serviceLocator;
            this.mappingList = mappingList;
        }

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType,
                                                        Func<object> modelAccessor, Type modelType, string propertyName) {

            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            AlterMetadata(metadata, new CreateMetadataArguments {
                Attributes = attributes,
                ContainerType = containerType,
                ModelAccessor = modelAccessor,
                ModelType = modelType,
                PropertyName = propertyName
            });

            return metadata;
        }

        public virtual void AlterMetadata(ModelMetadata metadata, CreateMetadataArguments args) {
			foreach (var handler in GetEveryHandlerForThisType(args)) {
				handler.AlterMetadata(metadata, args);
			}
        }

        protected virtual IEnumerable<IMetadataAttributeHandlerBase> GetEveryHandlerForThisType(CreateMetadataArguments args) {
            return mappingList
                .Where(map => ThisIsAHandlerforThisType(args, map))
                .Select(CreateTheHandler);
        }

        protected virtual IMetadataAttributeHandlerBase CreateTheHandler(MetadataAttributeMapping map) {
            return serviceLocator.Resolve(map.HandlerType) as IMetadataAttributeHandlerBase;
        }

        protected virtual bool ThisIsAHandlerforThisType(CreateMetadataArguments args, MetadataAttributeMapping map) {
            return args.Attributes.Any(x => x.GetType() == map.AttributeType);
        }
    }
}
