namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    [Serializable]
    public abstract class FilterReg {
        public Type Filter { get; set; }
        public int? Order { get; set; }
        public abstract FilterScope Scope { get; }
    }
}
