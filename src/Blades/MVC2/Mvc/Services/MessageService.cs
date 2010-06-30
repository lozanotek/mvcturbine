namespace Mvc.Services {
    public class MessageService : IMessageService {
        #region IMessageService Members

        public string GetWelcomeMessage() {
            return "Welcome to ASP.NET MVC!";
        }

        #endregion
    }
}
