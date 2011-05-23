namespace Okonau.Mvc.Controllers {
    using System.Web.Mvc;
    using Services;
    using ViewModels;

    /// <summary>
    /// Default controller for the application.
    /// </summary>
    [HandleError]
    public class TaskController : Controller {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="service"></param>
        public TaskController(ITaskService service) {
            TaskService = service;
        }

        /// <summary>
        /// Gets the associated <see cref="ITaskService"/> for the controller.
        /// </summary>
        public ITaskService TaskService { get; private set; }

        /// <summary>
        /// Performs the default action for the controller
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            var tasks = TaskService.GetTasks();
            return View(tasks);
        }

        /// <summary>
        /// Defines the creation for tasks, handled via HTTP POST.
        /// </summary>
        /// <param name="model">New task information.</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult New(TaskInputModel model) {
            TaskService.CreateNewTask(model);

            return RedirectToAction("Index");
        }

        // No HTTP GET action is defined, so MVC Turbine will
        // handle the action via an Inferred Action.

        /// <summary>
        /// Defines the deletion of a task, handled via HTTP GET.
        /// </summary>
        /// <param name="id">Id for the task to delete.</param>
        /// <returns></returns>
        public ActionResult Delete(int id) {
            TaskService.DeleteTask(id);

            return RedirectToAction("Index");            
        }

        // No need to specify an About action since 
        // empty actions are inferred.
    }
}
