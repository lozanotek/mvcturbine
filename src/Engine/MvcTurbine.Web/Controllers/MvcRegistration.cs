namespace MvcTurbine.Web.Controllers {
    using System;
    using System.Web.Mvc;
    using ComponentModel;
    using Models;
    using MvcTurbine.Web.Filters;

    /// <summary>
    /// Helper class for registration of ASP.MVC components.
    /// </summary>
    public static class MvcRegistration {

        /// <summary>
        /// Gets the default registration for <see cref="IController"/>.
        /// </summary>
        /// <returns></returns>
        public static ServiceRegistration RegisterController() {
            return RegisterController((locator, type) =>
                                      locator.Register(type, type));
        }

        /// <summary>
        /// Gets the registration for <see cref="IController"/> with the specified registration action.
        /// </summary>
        /// <param name="regAction"></param>
        /// <returns></returns>
        public static ServiceRegistration RegisterController(
            Action<IServiceLocator, Type> regAction) {
            return Registration.Custom<IController>(RegistrationFilters.DefaultFilter, regAction);
        }

        /// <summary>
        /// Gets the default registration for <see cref="IViewEngine"/>.
        /// </summary>
        /// <returns></returns>
        public static ServiceRegistration RegisterViewEngine() {
            return RegisterViewEngine((locator, type) => locator.Register<IViewEngine>(type));
        }

        /// <summary>
        /// Gets the registration for <see cref="IViewEngine"/> withthe specified registration action.
        /// </summary>
        /// <param name="regAction"></param>
        /// <returns></returns>
        public static ServiceRegistration RegisterViewEngine(Action<IServiceLocator, Type> regAction) {
            return Registration.Custom<IViewEngine>(RegistrationFilters.DefaultFilter, regAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="regAction"></param>
        /// <returns></returns>
        public static ServiceRegistration RegisterFilter<TFilter>(Action<IServiceLocator, Type> regAction)
            where TFilter : class {
            return Registration.Custom<TFilter>(RegistrationFilters.DefaultFilter, regAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ServiceRegistration RegisterBinder() {
            return Registration.Simple<IFilterableModelBinder>();
        }
    }
}
