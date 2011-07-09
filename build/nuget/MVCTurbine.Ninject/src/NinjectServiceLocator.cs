namespace MvcTurbine.Ninject {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ComponentModel;
    using global::Ninject;

    /// <summary>
    /// Default implementation of the <seealso cref="IServiceLocator"/> contract with Ninject IoC.
    /// </summary>
    /// <remarks>
    /// To learn more about Ninject, please visit its website: http://ninject.org
    /// </remarks>
    [Serializable]
    public class NinjectServiceLocator : IServiceLocator, IServiceInjector {
        private TurbineModule currentModule;
        private static bool isDisposing;

        /// <summary>
        /// Default constructor. Locator is instantiated with a new <seealso cref="StandardKernel"/> instance.
        /// </summary>
        public NinjectServiceLocator() : this(new StandardKernel()) {
        }

        /// <summary>
        /// Creates an instance of the locator with the specified <seealso cref="IKernel"/>.
        /// </summary>
        /// <param name="kernel">Pre-defined <see cref="IKernel"/> to use within the container.</param>
        public NinjectServiceLocator(IKernel kernel) {
            if (kernel == null) {
                throw new ArgumentNullException("kernel", "The specified Ninject IKernel cannot be null.");
            }

            Container = kernel;
        }

        /// <summary>
        /// Gets the current <see cref="IKernel"/> associated with this instance.
        /// </summary>
        public IKernel Container { get; private set; }

        public IList<object> ResolveServices(Type type) {
            return Container.GetAll(type).ToList();
        }

        /// <summary>
        /// Gets the associated <see cref="IServiceRegistrar"/> to process.
        /// </summary>
        /// <returns></returns>
        public IServiceRegistrar Batch() {
            currentModule = new TurbineModule(Container);
            return currentModule;
        }

        /// <summary>
        /// Resolves the service of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>() where T : class {
            try {
                return Container.Get<T>();
            }
            catch (ActivationException activationException) {
                return ResolveTheFirstBindingFromTheContainer(activationException, typeof(T)) as T;
            }
            catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        /// <summary>
        /// Resolves the service of the specified type by the given string key.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <param name="key">Unique key to distinguish the service.</param>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>(string key) where T : class {
            try {
                var value = Container.Get<T>(key);

                if (value == null) {
                    throw new ServiceResolutionException(typeof(T));
                }

                return value;
            }
            catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        /// <summary>
        /// Resolves the service of the specified type by the given type key.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <param name="type">Key type of the service.</param>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>(Type type) where T : class {
            try {
                return Container.Get(type) as T;
            }
            catch (ActivationException activationException) {
                return ResolveTheFirstBindingFromTheContainer(activationException, typeof(T)) as T;
            }
            catch (Exception ex) {
                throw new ServiceResolutionException(type, ex);
            }
        }

        ///<summary>
        /// Resolves the service of the specified type by the given type key.
        ///</summary>
        ///<param name="type">Type of service to resolve.</param>
        ///<returns>An instance of the type, null otherwise</returns>
        public object Resolve(Type type) {
            try {
                return Container.Get(type);
            }
            catch (ActivationException activationException) {
                return ResolveTheFirstBindingFromTheContainer(activationException, type);
            }
        }

        /// <summary>
        /// Resolves the list of services of type <see cref="T"/> that are registered 
        /// within the locator.
        /// </summary>
        /// <typeparam name="T">Type of the service to resolve.</typeparam>
        /// <returns>A list of service of type <see cref="T"/>, null otherwise.</returns>
        public IList<T> ResolveServices<T>() where T : class {
            return Container.GetAll<T>().ToList();
        }

        /// <summary>
        /// Registers the implemation type, <paramref name="implType"/>, with the locator under
        /// the <see cref="Interface"/> service type.
        /// </summary>
        /// <typeparam name="Interface">Type of the service to register.</typeparam>
        /// <param name="implType">Implementation type to use for registration.</param>
        public void Register<Interface>(Type implType) where Interface : class {
            currentModule.Register<Interface>(implType);
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
            currentModule.Register<Interface, Implementation>();
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
            currentModule.Register<Interface, Implementation>(key);
        }

        /// <summary>
        /// Registers the implementation type, <paramref name="type"/>, with the locator
        /// by the given string key.
        /// </summary>
        /// <param name="key">Unique key to distinguish the service.</param>
        /// <param name="type">Implementation type to use.</param>
        public void Register(string key, Type type) {
            currentModule.Register(key, type);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register(System.Type,System.Type)"/>.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implType"></param>
        public void Register(Type serviceType, Type implType) {
            currentModule.Register(serviceType, implType);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register(System.Type,System.Type, System.String)"/>.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implType"></param>
        /// <param name="key"></param>
        public void Register(Type serviceType, Type implType, string key) {
            currentModule.Register(serviceType, implType, key);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register{Interface}(Interface)"/>.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="instance"></param>
        public void Register<Interface>(Interface instance) where Interface : class {
            currentModule.Register(instance);
        }

        /// <summary>
        /// Resolves the service of the specified interface with the provided factory method.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="factoryMethod"></param>
        public void Register<Interface>(Func<Interface> factoryMethod) where Interface : class {
            currentModule.Register(factoryMethod);
        }


        /// <summary>
        /// Resolves the service of the specified interface with the provided factory method.
        /// </summary>
        /// <param name="factoryMethod">The factory method which will be used to resolve this interface.</param>
        /// <returns>An instance of the type, null otherwise</returns>
        public TService Inject<TService>(TService instance) where TService : class {
            Container.Inject(instance);
            return instance;
        }

        public void TearDown<TService>(TService instance) where TService : class {
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose() {
            // If we're in the process of disposing, return
            if (isDisposing) return;
            if (Container == null) return;

            // Signal the fact that we're disposing the container
            isDisposing = true;

            // Perform actual dispose
            Container.Dispose();
            
            // Clean up these pieces
            Container = null;
            currentModule = null;
        }

        #region Handle Activation Exception

        private object ResolveTheFirstBindingFromTheContainer(Exception activationException, Type type) {
            var firstBinding = GetNameOfFirstBinding(type);
            if (firstBinding.BindingExists)
                return Container.Get(type, firstBinding.Name);

            throw new ServiceResolutionException(type, activationException);
        }

        private FirstBindingInfo GetNameOfFirstBinding(Type type) {
            var binding = Container.GetBindings(type).OrderBy(x => x.Metadata.Name).FirstOrDefault();

            return binding == null ? new FirstBindingInfo { BindingExists = false } :
                new FirstBindingInfo { BindingExists = true, Name = binding.Metadata.Name };
        }

        private class FirstBindingInfo {
            public string Name { get; set; }
            public bool BindingExists { get; set; }
        }

        #endregion
    }
}
