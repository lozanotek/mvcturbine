namespace MvcTurbine.Mvc2 {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MvcTurbine.Blades;
    using MvcTurbine.ComponentModel;

    public class ModelValidatorBlade : Blade, ISupportAutoRegistration {
        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(Registration.Simple<ModelValidatorProvider>());
        }

        public override void Spin(IRotorContext context) {
            IServiceLocator serviceLocator = GetServiceLocatorFromContext(context);
            IList<ModelValidatorProvider> validatorList = GetValidationProviders(serviceLocator);

            if (validatorList == null || validatorList.Count == 0) return;

            // Clear the original state
            ModelValidatorProviders.Providers.Clear();

            foreach (ModelValidatorProvider validatorProvider in validatorList) {
                ModelValidatorProviders.Providers.Add(validatorProvider);
            }

            // Add the default providers
            ModelValidatorProviders.Providers.Add(new DataAnnotationsModelValidatorProvider());
            ModelValidatorProviders.Providers.Add(new DataErrorInfoModelValidatorProvider());
            ModelValidatorProviders.Providers.Add(new ClientDataTypeModelValidatorProvider());
        }

        public IList<ModelValidatorProvider> GetValidationProviders(IServiceLocator serviceLocator) {
            try {
                return serviceLocator.ResolveServices<ModelValidatorProvider>();
            }
            catch (Exception) {
                return null;
            }
        }
    }
}
