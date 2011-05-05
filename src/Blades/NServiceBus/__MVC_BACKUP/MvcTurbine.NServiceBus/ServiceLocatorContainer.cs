namespace MvcTurbine.NServiceBus {
    using System;
    using System.Collections.Generic;
    using ComponentModel;
    using global::NServiceBus.ObjectBuilder;
    using global::NServiceBus.ObjectBuilder.Common;

    public class ServiceLocatorContainer : IContainer {
        public IServiceLocator ServiceLocator { get; private set; }

        public ServiceLocatorContainer(IServiceLocator serviceLocator) {
            ServiceLocator = serviceLocator;
        }

        public object Build(Type typeToBuild) {
            return ServiceLocator.Resolve(typeToBuild);
        }

        public IEnumerable<object> BuildAll(Type typeToBuild) {
            return null;
        }

        public void Configure(Type component, ComponentCallModelEnum callModel) {
            ServiceLocator.Register(component, component);
        }

        public void ConfigureProperty(Type component, string property, object value) {
        }

        public void RegisterSingleton(Type lookupType, object instance) {
            ServiceLocator.Register(instance);
        }
    }
}