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
namespace MvcTurbine.Autofac {
    using System;
    using System.Collections.Generic;
    using ComponentModel;
    using global::Autofac;

    /// <summary>
    /// Default implementation of the <seealso cref="IServiceLocator"/> contract with Autofac IoC.
    /// </summary>
    /// <remarks>
    /// To learn more about Autofac, please visit its website: http://code.google.com/p/autofac
    /// </remarks>
    public class AutofacServiceLocator : IServiceLocator {
        private readonly IList<IContainer> builtContainers = new List<IContainer>();
        private TurbineAutofacModule currentModule;

        /// <summary>
        /// Default constructor. Locator is instantiated with a new <see cref="ContainerBuilder"/> instance.
        /// </summary>
        public AutofacServiceLocator() {
            Builder = new ContainerBuilder();
        }

        public AutofacServiceLocator(ContainerBuilder builder) {
            if (builder == null) {
                throw new ArgumentNullException("builder",
                    "The specified Autofac ContainerBuilder cannot be null.");
            }

            Builder = builder;
            builtContainers.Add(builder.Build());
        }

        public ContainerBuilder Builder { get; private set; }

        public void Dispose() {
            foreach (var container in builtContainers) {
                container.Dispose();
            }
        }

        public T Resolve<T>() where T : class {
            try {
                foreach (var container in builtContainers) {
                    if (!container.IsRegistered<T>()) continue;

                    return container.Resolve<T>();
                }

                throw new Exception();
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        public T Resolve<T>(string key) where T : class {
            try {
                foreach (var container in builtContainers) {
                    if (!container.IsRegistered<T>(key)) continue;

                    return container.Resolve<T>(key);
                }

                throw new Exception();
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        public T Resolve<T>(Type type) where T : class {
            try {
                foreach (var container in builtContainers) {
                    if (!container.IsRegistered(type)) continue;
                    return container.Resolve(type) as T;
                }

                throw new Exception();
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        public IList<T> ResolveServices<T>() where T : class {
            try {
                foreach (var container in builtContainers) {
                    if (!container.IsRegistered<IEnumerable<T>>()) continue;
                    var enumerable = container.Resolve<IEnumerable<T>>();
                    return new List<T>(enumerable);
                }

                throw new Exception();
            } catch (Exception ex) {
                throw new ServiceResolutionException(typeof(T), ex);
            }
        }

        public IServiceRegistrar Batch() {
            currentModule = new TurbineAutofacModule();
            currentModule.Disposed += module_Disposed;

            return currentModule;
        }

        void module_Disposed(object sender, EventArgs e) {
            var module = sender as TurbineAutofacModule;
            if (module == null) return;
            builtContainers.Add(module.Builder.Build());
        }

        public void Register<Interface>(Type implType) where Interface : class {
            currentModule.Register<Interface>(implType);
        }

        public void Register<Interface, Implementation>() where Implementation : class, Interface {
            currentModule.Register<Interface, Implementation>();
        }

        public void Register<Interface, Implementation>(string key) where Implementation : class, Interface {
            currentModule.Register<Interface, Implementation>(key);
        }

        public void Register(string key, Type type) {
            currentModule.Register(key, type);
        }

        public void Register(Type serviceType, Type implType) {
            currentModule.Register(serviceType, implType);
        }

        [Obsolete("Not used with this implementation of IServiceLocator.")]
        public void Release(object instance) {
        }

        public void Reset() {
            Dispose();
            //Container = null;
            Builder = null;
            currentModule = null;
        }

        public TService Inject<TService>(TService instance) where TService : class {
            TService oldInstance = instance;
            foreach (var container in builtContainers) {
                oldInstance = container.InjectProperties(oldInstance);
            }

            return oldInstance;
        }

        [Obsolete("Not used with this implementation of IServiceLocator.")]
        public void TearDown<TService>(TService instance) where TService : class {
        }
    }
}
