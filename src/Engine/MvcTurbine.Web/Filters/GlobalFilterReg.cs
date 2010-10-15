namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    [Serializable]
    public class GlobalFilterReg : FilterReg {
        public override FilterScope Scope {
            get { return FilterScope.Global; }
        }
    }
}