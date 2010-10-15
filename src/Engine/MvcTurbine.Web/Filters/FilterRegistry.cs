namespace MvcTurbine.Web.Filters {
    using System.Collections.Generic;

    ///<summary>
    ///</summary>
    public interface IFilterRegistry {
        /// <summary>
        /// Gets the registered filters (Action,Result,Exception,Authorization).
        /// </summary>
        /// <returns></returns>
        IEnumerable<FilterReg> GetFilterRegistrations();
    }
}
