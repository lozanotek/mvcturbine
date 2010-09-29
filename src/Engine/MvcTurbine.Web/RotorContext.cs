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

namespace MvcTurbine.Web {
    using System;
    using System.Linq;
    using System.Web;
    using Blades;
    using ComponentModel;
    using MvcTurbine.Blades;

    /// <summary>
    /// Defines the default flow of a <see cref="TurbineApplication"/> instance.
    /// </summary>
    [Serializable]
    public class RotorContext : IRotorContext {
        private static readonly object _lock = new object();
        private static readonly object _regLock = new object();
        private IAutoRegistrator autoRegistrator;

        /// <summary>
        /// Default constructor
        /// </summary>
        public RotorContext(ITurbineApplication application) {
            Application = application;
        }

        /// <summary>
        /// Gets or sets the current implementation of <see cref="IServiceLocator"/>.
        /// </summary>
        public IServiceLocator ServiceLocator {
            get { return Application.ServiceLocator; }
        }

        /// <summary>
        /// Gets or sets the current instance of <see cref="TurbineApplication"/>.
        /// </summary>
        public ITurbineApplication Application { get; private set; }

        /// <summary>
        /// Cleans up the current <see cref="IServiceLocator"/> associated with the context.
        /// </summary>
        public virtual void Dispose() {
            BladeList components = GetAllBlades();

            if (components != null) {
                foreach (IBlade component in components) {
                    try {
                        //HACK: Yes, I know this is ugly but need to figure out how to best handle this
                        component.Dispose();
                    } catch {
                    }
                }
            }

            if (ServiceLocator == null) return;

            try {
                //HACK: Yes, I know this is ugly but need to figure out how to best handle this
                ServiceLocator.Dispose();
            } catch {
            }
        }

        /// <summary>
        /// Initializes the current context by auto-registering the default components.
        /// </summary>
        public virtual void Initialize(ITurbineApplication application) {
            InitializeBlades();
        }

        /// <summary>
        /// Executes the current context.
        /// </summary>
        public virtual void Turn() {
            // Load assemblies into current AppDomain
            LoadAssembliesIntoAppDomain();

            // Do all auto-registrations for the context
            AutoRegistrationForContext();

            // Process all manual registrations
            // By doing this step here, we can provide dependencies
            // for all IBlades in the system.
            ProcessManualRegistrations();

            // Process all auto-registrations the registered
            // and instantiated blades care about
            AutoRegistrationForBlades();

            // Run all the blades since everything is already registered
            RunBlades();
        }

        /// <summary>
        /// Gets the list of components that are to be used for the application.
        /// </summary>
        /// <returns>A list of the components registered with the application.</returns>
        public virtual BladeList GetAllBlades() {
            var list = new BladeList(CoreBlades.GetBlades());

            BladeList commonBlades = GetCommonBlades();
            if (commonBlades != null) {
                list.AddRange(commonBlades);
            }

            return list;
        }

        /// <summary>
        /// Loads the assemblies from the <see cref="HttpRuntime.BinDirectory"/> into the 
        /// <see cref="AppDomain.CurrentDomain"/> to make the auto-registration process work after an AppPool reset.
        /// </summary>
        protected virtual void LoadAssembliesIntoAppDomain() {
            IBinAssemblyLoader binLoader = GetBinLoader();
            binLoader.LoadAssembliesFromBinFolder();
        }

        /// <summary>
        /// Initializes the registered <see cref="Blade"/> instances.
        /// </summary>
        protected virtual void InitializeBlades() {
            PerformBladeAction(blade => blade.Initialize(this));
        }

        /// <summary>
        /// Executes the registered <see cref="Blade"/> instances.
        /// </summary>
        protected virtual void RunBlades() {
            PerformBladeAction(blade => blade.Spin(this));
        }

        /// <summary>
        /// Performs the given <see cref="Action{T}"/> for all registered
        /// <see cref="IBlade"/> in the system.
        /// </summary>
        /// <param name="bladeAction">Action to perform for each <see cref="IBlade"/>.</param>
        private void PerformBladeAction(Action<IBlade> bladeAction) {
            if (bladeAction == null) {
                return;
            }

            BladeList components = GetAllBlades();
            if (components == null || components.Count == 0) {
                return;
            }

            foreach (IBlade component in components) {
                bladeAction(component);
            }
        }

        /// <summary>
        /// Queries all the registered <see cref="IBlade"/> to see if they implement, <see cref="ISupportAutoRegistration"/>
        /// then sets them up
        /// </summary>
        protected virtual void AutoRegistrationForBlades() {
            var registrationList = new AutoRegistrationList();

            // For every blade, check if it needs to auto-register anything
            Action<IBlade> autoRegAction = blade =>
            {
                var autoRegistration = blade as ISupportAutoRegistration;
                if (autoRegistration == null) {
                    return;
                }

                autoRegistration.AddRegistrations(registrationList);
            };

            PerformBladeAction(autoRegAction);

            if (registrationList.Count() == 0) return;

            ProcessAutomaticRegistration(registrationList);
        }

        /// <summary>
        /// Setup registration for "top" level pieces of the application
        /// </summary>
        private void AutoRegistrationForContext() {
            var registrationList = new AutoRegistrationList();

            registrationList
                .Add(Registration.Simple<IServiceRegistration>())
                .Add(Registration.Simple<IBlade>())
                .Add(Registration.Simple<IHttpModule>());

            ProcessAutomaticRegistration(registrationList);
        }

        /// <summary>
        /// Gets all the registered <see cref="IBlade"/> instances that are not part of the <see cref="CoreBlades"/> list.
        /// </summary>
        /// <returns></returns>
        public virtual BladeList GetCommonBlades() {
            var blades = ServiceLocator
                .ResolveServices<IBlade>()
                .Where(blade => !blade.IsCoreBlade());

            return new BladeList(blades);
        }

        /// <summary>
        /// Iterates through all the registered <see cref="IServiceRegistration"/> instances
        /// </summary>
        protected virtual void ProcessManualRegistrations() {
            var registrationList = ServiceLocator.ResolveServices<IServiceRegistration>();
            if (registrationList == null || registrationList.Count == 0) return;

            lock (_regLock)
                using (ServiceLocator.Batch())
                    foreach (IServiceRegistration reg in registrationList)
                        reg.Register(ServiceLocator);
        }

        /// <summary>
        /// Iterates through all the <see cref="ServiceRegistration"/> instances within 
        /// <paramref name="registrationList"/> and process them with the registered 
        /// <see cref="IAutoRegistrator"/> instance or uses <see cref="DefaultAutoRegistrator"/> 
        /// if one is not registered. 
        /// </summary>
        /// <param name="registrationList">Registrations to process</param>
        protected virtual void ProcessAutomaticRegistration(AutoRegistrationList registrationList) {
            IAutoRegistrator registrator = GetAutoRegistrator();

            lock (_regLock)
                using (ServiceLocator.Batch())
                    foreach (ServiceRegistration registration in registrationList)
                        registrator.AutoRegister(registration);
        }

        /// <summary>
        /// Gets the <seealso cref="IAutoRegistrator"/> to use. Checks the container first, it one not found, it returns
        /// the default one.
        /// </summary>
        /// <returns></returns>
        protected virtual IAutoRegistrator GetAutoRegistrator() {
            if (autoRegistrator == null) {
                lock (_lock) {
                    if (autoRegistrator == null) {
                        try {
                            autoRegistrator = ServiceLocator.Resolve<IAutoRegistrator>();
                        } catch (ServiceResolutionException) {
                            autoRegistrator = new DefaultAutoRegistrator(ServiceLocator);
                        }
                    }
                }
            }

            return autoRegistrator;
        }

        /// <summary>
        /// Gets the <see cref="IBinAssemblyLoader"/> to use. Checks the container first, if one is not fou,d it returns
        /// <see cref="DefaultBinAssemblyLoader"/>.
        /// </summary>
        /// <returns></returns>
        protected virtual IBinAssemblyLoader GetBinLoader() {
            try {
                return ServiceLocator.Resolve<IBinAssemblyLoader>();
            } catch {
                return new DefaultBinAssemblyLoader();
            }
        }
    }
}