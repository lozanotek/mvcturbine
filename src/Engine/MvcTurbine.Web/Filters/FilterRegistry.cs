namespace MvcTurbine.Web.Filters
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// Base class to provide Model (ViewModel) registration for <see cref="IModelBinder"/>.
    /// </summary>
    public abstract class FilterRegistry : FilterRegistratrionExpression {
        protected FilterRegistry()
            : base(new List<FilterReg>()) {
        }

        /// <summary>
        /// Gets the registered filters (Action,Result,Exception,Authorization).
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<FilterReg> GetFilterRegistrations() {
            return FilterList;
        }
    }
}