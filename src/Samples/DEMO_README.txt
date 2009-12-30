MVC Turbine Demos
These are the current demos for MVC Turbine v2.


Controller Injection
---------------------
The purpose of this demo is to show how controllers can take advantage of constructor injection (ctor injection). It uses service registration for the controller dependencies and the default route registration.

Pieces to see:

* Registration/ServiceRegistration.cs – registers the dependencies of the controller
* Routing/RouteConfigurator.cs – Shows the default routes
* Global.asax.cs – Shows where Turbine is wired into the application.
* All controllers – Since they need ctor injection.


Custom Blades
-------------
This demo shows how you can create your own custom blades and have them be used within your application. This custom blade add messages to the app instance so they can be pulled in the controller and passed into the view.

Pieces to see:
* Blades/IBladeDependency and BladeDependency – This type does “dummy” work that is injected into the custom blade.
* Blades/CustomBlade.cs – Custom blade that adds messages to the application instance.
* Registration/BladeRegistration.cs  – Registration of the dependencies for the custom blade.
* Controllers/HomeController.cs – Pulls the messages from the application instance and passes it to the view. 


Filter Injection
----------------
Shows how MVC filters (Action,Authentication,Result and Exception) benefit from ctor injection and are applied via an inherited InjectableFilterAttribute on a controller action.

Pieces to see:
* Registration/ServiceRegistration.cs – Wires up dependencies for the filters.
* Filters/*Attribute.cs – Show you can inherit the InjectableFilterAttribute type so your filter can be used within the app.
* Filters/*Filter.cs – Show how the messages get injected into the ViewData.
* Controllers/HomeController.cs – This wires up the attributes to the Index action.


Inferred Actions
----------------
Shows how inferred actions forward the URL request directly to the View.

Pieces to see:
* Controllers/HomeController.cs – Show how there are no actions but the views are still rendered.


Multiple View Engines
----------------------
Shows how IViewEngine implementations are wired up by the application. The application uses the NVelocity, WebForms and Spark view engines within the same application.
One thing to note is that this is a custom implementation of the NVelocity VE since the one that ships with MVC Contrib does not work well with other view engines.  I’m planning on committing these changes.

Pieces to see:
* Controllers/*Controller.cs – All but the NVelocityController used inferred actions to render the views. The NVelocityController had to specify the name of the action and master page due to the way the NVelocity VE was implemented.
* Views/All sub folders – Show the different views within the application.
