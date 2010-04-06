namespace FubuMvc.Blades.UI {
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.ServiceLocation;
    using ITurbineLocator=MvcTurbine.ComponentModel.IServiceLocator;

    /// <summary>
    /// This is a facade for the MS CSL to the MVC Turbine CSL. Minimal pieces have been implemented 
    /// for getting this demo to work.
    /// </summary>
    public sealed class ServiceLocatorProxy : ServiceLocatorImplBase {
        public ITurbineLocator Locator { get; private set; }

        // Inject the Turbine CSL from the container
        public ServiceLocatorProxy(ITurbineLocator locator) {
            Locator = locator;
        }

        protected override object DoGetInstance(Type serviceType, string key) {
            return string.IsNullOrEmpty(key) ? 
                Locator.Resolve<object>(serviceType) : 
                Locator.Resolve<object>(key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType) {
            string message =
                "This is not meant to be called!\r\n" + 
                "The purpose of this type is to just serve as a proxy for MS CSL -> MVC Turbine CSL.";
            
            throw new NotSupportedException(message);
        }
    }
}