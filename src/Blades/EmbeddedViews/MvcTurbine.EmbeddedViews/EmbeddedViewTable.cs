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
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class EmbeddedViewTable {
        private static readonly object _lock = new object();
        private readonly Dictionary<string, EmbeddedView> viewCache;

        public EmbeddedViewTable() {
            viewCache = new Dictionary<string, EmbeddedView>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void AddView(string viewName, string assemblyName) {
            lock (_lock) {
                viewCache[viewName] = new EmbeddedView { Name = viewName, AssemblyFullName = assemblyName };
            }
        }

        public IList<EmbeddedView> Views {
            get {
                return viewCache.Values.ToList();
            }
        }

        public bool ContainsEmbeddedView(string viewPath) {
            var foundView = FindEmbeddedView(viewPath);
            return (foundView != null);
        }

        public EmbeddedView FindEmbeddedView(string viewPath) {
            var name = GetNameFromPath(viewPath);
            if (string.IsNullOrEmpty(name)) return null;

            return Views.Where(view => view.Name.Contains(name)).SingleOrDefault();
        }

        protected string GetNameFromPath(string viewPath) {
            if (string.IsNullOrEmpty(viewPath)) return null;
            var name = viewPath.Replace("/", ".");
            return name.Replace("~", "");
        }
    }
}