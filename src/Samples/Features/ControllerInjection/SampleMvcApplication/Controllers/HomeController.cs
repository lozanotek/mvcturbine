namespace MvcTurbine.Samples.ControllerInjection.Controllers {
    using System.Web.Mvc;
    using Services;

    [HandleError]
    public class HomeController : Controller {
        public HomeController(IMessageService messageService) {
            MessageService = messageService;
        }

        public IMessageService MessageService { get; set; }

        public ActionResult Index() {
            ViewData["Message"] = MessageService.GetWelcomeMessage();
            return View();
        }

        public ActionResult About() {
            ViewData["Message"] = MessageService.GetAboutMessage();
            return View();
        }
    }
}