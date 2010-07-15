namespace Mvc.Controllers {
    using System.Web.Mvc;
    using Services;

    [HandleError]
    public class HomeController : Controller {
        public HomeController(IMessageService messageService, IDateService dateService) {
            MessageService = messageService;
            DateService = dateService;
        }

        public IMessageService MessageService { get; private set; }
        public IDateService DateService { get; private set; }

        public ActionResult Index() {
            ViewData["Message"] = MessageService.GetWelcomeMessage();
            ViewData["CurrentDate"] = DateService.GetCurrentDate();

            return View();
        }

        //NOTE: No need to specify an About action since empty actions are inferred.
    }
}
