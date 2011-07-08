using System.ComponentModel;
using System.Linq;
using Hiro;
using Hiro.Containers;

namespace MvcTurbine.Hiro {
    using System;
    using System.Collections.Generic;
    using ComponentModel;

    /// <summary>
    /// Default implementation of the <seealso cref="IServiceLocator"/> contract with Hiro IoC.
    /// </summary>
    /// <remarks>
    /// To learn more about Hiro, please visit its website: https://github.com/philiplaureano/Hiro
    /// </remarks>
    [Serializable]
    public class HiroServiceLocator : IServiceLocator, IServiceReleaser, IServiceInjector {
        private static bool isDisposing;

        private static readonly object _lock = new object();
        private bool containerNeedsToBeReset = true;


        /// <summary>
        /// Creates an instance of the locator with an empty <seealso cref="DependencyMap"/> instance.
        /// </summary>
        public HiroServiceLocator()
            : this(new DependencyMap()) {
        }

        /// <summary>
        /// Creates an instance of the locator with the specified <seealso cref="IHiroContainer"/> instance.
        /// </summary>
        /// <param name="container">Instance of <seealso cref="IHiroContainer"/> to use.</param>
        public HiroServiceLocator(DependencyMap dependencyMap)
        {
            if (dependencyMap == null)
                throw new ArgumentNullException("dependencyMap", "The specified Hiro dependency map cannot be null.");

            DependencyMap = dependencyMap;
        }

        /// <summary>
        /// Gets the current <seealso cref="DependencyMap"/> associated with this instance.
        /// </summary>
        public DependencyMap DependencyMap { private set; get; }

        public IList<object> ResolveServices(Type type)
        {
            var container = GetTheContainer();
            return container.GetAllInstances(type).ToList();
        }

        private IMicroContainer microContainer;
        private IMicroContainer lastMicroContainer = new DependencyMap().CreateContainer();
        private IMicroContainer GetTheContainer()
        {
            if (containerNeedsToBeReset)
                lock(_lock)
                    if (containerNeedsToBeReset)
                        ResetTheContainer();
            return microContainer;
        }

        private void ResetTheContainer()
        {
            try
            {
                microContainer = DependencyMap.CreateContainer();
                containerNeedsToBeReset = false;
                lastMicroContainer = microContainer;
            } catch
            {
                microContainer = lastMicroContainer;
            }
        }

        /// <summary>
        /// Gets the associated <see cref="IServiceRegistrar"/> to process.
        /// </summary>
        /// <returns></returns>
        public IServiceRegistrar Batch() {
            return new RegistrationStub();
        }

        /// <summary>
        /// Resolves the service of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>() where T : class
        {
            try
            {
                var instance = GetTheContainer().GetInstance<T>("__");

                if (instance == null)
                {
                    if (ThisIsAConcreteType(typeof(T)))
                        Register<T, T>();
                    instance = GetTheContainer().GetInstance<T>("__");
                    if (instance == null)
                        throw new ServiceResolutionException(typeof(T));
                }
                return instance;
            } catch
            {
                throw new ServiceResolutionException(typeof(T));
            }
        }

        private bool ThisIsAConcreteType(Type type)
        {
            return type.IsInterface == false && type.IsAbstract == false;
        }

        /// <summary>
        /// Resolves the service of the specified type by the given string key.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <param name="key">Unique key to distinguish the service.</param>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>(string key) where T : class {
            try {
                var container = GetTheContainer();
                var results = container.GetInstance<T>(key);
                if (results == null)
                    throw new ServiceResolutionException(typeof(T));
                return results;
            }
            catch
            {
                throw new ServiceResolutionException(typeof(T));
            }
        }

        /// <summary>
        /// Resolves the service of the specified type by the given type key.
        /// </summary>
        /// <typeparam name="T">Type of service to resolve.</typeparam>
        /// <param name="type">Key type of the service.</param>
        /// <returns>An instance of the type, null otherwise.</returns>
        public T Resolve<T>(Type type) where T : class {
            try
            {
                T instance = null;
                instance = GetTheContainer().GetInstance(type, "__") as T;
                if (instance == null)
                {
                    if (ThisIsAConcreteType(type))
                        Register(typeof(T), type);
                    instance = GetTheContainer().GetInstance<T>();
                    if (instance == null)
                        throw new ServiceResolutionException(type);
                }
                return instance;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///<summary>
        /// Resolves the service of the specified type by the given type key.
        ///</summary>
        ///<param name="type">Type of service to resolve.</param>
        ///<returns>An instance of the type, null otherwise</returns>
        public object Resolve(Type type) {
            try {
                var container = GetTheContainer();
                var instance = container.GetInstance(type, "__");
                if (instance == null)
                {
                    if (ThisIsAConcreteType(type))
                        Register(type, type);
                    instance = GetTheContainer().GetInstance(type, "__");
                    if (instance == null)
                        throw new ServiceResolutionException(type);
                }
                return instance;
            }
            catch
            {
                throw new ServiceResolutionException(type);
            }
        }

        /// <summary>
        /// Resolves the list of services of type <see cref="T"/> that are registered 
        /// within the locator.
        /// </summary>
        /// <typeparam name="T">Type of the service to resolve.</typeparam>
        /// <returns>A list of service of type <see cref="T"/>, null otherwise.</returns>
        public IList<T> ResolveServices<T>() where T : class {
            try
            {
                var container = GetTheContainer();
                var serviceType = typeof (T);
                var services = container.GetAllInstances(serviceType);
                return services.OfType<T>().ToList();
            }
            catch
            {
                throw new ServiceResolutionException(typeof(T));
            }
        }

        /// <summary>
        /// Registers the implemation type, <paramref name="implType"/>, with the locator under
        /// the <see cref="Interface"/> service type.
        /// </summary>
        /// <typeparam name="Interface">Type of the service to register.</typeparam>
        /// <param name="implType">Implementation type to use for registration.</param>
        public void Register<Interface>(Type implType) where Interface : class
        {
            if (ThisIsAConcreteType(implType) == false) return;
            var name = GetDefaultServiceKeyForThisType(typeof(Interface));
            DependencyMap.AddService(name, typeof(Interface), implType);
            MarkTheContainerAsNeedingToBeReset();
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
            if (ThisIsAConcreteType(typeof(Implementation)) == false) return;

            var name = GetDefaultServiceKeyForThisType(typeof(Interface));
            DependencyMap.AddService<Interface, Implementation>(name);
            
            MarkTheContainerAsNeedingToBeReset();
        }

        private string GetDefaultServiceKeyForThisType(Type type)
        {
            var name = "__";
            if (DependencyMap.Dependencies.Any(x => x.ServiceType == type && x.ServiceName == "__"))
                name = Guid.NewGuid().ToString();
            return name;
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
            where Implementation : class, Interface
        {
            if (ThisIsAConcreteType(typeof (Implementation)) == false) return;
            DependencyMap.AddService(key, typeof (Interface), typeof (Implementation));
            MarkTheContainerAsNeedingToBeReset();
        }

        /// <summary>
        /// Registers the implementation type, <paramref name="type"/>, with the locator
        /// by the given string key.
        /// </summary>
        /// <param name="key">Unique key to distinguish the service.</param>
        /// <param name="type">Implementation type to use.</param>
        public void Register(string key, Type type) {
            DependencyMap.AddService(key, type, type);
            MarkTheContainerAsNeedingToBeReset();
        }

        /// <summary>
        /// Registers the implementation type, <paramref name="implType"/>, with the locator
        /// by the given service type, <paramref name="serviceType"/>
        /// </summary>
        /// <param name="serviceType">Type of the service to register.</param>
        /// <param name="implType">Implementation to associate with the service.</param>
        public void Register(Type serviceType, Type implType) {
            DependencyMap.AddService("__", serviceType, implType);
            DependencyMap.AddService(serviceType, implType);
            MarkTheContainerAsNeedingToBeReset();
        }

        /// <summary>
        /// Registers the instance of type, <typeparamref name="Interface"/>, with the locator.
        /// </summary>
        /// <typeparam name="Interface">Type of the service to register.</typeparam>
        /// <param name="instance">Instance of the type to register.</param>
        public void Register<Interface>(Interface instance) where Interface : class {
            Func<IMicroContainer, Interface> functor = x => instance;

            DependencyMap.AddService("__", functor);
            MarkTheContainerAsNeedingToBeReset();
        }

        /// <summary>
        /// Resolves the service of the specified interface with the provided factory method.
        /// </summary>
        /// <param name="factoryMethod">The factory method which will be used to resolve this interface.</param>
        /// <returns>An instance of the type, null otherwise</returns>
        public void Register<Interface>(Func<Interface> factoryMethod) where Interface : class {
            Func<IMicroContainer, Interface> functor = c => factoryMethod();

            DependencyMap.AddService("__", functor);
            MarkTheContainerAsNeedingToBeReset();
        }

        /// <summary>
        /// Releases (disposes) the service instance from within the locator.
        /// </summary>
        /// <param name="instance">Instance of a service to dipose from the locator.</param>
        public void Release(object instance)
        {
            // not implemented in Hiro
        }

        public TService Inject<TService>(TService instance) where TService : class {
            // not implemented in Hiro
            return instance;
        }

        public void TearDown<TService>(TService instance) where TService : class {
            // not implemented in Hiro
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose() {
            if (isDisposing) return;
            if (microContainer == null) return;

            isDisposing = true;

            microContainer = null;
        }

        private void MarkTheContainerAsNeedingToBeReset()
        {
            containerNeedsToBeReset = true;
        }
    }

    /// <summary>
    /// This class is for stubbing purposes only.
    /// </summary>
    internal sealed class RegistrationStub : IServiceRegistrar {
        public void Dispose() {
        }

        public void RegisterAll<Interface>() {
            throw new NotImplementedException();
        }

        public void Register<Interface>(Type implType) where Interface : class {
            throw new NotImplementedException();
        }

        public void Register<Interface, Implementation>() where Implementation : class, Interface {
            throw new NotImplementedException();
        }

        public void Register<Interface, Implementation>(string key) where Implementation : class, Interface {
            throw new NotImplementedException();
        }

        public void Register(string key, Type type) {
            throw new NotImplementedException();
        }

        public void Register(Type serviceType, Type implType) {
            throw new NotImplementedException();
        }

        public void Register<Interface>(Interface instance) where Interface : class {
            throw new NotImplementedException();
        }

        public void Register<Interface>(Func<Interface> factoryMethod) where Interface : class {
            throw new NotImplementedException();
        }
    }
}
