namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    ///<summary>
    /// Simple class for registering an action based filters.
    ///</summary>
    [Serializable]
    public class ActionFilterReg : FilterReg {
        public Type Controller { get; set; }
        public string Action { get; set; }

        public override FilterScope  Scope {
            get { return FilterScope.Action; }
        }
    }
}