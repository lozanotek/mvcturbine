namespace MvcApplication.Areas.Sample {
    using System.Web.Mvc;
    using Mvc.Areas;

    public class SampleAreaRegistration : AreaRegistration {
        public SampleAreaRegistration(IAreaDependency areaDependency) {
            AreaDependency = areaDependency;
        }

        public IAreaDependency AreaDependency { get; private set; }

        public override string AreaName {
            get { return AreaDependency.GetAreaName(this); }
        }

        public override void RegisterArea(AreaRegistrationContext context) {
            context.MapRoute(
                "Sample_default",
                "Sample/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}