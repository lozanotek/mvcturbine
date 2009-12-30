using MvcTurbine.ComponentModel;
using NerdDinner.Models;

namespace NerdDinner.Registration {
    public class RepositoryRegistration : IServiceRegistration {
        #region IComponentRegistration Members

        public void Register(IServiceLocator locator) {
            locator.Register<IDinnerRepository, DinnerRepository>();
        }

        #endregion
    }
}