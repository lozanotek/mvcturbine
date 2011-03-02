namespace Mvc3Host.Controllers.Actions {
    using Mvc3Host.Models;
    using MvcTurbine.Web.Controllers;

    public class MyActions : InferredActionRegistry<HomeController> {
        public MyActions() {
            //FromContainer<Person>("info");
            WithInstance("InFo", new Person { Name = "Bob Smith" });
            //WithProvider("info", () => new Person {Name = "Bob Smith"});
        }
    }
}