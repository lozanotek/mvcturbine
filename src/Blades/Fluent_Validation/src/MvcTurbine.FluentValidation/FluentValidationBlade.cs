using System.Web.Mvc;
using FluentValidation.Mvc;
using MvcTurbine.Blades;
using MvcTurbine.FluentValidation.Helpers;

namespace MvcTurbine.FluentValidation
{
    public class FluentValidationBlade : Blade
    {
        public override void Spin(IRotorContext context)
        {
            StopMvcFromRequiringAllNonNullFields();
            AddAFluentValidationModelValidatorProvider(context);
        }

        private static void StopMvcFromRequiringAllNonNullFields()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }

        private static void AddAFluentValidationModelValidatorProvider(IRotorContext context)
        {
            var fluentValidationModelValidatorProvider = CreateFluentValidationModelValidatorProvider(context);
            ModelValidatorProviders.Providers.Add(fluentValidationModelValidatorProvider);
        }

        private static FluentValidationModelValidatorProvider CreateFluentValidationModelValidatorProvider(IRotorContext context)
        {
            var serviceLocator = context.ServiceLocator;
            var validatorFactory = new ServiceLocatorValidatorFactory(serviceLocator);

            var retriever = new ValidatorRetriever();
            foreach (var type in retriever.GetAllValidatorTypes())
                validatorFactory.AddValidatorToBeResolved(type);

            return new FluentValidationModelValidatorProvider(validatorFactory);
        }
    }
}