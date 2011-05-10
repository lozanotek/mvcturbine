namespace MvcTurbine.Samples.LoggingBlade {
    using System.Web.Mvc;
	using log4net;

	public class GlobalLoggingFilter : IActionFilter, IResultFilter, IExceptionFilter {
        public GlobalLoggingFilter(ILog logger) {
            Logger = logger;
        }

		public ILog Logger { get; private set; }

        // IActionFilter pieces

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            LogExecution("[global] -- Executing action '{0}' ...", filterContext.ActionDescriptor);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {
            LogExecution("[global] -- Executed action '{0}' ...", filterContext.ActionDescriptor);
        }

        // IExceptionFilter pieces

        public void OnException(ExceptionContext filterContext) {
			Logger.InfoFormat("[global] -- Exception '{0}' ...", filterContext.Exception);
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
			Logger.InfoFormat(format, actionDescriptor.ActionName);
        }

        private void LogExecution(string format, ActionResult result) {
            if (Logger == null) return;
			Logger.InfoFormat(format, result);
        }
    }
}
