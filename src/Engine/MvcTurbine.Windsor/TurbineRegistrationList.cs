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

namespace MvcTurbine.Windsor {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using ComponentModel;

    /// <summary>
    /// Defines the list of registrations to process
    /// </summary>
    public class TurbineRegistrationList : IServiceRegistrator {
        private readonly IList<IRegistration> registrationList;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public TurbineRegistrationList(IWindsorContainer container) {
            Container = container;
            registrationList = new List<IRegistration>();
        }

        ///<summary>
        /// Sets the associated <see cref="IWindsorContainer"/> instance.
        ///</summary>
        public IWindsorContainer Container { get; private set; }

        /// <summary>
        /// Registers all the services of type <typeparamref name="Interface"/> into the container.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        public void RegisterAll<Interface>() {
            //TODO: see if this works
            AllTypes.Of<Interface>();
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register{Interface}<>"/>.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="implType"></param>
        public void Register<Interface>(Type implType) where Interface : class {
            var key = GetKey(typeof(Interface), implType);

            var registration = Component.For<Interface>()
                .Named(key)
                .ImplementedBy(implType)
                .LifeStyle.Transient;

            registrationList.Add(registration);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register{Interface,Implementation}()<>"/>.
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <typeparam name="Implementation"></typeparam>
        public void Register<Interface, Implementation>()
            where Implementation : class, Interface {
            var key = GetKey(typeof(Interface), typeof(Implementation));

            Register<Interface, Implementation>(key);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register{Interface,Implementation}(string)<>"/>
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <typeparam name="Implementation"></typeparam>
        /// <param name="key"></param>
        public void Register<Interface, Implementation>(string key)
            where Implementation : class, Interface {

            var registration = Component.For<Interface>()
                .Named(key)
                .ImplementedBy<Implementation>()
                .LifeStyle.Transient;

            registrationList.Add(registration);
        }

        /// <summary>
        /// See <see cref="IServiceLocator.Register(string,System.Type)"/>.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        public void Register(string key, Type type) {
            var registration = Component.For(type)
                .Named(key)
                .LifeStyle.Transient;

            registrationList.Add(registration);
        }

        /// <summary>
        /// See <seealso cref="IServiceLocator.Register(System.Type,System.Type)"/>.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implType"></param>
        public void Register(Type serviceType, Type implType) {
            var registration = Component.For(serviceType)
                .ImplementedBy(implType)
                .LifeStyle.Transient;

            registrationList.Add(registration);
        }

        /// <summary>
        /// Gets the key generated from the type and implementation.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="impl"></param>
        /// <returns></returns>
        private static string GetKey(Type service, Type impl) {
            return string.Format("{0}-{1}", service.Name, impl.FullName);
        }

        public void Dispose() {
            var registrations = registrationList.ToArray();
            Container.Register(registrations);
        }
    }
}