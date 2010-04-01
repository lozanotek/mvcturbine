namespace MvcTurbine.Samples.LoggingBlade {
    using System.Web.Mvc;

    public class GlobalLoggingFilter : IActionFilter, IResultFilter, IExceptionFilter {
        public GlobalLoggingFilter(ILogger logger) {
            Logger = logger;
        }

        public ILogger Logger { get; private set; }

        // IActionFilter pieces

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            LogExecution("[global] -- Executing action '{0}' ...", filterContext.ActionDescriptor);
        }


        public void OnActionExecuted(ActionExecutedContext filterContext) {
            LogExecution("[global] -- Executed action '{0}' ...", filterContext.ActionDescriptor);
        }

        // IExceptionFilter pieces

        public void OnException(ExceptionContext filterContext) {
            string message = string.Format("[global] -- Exception '{0}' ...", filterContext.Exception);
            Logger.LogMessage(message);
        }

        // IResultFilter pieces
        
        public void OnResultExecuting(ResultExecutingContext filterContext) {
            LogExecution("[global] -- Executing result '{0}' ...", filterContext.Result);
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) {
            LogExecution("[global] -- Executed result '{0}' ...", filterContext.Result);
        }

        
        private void LogExecution(string format, ActionDescriptor actionDescriptor) {
            if (Logger == null) return;
            string message = string.Format(format, actionDescriptor.ActionName);
            Logger.LogMessage(message);
        }

        private void LogExecution(string format, ActionResult result) {
            if (Logger == null) return;
            string message = string.Format(format, result);
            Logger.LogMessage(message);
        }
    }
}
