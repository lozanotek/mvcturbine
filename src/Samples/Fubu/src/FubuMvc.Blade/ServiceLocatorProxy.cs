namespace FubuMvc.Blades.UI {
    using System;
    using System.Collections.Generic;
    using Microsoft.Practices.ServiceLocation;
    using ITurbineLocator=MvcTurbine.ComponentModel.IServiceLocator;

    public sealed class ServiceLocatorProxy : ServiceLocatorImplBase {
        public ITurbineLocator Locator { get; private set; }

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