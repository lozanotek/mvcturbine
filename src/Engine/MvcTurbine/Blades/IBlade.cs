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

    /// <summary>
    /// Defines the contract for all blades (components) to use.
    /// </summary>
    public interface IBlade : IDisposable {
        /// <summary>
        /// Initializes the blade.
        /// </summary>
        /// <param name="context">Current context for the <see cref="Blade"/> instance.</param>
        void Initialize(IRotorContext context);

        /// <summary>
        /// Executes the current component.
        /// </summary>
        void Spin(IRotorContext context);
    }
}