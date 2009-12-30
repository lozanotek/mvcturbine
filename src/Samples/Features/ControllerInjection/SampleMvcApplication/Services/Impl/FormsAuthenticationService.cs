namespace MvcTurbine.Samples.ControllerInjection.Services.Impl {
    using System.Web.Security;

    /// <summary>
    /// This is from the default ASP.NET MVC Application sample
    /// </summary>
    public class FormsAuthenticationService : IFormsAuthentication {
        public void SignIn(string userName, bool createPersistentCookie) {
            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut() {
            FormsAuthentication.SignOut();
        }
    }
}