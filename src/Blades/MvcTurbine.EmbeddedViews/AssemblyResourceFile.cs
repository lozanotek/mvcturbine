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
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Hosting;

    public class AssemblyResourceFile : VirtualFile {
        private readonly EmbeddedView embeddedView;

        public AssemblyResourceFile(EmbeddedView view, string virtualPath) :
            base(virtualPath) {
            if(view == null) throw new ArgumentNullException("view", "EmbeddedView cannot be null.");

            embeddedView = view;
        }

        public override Stream Open() {
            Assembly assembly = GetResourceAssembly();
            return assembly == null ? null : assembly.GetManifestResourceStream(embeddedView.Name);
        }

        protected virtual Assembly GetResourceAssembly() {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            return assemblies.Where(assembly =>
                                    string.Equals(assembly.FullName, embeddedView.AssemblyFullName,
                                                  StringComparison.InvariantCultureIgnoreCase))
                .SingleOrDefault();
        }
    }
}