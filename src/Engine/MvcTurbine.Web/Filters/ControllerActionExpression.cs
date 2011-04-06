namespace MvcTurbine.Web.Filters {
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    /// <summary>
    /// Defines the expression for registration of filters to actions within a controller.
    /// </summary>
    public class ControllerActionExpression<TController> where TController : IController {
        private IList<Filter> Filters { get; set; }
        private Type FilterType { get; set; }
        private Action<object> Initializer { get; set; }
        private ActionFilter currentRegistration;
		private int Order { get; set; }

        /// <summary>
        /// Creates the current action filter pieces for the registration.
        /// </summary>
        /// <param name="filters"></param>
        public ControllerActionExpression(IList<Filter> filters, Type filterType, Action<object> initializer, int order) {
            Filters = filters;
            FilterType = filterType;
            Initializer = initializer;
			Order = order;
        }

        /// <summary>
        /// Registers the filter with the associated action name (inferred or real) and 
        /// allows the initialization of the filter.        
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public virtual ControllerActionExpression<TController> ToAction(string actionName) {
            if (!FilterType.IsMvcFilter()) {
                throw new ArgumentException("Specified argument is not an MVC filter!", "filterType");
            }

            if (currentRegistration != null) {
                // Remove the old registration
                Filters.Remove(currentRegistration);
            }

            // Create a new one for the
            Filters.Add(new ActionFilter {
                FilterType = this.FilterType,
                ControllerType = typeof(TController),
                Action = actionName,
                ModelInitializer = Initializer,
				Order = this.Order
            });

            // Clear it
            currentRegistration = null;
            return this;
        }

        /// <summary>
        /// Registers the filter at the controller level.
        /// </summary>
        /// <returns></returns>
        public void Register() {
            currentRegistration = new ActionFilter {
                FilterType = FilterType,
                ControllerType = typeof(TController),
                ModelInitializer = Initializer,
				Order = this.Order
            };

            Filters.Add(currentRegistration);
        }

        /// <summary>
        /// Registers the filter to the specified action (expression).
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual ControllerActionExpression<TController> ToAction(Expression<Action<TController>> action) {
            var call = action.Body as MethodCallExpression;
            return ToAction(call.Method.Name);
        }
    }
}
