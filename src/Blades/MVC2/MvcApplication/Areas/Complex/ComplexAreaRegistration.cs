using System.Web.Mvc;

namespace MvcApplication.Areas.Complex
{
    public class ComplexAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Complex";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Complex_default",
                "Complex/{controller}/{action}/{id}",
                new { action = "Index", controller="Sample", id = UrlParameter.Optional }
            );
        }
    }
}
