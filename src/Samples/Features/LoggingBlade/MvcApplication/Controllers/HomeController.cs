namespace MvcTurbine.Samples.LoggingBlade.Web.Controllers {
    using System.Web.Mvc;
    using Services;
	using log4net;

    [HandleError]
    public class HomeController : Controller {
        // Inject the logger via ctor injection
        public HomeController(IMessageService messageService, ILog logger) {
            MessageService = messageService;
            Logger = logger;
        }

        public IMessageService MessageService { get; private set; }
        public ILog Logger { get; private set; }

        [Log]
        public ActionResult Index() {
            Logger.Info("In the HomeController.Index method!");

            ViewData["Message"] = MessageService.GetWelcomeMessage();
            return View();
        }

        [Log]
        public ActionResult About() {
            Logger.Info("In the HomeController.About method!");

            ViewData["Message"] = MessageService.GetAboutMessage();
            return View();
        }
    }
}