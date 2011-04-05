namespace Mvc.Controllers {
    using System.Web.Mvc;
    using Services;

    [HandleError]
    public class HomeController : Controller {
        public IBroadcastService BroadcastService { get; private set; }

        public HomeController(IBroadcastService broadcastService) {
            BroadcastService = broadcastService;
        }

        [HttpPost]
        public ActionResult PostMessage(string message) {
            var result = BroadcastService.Broadcast(message);
            return this.MessageOk(result);
        }
    }
}
