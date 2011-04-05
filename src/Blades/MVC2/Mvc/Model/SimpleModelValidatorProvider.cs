namespace Mvc.Model {
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SimpleModelValidatorProvider : ModelValidatorProvider {
        public SimpleModelValidatorProvider(IValidatorProviderDependency dependency) {
            Dependency = dependency;
        }

        public IValidatorProviderDependency Dependency { get; private set; }

        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context) {
            return Dependency.GetValidators();
        }
    }

    public interface IValidatorProviderDependency {
        IList<ModelValidator> GetValidators();
    }

    public class EmptyValidatorProviderDependency : IValidatorProviderDependency {
        public IList<ModelValidator> GetValidators() {
            return new List<ModelValidator>();
        }
    }
}
