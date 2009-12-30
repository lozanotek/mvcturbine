namespace MvcTurbine.Samples.ControllerInjection.Services.Impl {
    using System.Web.Security;

    /// <summary>
    /// This is from the default ASP.NET MVC Application sample
    /// </summary>
    public class AccountMembershipService : IMembershipService {
        private MembershipProvider _provider;

        public AccountMembershipService() {
            _provider = Membership.Provider;
        }

        public int MinPasswordLength {
            get {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password) {
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email) {
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword) {
            MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
            return currentUser.ChangePassword(oldPassword, newPassword);
        }
    }
}