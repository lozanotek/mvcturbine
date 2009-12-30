using MvcTurbine.ComponentModel;
using NerdDinner.Controllers;

namespace NerdDinner.Registration {
    public class ServiceRegistration : IServiceRegistration {
        #region IComponentRegistration Members

        public void Register(IServiceLocator locator) {
            locator.Register<IFormsAuthentication, FormsAuthenticationService>();
            locator.Register<IMembershipService, AccountMembershipService>();
        }

        #endregion
    }
}