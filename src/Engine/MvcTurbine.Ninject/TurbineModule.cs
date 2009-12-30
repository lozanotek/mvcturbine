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

namespace MvcTurbine.Ninject {
    using System;
    using ComponentModel;
    using global::Ninject;
    using global::Ninject.Modules;

    /// <summary>
    /// Defines a module that can be used for registering components
    /// across the application.
    /// </summary>
    public class TurbineModule : NinjectModule, IBatchRegistration {

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
    }
}