namespace MvcTurbine.Web.Blades {
    using System.Web.Mvc;
    using ComponentModel;
    using Controllers;
    using MvcTurbine.Blades;

    /// <summary>
    /// Blade for all controller related components.
    /// </summary>
    public class ControllerBlade : Blade, ISupportAutoRegistration {
        /// <summary>
        /// Sets the instance of <see cref="IControllerFactory"/> to use.  If one is not registered,
        /// <see cref="IControllerActivator"/> is used.
        /// </summary>
        public override void Spin(IRotorContext context) {
            // Get the current IServiceLocator
            var locator = GetServiceLocatorFromContext(context);

            if (locator is IServiceReleaser) {
                var factory = GetControllerFactory(locator);
                ControllerBuilder.Current.SetControllerFactory(factory);
            }
            else {
                // Get the registered controller activator - if any, then skip registering our own.
                var controllerActivator = GetControllerActivator(locator);
                if (controllerActivator != null) return;

                // Register with the runtime -- this is due to the fact that we're using the DefaultControllerFactory uses
                // this new type for creation of the controllers -- we need to inject our own if one is not specified
                using (locator.Batch()) {
                    // Set the default controller factory
                    locator.Register<IControllerActivator, TurbineControllerActivator>();
                }
            }
        }

        /// <summary>
        /// Gets the registered <seealso cref="IControllerActivator"/>, if one is not found the default one is used.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        protected virtual IControllerActivator GetControllerActivator(IServiceLocator locator) {
            try {
                return locator.Resolve<IControllerActivator>();
            }
            catch { return null; }
        }

        /// <summary>
        /// Gets the registered <seealso cref="IControllerFactory"/>, if one is not found the default one is used.
        /// </summary>
        /// <param name="locator"></param>
        /// <returns></returns>
        protected virtual IControllerFactory GetControllerFactory(IServiceLocator locator) {
            IControllerFactory controllerFactory;

            try {
                controllerFactory = locator.Resolve<IControllerFactory>() ??
                                    new TurbineControllerFactory(locator, locator as IServiceReleaser);
            }
            catch {
                // Use default factory since one was not specified
                return new TurbineControllerFactory(locator, locator as IServiceReleaser);
            }

            return controllerFactory;
        }

        /// <summary>
        /// Provides the auto-registration for <see cref="IController"/> types.
        /// </summary>
        /// <param name="registrationList"></param>
        public virtual void AddRegistrations(AutoRegistrationList registrationList) {
            registrationList.Add(MvcRegistration.RegisterController());
        }
    }
}
