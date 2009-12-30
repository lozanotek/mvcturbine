namespace MvcTurbine.Samples.CustomBlades.Blades {
    using System;
    using System.Web;
    using MvcTurbine.Blades;

    public class CustomBlade : Blade {
        public CustomBlade(IBladeDependency dependency) {
            Dependency = dependency;
        }

        public IBladeDependency Dependency { get; private set; }

        public override void Spin(IRotorContext context) {
            HttpContext.Current.Application["BeforeDependency"] = string.Format("Time before dependency {0}",
                                                                                DateTime.Now);
            //TODO: Place a break point on the line below
            Dependency.DoWork();

            HttpContext.Current.Application["AfterDependency"] = string.Format("Time after dependency {0}",
                                                                               DateTime.Now);
        }
    }
}