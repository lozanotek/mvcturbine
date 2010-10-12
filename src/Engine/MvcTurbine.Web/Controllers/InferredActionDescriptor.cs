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
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Custom implementation of <see cref="ActionDescriptor"/> to wire up inferred action execution.
    /// </summary>
    public class InferredActionDescriptor : ActionDescriptor {
        private readonly string actionName;
        private readonly ControllerDescriptor controllerDescriptor;

        /// <summary>
        /// See <see cref="ActionDescriptor"/>
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerDescriptor"></param>
        public InferredActionDescriptor(string actionName, ControllerDescriptor controllerDescriptor) {
            this.actionName = actionName;
            this.controllerDescriptor = controllerDescriptor;
        }

        /// <summary>
        /// See <see cref="ActionDescriptor.ActionName"/>.
        /// </summary>
        public override string ActionName {
            get { return actionName; }
        }

        /// <summary>
        /// See <see cref="ActionDescriptor.ControllerDescriptor"/>.
        /// </summary>
        public override ControllerDescriptor ControllerDescriptor {
            get { return controllerDescriptor; }
        }

        /// <summary>
        /// Always returns a <see cref="ViewResult"/> with the specified <see cref="ActionName"/> as the name of the view.
        /// For more information, see <see cref="ActionDescriptor.Execute"/>.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override object Execute(ControllerContext controllerContext,
                                       IDictionary<string, object> parameters) {
            return new InferredViewResult { ViewName = ActionName };
        }

        /// <summary>
        /// Gets an one item array of <see cref="InferredParameterDescriptor"/> containing the current instance.
        /// </summary>
        /// <returns></returns>
        public override ParameterDescriptor[] GetParameters() {
            return new ParameterDescriptor[] { new InferredParameterDescriptor(this, ActionName) };
        }
    }
}