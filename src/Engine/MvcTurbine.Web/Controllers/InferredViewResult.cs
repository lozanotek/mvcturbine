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

namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Class to work around the pieces for invalid inferred actions.
    /// </summary>
    public class InferredViewResult : ViewResult {
        /// <summary>
        /// Checks whether the <see cref="ViewEngineResult"/> is valid, if not an HTTP 404 is thrown.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override ViewEngineResult FindView(ControllerContext context) {
            try {
                return base.FindView(context);
            }
            catch (InvalidOperationException e) {
                throw new HttpException(404, e.Message);
            }
        }
    }
}