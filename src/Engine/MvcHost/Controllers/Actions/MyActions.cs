namespace Mvc3Host.Controllers.Actions {
    using Mvc3Host.Models;
    using MvcTurbine.Web.Controllers;

    public class MyActions : InferredActionRegistry<HomeController> {
        public MyActions() {
            //FromContainer<Person>("info");
            
            WithProvider("info", () => new Person {Name = "Rob"});
			WithProvider("bar", () => new Person { Name = "Brad" });
			WithProvider("spaz", () => new Person { Name = "Adam" });
			WithProvider("widget", () => new Person { Name = "Javier" });
        }
    }
}
