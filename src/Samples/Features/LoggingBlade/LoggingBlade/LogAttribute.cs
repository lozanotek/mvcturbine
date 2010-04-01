namespace MvcTurbine.Samples.LoggingBlade {
    using System.Web.Mvc;

    public class LogAttribute : ActionFilterAttribute {
        public ILogger Logger { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            LogExecution("[filter] -- Executed '{0}' ... ", filterContext.ActionDescriptor);
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            LogExecution("[filter] -- Executing '{0}' ... ", filterContext.ActionDescriptor);
            base.OnActionExecuting(filterContext);
        }

        private void LogExecution(string format, ActionDescriptor actionDescriptor) {
            if (Logger == null) return;
            var message = string.Format(format, actionDescriptor.ActionName);
            Logger.LogMessage(message);
        }
    }
}
