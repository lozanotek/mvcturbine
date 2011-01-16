#region License

//
// Author: Javier Lozano <javier@lozanotek.com>
// Copyright (c) 2009-2010, lozanotek, inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//

#endregion

namespace MvcTurbine.StructureMap {
    using System;
    using ComponentModel;
    using global::StructureMap;
    using global::StructureMap.Configuration.DSL;

    /// <summary>
    /// Internal registry for Turbine to use
    /// </summary>
    public class TurbineRegistry : Registry, IServiceRegistrar {

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="container"></param>
        public TurbineRegistry(IContainer container) {
            Container = container;
        }

        /// <summary>
        /// Gets the associated <see cref="IContainer"/> with this registry.
        /// </summary>
        public IContainer Container { get; private set; }

        /// <summary>
        /// Registers all the services of type <typeparamref name="Interface"/> into the container.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterAll<Interface>() {
            Scan(scanner => scanner.AddAllTypesOf<Interface>());
        }

        /// <summary>
        /// Registers the specified <paramref name="implType"/> for the <typeparamref name="Interface"/> contract.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="implType"></param>
        public void Register<Interface>(Type implType) where Interface : class {
            ForRequestedType(typeof(Interface))
                .AddType(implType);
        }

        /// <summary>
        /// Registers the implemation type, <see cref="Implementation"/>, with the locator under
        /// the <see cref="Interface"/> service type.
        /// </summary>
        /// <typeparam name="Interface">Type of the service to register.</typeparam>
        /// <typeparam name="Implementation">Implementation type to use for registration.
        /// </typeparam>
        public void Register<Interface, Implementation>() where Implementation : class, Interface {
            ForRequestedType<Interface>()
                .AddConcreteType<Implementation>();
        }

        /// <summary>
        /// Registers the implemation type, <see cref="Implementation"/>, with the locator under
        /// the <see cref="Interface"/> service type.
        /// </summary>
        /// <typeparam name="Interface">Type of the service to register.</typeparam>
        /// <typeparam name="Implementation">Implementation type to use for registration.
        /// </typeparam>
        /// <param name="key">Unique key to distinguish the service.</param>
        public void Register<Interface, Implementation>(string key) where Implementation : class, Interface {
            Type serviceType = typeof(Interface);
            Type implType = typeof(Implementation);

            ForRequestedType(serviceType)
                .AddType(implType)
                .WithName(key);
        }

        /// <summary>
        /// Registers the implementation type, <paramref name="type"/>, with the locator
        /// by the given string key.
        /// </summary>
        /// <param name="key">Unique key to distinguish the service.</param>
        /// <param name="type">Implementation type to use.</param>
        public void Register(string key, Type type) {
            ForRequestedType(type)
                .AddType(type)
                .WithName(key);
        }

        /// <summary>
        /// Registers the implementation type, <paramref name="implType"/>, with the locator
        /// by the given service type, <paramref name="serviceType"/>
        /// </summary>
        /// <param name="serviceType">Type of the service to register.</param>
        /// <param name="implType">Implementation to associate with the service.</param>
        public void Register(Type serviceType, Type implType) {
            ForRequestedType(serviceType)
                .AddType(implType);
        }

        /// <summary>
        /// See <see cref="IServiceRegistrar.Register{Interface}(Interface)"/>.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="instance"></param>
        public void Register<Interface>(Interface instance) where Interface : class {
            For<Interface>().Use(instance);
        }

        /// <summary>
        /// See <see cref="IServiceRegistrar.Register{Interface}(Func{Interface})"/>.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="factoryMethod"></param>
        public void Register<Interface>(Func<Interface> factoryMethod) where Interface : class
        {
            Container.Configure(cfg => cfg.For<Interface>().Use(factoryMethod.Invoke));
        }

        public void Dispose() {
            // Process the current registration
            Container.Configure(x => x.AddRegistry(this));
        }
    }
}