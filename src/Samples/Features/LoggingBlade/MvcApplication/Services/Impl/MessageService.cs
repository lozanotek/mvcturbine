using log4net;
namespace MvcTurbine.Samples.LoggingBlade.Web.Services.Impl {
    public class MessageService : IMessageService {
        public MessageService(ILog logger) {
            Logger = logger;
        }

        public ILog Logger { get; private set; }

        public string GetWelcomeMessage() {
            var message = "Welcome to ASP.NET MVC!";
            Logger.InfoFormat("The welcome message is '{0}'", message);

            return message;
        }

        public string GetAboutMessage() {
            var message = "About ASP.NET MVC...";
            Logger.InfoFormat("The about message is '{0}'", message);

            return message;
        }
    }
}