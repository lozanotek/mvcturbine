namespace Mvc.Controllers
{
    using System.Web.Mvc;
    using Mvc.Services;

    public class SimpleController : AsyncController
    {
        public SimpleController(IMessageService messageService)
        {
            MessageService = messageService;
        }
        public IMessageService MessageService { get; private set; }

        public ActionResult Index()
        {
            ViewData["Message"] = MessageService.GetWelcomeMessage();

            return View();
        }
    }
}
