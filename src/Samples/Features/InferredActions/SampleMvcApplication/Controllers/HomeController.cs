﻿namespace MvcTurbine.Samples.InferredActions.Controllers {
    using System.Web.Mvc;

    [HandleError]
    public class HomeController : Controller {
        // There are no actions defined in this controller, 
        // so we'll let MVC Turbine infer them all.

        // This means that MVC Turbine will automatically
        // forward the request to the view.

        // When the action is defined, it will then use the 
        // method within the controller class.
    }
}