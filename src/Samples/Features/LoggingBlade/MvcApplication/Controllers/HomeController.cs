namespace MvcTurbine.Samples.LoggingBlade.Web.Controllers {
    using System.Web.Mvc;
    using Services;

    [HandleError]
    public class HomeController : Controller {
        // Inject the logger via ctor injection
        public HomeController(IMessageService messageService, ILogger logger) {
            MessageService = messageService;
            Logger = logger;
        }

        public IMessageService MessageService { get; private set; }
        public ILogger Logger { get; private set; }

        [Log]
        public ActionResult Index() {
            Logger.LogMessage("In the HomeController.Index method!");

            ViewData["Message"] = MessageService.GetWelcomeMessage();
            return View();
        }

        [Log]
        public ActionResult About() {
            Logger.LogMessage("In the HomeController.About method!");

            ViewData["Message"] = MessageService.GetAboutMessage();
            return View();
        }
    }
}