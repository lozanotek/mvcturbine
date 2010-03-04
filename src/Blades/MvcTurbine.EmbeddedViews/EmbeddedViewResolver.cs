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

namespace MvcTurbine.EmbeddedViews {
    using System;
    using System.Reflection;

    public class EmbeddedViewResolver : IEmbeddedViewResolver {
        public EmbeddedViewTable GetEmbeddedViews() {
            Assembly[] assemblies = GetAssemblies();
            if (assemblies == null || assemblies.Length == 0) return null;

            var table = new EmbeddedViewTable();

            foreach (var assembly in assemblies) {
                var names = GetNamesOfAssemblyResources(assembly);
                if (names == null || names.Length == 0) continue;

                foreach (var name in names) {
                    var key = name.ToLowerInvariant();
                    if (!key.Contains(".views.")) continue;

                    table.AddView(name, assembly.FullName);
                }
            }

            return table;
        }

        protected virtual Assembly[] GetAssemblies() {
            try {
                return AppDomain.CurrentDomain.GetAssemblies();
            } catch {
                return null;
            }
        }

        private static string[] GetNamesOfAssemblyResources(Assembly assembly)
        {
            // GetManifestResourceNames will throw a NotSupportedException when run on a dynamic assembly
            try{
                return assembly.GetManifestResourceNames();
            }
            catch{
                return new string[] { };
            }
        }
    }
}