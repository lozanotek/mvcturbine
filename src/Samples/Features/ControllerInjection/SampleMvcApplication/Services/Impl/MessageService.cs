namespace MvcTurbine.Samples.ControllerInjection.Services.Impl {
    /// <summary>
    /// Custom service to show how Controller ctor injection works
    /// </summary>
    public class MessageService : IMessageService {
        #region IMessageService Members

        public string GetWelcomeMessage() {
            return "Welcome to ASP.NET MVC!";
        }

        public string GetAboutMessage() {
            return "About ASP.NET MVC...";
        }

        #endregion
    }
}