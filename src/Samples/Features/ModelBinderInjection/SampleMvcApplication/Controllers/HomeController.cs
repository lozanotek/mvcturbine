namespace MvcTurbine.Samples.ModelBinders.Controllers {
    using System.Web.Mvc;
    using Models;

    [HandleError]
    public class HomeController : Controller {
        // There is no empty action to handle the /GET Index request
        // because inferred actions handle that request.

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(PersonInputModel inputModel) {
            if (inputModel == null) {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}