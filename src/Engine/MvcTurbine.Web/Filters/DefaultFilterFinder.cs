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

namespace MvcTurbine.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Default implementation for <see cref="IFilterFinder"/> to get all filters through IoC.
    /// </summary>
    public class DefaultFilterFinder : IFilterFinder
    {
        private IDictionary<Type, IEnumerable<Type>> filterTypes;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="serviceLocator"></param>
        public DefaultFilterFinder(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
            filterTypes = new Dictionary<Type, IEnumerable<Type>>();
        }

        /// <summary>
        /// Gets the current <see cref="IServiceLocator"/>.
        /// </summary>
        public IServiceLocator ServiceLocator { get; private set; }

        /// <summary>
        /// Finds all the <see cref="InjectableFilterAttribute"/> on the specified <see cref="ActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">Current action being executed.</param>
        /// <returns></returns>
        public FilterInfo FindFilters(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor == null ? null : GetGlobalFilters();
        }

        private FilterInfo GetGlobalFilters()
        {
            var actionFilters = GetGlobalFilterFromContainer<IActionFilter>();
            var resultFilters = GetGlobalFilterFromContainer<IResultFilter>();
            var exceptionFilters = GetGlobalFilterFromContainer<IExceptionFilter>();
            var authFilters = GetGlobalFilterFromContainer<IAuthorizationFilter>();

            return CreateFilterInfo(authFilters, actionFilters, resultFilters, exceptionFilters);
        }

        /// <summary>
        /// Creates a new list of filters to process.
        /// </summary>
        /// <param name="authFilters"></param>
        /// <param name="actionFilters"></param>
        /// <param name="resultFilters"></param>
        /// <param name="exceptionFilters"></param>
        /// <returns></returns>
        protected virtual FilterInfo CreateFilterInfo(IEnumerable<IAuthorizationFilter> authFilters,
                                                      IEnumerable<IActionFilter> actionFilters,
                                                      IEnumerable<IResultFilter> resultFilters,
                                                      IEnumerable<IExceptionFilter> exceptionFilters)
        {
            var registeredFilters = new FilterInfo();

            authFilters.ForEach(registeredFilters.AuthorizationFilters.Add);
            actionFilters.ForEach(registeredFilters.ActionFilters.Add);
            resultFilters.ForEach(registeredFilters.ResultFilters.Add);
            exceptionFilters.ForEach(registeredFilters.ExceptionFilters.Add);

            return registeredFilters;
        }

        protected virtual IList<TFilter> GetGlobalFilterFromContainer<TFilter>()
            where TFilter : class
        {
            if (TheTypesForTFilterHaveBeenCached<TFilter>())
                return ResolveTheCachedTypesForTFilter<TFilter>();

            var attributeList = ServiceLocator.ResolveServices<TFilter>()
                .Where(filter => !filter.IsType<IController>());

            //HACK: For some reason 'distict' couldn't work, so I had to make this 'workaround' :)
            var distinctList = new Dictionary<Type, TFilter>();

            foreach (var filter in attributeList)
                distinctList[filter.GetType()] = filter;

            CacheTheTypeMatchesForResolutionLater(distinctList);

            return distinctList.Values.ToList();
        }

        private IList<TFilter> ResolveTheCachedTypesForTFilter<TFilter>()
        {
            return filterTypes
                .Where(x => x.Key == typeof(TFilter))
                .First().Value
                .Select(x => ServiceLocator.Resolve(x))
                .Cast<TFilter>()
                .ToList();
        }

        private bool TheTypesForTFilterHaveBeenCached<TFilter>()
        {
            return filterTypes.ContainsKey(typeof(TFilter));
        }

        private void CacheTheTypeMatchesForResolutionLater<TFilter>(Dictionary<Type, TFilter> distinctList)
        {
            if (filterTypes.ContainsKey(typeof(TFilter)) == false)
                filterTypes.Add(typeof(TFilter), distinctList.Select(x => x.Key));
        }
    }
}
