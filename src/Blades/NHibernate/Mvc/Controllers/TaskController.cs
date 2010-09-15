namespace Mvc.Controllers {
	using AnotherModel;
	using MvcTurbine.Data;

	public class TaskController : CrudController<Task> {
		public TaskController(IRepository<Task> entityRepository) : base(entityRepository) { }
	}
}
