namespace MvcTurbine.Samples.ExtensionMethods.Controllers {
    using System.Web.Mvc;
    using Services;
    using Web.Controllers;

    [HandleError]
    public class HomeController : Controller {
        public ActionResult Index() {
            var messageService = this.ServiceLocator().Resolve<IMessageService>();
            ViewData["WelcomeMessage"] = messageService.GetWelcomeMessage();

            return View();
        }

        public ActionResult About()
        {
            var messageService = this.ServiceLocator().Resolve<IMessageService>();
            ViewData["AboutMessage"] = messageService.GetAboutMessage();

            return View();
        }

    }
}