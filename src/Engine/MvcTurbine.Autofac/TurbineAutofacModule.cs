namespace MvcTurbine.Autofac {
    using System;
    using System.Collections.Generic;
    using ComponentModel;
    using global::Autofac;

    public class TurbineAutofacModule : Module, IServiceRegistrar {
        private readonly IList<Action<ContainerBuilder>> batchedRegistrations;

        public TurbineAutofacModule() {
            batchedRegistrations = new List<Action<ContainerBuilder>>();
        }
        
        public void Dispose() {
        }

        public void RegisterAll<Interface>() {
        }

        protected override void Load(ContainerBuilder builder) {
            foreach (var registration in batchedRegistrations) {
                registration(builder);
            }
        }

        void AddRegistration(Action<ContainerBuilder> builderAction) {
            batchedRegistrations.Add(builderAction);
        }

        public void Register<Interface>(Type implType) where Interface : class {
            AddRegistration(builder => 
                builder.RegisterType(implType).As<Interface>());
        }

        public void Register<Interface, Implementation>() where Implementation : class, Interface {
            AddRegistration(builder => 
                builder.RegisterType<Implementation>().As<Interface>());
        }

        public void Register<Interface, Implementation>(string key) where Implementation : 
            class, Interface {
            AddRegistration(builder => 
                builder.RegisterType<Implementation>().Named<Interface>(key));
        }

        public void Register(string key, Type type) {
            AddRegistration(builder => 
                builder.RegisterType(type).Named(key, type));
        }

        public void Register(Type serviceType, Type implType) {
            AddRegistration(builder => 
                builder.RegisterType(implType).As(implType).As(serviceType));
        }

        public void Register(Type serviceType, Type implType, string key)
        {
            throw new NotImplementedException();
        }

        public void Register<Interface>(Interface instance) where Interface : class
        {
            AddRegistration(builder => builder.RegisterInstance(instance));
        }

        public void Register<Interface>(Func<Interface> factoryMethod) where Interface : class
        {
            AddRegistration(builder => builder.Register(c => factoryMethod.Invoke()));
        }
    }
}
