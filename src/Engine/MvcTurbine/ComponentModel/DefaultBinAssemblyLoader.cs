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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Web;

    /// <summary>
    /// Default implementation of the <see cref="IBinAssemblyLoader"/>.
    /// </summary>
    public class DefaultBinAssemblyLoader : IBinAssemblyLoader {
        
        /// <summary>
        /// Loads the assemblies in the bin folder that are not currently in the <see cref="AppDomain.CurrentDomain"/>.
        /// </summary>
        /// <returns>A list of assemblies that were loaded into the <see cref="AppDomain.CurrentDomain"/>.</returns>
        public IList<string> LoadAssembliesFromBinFolder() {
            var currentAssemblies = GetCurrentAppDomainAssemblies();

            var assemblyFiles = GetAssembliesInBinFolder();
            var loadedAssembly = new List<string>();

            foreach (var file in assemblyFiles) {
                var assemblyName = Path.GetFileNameWithoutExtension(file);
                if (currentAssemblies.Contains(assemblyName)) continue;

                try {
                    var assembly = Assembly.LoadFrom(file);
                    loadedAssembly.Add(assembly.FullName);
                } catch (Exception exception) {
                    throw new AssemblyDependencyException(file, exception);
                }
            }

            return loadedAssembly;
        }

        /// <summary>
        /// Gets the assemblies that are currently in the <see cref="AppDomain.CurrentDomain"/>
        /// </summary>
        /// <returns>List of the simple name for the assemblies in the <see cref="AppDomain.CurrentDomain"/>.</returns>
        protected virtual IList<string> GetCurrentAppDomainAssemblies() {
            var domain = AppDomain.CurrentDomain;
            var assemblies = domain.GetAssemblies();

            var nameList = new List<string>();
            foreach (var assembly in assemblies) {
                nameList.Add(assembly.GetName().Name);
            }

            return nameList;
        }

        /// <summary>
        /// Gets the assembly files (*.dll) in the <see cref="HttpRuntime.BinDirectory"/> for the application.
        /// </summary>
        /// <returns>List of paths for the assemblies in the bin folder.</returns>
        protected virtual IList<string> GetAssembliesInBinFolder() {
            var binDir = HttpRuntime.BinDirectory;
            var dllFiles = Directory.GetFiles(binDir, "*.dll");
            return new List<string>(dllFiles);
        }
    }
}
