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

namespace MvcTurbine.Web.Blades {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MvcTurbine.Blades;

    /// <summary>
    /// Helper class for <see cref="IBlade"/> types.
    /// </summary>
    public static class BladeExtensions {
        private static readonly IList<Type> bladeTypes = new List<Type> {
            typeof (ViewBlade),
            typeof (FilterBlade),
            typeof (ControllerBlade),
            typeof (ModelBinderBlade),
            typeof (DependencyResolverBlade)
        };

        /// <summary>
        /// Checks whether the specified <see cref="IBlade"/> is a core blade, <see cref="ViewBlade"/> 
        /// or <see cref="RoutingBlade"/>.
        /// </summary>
        /// <param name="blade"></param>
        /// <returns></returns>
        public static bool IsCoreBlade(this IBlade blade) {
            var type = blade.GetType();
            return bladeTypes.Any(bladeType => bladeType.IsAssignableFrom(type));
        }
    }
}
