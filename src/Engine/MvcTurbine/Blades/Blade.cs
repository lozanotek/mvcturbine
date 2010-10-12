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

namespace MvcTurbine.Blades {
    using System;
    using ComponentModel;

    /// <summary>
    /// Defines the base implementation of a component within a <see cref="IRotorContext"/>.
    /// </summary>
    [Serializable]
    public abstract class Blade : IBlade {
        
        /// <summary>
        /// Cleans up the current component.
        /// </summary>
        public virtual void Dispose() {
            InvokeDisposed(EventArgs.Empty);
        }

        /// <summary>
        /// Informs registrants of the initialization of the component.
        /// </summary>
        public event EventHandler Initialized;

        /// <summary>
        /// Informs the registrants of the disposing of the component.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Raises the <see cref="Initialized"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void InvokeInitialized(EventArgs e) {
            EventHandler initialized = Initialized;
            if (initialized != null) initialized(this, e);
        }

        /// <summary>
        /// Raises the <see cref="Disposed"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void InvokeDisposed(EventArgs e) {
            EventHandler disposed = Disposed;
            if (disposed != null) disposed(this, e);
        }

        /// <summary>
        /// Initializes the blade
        /// </summary>
        /// <param name="context">Current context for the <see cref="Blade"/> instance.</param>
        public virtual void Initialize(IRotorContext context) {
            InvokeInitialized(EventArgs.Empty);
        }

        /// <summary>
        /// Executes the current blade.
        /// </summary>
        public abstract void Spin(IRotorContext context);

        /// <summary>
        /// Gets the current <see cref="IServiceLocator"/> from the current <see cref="IRotorContext"/>
        /// to use within this blade.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected virtual IServiceLocator GetServiceLocatorFromContext(IRotorContext context) {
            if (context == null) {
                throw new ArgumentNullException("context");
            }

            // Check the current IServiceLocator instance
            IServiceLocator locator = context.ServiceLocator;

            if (locator == null) {
                throw new InvalidOperationException("The current IRotorContext does not have a valid IServiceLocator,");
            }

            return locator;
        }
    }
}
