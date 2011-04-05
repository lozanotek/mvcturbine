namespace Mvc.Controllers {
    using System;
    using System.Web.Mvc;

    public static class ControllerExt {
        public static ActionResult MessageOk(this Controller controller, Guid messageId) {
            return new MessageResult(messageId);
        }
    }
}