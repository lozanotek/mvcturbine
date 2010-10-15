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

namespace MvcTurbine.Web.Models {
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ComponentModel;

    ///<summary>
    /// Expression for the registration of <see cref="IModelBinder"/> within the system.
    ///</summary>
    public class BinderRegistratrionExpression {
        protected TypeCache BinderTable { get; set; }

        public BinderRegistratrionExpression(TypeCache cache) {
            BinderTable = cache ?? new TypeCache();
        }

        public BinderRegistratrionExpression Bind<TModel, TBinder>()
            where TModel : class
            where TBinder : IModelBinder {
            return Bind(typeof(TModel), typeof(TBinder));
        }

        public BinderRegistratrionExpression Bind(Type modelType, Type binderType) {
            BinderTable.Add(new KeyValuePair<Type, Type>(modelType, binderType));
            return this;
        }
    }
}
