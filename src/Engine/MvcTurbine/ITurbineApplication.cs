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
    using ComponentModel;

    /// <summary>
    /// Provides the infrastructure for Turbine flow.
    /// </summary>
    public interface ITurbineApplication {

        /// <summary>
        /// Gets or sets the current implementation of <see cref="IServiceLocator"/>
        /// the application instance will use.
        /// </summary>
        IServiceLocator ServiceLocator { get; set; }

        /// <summary>
        /// Gets or sets the current <see cref="IRotorContext"/> for the application instance to use.
        /// </summary>
        IRotorContext CurrentContext { get; set; }

        /// <summary>
        /// Performs any startup processing.
        /// </summary>
        void Startup();

        /// <summary>
        /// Turns the current <see cref="CurrentContext"/>
        /// </summary>
        void TurnRotor();

        /// <summary>
        /// Shuts down the current application.
        /// </summary>
        void Shutdown();
    }
}