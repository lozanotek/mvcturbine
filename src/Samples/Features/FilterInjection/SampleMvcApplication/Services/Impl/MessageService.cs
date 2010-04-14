namespace MvcTurbine.Samples.FilterInjection.Services.Impl {
    using System.Diagnostics;

    public class MessageService : IMessageService {
        public string GetWelcomeMessage() {
            return "Welcome to ASP.NET MVC!";
        }

        public string GetFilterMessage() {
            var trace = new StackTrace();
            StackFrame frame = trace.GetFrames()[2];
            string name = frame.GetMethod().Name;

            return "I'm in method " + name;
        }

        public string ReplayMessage(string message) {
            return message;
        }
    }
}