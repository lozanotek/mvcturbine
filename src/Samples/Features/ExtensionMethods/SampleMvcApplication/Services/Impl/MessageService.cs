namespace MvcTurbine.Samples.ExtensionMethods.Services.Impl {
    using Services;

    public class MessageService : IMessageService {
        public string GetWelcomeMessage() {
            return "Welcome to ASP.NET MVC!";
        }

        public string GetAboutMessage() {
            return "About ASP.NET MVC...";
        }

        public string GetContent() {
            return "Here's some content!";
        }
    }
}