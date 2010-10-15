namespace MvcTurbine.Web.Filters {
    using System;
    using System.Web.Mvc;

    [Serializable]
    public class ControllerFilterReg : FilterReg {
        public Type Controller { get; set; }

        public override FilterScope Scope {
            get { return FilterScope.Controller; }
        }
    }
}