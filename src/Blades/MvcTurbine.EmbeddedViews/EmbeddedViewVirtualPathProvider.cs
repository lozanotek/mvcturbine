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
    using System.Collections;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Hosting;

    public class EmbeddedViewVirtualPathProvider : VirtualPathProvider {
        private readonly EmbeddedViewTable embeddedViews;
        private VirtualPathProvider defaultProvider;

        public EmbeddedViewVirtualPathProvider(EmbeddedViewTable table) {
            if (table == null) {
                throw new ArgumentNullException("table", "EmbeddedViewTable cannot be null.");
            }

            embeddedViews = table;
        }

        public void SetDefaultVirtualPathProvider(VirtualPathProvider provider) {
            defaultProvider = provider;
        }

        private bool IsEmbeddedView(string virtualPath) {
            string checkPath = VirtualPathUtility.ToAppRelative(virtualPath);

            return checkPath.StartsWith("~/Views/", StringComparison.InvariantCultureIgnoreCase) 
                   && embeddedViews.ContainsEmbeddedView(checkPath);
        }

        public override bool FileExists(string virtualPath) {
            return (IsEmbeddedView(virtualPath) ||
                    defaultProvider.FileExists(virtualPath));
        }

        public override VirtualFile GetFile(string virtualPath) {
            if (IsEmbeddedView(virtualPath)) {
                EmbeddedView embeddedView = embeddedViews.FindEmbeddedView(virtualPath);
                return new AssemblyResourceFile(embeddedView, virtualPath);
            }

            return defaultProvider.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(
            string virtualPath,
            IEnumerable virtualPathDependencies,
            DateTime utcStart) {
            return IsEmbeddedView(virtualPath)
                       ? null
                       : defaultProvider.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }
}