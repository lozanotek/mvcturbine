namespace MvcTurbine.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [Serializable]
    public class FilterReg {
        public Type FilterType { get; set; }
        public FilterScope Scope { get; set; }
        public int? Order { get; set; }
    }

    [Serializable]
    public class ControllerFilterReg : FilterReg {
        public Type ControllerType { get; set; }
        public IEnumerable<string> Actions { get; set; }
    }
}