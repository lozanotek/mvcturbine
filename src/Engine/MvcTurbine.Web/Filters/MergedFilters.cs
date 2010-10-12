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

namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Merges two <see cref="FilterInfo"/> instances together.
    /// </summary>
    [Serializable]
    public class MergedFilters : FilterInfo {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="defaultFilters"></param>
        /// <param name="registeredFilters"></param>
        public MergedFilters(FilterInfo defaultFilters, FilterInfo registeredFilters) {
            DefaultFilters = defaultFilters;
            RegisteredFilters = registeredFilters;

            Merge();
        }

        /// <summary>
        /// Gets the MVC filters that ship from System.Web.Mvc assembly.
        /// </summary>
        public FilterInfo DefaultFilters { get; private set; }

        /// <summary>
        /// Gets the MVC filters that have been registered with the service locator.
        /// </summary>
        public FilterInfo RegisteredFilters { get; private set; }

        /// <summary>
        /// Merges <see cref="DefaultFilters"/> and <see cref="RegisteredFilters"/>.
        /// </summary>
        public void Merge() {
            MergeFilterInfo(DefaultFilters);
            MergeFilterInfo(RegisteredFilters);
        }

        /// <summary>
        /// Merges a single <see cref="FilterInfo"/> to the base list.
        /// </summary>
        /// <param name="info"></param>
        protected virtual void MergeFilterInfo(FilterInfo info) {
            if (info == null) return;

            info.ActionFilters.ForEach(AddActionFilter);
            info.AuthorizationFilters.ForEach(AddAuthorizationFilter);
            info.ExceptionFilters.ForEach(AddExceptionFilter);
            info.ResultFilters.ForEach(AddResultFilter);
        }

        /// <summary>
        /// Adds the specified filter to the ActionFilter list.
        /// </summary>
        /// <param name="filter"></param>
        protected virtual void AddActionFilter(IActionFilter filter) {
            if (ActionFilters.Contains(filter)) return;
            ActionFilters.Add(filter);
        }

        /// <summary>
        /// Adds the specified filter to the AuthorizationFilter list.        
        /// </summary>
        /// <param name="filter"></param>
        protected virtual void AddAuthorizationFilter(IAuthorizationFilter filter) {
            if (AuthorizationFilters.Contains(filter)) return;
            AuthorizationFilters.Add(filter);
        }

        /// <summary>
        /// Adds the specified filter to the ExceptionFilter list.
        /// </summary>
        /// <param name="filter"></param>
        protected virtual void AddExceptionFilter(IExceptionFilter filter) {
            if (ExceptionFilters.Contains(filter)) return;
            ExceptionFilters.Add(filter);
        }

        /// <summary>
        /// Adds the specified filter to the ResultFilter list.        
        /// </summary>
        /// <param name="filter"></param>
        protected virtual void AddResultFilter(IResultFilter filter) {
            if (ResultFilters.Contains(filter)) return;
            ResultFilters.Add(filter);
        }
    }
}
