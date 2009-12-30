namespace MvcTurbine.Samples.LoggingBlade.Web.Services.Impl {
    public class MessageService : IMessageService {
        public MessageService(ILogger logger) {
            Logger = logger;
        }

        public ILogger Logger { get; private set; }

        public string GetWelcomeMessage() {
            var message = "Welcome to ASP.NET MVC!";
            Logger.LogMessage(string.Format("The welcome message is '{0}'", message));

            return message;
        }

        public string GetAboutMessage() {
            var message = "About ASP.NET MVC...";
            Logger.LogMessage(string.Format("The about message is '{0}'", message));

            return message;
        }
    }
}