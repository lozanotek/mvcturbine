#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

namespace MvcTurbine.Mvc2 {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MvcTurbine.Blades;
    using MvcTurbine.ComponentModel;

    public class ModelValidatorBlade : Blade, ISupportAutoRegistration {
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
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

        public virtual IList<ModelValidatorProvider> GetValidationProviders(IServiceLocator serviceLocator) {
            try {
                return serviceLocator.ResolveServices<ModelValidatorProvider>();
            }
            catch (Exception) {
                return null;
            }
        }
    }
}
