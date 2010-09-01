namespace PocoSample.Mvc.Controllers {
	using System.Web.Mvc;
	using Models;

	[HandleError]
	public class HomeController : Controller {
		public ActionResult Index() {
			return View();
		}

		public Person Info() {
			return new Person
			{
				Id = 1000,
				FullName = new Name
				{
					FirstName = "Test",
					LastName = "User"
				}
			};
		}
	}
}
