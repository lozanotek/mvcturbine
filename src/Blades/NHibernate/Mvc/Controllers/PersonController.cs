namespace Mvc.Controllers {
	using MvcTurbine.Data;
	using SomeModel;

	public class PersonController : CrudController<Person> {
		public PersonController(IRepository<Person> entityRepository) : base(entityRepository) {
		}
	}
}
