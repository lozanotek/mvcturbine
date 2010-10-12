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

namespace MvcTurbine {
    using System;
    using Blades;
    using ComponentModel;

    /// <summary>
    /// Defines the default flow of a <see cref="ITurbineApplication"/> instance.
    /// </summary>
    public interface IRotorContext : IDisposable {
        /// <summary>
        /// Gets or sets the current implementation of <see cref="IServiceLocator"/>.
        /// </summary>
        IServiceLocator ServiceLocator { get; }

        /// <summary>
        /// Gets or sets the current instance of <see cref="ITurbineApplication"/>.
        /// </summary>
        ITurbineApplication Application { get; }

        /// <summary>
        /// Initializes the current context by auto-registering the default components.
        /// </summary>
        void Initialize(ITurbineApplication application);

        /// <summary>
        /// Executes the current context.
        /// </summary>
        void Turn();

        /// <summary>
        /// Gets the list of components that are to be used for the application.
        /// </summary>
        /// <returns>A list of the components registered with the application.</returns>
        BladeList GetAllBlades();
    }
}