namespace MvcTurbine.Samples.MultipleViewEngines.Controllers {
    using System.Web.Mvc;

    public class NVelocityController : Controller {
        public ActionResult Index() {
            // Had to specify the Master page for the view
            // since there were some issues with the NVelocity VE.
            return View("Index", "Site");
        }
    }
}