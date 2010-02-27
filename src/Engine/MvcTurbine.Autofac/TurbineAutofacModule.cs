namespace MvcTurbine.Autofac {
    using System;
    using ComponentModel;
    using global::Autofac;

    public class TurbineAutofacModule : Module, IServiceRegistrar {
        public ContainerBuilder Builder { get; private set; }

        public TurbineAutofacModule()
            : this(new ContainerBuilder()) {
        }

        public TurbineAutofacModule(ContainerBuilder builder) {
            Builder = builder;
        }

        public void Dispose() {
            InvokeDisposed(EventArgs.Empty);
        }

        public event EventHandler Disposed;

        private void InvokeDisposed(EventArgs e) {
            EventHandler disposed = Disposed;
            if (disposed != null) {
                disposed(this, e);
            }
        }

        public void RegisterAll<Interface>() {
        }

        public void Register<Interface>(Type implType) where Interface : class {
            Builder.RegisterType(implType).As<Interface>();
        }

        public void Register<Interface, Implementation>() where Implementation : class, Interface {
            Builder.RegisterType<Implementation>().As<Interface>();
        }

        public void Register<Interface, Implementation>(string key) where Implementation : class, Interface {
            Builder.RegisterType<Implementation>().Named<Interface>(key);
        }

        public void Register(string key, Type type) {
            Builder.RegisterType(type).Named(key, type);
        }

        public void Register(Type serviceType, Type implType) {
            Builder.RegisterType(implType).As(implType).As(serviceType);
        }
    }
}