namespace Mvc.Services
{
    using log4net;

    public class LogService
    {
        private readonly ILog log = LogManager.GetLogger(typeof(LogService));
        public void LogMessage(string message)
        {
            log.InfoFormat("Received message with contents '{0}'", message);
        }
    }
}