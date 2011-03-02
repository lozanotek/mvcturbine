using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mvc.Controllers {
    using System.Web.Mvc;
    using MvcTurbine.Data;
    using SomeModel;

    public class FooController : Controller {
        public IRepository<Person> PersonRepository { get; set; }

        public FooController(IRepository<Person> personRepository) {
            PersonRepository = personRepository;
        }

        public ActionResult Index() {
            var list = PersonRepository.ToList();
            return View(list);
        }
    }
}
