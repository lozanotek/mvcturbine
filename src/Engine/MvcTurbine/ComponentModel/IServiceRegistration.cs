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

namespace MvcTurbine.ComponentModel {
    /// <summary>
    /// Provides a simple way register components within your application.
    /// </summary>
    public interface IServiceRegistration {

        /// <summary>
        /// Registers the components with the specified <see cref="IServiceLocator"/> instance.
        /// </summary>
        /// <param name="locator">Instance of <see cref="IServiceLocator"/> to use.</param>
        void Register(IServiceLocator locator);
    }
}