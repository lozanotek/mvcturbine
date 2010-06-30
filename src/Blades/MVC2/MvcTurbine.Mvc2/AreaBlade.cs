namespace MvcTurbine.Mvc2 {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MvcTurbine.Blades;
    using MvcTurbine.ComponentModel;

    public class AreaBlade : Blade, ISupportAutoRegistration {
        public void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(Registration.Simple<AreaRegistration>());
        }

        public override void Spin(IRotorContext context) {
            var serviceLocator = GetServiceLocatorFromContext(context);
            var areaList = GetRegisteredAreas(serviceLocator);

            if (areaList == null || areaList.Count == 0) return;

            foreach (AreaRegistration registration in areaList) {
                var registrationContext = new AreaRegistrationContext(registration.AreaName, RouteTable.Routes);
                var areaNamespace = registration.GetType().Namespace;

                if (areaNamespace != null) {
                    registrationContext.Namespaces.Add(areaNamespace + ".*");
                }

                registration.RegisterArea(registrationContext);
            }
        }

        public virtual IList<AreaRegistration> GetRegisteredAreas(IServiceLocator locator) {
            try {
                return locator.ResolveServices<AreaRegistration>();
            }
            catch (Exception) {
                return null;
            }
        }
    }
}
