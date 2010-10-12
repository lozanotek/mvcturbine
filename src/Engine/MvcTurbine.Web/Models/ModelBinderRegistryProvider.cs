﻿#region License

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

namespace MvcTurbine.Web.Models {
    using System;
    using System.Web.Mvc;
    using ComponentModel;

    /// <summary>
    /// Provides resolution of <see cref="IModelBinder"/> using the types from <see cref="ModelBinderRegistry"/>.
    /// </summary>
    public class ModelBinderRegistryProvider : IModelBinderProvider {
        public static TypeCache _binderCache;

        public IServiceLocator ServiceLocator { get; set; }

        public TypeCache BinderCache {
            get { return _binderCache; }
            set { _binderCache = value; }
        }

        public ModelBinderRegistryProvider(IServiceLocator serviceLocator) {
            ServiceLocator = serviceLocator;
        }

        public IModelBinder GetBinder(Type modelType) {
            if (BinderCache == null) return null;
            if (!BinderCache.ContainsKey(modelType)) return null;

            var binderType = BinderCache[modelType];
            return ServiceLocator.Resolve(binderType) as IModelBinder;
        }
    }
}