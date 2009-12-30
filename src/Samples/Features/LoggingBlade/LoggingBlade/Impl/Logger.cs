namespace MvcTurbine.Samples.LoggingBlade.Impl {
    using System;
    using log4net;

    // This class should be auto-registered.
    public class Logger : ILogger {
        // Declare the log4net logger to use
        private static readonly ILog log = LogManager.GetLogger(typeof(Log4netBlade));

        public ILog InternalLog {
            get { return log; }
        }

        public void LogMessage(string message) {
            InternalLog.Debug(message);
        }

        public void LogException(string message, Exception ex) {
            InternalLog.Error(message, ex);
        }
    }
}