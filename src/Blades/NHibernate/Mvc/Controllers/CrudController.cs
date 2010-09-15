namespace Mvc.Controllers {
	using System;
	using System.Linq;
	using System.Web.Mvc;
	using Domain;
	using MvcTurbine.Data;

	public abstract class CrudController<TEntity> : Controller
		where TEntity : EntityBase {

		protected CrudController(IRepository<TEntity> entityRepository) {
			EntityRepository = entityRepository;
		}

		public IRepository<TEntity> EntityRepository { get; private set; }

		public ActionResult Index() {
			var taskList = EntityRepository.ToList();
			return View(taskList);
		}

		public ActionResult Create(TEntity newEntity) {
			if (newEntity != null) {
				newEntity.Created = DateTime.Now;
				EntityRepository.Add(newEntity);
			}

			return RedirectToAction("index");
		}

		public ActionResult Delete(int id) {
			var foundEntity = EntityRepository
				.Where(task => task.Id == id)
				.SingleOrDefault();

			if (foundEntity != null) {
				EntityRepository.Remove(foundEntity);
			}

			return RedirectToAction("index");
		}
	}
}