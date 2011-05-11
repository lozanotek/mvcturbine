namespace MvcTurbine.Web.Blades {

	using MvcTurbine.Blades;
	using MvcTurbine.ComponentModel;
	using System.Web;

	/// <summary>
	/// Blade for all http modules with auto-registration
	/// </summary>
	public class HttpModuleBlade : Blade, ISupportAutoRegistration {
		public void AddRegistrations(AutoRegistrationList registrationList) {
			registrationList.Add(Registration.Simple<IHttpModule>());
		}

		public override void Spin(IRotorContext context) { }
		
	}
}
