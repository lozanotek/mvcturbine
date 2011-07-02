namespace MvcTurbine.Ninject {
    using System;
    using ComponentModel;
    using global::Ninject;
    using global::Ninject.Modules;

    /// <summary>
    /// Defines a module that can be used for registering components
    /// across the application.
    /// </summary>
    public class TurbineModule : NinjectModule, IServiceRegistrar {

        private Guid moduleId;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="kernel"></param>
        public TurbineModule(IKernel kernel) {
            Container = kernel;
            Container.Load(this);
        }

        /// <summary>
        /// Gets the associated <see cref="IKernel"/> for the registration.
        /// </summary>
        public IKernel Container { get; private set; }

        /// <summary>
        /// Sets the unique ID for the module
        /// </summary>
        public override void Load() {
            moduleId = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the name for the module
        /// </summary>
        public override string Name {
            get {
                return moduleId.ToString();
            }
        }

        /// <summary>
        /// Registers all the services of type <typeparamref name="Interface"/> into the container.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterAll<Interface>() {
        }

        /// <summary>
        /// Registers the implemation type, <paramref name="implType"/>, with the locator under
        /// the <see cref="Interface"/> service type.
        /// </summary>
        /// <typeparam name="Interface">Type of the service to register.</typeparam>
        /// <param name="implType">Implementation type to use for registration.</param>
        public void Register<Interface>(Type implType) where Interface : class {
            string key = string.Format("{0}-{1}", typeof(Interface).Name, implType.FullName);

            Bind<Interface>().To(implType).Named(key);
        }

        /// <summary>
        /// Registers the implemation type, <see cref="Implementation"/>, with the locator under
        /// the <see cref="Interface"/> service type.
        /// </summary>
        /// <typeparam name="Interface">Type of the service to register.</typeparam>
        /// <typeparam name="Implementation">Implementation type to use for registration.
        /// </typeparam>
        public void Register<Interface, Implementation>()
            where Implementation : class, Interface {

            Bind<Interface>().To<Implementation>();
        }

        /// <summary>
        /// Registers the implemation type, <see cref="Implementation"/>, with the locator under
        /// the <see cref="Interface"/> service type.
        /// </summary>
        /// <typeparam name="Interface">Type of the service to register.</typeparam>
        /// <typeparam name="Implementation">Implementation type to use for registration.
        /// </typeparam>
        /// <param name="key">Unique key to distinguish the service.</param>
        public void Register<Interface, Implementation>(string key)
            where Implementation : class, Interface {

            Bind<Interface>().To(typeof(Implementation)).Named(key);
        }

        /// <summary>
        /// Registers the implementation type, <paramref name="type"/>, with the locator
        /// by the given string key.
        /// </summary>
        /// <param name="key">Unique key to distinguish the service.</param>
        /// <param name="type">Implementation type to use.</param>
        public void Register(string key, Type type) {
            Bind(type).ToSelf().Named(key);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register(System.Type,System.Type)"/>.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implType"></param>
        public void Register(Type serviceType, Type implType) {
            Bind(serviceType).To(implType);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register(System.Type,System.Type,System.String)"/>.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implType"></param>
        /// <param name="key"></param>
        public void Register(Type serviceType, Type implType, string key) {
            Bind(serviceType).To(implType).Named(key);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register{Interface}<>"/>
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="instance"></param>
        public void Register<Interface>(Interface instance) where Interface : class {
            Bind<Interface>().ToConstant(instance);
        }
        
        /// <summary>
        /// See <see cref="IServiceLocator.Register{Interface}<>(Func{Interface})"/>
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="factoryMethod"></param>
        public void Register<Interface>(Func<Interface> factoryMethod) where Interface : class {
            Bind<Interface>().ToMethod(c => factoryMethod.Invoke());
        }
    }
}
