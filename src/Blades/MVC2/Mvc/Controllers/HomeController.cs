namespace Mvc.Controllers {
    using System.Web.Mvc;
    using Mvc.Services;

    [HandleError]
    public class HomeController : Controller {
        public HomeController(IMessageService messageService) {
            MessageService = messageService;
        }

        public IMessageService MessageService { get; private set; }

        public ActionResult Index() {
            ViewData["Message"] = MessageService.GetWelcomeMessage();

            return View();
        }

        //NOTE: No need to specify an About action since empty actions are inferred.
    }
}
