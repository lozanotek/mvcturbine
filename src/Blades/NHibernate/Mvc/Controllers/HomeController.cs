namespace Mvc.Controllers {
	using System.Linq;
	using System.Web.Mvc;
	using MvcTurbine.Data;
	using SomeModel;

	[HandleError]
	public class HomeController : Controller {
		public IRepository<Person> PersonRepository { get; private set; }

		public HomeController(IRepository<Person> personRepository) {
			PersonRepository = personRepository;
		}

		public ActionResult Index() {
			var personList = PersonRepository.ToList();
			return View(personList);
		}

		//NOTE: No need to specify an About action since empty actions are inferred.
	}
}
